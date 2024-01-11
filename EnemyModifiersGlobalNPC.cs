using System;
using System.Collections.Generic;
using System.Linq;
using FargoEnemyModifiers.Modifiers;
using FargoEnemyModifiers.NetCode;
using Microsoft.CodeAnalysis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Terraria.WorldBuilding;

namespace FargoEnemyModifiers
{
    public class EnemyModifiersGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public List<Modifier> Modifiers = new List<Modifier>();

        public List<int> modifierTypes = new List<int>();

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

        public bool firstTick = true;

        public const int ANNOUNCEMENT_DURATION = 300;
        private int modifierNameLength;
        public int combatTextIndex = -1;
        private int nameSpawn = 0;
        private bool noAnnouncement = false;
        private string combinedModifierName;

        public override bool PreAI(NPC npc)
        {
            if (firstTick)
            {
                if (Main.rand.Next(100) < EnemyModifiersConfig.Instance.ChanceForModifier)
                {
                    if (!((npc.boss && !EnemyModifiersConfig.Instance.BossModifiers) || 
                          npc.townNPC || npc.friendly || NPCID.Sets.CountsAsCritter[npc.type] || 
                          npc.dontTakeDamage || npc.realLife != -1 || npc.SpawnedFromStatue ||
                          npc.type == NPCID.TargetDummy || 
                          EnemyModifiersConfig.Instance.NPCBlacklist.Contains(new NPCDefinition(npc.type))))
                    {
                        if (Main.netMode == NetmodeID.SinglePlayer || Main.netMode == NetmodeID.Server)
                        {
                            for (int i = 0; i < EnemyModifiersConfig.Instance.ModifierAmount; i++)
                            {
                                int modifierType = PickModifier(npc);
                                ApplyModifier(npc, modifierType);

                                if (!(Main.rand.Next(100) < EnemyModifiersConfig.Instance.ChanceForExtraModifier))
                                {
                                    break;
                                }
                            }
                            
                            if (Main.netMode == NetmodeID.SinglePlayer)
                            {
                                finalizeModifierName(npc); // Server doesn't want that. MP Client handles it on packet receive.
                            }
                            else if (Main.netMode == NetmodeID.Server)
                            {
                                EnemyModifiersGlobalNPC globalNPC = npc.GetGlobalNPC<EnemyModifiersGlobalNPC>();

                                ModPacket packet = Mod.GetPacket();
                                packet.Write((byte)0);
                                packet.Write((byte)npc.whoAmI);
                                packet.Write((byte)globalNPC.modifierTypes.Count);
                                foreach (int modifierType in globalNPC.modifierTypes)
                                {
                                    packet.Write((byte)modifierType);
                                }

                                packet.Send();
                            }
                        }
                    }
                }
                
            }

            firstTick = false;

            if (Modifiers.Count == 0)
                return base.PreAI(npc);

            bool retVal = base.PreAI(npc);

            foreach (Modifier modifier in Modifiers)
            {
                retVal &= modifier.PreAI(npc);
            }

            // Return if we're on a server, since the rest of this code is only for clients
            if (Main.netMode == NetmodeID.Server) return retVal;

            //spawn when on screen, rarities have their own color
            if (EnemyModifiersConfig.Instance.ModifierAnnouncements && nameSpawn <= ANNOUNCEMENT_DURATION)
            {
                int highestRarity = 0;

                foreach (Modifier modifier in Modifiers)
                {
                    if (!modifier.AllowAnnounceModifier())
                    {
                        noAnnouncement = true;
                        break;
                    }

                    if (npc.realLife == -1)
                    {
                        if (Vector2.Distance(npc.Center, Main.player[Main.myPlayer].Center) < 800 && nameSpawn == 0)
                        {
                            nameSpawn = 1;
                        }
                    }
                }

                if (!noAnnouncement)
                {
                    if (nameSpawn == 1)
                    {
                        nameSpawn++;
                        modifierNameLength = combinedModifierName.Length / 2 * 8;

                        Color color = Color.White;

                        foreach (Modifier modifier in Modifiers)
                        {
                            if (modifier.Rarity > highestRarity)
                            {
                                highestRarity = modifier.Rarity;
                            }
                        }

                        switch (highestRarity)
                        {
                            case 1:
                                color = Color.White;
                                break;

                            case 2:
                                color = Color.Yellow;
                                break;

                            case 3:
                                color = Color.Red;
                                break;

                            case 4:
                                color = Main.DiscoColor;
                                break;
                        }

                        combatTextIndex = CombatText.NewText(npc.Hitbox, color, combinedModifierName);

                        Main.combatText[combatTextIndex].lifeTime = 2;
                    }
                    else if (nameSpawn != 0 && ((EnemyModifiersConfig.Instance.AnnouncementsForever) || (++nameSpawn <= ANNOUNCEMENT_DURATION)))
                    {
                        Main.combatText[combatTextIndex].lifeTime++;
                        Main.combatText[combatTextIndex].position = new Vector2(npc.Center.X - (modifierNameLength), npc.Center.Y - 50);
                        Main.combatText[combatTextIndex].alpha -= 0.0075f;
                    }
                }
            }

            return retVal;
        }

        public void finalizeModifierName(NPC npc)
        {
            getCombinedModifierName();
            npc.GivenName = combinedModifierName + npc.FullName;
        }

        private void getCombinedModifierName()
        {
            combinedModifierName = "";

            foreach (Modifier modifier in Modifiers)
            {
                combinedModifierName = combinedModifierName + " " + modifier.Name;
            }

            combinedModifierName += " ";
        }

        public int PickModifier(NPC npc)
        {
            int modifierType;
            Modifier modifier;

            if (EnemyModifiersConfig.Instance.ForceModifier && !modifierTypes.Contains((int)EnemyModifiersConfig.Instance.ModifierEnum))
            {
                modifierType = (int)EnemyModifiersConfig.Instance.ModifierEnum;
            }
            else
            {
                do
                {
                    modifierType = Main.rand.Next(EnemyModifiers.Modifiers.Count);
                    modifier = EnemyModifiers.Modifiers[modifierType];

                } while (IsBlacklistedModifier(modifierType)
                || !modifier.ExtraCondition(npc) || !RarityCheck(modifier) || !AddColorChanger(modifier) || modifierTypes.Contains(modifierType));
            }

            modifierTypes.Add(modifierType);
            return modifierType;
        }

        public void ApplyModifier(NPC npc, int type)
        {
            if (type < 0 || type >= EnemyModifiers.Modifiers.Count)
                return;

            // Main.NewText("Applying " + type + " modifiers list: " + Modifiers.Count);

            Modifier modifier = Activator.CreateInstance(EnemyModifiers.Modifiers[type].GetType()) as Modifier;
            modifier.Setup(npc);
            modifier.UpdateModifierStats(npc);

            Modifiers.Add(modifier);
        }

        private bool IsBlacklistedModifier(int type)
        {
            if (EnemyModifiersConfig.Instance.ModifierBlacklist == null)
            {
                return false;
            }

            foreach (EnemyModifiersConfig.ModifierPicker picker in EnemyModifiersConfig.Instance.ModifierBlacklist)
            {
                int blacklistedModifier = (int)picker.ModifierEnum;

                if (type == blacklistedModifier)
                {
                    return true;
                }
            }

            return false;
        }

        private bool RarityCheck(Modifier type)
        {
            int rarity = type.Rarity;

            if (rarity == 1 || (rarity == 2 && Main.rand.NextBool(2)) || ( rarity == 3 && Main.rand.NextBool(3)) || ( rarity == 4 && Main.rand.NextBool(4)))
            {
                return true;
            }

            return false;
        }

        //colors show up 5x less when they already have one
        private bool AddColorChanger(Modifier type)
        {
            if (!type.ColorChanger)
            {
                return true;
            }

            //only gets her if new modifier is color changer
            bool retVal = true;

            foreach (Modifier modifier in Modifiers)
            {
                if (modifier.ColorChanger && Main.rand.NextBool(5))
                {
                    retVal = false;
                    break;
                }
            }

            return retVal;
        }

        public override void AI(NPC npc)
        {
            if (npc.realLife != -1 && Modifiers.Count == 0)
            {
                NPC head = Main.npc[npc.realLife];

                if (head.GetGlobalNPC<EnemyModifiersGlobalNPC>().Modifiers.Count != 0)
                {
                    Modifiers = new List<Modifier>(head.GetGlobalNPC<EnemyModifiersGlobalNPC>().Modifiers);
                    Modifiers.ForEach(x => x.UpdateModifierStats(npc));
                    //modifier.UpdateModifierStats(npc);
                }
            }

            float speedMulti = 1f;

            if (Modifiers.Count != 0)
            {
                Modifiers.ForEach(x => speedMulti *= x.SpeedMultiplier);
                foreach (Modifier modifier in Modifiers)
                {
                    switch (modifier.AiOverride)
                    {
                        case AiOverrideStyle.None:
                            base.AI(npc);
                            break;
                        case AiOverrideStyle.Override:
                            modifier.AI(npc);
                            break;
                        case AiOverrideStyle.PreVanilla:
                            modifier.AI(npc);
                            base.AI(npc);
                            break;
                        case AiOverrideStyle.PostVanilla:
                            base.AI(npc);
                            modifier.AI(npc);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(modifier.AiOverride), modifier.AiOverride, "Invalid AiOverrideStyle");
                    }
                }
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

        public override bool? CanChat(NPC npc)
        {
            bool? retVal = base.CanChat(npc);

            foreach (Modifier modifier in Modifiers)
            {
                if (modifier.CanChat(npc) != null)
                {
                    if (retVal == null)
                    {
                        retVal = modifier.CanChat(npc);
                    }
                    else
                    {
                        retVal &= modifier.CanChat(npc);
                    }
                }
            }

            return retVal;
        }

        public override void GetChat(NPC npc, ref string chat)
        {
            foreach (Modifier modifier in Modifiers)
            {
                modifier.GetChat(npc, ref chat);
            }
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            foreach (Modifier modifier in Modifiers)
            {
                modifier.UpdateLifeRegen(npc, ref damage);
            }            
        }

        public override void OnHitByItem(NPC npc, Player player, Item item, NPC.HitInfo hit, int damageDone)
        {
            foreach (Modifier modifier in Modifiers)
            {
                modifier.OnHitByItem(npc, player);
            }
        }

        public override void OnHitByProjectile(NPC npc, Projectile projectile, NPC.HitInfo hit, int damageDone)
        {
            Modifiers?.ForEach(x => x.OnHitByProjectile(npc, projectile));
        }

        public override void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
        {
            Modifiers?.ForEach(x => x.OnHitPlayer(npc, target));
        }

        public override void ModifyHitByItem(NPC npc, Player player, Item item, ref NPC.HitModifiers modifiers)
        {
            foreach (Modifier modifier in Modifiers)
            {
                modifier.ModifyHitByItem(npc, player, item, ref modifiers);
            }
        }

        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            foreach (Modifier modifier in Modifiers)
            {
                modifier.ModifyHitByProjectile(npc, projectile, ref modifiers);
            }
        }

        public override void ModifyHitPlayer(NPC npc, Player target, ref Player.HurtModifiers modifiers)
        {
            if (Rallied)
            {
                modifiers.SourceDamage *= 1.25f;
            }
        }

        public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
        {
            if (Fortified)
                modifiers.FinalDamage *= 0.5f;

            foreach (Modifier modifier in Modifiers)
            {
                modifier.ModifyIncomingHit(npc, ref modifiers);
            }
        }

        public override bool PreKill(NPC npc)
        {
            bool retVal = base.PreKill(npc);

            foreach (Modifier modifier in Modifiers)
            {
                retVal &= modifier.PreNPCLoot(npc);
            }

            return retVal && DropLoot;
        }

        public override bool CheckDead(NPC npc)
        {
            //if (nameSpawn > 0 && nameSpawn < 300)
            //{
            //    Main.combatText[combatTextIndex].lifeTime = 0;
            //}

            bool retVal = base.CheckDead(npc);

            foreach (Modifier modifier in Modifiers)
            {
                retVal &= modifier.CheckDead(npc);
            }

            return retVal;
        }

        public override bool SpecialOnKill(NPC npc)
        {
            bool retVal = base.SpecialOnKill(npc);

            foreach (Modifier modifier in Modifiers)
            {
                retVal &= modifier.SpecialOnKill(npc);
            }

            return retVal;
        }

        private bool firstLoot = true;

        public override void OnKill(NPC npc)
        {
            int lootMulti = 1;

            foreach (Modifier modifier in Modifiers)
            {
                modifier.OnKill(npc);
                lootMulti += modifier.LootMultiplier;
            }

            if (firstLoot)
            {
                firstLoot = false;

                for (int i = 1; i < lootMulti; i++)
                {
                    npc.NPCLoot();
                    //npc.value = 0;

                    if (NPC.killCount[Item.NPCtoBanner(npc.BannerID())] % 50 != 0)
                        NPC.killCount[Item.NPCtoBanner(npc.BannerID())]--;
                }
            }

            firstLoot = false;


        }

        public override Color? GetAlpha(NPC npc, Color drawColor)
        {
            Color? retVal = base.GetAlpha(npc, drawColor);

            foreach (Modifier modifier in Modifiers)
            {
                if (modifier.GetAlpha() != null)
                {
                    if (retVal == null)
                    {
                        retVal = modifier.GetAlpha();
                    }
                    else
                    {
                        Color newColor = modifier.GetAlpha().Value;
                        retVal = MultiplyColors(retVal.Value, newColor);
                    }
                }
            }

            return retVal;
        }

        private Color MultiplyColors(Color firstColor, Color secondColor)
        {
            int r = multiplyColor(firstColor.R, secondColor.R);
            int g = multiplyColor(firstColor.G, secondColor.G);
            int b = multiplyColor(firstColor.B, secondColor.B);
            int a = firstColor.A;

            if (secondColor.A < a)
            {
                a = secondColor.A;
            }

            return new Color(r, g, b, a);
        }

        private int multiplyColor(byte color1, byte color2)
        {
            if (color1 == 0 && color2 != 0)
            {
                return color2;
            }
            if (color2 == 0 && color1 != 0)
            {
                return color1;
            }

            return (int)((float)(color1 * color2) / 255f);
        }

        public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            bool retVal = base.PreDraw(npc, spriteBatch, screenPos, drawColor);

            foreach (Modifier modifier in Modifiers)
            {
                retVal &= modifier.PreDraw(npc, spriteBatch, drawColor);
            }
            
            return retVal;
        }
    }
}