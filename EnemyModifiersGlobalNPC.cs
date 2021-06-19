using System;
using System.Collections.Generic;
using System.Linq;
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

        public virtual List<Modifier> Modifiers { get; set; } = new List<Modifier>();

        public virtual bool Rallied { get; set; }

        public virtual int RallyTimer { get; set; }

        public virtual bool Fortified { get; set; }

        public virtual int FortTimer { get; set; }

        public virtual bool DropLoot { get; set; } = true;

        public List<int> modifierTypes = new List<int>();

        public override void ResetEffects(NPC npc)
        {
            if (RallyTimer > 0 && --RallyTimer <= 0)
                Rallied = false;

            if (FortTimer > 0 && --FortTimer <= 0)
                Fortified = false;
        }

        public void ApplyModifier(NPC npc, int type)
        {
            if (type <= 0 || type >= EnemyModifiers.Modifiers.Count)
                return;

            Modifiers.Add(Activator.CreateInstance(EnemyModifiers.Modifiers[type].GetType()) as Modifier);
            Modifiers?.ForEach(x => x.Setup(npc));
            Modifiers?.ForEach(x => x.UpdateModifierStats(npc));
        }

        public bool firstTick = true;

        public override bool PreAI(NPC npc)
        {
            if (firstTick)
            {
                if (Main.netMode == NetmodeID.MultiplayerClient) //client sends modifier request to server
                {
                    ModPacket packet = mod.GetPacket();
                    packet.Write((byte) 1);
                    packet.Write((byte) npc.whoAmI);
                    packet.Write((byte) Main.myPlayer);
                    packet.Send();
                }
                else if (Main.rand.Next(100) <= EnemyModifiersConfig.Instance.ChanceForModifier)
                {
                    if (!(npc.boss && !EnemyModifiersConfig.Instance.BossModifiers || npc.townNPC || npc.friendly
                          || npc.dontTakeDamage || npc.realLife != -1 || npc.SpawnedFromStatue ||
                          npc.type == NPCID.TargetDummy
                          || EnemyModifiersConfig.Instance.NPCBlacklist.Contains(new NPCDefinition(npc.type))))
                    {
                        modifierTypes = new List<int> { Main.rand.Next(EnemyModifiers.Modifiers.Count) };

                        while (Main.rand.NextBool() && modifierTypes.Count <= EnemyModifiersConfig.Instance.ModifierAmount)
                            modifierTypes.Add(Main.rand.Next(EnemyModifiers.Modifiers.Count));

                        foreach (int modifier in modifierTypes)
                            ApplyModifier(npc, modifier);
                    }
                }
            }

            firstTick = false;

            if (Modifiers is null)
                return base.PreAI(npc);

            bool retVal = base.PreAI(npc);

            foreach (Modifier modifier in Modifiers)
                retVal = modifier.PreAI(npc);

            return retVal;
        }

        public override void AI(NPC npc)
        {
            if (npc.realLife != -1 && Modifiers == null)
            {
                NPC head = Main.npc[npc.realLife];

                if (head.GetGlobalNPC<EnemyModifiersGlobalNPC>().Modifiers != null)
                {
                    Modifiers = head.GetGlobalNPC<EnemyModifiersGlobalNPC>().Modifiers;
                    Modifiers.ForEach(x => x.UpdateModifierStats(npc));
                }
            }

            float speedMulti = 1f;

            if (Modifiers != null)
            {
                speedMulti += Modifiers.Sum(modifier => modifier.SpeedMultiplier);
                Modifiers.ForEach(x => x.AI(npc));
            }

            if (Rallied)
                speedMulti += .25f;

            UpdateSpeed(npc, speedMulti);
        }

        public override void PostAI(NPC npc)
        {
            Modifiers?.ForEach(x => x.PostAI(npc));
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
            foreach (Modifier modifier in Modifiers)
                modifier.GetChat(npc, ref chat);
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            foreach (Modifier modifier in Modifiers)
                modifier.UpdateLifeRegen(npc, ref damage);
        }

        public override void OnHitByItem(NPC npc, Player player, Item item, int damage, float knockback, bool crit)
        {
            Modifiers?.ForEach(x => x.OnHitByItem(npc, player));
        }

        public override void OnHitByProjectile(NPC npc, Projectile projectile, int damage, float knockback, bool crit)
        {
            Modifiers?.ForEach(x => x.OnHitByProjectile(npc, projectile));
        }

        public override void OnHitPlayer(NPC npc, Player target, int damage, bool crit)
        {
            Modifiers?.ForEach(x => x.OnHitPlayer(npc, target));
        }

        public override void ModifyHitByItem(NPC npc, Player player, Item item, ref int damage, ref float knockback,
            ref bool crit)
        {
            foreach (Modifier modifier in Modifiers)
                modifier.ModifyHitByItem(npc, player, item, ref damage, ref knockback);
        }

        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref int damage, ref float knockback,
            ref bool crit, ref int hitDirection)
        {
            foreach (Modifier modifier in Modifiers)
                modifier.ModifyHitByProjectile(npc, projectile, ref damage, ref knockback);
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
            bool retVal = base.PreNPCLoot(npc);

            foreach (Modifier modifier in Modifiers)
                retVal = modifier.PreNPCLoot(npc);

            return retVal && DropLoot;
        }

        public override void NPCLoot(NPC npc)
        {
            Modifiers?.ForEach(x => x.NPCLoot(npc));
        }

        public override Color? GetAlpha(NPC npc, Color drawColor)
        {
            Color? retVal = base.GetAlpha(npc, drawColor);

            foreach (Modifier modifier in Modifiers)
                retVal = modifier.GetAlpha();

            return retVal;
        }

        public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Color drawColor)
        {
            bool retVal = base.PreDraw(npc, spriteBatch, drawColor);

            foreach (Modifier modifier in Modifiers)
                retVal = modifier.PreDraw(npc, spriteBatch, drawColor);

            return retVal;
        }
    }
}