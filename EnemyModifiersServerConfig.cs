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


        //old
        public List<ModifierPicker> ModifierBlacklist;

        [BackgroundColor(0, 255, 255)]
        public class ModifierPicker
        {
            [DefaultValue(0)]
            public ConfigModifierID ModifierEnum;
        }


        ////new hellish
        //public List<ModifierToggle> ModifierToggles = getModifierToggles();

        //[BackgroundColor(0, 255, 255)]
        //public class ModifierToggle
        //{
        //    private int enumVal;
        //    private string Name, description;
        //    public bool Enabled;

        //    public ModifierToggle(int enumVal)
        //    {
        //        this.enumVal = enumVal;
        //        this.Enabled = true;
        //        this.Name = ((ConfigModifierID)enumVal).ToString();
        //        this.description = getDescription((ConfigModifierID)enumVal);
        //    }

        //    public int getEnumVal()
        //    {
        //        return this.enumVal;
        //    }

        //    public override string ToString()
        //    {
        //        return $"{this.Name + ": " +  this.description}";
        //    }
        //}

        //private static List<ModifierToggle> getModifierToggles()
        //{
        //    List<ModifierToggle> modifierBlacklist = new List<ModifierToggle>();

        //    //for (int i =0;i<80;i++)
        //    //{
        //    //    modifierBlacklist.Add(new ModifierToggle(i));
        //    //}

        //    //foreach (int modId in Enum.GetValues<ConfigModifierID>())
        //    //{
        //    //   // Main.NewText(modId);
        //    //    modifierBlacklist.Add(new ModifierToggle(modId));
        //    //}

        //    modifierBlacklist.Add(new ModifierToggle(0));
        //    modifierBlacklist.Add(new ModifierToggle(1));
        //    modifierBlacklist.Add(new ModifierToggle(2));
        //    modifierBlacklist.Add(new ModifierToggle(3));

        //    return modifierBlacklist;
        //}

        //public static string getDescription(ConfigModifierID enumValue)
        //{
        //    string description = "";

        //    switch (enumValue)
        //    {
        //        case ConfigModifierID.Armored:
        //            description = "Defense is increased by 50%";
        //            break;

        //        case ConfigModifierID.Blue:
        //            description = "Colored blue";
        //            break;

        //        case ConfigModifierID.Green:
        //            description = "Colored green";
        //            break;

        //        case ConfigModifierID.Healthy:
        //            description = "HP is increased by 50%";
        //            break;

        //        case ConfigModifierID.Impenetrable:
        //            description = "Piercing projectiles are destroyed on hit";
        //            break;

        //        case ConfigModifierID.Light:
        //            description = "Takes increased knockback and has reduced gravity";
        //            break;

        //        case ConfigModifierID.Menacing:
        //            description = "Damage is increased by 50%";
        //            break;

        //        case ConfigModifierID.Orange:
        //            description = "Colored orange";
        //            break;

        //        case ConfigModifierID.Purple:
        //            description = "Colored purple";
        //            break;

        //        case ConfigModifierID.Rampaging:
        //            description = "Deals extremely high knockback to you";
        //            break;

        //        case ConfigModifierID.Red:
        //            description = "Colored red";
        //            break;

        //        case ConfigModifierID.Regenerating:
        //            description = "Regenerates 2% HP every second";
        //            break;

        //        case ConfigModifierID.Slow:
        //            description = "Speed is decreased by 50%";
        //            break;

        //        case ConfigModifierID.Sluggish:
        //            description = "Performs all actions half as fast";
        //            break;

        //        case ConfigModifierID.Splitting:
        //            description = "Spawns 2-3 smaller versions on death";
        //            break;

        //        case ConfigModifierID.Swift:
        //            description = "Speed is increased by 50%";
        //            break;

        //        case ConfigModifierID.Unrelenting:
        //            description = "Gains knockback immunity";
        //            break;

        //        case ConfigModifierID.Wealthy:
        //            description = "Drops 5x more coins";
        //            break;

        //        case ConfigModifierID.Yellow:
        //            description = "Colored yellow";
        //            break;

        //        case ConfigModifierID.Shiny:
        //            description = "Glows in the dark and drops torches";
        //            break;

        //        case ConfigModifierID.Stinky:
        //            description = "It's stinky and applies stinky on hit. Deals 25% more damage";
        //            break;

        //        case ConfigModifierID.Lovestruck:
        //            description = "Deals 50% less damage, spawns bursts of hearts, and drops 2x loot";
        //            break;

        //        case ConfigModifierID.Poor:
        //            description = "Drops no loot, moves 25% slower and has 25% less HP";
        //            break;

        //        case ConfigModifierID.Pink:
        //            description = "Colored pink";
        //            break;

        //        case ConfigModifierID.Big:
        //            description = "25% increased size and HP, 20% increased damage, 10% reduced speed";
        //            break;

        //        case ConfigModifierID.Small:
        //            description = "25% reduced size, damage and HP. 20% increased speed";
        //            break;

        //        case ConfigModifierID.Fishy:
        //            description = "Drops the quest fish and is wet";
        //            break;

        //        case ConfigModifierID.Accelerating:
        //            description = "Gains speed up to 4x normal speed. Resets on hit";
        //            break;

        //        case ConfigModifierID.Bloodthirsty:
        //            description = "Gains 25% damage every time they hit you";
        //            break;

        //        case ConfigModifierID.Bulletproof:
        //            description = "Gains 80% resistance to ranged damage";
        //            break;

        //        case ConfigModifierID.Flaming:
        //            description = "Spawns On Fire, leaves a trail of flames, 50% increased speed";
        //            break;

        //        case ConfigModifierID.Fortifying:
        //            description = "Gains an aura that gives other enemies 50% DR";
        //            break;

        //        case ConfigModifierID.Hexproof:
        //            description = "Damage over time does not work on them";
        //            break;

        //        case ConfigModifierID.Huge:
        //            description = "HP, size, and damage increased. Speed decreased";
        //            break;

        //        case ConfigModifierID.Infested:
        //            description = "Periodically spawns Baby Spiders";
        //            break;

        //        case ConfigModifierID.Metallic:
        //            description = "Has 2x defense, takes less knockback, and drops ore";
        //            break;

        //        case ConfigModifierID.Miniature:
        //            description = "HP, size, and damage decreased. Speed increased";
        //            break;

        //        case ConfigModifierID.Possessed:
        //            description = "Replaces its normal AI with Ghost AI";
        //            break;

        //        case ConfigModifierID.Rallying:
        //            description = "Gains an aura that buffs other enemies damage and speed";
        //            break;

        //        case ConfigModifierID.Shielded:
        //            description = "Gains 80% resistance to melee damage";
        //            break;

        //        case ConfigModifierID.Stealthy:
        //            description = "Slowly turns nearly invisible, becomes fully visible when hit";
        //            break;

        //        case ConfigModifierID.Vaccinated:
        //            description = "Immune to all debuffs";
        //            break;

        //        case ConfigModifierID.Vampiric:
        //            description = "Heals some HP when they deal damage";
        //            break;

        //        case ConfigModifierID.Veiled:
        //            description = "Gains 80% resistance to magic and summon damage";
        //            break;

        //        case ConfigModifierID.Reflective:
        //            description = "Reflects projectiles occasionally";
        //            break;

        //        case ConfigModifierID.Volatile:
        //            description = "Explodes on death";
        //            break;

        //        case ConfigModifierID.Frenzied:
        //            description = "Gains increased damage and speed as HP drops";
        //            break;

        //        case ConfigModifierID.Scary:
        //            description = "Has an aura of confusion";
        //            break;

        //        case ConfigModifierID.Sleepy:
        //            description = "After not taking damage for 10 seconds, falls asleep";
        //            break;

        //        case ConfigModifierID.Polite:
        //            description = "Greets you when first seeing you and when dying";
        //            break;

        //        case ConfigModifierID.Smoky:
        //            description = "Creates smoke";
        //            break;

        //        case ConfigModifierID.Chained:
        //            description = "Chained to you and cannot leave a radius around you";
        //            break;

        //        case ConfigModifierID.Devouring:
        //            description = "Devours any enemy it comes across with lesser or equal HP";
        //            break;

        //        case ConfigModifierID.Hyper:
        //            description = "Performs all actions twice as fast";
        //            break;

        //        case ConfigModifierID.Imaginary:
        //            description = "Transparent. Ceases to exist when contact is made";
        //            break;

        //        case ConfigModifierID.Juggernaut:
        //            description = "Increased stats. No knockback. Sluggish. Drops 2x loot";
        //            break;

        //        case ConfigModifierID.Merchant:
        //            description = "Friendly. It will give you one random free item then vanish";
        //            break;

        //        case ConfigModifierID.Showman:
        //            description = "On death explodes into smoke and birds";
        //            break;

        //        case ConfigModifierID.Solidified:
        //            description = "Can no longer pass through tiles";
        //            break;

        //        case ConfigModifierID.Undying:
        //            description = "Hangs on with 5 HP for 5 seconds before dying";
        //            break;

        //        case ConfigModifierID.Worm:
        //            description = "Spawns with several body segments that lag behind it";
        //            break;

        //        case ConfigModifierID.Swarming:
        //            description = "Spawns with several copies of itself when it spawns";
        //            break;

        //        case ConfigModifierID.Aquarian:
        //            description = "Occassionally duke dashes";
        //            break;

        //        case ConfigModifierID.Fluctuating:
        //            description = "2x HP, Grows bigger and smaller over time";
        //            break;

        //        case ConfigModifierID.Skeletal:
        //            description = "Spawns with 2 skeletron hands";
        //            break;

        //        case ConfigModifierID.Disguised:
        //            description = "Changes its sprite to a blue slime";
        //            break;

        //        case ConfigModifierID.Shimmering:
        //            description = "Phases in and out of visibility, moves 25% faster, drops 2x loot";
        //            break;

        //        case ConfigModifierID.Celebratory:
        //            description = "Spawns confetti occasionally. Explodes into fireworks on death";
        //            break;

        //        case ConfigModifierID.Sticky:
        //            description = "Leaves a trail of cobwebs";
        //            break;

        //        case ConfigModifierID.Medic:
        //            description = "Heals all other enemies in a radius";
        //            break;

        //        case ConfigModifierID.Sorcerous:
        //            description = "Occasionally shoots Chaos Balls towards the player";
        //            break;

        //        case ConfigModifierID.Creepy:
        //            description = "Spawns with 10 creepers";
        //            break;

        //        case ConfigModifierID.Shy:
        //            description = "Stops moving and takes much less damage while looking at them";
        //            break;

        //        case ConfigModifierID.Dramatic:
        //            description = "Has it's own spawning and defeated messages";
        //            break;

        //        case ConfigModifierID.Buzzing:
        //            description = "Creates bees when taking damage";
        //            break;

        //        case ConfigModifierID.Royal:
        //            description = "Increased stats. Spawns mini versions of itself";
        //            break;

        //        case ConfigModifierID.Rainbow:
        //            description = "It's rainbow and drops 5x more drops";
        //            break;

        //        case ConfigModifierID.Modifying:
        //            description = "Adds a new modifier every 10 seconds up to the max allowed";
        //            break;


        //    }

        //    return description;
        //}

        //public override 
    }
}