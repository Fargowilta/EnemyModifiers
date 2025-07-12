using FargoEnemyModifiers.Modifiers;
using FargoEnemyModifiers.Modifiers.Rarity1;
using FargoEnemyModifiers.Modifiers.Rarity2;
using FargoEnemyModifiers.Modifiers.Rarity3;
using FargoEnemyModifiers.Modifiers.Rarity4;
using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace FargoEnemyModifiers
{
    //[Label("Enemy Modifiers Config")]
    public class EnemyModifiersServerConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        public static EnemyModifiersServerConfig Instance { get; private set; }

        public override void OnLoaded()
        {
            Instance = this;
        }

        [Header("ModifierConfiguration")]
        [DefaultValue(false)]
        public bool BossModifiers;

        [Increment(1)]
        [Range(0, 100)]
        [DefaultValue(50)]
        [Slider]
        public int ChanceForModifier;

        [Range(1, 10)]
        [DefaultValue(3)]
        [Slider]
        public int ModifierAmount;

        [Increment(1)]
        [Range(0, 100)]
        [DefaultValue(50)]
        [Slider]
        public int ChanceForExtraModifier;

        [DefaultValue(false)]
        public bool ForceModifier;

        [DefaultValue(0)]
        [DrawTicks]
        public ConfigModifierID ModifierEnum;

        [Header("ModifierToggleHeader")]
        [DefaultValue(true)]
        public bool Red;

        [DefaultValue(true)]
        public bool Yellow;

        [DefaultValue(true)]
        public bool Blue;

        [DefaultValue(true)]
        public bool Green;

        [DefaultValue(true)]
        public bool Orange;

        [DefaultValue(true)]
        public bool Purple;

        [DefaultValue(true)]
        public bool Pink;

        [Header("Rarity1Header")]
        [DefaultValue(true)]
        public bool Armored;

        [DefaultValue(true)]
        public bool Healthy;

        [DefaultValue(true)]
        public bool Impenetrable;

        [DefaultValue(true)]
        public bool Light;

        [DefaultValue(true)]
        public bool Menacing;



        [DefaultValue(true)]
        public bool Rampaging;

        [DefaultValue(true)]
        public bool Regenerating;

        [DefaultValue(true)]
        public bool Slow;

        [DefaultValue(true)]
        public bool Sluggish;

        [DefaultValue(true)]
        public bool Splitting;

        [DefaultValue(true)]
        public bool Swift;

        [DefaultValue(true)]
        public bool Unrelenting;

        [DefaultValue(true)]
        public bool Wealthy;

        [DefaultValue(true)]
        public bool Stinky;

        [DefaultValue(true)]
        public bool Shiny;

        [DefaultValue(true)]
        public bool Poor;

        [DefaultValue(true)]
        public bool Big;

        [DefaultValue(true)]
        public bool Small;

        [DefaultValue(true)]
        public bool Fishy;

        [Header("Rarity2Header")]

        [DefaultValue(true)]
        public bool Lovestruck;

        [DefaultValue(true)]
        public bool Accelerating;

        [DefaultValue(true)]
        public bool Bloodthirsty;

        [DefaultValue(true)]
        public bool Bulletproof;

        [DefaultValue(true)]
        public bool Flaming;

        [DefaultValue(true)]
        public bool Fortifying;

        [DefaultValue(true)]
        public bool Hexproof;

        [DefaultValue(true)]
        public bool Huge;

        [DefaultValue(true)]
        public bool Infested;

        [DefaultValue(true)]
        public bool Metallic;

        [DefaultValue(true)]
        public bool Miniature;

        [DefaultValue(true)]
        public bool Possessed;

        [DefaultValue(true)]
        public bool Rallying;

        [DefaultValue(true)]
        public bool Shielded;

        [DefaultValue(true)]
        public bool Stealthy;

        [DefaultValue(true)]
        public bool Vaccinated;

        [DefaultValue(true)]
        public bool Vampiric;

        [DefaultValue(true)]
        public bool Veiled;

        [DefaultValue(true)]
        public bool Reflective;

        [DefaultValue(true)]
        public bool Volatile;

        [DefaultValue(true)]
        public bool Frenzied;

        [DefaultValue(true)]
        public bool Scary;

        [DefaultValue(true)]
        public bool Sleepy;

        [DefaultValue(true)]
        public bool Polite;

        [DefaultValue(true)]
        public bool Smoky;

        [Header("Rarity3Header")]
        [DefaultValue(true)]
        public bool Chained;

        [DefaultValue(true)]
        public bool Devouring;

        [DefaultValue(true)]
        public bool Hyper;

        [DefaultValue(true)]
        public bool Imaginery;

        [DefaultValue(true)]
        public bool Juggernaut;

        [DefaultValue(true)]
        public bool Merchant;

        [DefaultValue(true)]
        public bool Showman;

        [DefaultValue(true)]
        public bool Solidifed;

        [DefaultValue(true)]
        public bool Undying;

        [DefaultValue(true)]
        public bool Worm;

        [DefaultValue(true)]
        public bool Swarming;

        [DefaultValue(true)]
        public bool Aquarian;

        [DefaultValue(true)]
        public bool Flucuating;

        [DefaultValue(true)]
        public bool Skeletal;

        [DefaultValue(true)]
        public bool Disguised;

        [DefaultValue(true)]
        public bool Shimmering;

        [DefaultValue(true)]
        public bool Celebratory;

        [DefaultValue(true)]
        public bool Sticky;

        [DefaultValue(true)]
        public bool Medic;

        [DefaultValue(true)]
        public bool Sorcerous;

        [DefaultValue(true)]
        public bool Creepy;

        [DefaultValue(true)]
        public bool Shy;

        [DefaultValue(true)]
        public bool Dramatic;

        [DefaultValue(true)]
        public bool Buzzing;

        [Header("Rarity4Header")]
        
        [DefaultValue(true)]
        public bool Royal;

        [DefaultValue(true)]
        public bool Rainbow;

        [DefaultValue(true)]
        public bool Modifying;



        [Header("Blacklists")]
        public List<NPCDefinition> NPCBlacklist = getDefaultNPCBlacklist();

        private static List<NPCDefinition> getDefaultNPCBlacklist()
        {
            List<NPCDefinition> NPCBlacklist = new List<NPCDefinition>
            {
            new NPCDefinition(NPCID.WallCreeper),
            new NPCDefinition(NPCID.WallCreeperWall),
            new NPCDefinition(NPCID.BlackRecluse),
            new NPCDefinition(NPCID.BlackRecluseWall),
            new NPCDefinition(NPCID.JungleCreeper),
            new NPCDefinition(NPCID.JungleCreeperWall),
            new NPCDefinition(NPCID.BloodCrawler),
            new NPCDefinition(NPCID.BloodCrawlerWall),
            new NPCDefinition(NPCID.DesertScorpionWall),
            new NPCDefinition(NPCID.DesertScorpionWall),

            new NPCDefinition(NPCID.BurningSphere),
            new NPCDefinition(NPCID.ChaosBall),
            new NPCDefinition(NPCID.WaterSphere),
            new NPCDefinition(NPCID.VileSpit),
            new NPCDefinition(NPCID.VileSpitEaterOfWorlds),
            new NPCDefinition(NPCID.DetonatingBubble),
            new NPCDefinition(NPCID.SolarFlare),
            new NPCDefinition(NPCID.SolarGoop),
            new NPCDefinition(NPCID.AncientLight),
            new NPCDefinition(NPCID.AncientDoom),

            new NPCDefinition(NPCID.CultistBossClone),

            new NPCDefinition(NPCID.SpikeBall),
            new NPCDefinition(NPCID.DD2EterniaCrystal),
            new NPCDefinition(NPCID.DD2LanePortal),

            new NPCDefinition(NPCID.EaterofWorldsHead),
            new NPCDefinition(NPCID.EaterofWorldsBody),
            new NPCDefinition(NPCID.EaterofWorldsTail),
            new NPCDefinition(NPCID.DD2LanePortal),
            new NPCDefinition(NPCID.DD2EterniaCrystal),
            new NPCDefinition(NPCID.LunarTowerNebula),
            new NPCDefinition(NPCID.LunarTowerSolar),
            new NPCDefinition(NPCID.LunarTowerStardust),
            new NPCDefinition(NPCID.LunarTowerVortex),
            };

            try
            {
                if (ModLoader.TryGetMod("Fargowiltas", out Mod fargoMod))
                {
                    NPCBlacklist.Add(new NPCDefinition(ModContent.Find<ModNPC>("Fargowiltas", "SuperDummy").Type));
                }

                if (ModLoader.TryGetMod("FargowiltasSouls", out Mod fargoMod2))
                {
                    NPCBlacklist.Add(new NPCDefinition(ModContent.Find<ModNPC>("FargowiltasSouls", "ShadowOrbNPC").Type));
                    NPCBlacklist.Add(new NPCDefinition(ModContent.Find<ModNPC>("FargowiltasSouls", "CrystalLeaf").Type));
                }

                if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
                {
                    //EnemyModifiersConfig.Instance.NPCBlacklist.Add(new NPCDefinition(calamityMod.GetContent("")));
                }
            }
            catch
            {

            }

            return NPCBlacklist;
        }

    }
}