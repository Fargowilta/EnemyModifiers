using System;
using System.Collections.Generic;
using System.Linq;
using FargoEnemyModifiers.Modifiers;
using FargoEnemyModifiers.NetCode;
using FargoEnemyModifiers.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace FargoEnemyModifiers
{
    public class EnemyModifiersGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public List<Modifier> Modifiers = new List<Modifier>();

        public List<ModifierID> modifierTypes = new List<ModifierID>();

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
        private int _modifierNameLength;
        private int _combatTextIndex = -1;
        private RarityID _highestRarity = RarityID.Hidden;
        private bool _nameSpawned;
        private bool? _noAnnouncement = null;
        private string _originalName;
        private string _combinedModifierName;
        private int _tickCounter;

        public override bool PreAI(NPC npc)
        {
            if (firstTick)
            {
                _originalName = npc.FullName;

                if (Main.netMode == NetmodeID.SinglePlayer || Main.netMode == NetmodeID.Server)
                {
                    if (!((npc.boss && !EnemyModifiersServerConfig.Instance.BossModifiers) ||
                          npc.townNPC || npc.friendly || npc.CountsAsACritter ||
                          npc.dontTakeDamage || npc.realLife != -1 || npc.SpawnedFromStatue ||
                          npc.type == NPCID.TargetDummy || 
                          EnemyModifiersServerConfig.Instance.NPCBlacklist.Contains(new NPCDefinition(npc.type))))
                    {
                        if (Main.rand.Next(100) < EnemyModifiersServerConfig.Instance.ChanceForModifier)
                        {
                            for (int i = 0; i < EnemyModifiersServerConfig.Instance.ModifierAmount; i++)
                            {
                                ModifierID modifierType = PickModifier(npc);
                                ApplyModifier(npc, modifierType);

                                if (!(Main.rand.Next(100) < EnemyModifiersServerConfig.Instance.ChanceForExtraModifier))
                                {
                                    break;
                                }
                            }

                            switch (Main.netMode)
                            {
                                case NetmodeID.SinglePlayer:
                                    FinalizeModifierName(npc); // Server doesn't want that. MP Client handles it on packet receive.
                                    break;
                                case NetmodeID.Server:
                                {
                                    EnemyModifiersGlobalNPC globalNPC = npc.GetGlobalNPC<EnemyModifiersGlobalNPC>();

                                    ModPacket packet = Mod.GetPacket();
                                    packet.Write((byte)PacketID.MobSpawn);
                                    packet.Write((byte)npc.whoAmI);
                                    packet.Write((byte)globalNPC.modifierTypes.Count);
                                    foreach (int modifierType in globalNPC.modifierTypes)
                                    {
                                        packet.Write((byte)modifierType);
                                    }

                                    packet.Send();
                                    break;
                                }
                                // No implementation for NetmodeID.MultiplayerClient
                            }
                        }
                    }
                }
                
            }

            firstTick = false;

            if (Modifiers.Count == 0)
                return base.PreAI(npc);

            bool retVal = base.PreAI(npc);

            foreach (Modifier modifier in Modifiers.ToList())
            {
                retVal &= modifier.PreAI(npc);
            }

            if (_noAnnouncement == null || !_noAnnouncement.Value)
            {
                ShowModifierName(npc);
            }

            return retVal;
        }

        public void ShowModifierName(NPC npc)
        {
            if (Main.netMode == NetmodeID.Server)
                return;

            if (!EnemyModifiersClientConfig.Instance.announcementsEnabled)
                return;

            if (!npc.active)
            {
                return;
            }

            if (_noAnnouncement == null)
            {
                foreach (Modifier modifier in Modifiers)
                {
                    if (!modifier.AllowAnnounceModifier())
                    {
                        _noAnnouncement = true;
                        break;
                    }

                    if ((int)modifier.Rarity > (int)_highestRarity)
                    {
                        _highestRarity = modifier.Rarity;
                    }
                }

                _noAnnouncement = false;
            }

            if (!EnemyModifiersClientConfig.Instance.announcementsForever &&
                _tickCounter >= (EnemyModifiersClientConfig.Instance.announcementsDuration * 60))
            {
                _noAnnouncement = true;
                return;
            }

            if (_nameSpawned)
            {
                if (_combatTextIndex == -1)
                    return;

                CombatText text = Main.combatText[_combatTextIndex];
                text.lifeTime =
                    2; // Reset it to 2 instead of adding; adding causes issues with HYPER and SLUGGISH
                text.position =
                    new Vector2(npc.Center.X - (_modifierNameLength), npc.Center.Y - 50);
                text.color = GetColor(_highestRarity);
                
                //Color lightNearNPC =  (Lighting.GetColor((int)(npc.Center.X / 16), (int)(npc.Center.Y / 16)));

                //text.alpha = (lightNearNPC.R + lightNearNPC.G + lightNearNPC.B) / 255;
                //Main.NewText((lightNearNPC.R + lightNearNPC.G + lightNearNPC.B) + " " + text.alpha);

                _tickCounter++;
            }
            else
            {
                if (string.IsNullOrEmpty(_combinedModifierName))
                {
                    return;
                }

                if (npc.realLife != -1)
                {
                    _nameSpawned = true;
                    return;
                }

                _modifierNameLength = _combinedModifierName.Length / 2 * 8;
                _combatTextIndex =
                    CombatText.NewText(npc.Hitbox, GetColor(_highestRarity), _combinedModifierName);
                if (_combatTextIndex == 100) // This occurs when too many texts spawn at once
                {
                    _combatTextIndex = -1;
                    return;
                }

                Main.combatText[_combatTextIndex].lifeTime = 2;
                _nameSpawned = true;
            }
        }

        public void ResetAnnouncement()
        {
            _nameSpawned = false;
            _noAnnouncement = null;
            _tickCounter = 0;
        }

        private Color GetColor(RarityID rarity)
        {
            return rarity switch
            {
                RarityID.Common => Color.White,
                RarityID.Uncommon => Color.Yellow,
                RarityID.Rare => Color.Red,
                RarityID.Mythic => Main.DiscoColor,
                _ => Color.White
            };
        }

        public void FinalizeModifierName(NPC npc)
        {
            GetCombinedModifierName();
            npc.GivenName = (_combinedModifierName + _originalName);
        }

        public void GetCombinedModifierName()
        {
            _combinedModifierName = "";

            foreach (Modifier modifier in Modifiers)
            {
                _combinedModifierName = _combinedModifierName + " " + modifier.Name;
            }

            _combinedModifierName= _combinedModifierName.Trim();
            _combinedModifierName += " ";
        }

        public ModifierID PickModifier(NPC npc)
        {
            ModifierID modifierType;
            Modifier modifier;

            List<ModifierID> modifierKeys = EnemyModifiers.Modifiers.Keys.ToList();

            if (EnemyModifiersServerConfig.Instance.ForceModifier && !modifierTypes.Contains((ModifierID)EnemyModifiersServerConfig.Instance.ModifierEnum))
            {
                modifierType = (ModifierID)EnemyModifiersServerConfig.Instance.ModifierEnum;
            }
            else
            {
                do
                {
                    int modifierIndex = Main.rand.Next(EnemyModifiers.Modifiers.Count);
                    modifierType = modifierKeys[modifierIndex];
                    modifier = EnemyModifiers.Modifiers[modifierType];

                } while (IsBlacklistedModifier(modifierType) ||
                         !modifier.ExtraCondition(npc) ||
                         !RarityCheck(modifier) ||
                         !AddColorChanger(modifier) ||
                         modifierTypes.Contains(modifierType) ||
                         modifier.Rarity == RarityID.Hidden);
            }

            return modifierType;
        }

        public void ApplyModifier(NPC npc, ModifierID type)
        {
            // Main.NewText("Applying " + type + " modifiers list: " + Modifiers.Count);

            Modifier modifier = Activator.CreateInstance(EnemyModifiers.Modifiers[type].GetType()) as Modifier;
            modifier.Setup(npc);
            modifier.UpdateModifierStats(npc, true);

            Modifiers.Add(modifier);
            modifierTypes.Add(type);
        }

        private bool IsBlacklistedModifier(ModifierID type)
        {
            // If we get more of these, it would be nice to have a const list/array
            if (Main.netMode != NetmodeID.SinglePlayer && type == ModifierID.Worm)
            {
                return true;
            }

            switch (type)
            {
                case ModifierID.Armored:
                    return !EnemyModifiersServerConfig.Instance.Armored;

                case ModifierID.Blue:
                    return !EnemyModifiersServerConfig.Instance.Blue;

                case ModifierID.Green:
                    return !EnemyModifiersServerConfig.Instance.Green;

                case ModifierID.Healthy:
                    return !EnemyModifiersServerConfig.Instance.Healthy;

                case ModifierID.Impenetrable:
                    return !EnemyModifiersServerConfig.Instance.Impenetrable;

                case ModifierID.Light:
                    return !EnemyModifiersServerConfig.Instance.Light;

                case ModifierID.Menacing:
                    return !EnemyModifiersServerConfig.Instance.Menacing;

                case ModifierID.Orange:
                    return !EnemyModifiersServerConfig.Instance.Orange;

                case ModifierID.Purple:
                    return !EnemyModifiersServerConfig.Instance.Purple;

                case ModifierID.Rampaging:
                    return !EnemyModifiersServerConfig.Instance.Rampaging;

                case ModifierID.Red:
                    return !EnemyModifiersServerConfig.Instance.Red;

                case ModifierID.Regenerating:
                    return !EnemyModifiersServerConfig.Instance.Regenerating;

                case ModifierID.Slow:
                    return !EnemyModifiersServerConfig.Instance.Slow;

                case ModifierID.Sluggish:
                    return !EnemyModifiersServerConfig.Instance.Sluggish;

                case ModifierID.Splitting:
                    return !EnemyModifiersServerConfig.Instance.Splitting;

                case ModifierID.Swift:
                    return !EnemyModifiersServerConfig.Instance.Swift;

                case ModifierID.Unrelenting:
                    return !EnemyModifiersServerConfig.Instance.Unrelenting;

                case ModifierID.Wealthy:
                    return !EnemyModifiersServerConfig.Instance.Wealthy;

                case ModifierID.Yellow:
                    return !EnemyModifiersServerConfig.Instance.Yellow;

                case ModifierID.Stinky:
                    return !EnemyModifiersServerConfig.Instance.Stinky;

                case ModifierID.Shiny:
                    return !EnemyModifiersServerConfig.Instance.Shiny;

                case ModifierID.Lovestruck:
                    return !EnemyModifiersServerConfig.Instance.Lovestruck;

                case ModifierID.Poor:
                    return !EnemyModifiersServerConfig.Instance.Poor;

                case ModifierID.Pink:
                    return !EnemyModifiersServerConfig.Instance.Pink;

                case ModifierID.Big:
                    return !EnemyModifiersServerConfig.Instance.Big;

                case ModifierID.Small:
                    return !EnemyModifiersServerConfig.Instance.Small;

                case ModifierID.Fishy:
                    return !EnemyModifiersServerConfig.Instance.Fishy;

                case ModifierID.Accelerating:
                    return !EnemyModifiersServerConfig.Instance.Accelerating;

                case ModifierID.Bloodthirsty:
                    return !EnemyModifiersServerConfig.Instance.Bloodthirsty;

                case ModifierID.Bulletproof:
                    return !EnemyModifiersServerConfig.Instance.Bulletproof;

                case ModifierID.Flaming:
                    return !EnemyModifiersServerConfig.Instance.Flaming;

                case ModifierID.Fortifying:
                    return !EnemyModifiersServerConfig.Instance.Fortifying;

                case ModifierID.Hexproof:
                    return !EnemyModifiersServerConfig.Instance.Hexproof;

                case ModifierID.Huge:
                    return !EnemyModifiersServerConfig.Instance.Huge;

                case ModifierID.Infested:
                    return !EnemyModifiersServerConfig.Instance.Infested;

                case ModifierID.Metallic:
                    return !EnemyModifiersServerConfig.Instance.Metallic;

                case ModifierID.Miniature:
                    return !EnemyModifiersServerConfig.Instance.Miniature;

                case ModifierID.Possessed:
                    return !EnemyModifiersServerConfig.Instance.Possessed;

                case ModifierID.Rallying:
                    return !EnemyModifiersServerConfig.Instance.Rallying;

                case ModifierID.Shielded:
                    return !EnemyModifiersServerConfig.Instance.Shielded;

                case ModifierID.Stealthy:
                    return !EnemyModifiersServerConfig.Instance.Stealthy;

                case ModifierID.Vaccinated:
                    return !EnemyModifiersServerConfig.Instance.Vaccinated;

                case ModifierID.Vampiric:
                    return !EnemyModifiersServerConfig.Instance.Vampiric;

                case ModifierID.Veiled:
                    return !EnemyModifiersServerConfig.Instance.Veiled;

                case ModifierID.Reflective:
                    return !EnemyModifiersServerConfig.Instance.Reflective;

                case ModifierID.Volatile:
                    return !EnemyModifiersServerConfig.Instance.Volatile;

                case ModifierID.Frenzied:
                    return !EnemyModifiersServerConfig.Instance.Frenzied;

                case ModifierID.Scary:
                    return !EnemyModifiersServerConfig.Instance.Scary;

                case ModifierID.Sleepy:
                    return !EnemyModifiersServerConfig.Instance.Sleepy;

                case ModifierID.Polite:
                    return !EnemyModifiersServerConfig.Instance.Polite;

                case ModifierID.Smoky:
                    return !EnemyModifiersServerConfig.Instance.Smoky;

                case ModifierID.Chained:
                    return !EnemyModifiersServerConfig.Instance.Chained;

                case ModifierID.Devouring:
                    return !EnemyModifiersServerConfig.Instance.Devouring;

                case ModifierID.Hyper:
                    return !EnemyModifiersServerConfig.Instance.Hyper;

                case ModifierID.Imaginary:
                    return !EnemyModifiersServerConfig.Instance.Imaginery;

                case ModifierID.Juggernaut:
                    return !EnemyModifiersServerConfig.Instance.Juggernaut;

                case ModifierID.Merchant:
                    return !EnemyModifiersServerConfig.Instance.Merchant;

                case ModifierID.Showman:
                    return !EnemyModifiersServerConfig.Instance.Showman;

                case ModifierID.Solidified:
                    return !EnemyModifiersServerConfig.Instance.Solidifed;

                case ModifierID.Undying:
                    return !EnemyModifiersServerConfig.Instance.Undying;

                case ModifierID.Worm:
                    return !EnemyModifiersServerConfig.Instance.Worm;

                case ModifierID.Swarming:
                    return !EnemyModifiersServerConfig.Instance.Swarming;

                case ModifierID.Aquarian:
                    return !EnemyModifiersServerConfig.Instance.Aquarian;

                case ModifierID.Fluctuating:
                    return !EnemyModifiersServerConfig.Instance.Flucuating;

                case ModifierID.Skeletal:
                    return !EnemyModifiersServerConfig.Instance.Skeletal;

                case ModifierID.Disguised:
                    return !EnemyModifiersServerConfig.Instance.Disguised;

                case ModifierID.Shimmering:
                    return !EnemyModifiersServerConfig.Instance.Shimmering;

                case ModifierID.Celebratory:
                    return !EnemyModifiersServerConfig.Instance.Celebratory;

                case ModifierID.Sticky:
                    return !EnemyModifiersServerConfig.Instance.Sticky;

                case ModifierID.Medic:
                    return !EnemyModifiersServerConfig.Instance.Medic;

                case ModifierID.Sorcerous:
                    return !EnemyModifiersServerConfig.Instance.Sorcerous;

                case ModifierID.Creepy:
                    return !EnemyModifiersServerConfig.Instance.Creepy;

                case ModifierID.Shy:
                    return !EnemyModifiersServerConfig.Instance.Shy;

                case ModifierID.Dramatic:
                    return !EnemyModifiersServerConfig.Instance.Dramatic;

                case ModifierID.Buzzing:
                    return !EnemyModifiersServerConfig.Instance.Buzzing;

                case ModifierID.Royal:
                    return !EnemyModifiersServerConfig.Instance.Royal;

                case ModifierID.Rainbow:
                    return !EnemyModifiersServerConfig.Instance.Rainbow;

                case ModifierID.Modifying:
                    return !EnemyModifiersServerConfig.Instance.Modifying;

            }

            //foreach (EnemyModifiersServerConfig.ModifierPicker picker in EnemyModifiersServerConfig.Instance.ModifierBlacklist)
            //{
            //    ModifierID blacklistedModifier = (ModifierID)picker.ModifierEnum;

            //    if (type == blacklistedModifier)
            //    {
            //        return true;
            //    }
            //}

            return false;
        }

        private bool RarityCheck(Modifier type)
        {
            int rarity = (int)type.Rarity;

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
                    Modifiers.ForEach(x => x.UpdateModifierStats(npc, true));
                    //modifier.UpdateModifierStats(npc);
                }
            }

            float speedMulti = 1f;

            if (Modifiers.Count != 0)
            {
                Modifiers.ForEach(x => speedMulti *= x.SpeedMultiplier);
                foreach (Modifier modifier in Modifiers.ToList())
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
                            if (!npc.active) return;
                            base.AI(npc);
                            break;
                        case AiOverrideStyle.PostVanilla:
                            base.AI(npc);
                            if (!npc.active) return;
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

        public override void HitEffect(NPC npc, NPC.HitInfo hit)
        {
            foreach (Modifier modifier in Modifiers)
            {
                modifier.HitEffect(npc, hit);
            }
        }

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
                retVal &= modifier.PreDraw(npc, spriteBatch, screenPos, drawColor);
            }
            
            return retVal;
        }

        
    }
}