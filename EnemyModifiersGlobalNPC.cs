using System;
using FargoEnemyModifiers.Modifiers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace FargoEnemyModifiers
{
    public class EnemyModifiersGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        // TODO: modifier list?
        public virtual Modifier Modifier { get; set; }

        public virtual bool Rallied { get; set; }

        public virtual int RallyTimer { get; set; }

        public virtual bool Fortified { get; set; }

        public virtual int FortTimer { get; set; }

        public virtual bool DropLoot { get; set; } = true;

        public override void ResetEffects(NPC npc)
        {
            if (RallyTimer > 0 && --RallyTimer <= 0)
                Rallied = false;

            if (FortTimer > 0 && --FortTimer <= 0)
                Fortified = false;
        }

        public void ApplyModifier(NPC npc, int randomType)
        {
            //if (EnemyModifiersConfig.Instance.SetModifier)
            //    randomType = (int) EnemyModifiersConfig.Instance.ModifierEnum;

            Modifier = Activator.CreateInstance(EnemyModifiers.Modifiers[randomType].GetType()) as Modifier;
            Modifier?.Setup(npc);
            Modifier?.UpdateModifierStats(npc);
        }

        public void NetUpdateModifier(int npc, int modifierType)
        {
            if (Main.netMode == NetmodeID.SinglePlayer)
                return;

            ModPacket netMessage = mod.GetPacket();
            netMessage.Write((byte) 1);
            netMessage.Write((byte) npc);
            netMessage.Write(modifierType); //these are the variables of the instance THAT CALLS THIS METHOD
            netMessage.Send();
        }

        public bool firstTick = true;

        public override bool PreAI(NPC npc)
        {
            if (firstTick && Main.rand.Next(100) <= EnemyModifiersConfig.Instance.ChanceForModifier)
            {
                if (!(npc.boss && !EnemyModifiersConfig.Instance.BossModifiers || npc.townNPC || npc.friendly ||
                      npc.dontTakeDamage || npc.realLife != -1 || npc.SpawnedFromStatue ||
                      npc.type == NPCID.TargetDummy ||
                      EnemyModifiersConfig.Instance.NPCBlacklist.Contains(new NPCDefinition(npc.type))))
                {
                    int randomType = Main.rand.Next(EnemyModifiers.Modifiers.Count);

                    NetUpdateModifier(npc.whoAmI, randomType);
                    ApplyModifier(npc, randomType);
                }
            }

            firstTick = false;
            return Modifier == null || Modifier.PreAI(npc);
        }

        public override void AI(NPC npc)
        {
            if (npc.realLife != -1 && Modifier == null)
            {
                NPC head = Main.npc[npc.realLife];

                if (head.GetGlobalNPC<EnemyModifiersGlobalNPC>().Modifier != null)
                {
                    Modifier = head.GetGlobalNPC<EnemyModifiersGlobalNPC>().Modifier;
                    Modifier.UpdateModifierStats(npc);
                }
            }

            float speedMulti = 1f;

            if (Modifier != null)
            {
                speedMulti = Modifier.SpeedMultiplier;
                Modifier.AI(npc);
            }

            if (Rallied)
                speedMulti += .25f;

            UpdateSpeed(npc, speedMulti);
        }

        public override void PostAI(NPC npc)
        {
            Modifier?.PostAI(npc);
        }

        public void UpdateSpeed(NPC npc, float speedMultiplier)
        {
            if (speedMultiplier < 1f)
            {
                float speedToSubtract = 1f - speedMultiplier;

                npc.position -= npc.velocity * speedToSubtract;
            }
            else if (speedMultiplier > 1f)
            {
                float speedToAdd = speedMultiplier - 1f;
                Vector2 newPos = npc.position + npc.velocity * speedToAdd;

                if (!Collision.SolidCollision(newPos, npc.width, npc.height))
                    npc.position = newPos;
            }
        }

        public override void GetChat(NPC npc, ref string chat)
        {
            Modifier?.GetChat(npc, ref chat);
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            Modifier?.UpdateLifeRegen(npc, ref damage);
        }

        public override void OnHitByItem(NPC npc, Player player, Item item, int damage, float knockback, bool crit)
        {
            Modifier?.OnHitByItem(npc, player);
        }

        public override void OnHitByProjectile(NPC npc, Projectile projectile, int damage, float knockback, bool crit)
        {
            Modifier?.OnHitByProjectile(npc, projectile);
        }

        public override void OnHitPlayer(NPC npc, Player target, int damage, bool crit)
        {
            Modifier?.OnHitPlayer(npc, target);
        }

        public override void ModifyHitByItem(NPC npc, Player player, Item item, ref int damage, ref float knockback,
            ref bool crit)
        {
            Modifier?.ModifyHitByItem(npc, player, item, ref damage, ref knockback);
        }

        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref int damage, ref float knockback,
            ref bool crit, ref int hitDirection)
        {
            Modifier?.ModifyHitByProjectile(npc, projectile, ref damage, ref knockback);
        }

        public override void ModifyHitPlayer(NPC npc, Player target, ref int damage, ref bool crit)
        {
            if (Rallied)
                damage = (int) (damage * 1.25f);
        }

        public override bool StrikeNPC(NPC npc, ref double damage, int defense, ref float knockback, int hitDirection,
            ref bool crit)
        {
            if (Fortified)
                damage /= 2;

            return true;
        }

        public override bool PreNPCLoot(NPC npc)
        {
            return (Modifier?.PreNPCLoot(npc) ?? false) && DropLoot;
        }

        public override void NPCLoot(NPC npc)
        {
            Modifier?.NPCLoot(npc);
        }

        public override Color? GetAlpha(NPC npc, Color drawColor)
        {
            return Modifier?.GetAlpha();
        }

        public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Color drawColor)
        {
            return Modifier?.PreDraw(npc, spriteBatch, drawColor) ?? base.PreDraw(npc, spriteBatch, drawColor);
        }
    }
}