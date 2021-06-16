using System;
using System.Collections.Generic;
using FargoEnemyModifiers.Modifiers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace FargoEnemyModifiers
{
    public class EnemyModifiersGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public Modifier modifier = null;

        public bool Rallied;
        public int RallyTimer = 0;

        public bool Fortified;
        public int FortTimer = 0;

        public override void ResetEffects(NPC npc)
        {
            if (RallyTimer > 0 && --RallyTimer <= 0)
            {
                Rallied = false;
            }

            if (FortTimer > 0 && --FortTimer <= 0)
            {
                Fortified = false;
            }


        }

        public void ApplyModifier(NPC npc, int randomType)
        {
            if (EnemyModifiersConfig.Instance.SetModifier)
            {
                randomType = (int)EnemyModifiersConfig.Instance.ModifierEnum;
            }

            switch (randomType)
            {
                case (int)ModifierID.Unrelenting:
                    modifier = new Unrelenting();
                    break;
                case (int)ModifierID.Regenerating:
                    modifier = new Regenerating();
                    break;
                case (int)ModifierID.Menacing:
                    modifier = new Menacing();
                    break;
                case (int)ModifierID.Armored:
                    modifier = new Armored();
                    break;
                case (int)ModifierID.Huge:
                    modifier = new Huge();
                    break;
                case (int)ModifierID.Miniature:
                    modifier = new Miniature();
                    break;
                case (int)ModifierID.Swift:
                    modifier = new Swift();
                    break;
                case (int)ModifierID.Slow:
                    modifier = new Slow();
                    break;
                case (int)ModifierID.Hyper:
                    modifier = new Hyper();
                    break;
                case (int)ModifierID.Sluggish:
                    modifier = new Sluggish();
                    break;
                case (int)ModifierID.Inpenetrable:
                    modifier = new Inpenetrable();
                    break;
                case (int)ModifierID.Light:
                    modifier = new Light();
                    break;
                case (int)ModifierID.Rampaging:
                    modifier = new Rampaging();
                    break;
                case (int)ModifierID.Stealthy:
                    modifier = new Stealthy();
                    break;
                case (int)ModifierID.Splitting:
                    modifier = new Splitting();
                    break;
                case (int)ModifierID.Wealthy:
                    modifier = new Wealthy();
                    break;
                case (int)ModifierID.Rare:
                    modifier = new Rare();
                    break;
                case (int)ModifierID.Infested:
                    modifier = new Infested();
                    break;
                case (int)ModifierID.Accelerating:
                    modifier = new Accelerating();
                    break;
                case (int)ModifierID.Hexproof:
                    modifier = new Hexproof();
                    break;
                case (int)ModifierID.Bulletproof:
                    modifier = new Bulletproof();
                    break;
                case (int)ModifierID.Veiled:
                    modifier = new Veiled();
                    break;
                case (int)ModifierID.Shielded:
                    modifier = new Shielded();
                    break;
                case (int)ModifierID.Possessed:
                    modifier = new Possessed();
                    break;
                case (int)ModifierID.Bloodthirsty:
                    modifier = new Bloodthirsty();
                    break;
                case (int)ModifierID.Warping:
                    modifier = new Warping();
                    break;
                case (int)ModifierID.Rallying:
                    modifier = new Rallying();
                    break;
                case (int)ModifierID.Fortifying:
                    modifier = new Fortifying();
                    break;
                case (int)ModifierID.Chained:
                    modifier = new Chained();
                    break;
                case (int)ModifierID.Merchant:
                    modifier = new Merchant();
                    break;
                case (int)ModifierID.Worm:
                    modifier = new Worm();
                    break;
                case (int)ModifierID.Rainbow:
                    modifier = new Rainbow();
                    break;
                case (int)ModifierID.Devouring:
                    modifier = new Devouring(npc);
                    break;
                case (int)ModifierID.Juggernaut:
                    modifier = new Juggernaut();
                    break;
                    //case (int)ModifierID.Healer:
                    //    //name = "Healer";
                    //    break;
                    //case (int)ModifierID.Flammable:
                    //    //name = "Flammable";
                    //    break;
                    //case (int)ModifierID.Puppet:
                    //    //name = "Puppet";
                    //    break;
                    //case (int)ModifierID.ManaSapper:
                    //    //name = "ManaSapper";
                    //    break;
                    //case (int)ModifierID.Hydrophobic:
                    //    //name = "Hydrophobic";
                    //    break;
                    //case (int)ModifierID.Nocturnal:
                    //    //name = "Nocturnal";
                    //    break;
                    //case (int)ModifierID.Mutated:
                    //    //name = "Mutated";
                    //    break;
                    //case (int)ModifierID.Abominable:
                    //    //name = "Abominable";
                    //    break;
                    //case (int)ModifierID.Deviating:
                    //    //name = "Deviating";
                    //    break;

                    //case (int)ModifierID.Shapeshifting:
                    //    //name = "Shapeshifting";
                    //    break;
                    //case (int)ModifierID.Royal:
                    //    //name = "Royal";
                    //    break;

                    //case (int)ModifierID.Burning:
                    //    //name = "Burning";
                    //    break;
                    //case (int)ModifierID.Delerious:
                    //    modifier = new Delerious();
                    //    break;

                    //case (int)ModifierID.Laggy:
                    //    //name = "Laggy";
                    //    break;
                    //case (int)ModifierID.Unstable:
                    //    //name = "Unstable";
                    //    break;
                    //case (int)ModifierID.Vampiric:
                    //    //name = "Vampiric";
                    //    break;
                    //case (int)ModifierID.Contagious:
                    //    //name = "Contagious";
                    //    break;
                    //case (int)ModifierID.Assassinating:
                    //    //name = "Assassinating";
                    //    break;
                    //case (int)ModifierID.Vaccinated:
                    //    //name = "Vaccinated";
                    //    break;

                    //case (int)ModifierID.Shadow:
                    //    //name = "Shadow";
                    //    break;

                    //case (int)ModifierID.Solidifed:
                    //    //name = "Solidifed";
                    //    break;
                    //case (int)ModifierID.Undying:
                    //    //name = "Undying";
                    //    break;
                    //case (int)ModifierID.TwentyTwo:
                    //    //name = "TwentyTwo";
                    //    break;
                    //case (int)ModifierID.Runic:
                    //    //name = "Runic";
                    //    break;
                    //case (int)ModifierID.Buoyant:
                    //    //name = "Buoyant";
                    //    break;
                    //case (int)ModifierID.Reflective:
                    //    //name = "Reflective";
                    //    break;
                    //case (int)ModifierID.Copycat:
                    //    //name = "Copycat";
                    //    break;
                    //case (int)ModifierID.Zombified:
                    //    //name = "Zombified";
                    //    break;
                    //case (int)ModifierID.Draining:
                    //    //name = "Draining";
                    //    break;
                    //case (int)ModifierID.Explosive:
                    //    //name = "Explosive";
                    //    break;

                    //case (int)ModifierID.Duke:
                    //    //name = "Duke";
                    //    break;
                    //case (int)ModifierID.Multiplying:
                    //    //name = "Multiplying";
                    //    break;
                    //case (int)ModifierID.Pyro:
                    //    //name = "Pyro";
                    //    break;
            }

            modifier.UpdateModifierStats(npc);
        }

        public void NetUpdateModifier(int npc, int modifierType)
        {
            if (Main.netMode == NetmodeID.SinglePlayer)
                return;

            var netMessage = mod.GetPacket();
            netMessage.Write((byte)1);
            netMessage.Write((byte)npc);
            netMessage.Write(modifierType); //these are the variables of the instance THAT CALLS THIS METHOD
            netMessage.Send();
        }

        public bool firstTick = true;
        
        public override bool PreAI(NPC npc)
        {
            if (firstTick && Main.rand.Next(100) <= EnemyModifiersConfig.Instance.ChanceForModifier)
            {
                if (!((npc.boss && !EnemyModifiersConfig.Instance.BossModifiers) || npc.townNPC || npc.friendly || npc.dontTakeDamage || npc.realLife != -1 || npc.SpawnedFromStatue || npc.type == NPCID.TargetDummy || EnemyModifiersConfig.Instance.NPCBlacklist.Contains(new NPCDefinition(npc.type))))
                {
                    int randomType = Main.rand.Next(EnemyModifiers.ModifierTypes);

                    NetUpdateModifier(npc.whoAmI, randomType);
                    ApplyModifier(npc, randomType);
                }
            }

            firstTick = false;

            if (modifier == null)
            {
                return true;
            }

            return modifier.PreAI(npc);
        }

        public override void AI(NPC npc)
        {
            if (npc.realLife != -1 && modifier == null)
            {
                NPC head = Main.npc[npc.realLife];

                if (head.GetGlobalNPC<EnemyModifiersGlobalNPC>().modifier != null)
                {
                    modifier = head.GetGlobalNPC<EnemyModifiersGlobalNPC>().modifier;
                    modifier.UpdateModifierStats(npc);
                }
            }

            float speedMulti = 1f;

            if (modifier != null)
            {
                speedMulti = modifier.speedMultiplier;
                modifier.AI(npc);
            }

            if (Rallied)
            {
                speedMulti += .25f;
            }

            UpdateSpeed(npc, speedMulti);
        }

        public override void PostAI(NPC npc)
        {
            if (modifier == null)
            {
                return;
            }

            modifier.PostAI(npc);
        }

        public void UpdateSpeed(NPC npc, float speedMultiplier)
        {
            if (speedMultiplier != 1)
            {
                //slower
                if (speedMultiplier < 1)
                {
                    float speedToSubtract = 1f - speedMultiplier;
                    npc.position -= npc.velocity * speedToSubtract;
                }
                //faster
                else
                {
                    float speedToAdd = speedMultiplier - 1f;
                    Vector2 newPos = npc.position + npc.velocity * speedToAdd;

                    if (!Collision.SolidCollision(newPos, npc.width, npc.height))
                    {
                        npc.position = newPos;
                    }
                }
            }
        }

        public override void GetChat(NPC npc, ref string chat)
        {
            if (modifier == null)
            {
                return;
            }

            modifier.GetChat(npc, ref chat);
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (modifier == null)
            {
                return;
            }

            modifier.UpdateLifeRegen(npc, ref damage);
        }

        public override void OnHitByItem(NPC npc, Player player, Item item, int damage, float knockback, bool crit)
        {
            if (modifier == null)
            {
                return;
            }

            modifier.OnHitByItem(npc, player);
        }

        public override void OnHitByProjectile(NPC npc, Projectile projectile, int damage, float knockback, bool crit)
        {
            if (modifier == null)
            {
                return;
            }

            modifier.OnHitByProjectile(npc, projectile);
        }

        public override void OnHitPlayer(NPC npc, Player target, int damage, bool crit)
        {
            if (modifier == null)
            {
                return;
            }

            modifier.OnHitPlayer(npc, target);
        }

        public override void ModifyHitByItem(NPC npc, Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            if (modifier == null)
            {
                return;
            }

            modifier.ModifyHitByItem(npc, player, item, ref damage, ref knockback);
        }

        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (modifier == null)
            {
                return;
            }

            modifier.ModifyHitByProjectile(npc, projectile, ref damage, ref knockback);
        }

        public override void ModifyHitPlayer(NPC npc, Player target, ref int damage, ref bool crit)
        {
            if (Rallied)
            {
                damage = (int)(damage * 1.25f);
            }
        }

        public override bool StrikeNPC(NPC npc, ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            if (Fortified)
                damage /= 2;

            return true;
        }

        public bool DropLoot = true;

        public override bool PreNPCLoot(NPC npc)
        {
            if (modifier == null)
            {
                return DropLoot;
            }

            return modifier.PreNPCLoot(npc) && DropLoot;
        }

        public override void NPCLoot(NPC npc)
        {
            if (modifier == null)
            {
                return;
            }

            modifier.NPCLoot(npc);
        }

        public override Color? GetAlpha(NPC npc, Color drawColor)
        {
            if (modifier == null)
            {
                return null;
            }

            return modifier.GetAlpha();
        }

        public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Color drawColor)
        {
            if (modifier == null)
            {
                return true;
            }

            return modifier.PreDraw(npc, spriteBatch, drawColor);
        }
    }
}
