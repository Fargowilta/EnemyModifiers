using System.Collections.Generic;
using System.ComponentModel;
using Terraria.ID;
using Terraria.ModLoader.Config;

namespace FargoEnemyModifiers
{
    [Label("Enemy Modifiers Config")]
    public class EnemyModifiersConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        public static EnemyModifiersConfig Instance { get; private set; }

        public override void OnLoaded()
        {
            Instance = this;
        }

        [Header("Modifier Configuration")] [Label("Bosses can get Modifiers")] [DefaultValue(false)]
        public bool BossModifiers;

        [Label("Modifier Chance (%)")] [Increment(1)] [Range(1, 100)] [DefaultValue(25)] [Slider]
        public int ChanceForModifier;

        //[Label("Set Modifier")]
        //[Tooltip("If true, all enemies will get the chosen modifier if any")]
        //[DefaultValue(false)]
        //public bool SetModifier;

        // TODO: finish this
        //[DefaultValue(ModifierID.Unrelenting)] [DrawTicks]
        //public ModifierID ModifierEnum;

        [Header("Blacklists")] [Label("NPC Blacklist")] [Tooltip("NPCs here can never receive modifiers")]
        public List<NPCDefinition> NPCBlacklist = new List<NPCDefinition>
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
            new NPCDefinition(NPCID.DetonatingBubble),
            new NPCDefinition(NPCID.SolarFlare),
            new NPCDefinition(NPCID.SolarGoop),
            new NPCDefinition(NPCID.AncientLight),
            new NPCDefinition(NPCID.AncientDoom),

            new NPCDefinition(NPCID.CultistBossClone),
            //new NPCDefinition(NPCID.PlanterasHook),
            //new NPCDefinition(NPCID.PlanterasTentacle),
            //new NPCDefinition(NPCID.PirateShipCannon),

            new NPCDefinition(NPCID.SpikeBall),
            new NPCDefinition(NPCID.DD2EterniaCrystal),
            new NPCDefinition(NPCID.DD2LanePortal),
        };
    }
}