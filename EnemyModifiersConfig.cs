using FargoEnemyModifiers.Modifiers;
using System.Collections.Generic;
using System.ComponentModel;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace FargoEnemyModifiers
{
    //[Label("Enemy Modifiers Config")]
    public class EnemyModifiersConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        public static EnemyModifiersConfig Instance { get; private set; }

        public override void OnLoaded()
        {
            Instance = this;
        }

        [Header("$Mods.FargoEnemyModifiers.Configs.EnemyModifiersConfig.Header1")]
        [Label("Modifier Announcements")]
        [Tooltip("An enemies modifier will appear above its head for a while")]
        [DefaultValue(true)]
        public bool ModifierAnnouncements;

        [Label("Announcements Last Forever")]
        [Tooltip("The Modifier name will always appear above the enemy")]
        [DefaultValue(false)]
        public bool AnnouncementsForever;

        [Label("Bosses can get Modifiers")]
        [DefaultValue(false)]
        public bool BossModifiers;

        [Label("Modifier Chance (%)")]
        [Increment(1)]
        [Range(0, 100)]
        [DefaultValue(50)]
        [Slider]
        public int ChanceForModifier;

        [Label("Maximum Amount of Possible Modifiers")]
        [Range(1, 10)]
        [DefaultValue(3)]
        [Slider]
        public int ModifierAmount;

        [Label("Extra Modifier Chance (%)")]
        [Increment(1)]
        [Range(0, 100)]
        [DefaultValue(50)]
        [Slider]
        public int ChanceForExtraModifier;

        

        [Label("Force A Modifier")]
        [DefaultValue(false)]
        public bool ForceModifier;

        [Label("Modifier to Force")]
        [DefaultValue(0)]
        [DrawTicks]
        public ModifierID ModifierEnum;

        //[Header("Blacklists")]
        [Header("$Mods.FargoEnemyModifiers.Configs.EnemyModifiersConfig.Header2")]
        [Label("NPC Blacklist")]
        [Tooltip("NPCs here can never receive modifiers")]
        public List<NPCDefinition> NPCBlacklist = getDefaultNPCBlacklist();


        [Label("Modifier Blacklist")]
        [Tooltip("These modifiers will not appear")]
        public List<ModifierPicker> ModifierBlacklist;

        [BackgroundColor(0, 255, 255)]
        [Label("Pair label")]
        public class ModifierPicker
        {
            [Label("Modifier to Blacklist")]
            [DefaultValue(0)]
            [DrawTicks]
            public ModifierID ModifierEnum;

            //public string description
            //{
            //    get
            //    {
            //        return EnemyModifiers.Modifiers[(int)ModifierEnum].Description;
            //    }
            //    set
            //    {

            //    }
            //}
        }

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