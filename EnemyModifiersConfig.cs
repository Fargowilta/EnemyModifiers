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

        [Label("Extra Modifier Chance (1/x)")] [Increment(1)] [Range(1, 100)] [DefaultValue(2)] [Slider]
        public int ChanceForExtraModifier;

        [Label("Amount of Possible Modifiers")] [Range(1, 20)] [DefaultValue(10)] [Slider]
        public int ModifierAmount;

        [Label("Force A Modifier")] [DefaultValue(false)]
        public bool ForceModifier;

        private int modifierToForce;
        [Label("Modifier to Force")] [DefaultValue(0)]
        public int ModifierToForce
        {
            get { return modifierToForce; }
            set
            {
                modifierToForce = value;
                if (modifierToForce < 0)
                    modifierToForce = 0;
                if (EnemyModifiers.Modifiers != null && EnemyModifiers.Modifiers.Count > 0 && modifierToForce > EnemyModifiers.Modifiers.Count - 1)
                    modifierToForce = EnemyModifiers.Modifiers.Count - 1;
            }
        }

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