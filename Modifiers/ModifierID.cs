using System.Security.AccessControl;

namespace FargoEnemyModifiers.Modifiers
{
    public enum ModifierID
    {
        //Sorted by rarity
        //COMMON
        Armored = 0, //Defense is increased by 50%
        Blue = 1,
        Green = 2,
        Healthy = 3, // HP buff
        Impenetrable = 4, // Piercing projectiles are destroyed on hit
        Light = 5, // Takes increased knockback
        Menacing = 6, //Damage is increased by 50%
        Orange = 7,
        Purple = 8,
        Rampaging = 9, // Deals extremely high knockback to you
        Red = 10,
        Regenerating = 11, // Regenerates 10% HP every 5 seconds
        Slow = 12, // Speed is decreased by 50%
        Sluggish = 13, //Performs all actions half as fast
        Splitting = 14, // Spawns 2-3 smaller versions of itself with reduced stats on death
        Swift = 15, // Speed is increased by 50%
        Unrelenting = 16, //Gains knockback immunity
        Wealthy = 17, // Drops 5x more coins
        Yellow = 18,
        Shiny = 53,
        Stinky = 57,
        Lovestruck = 58,
        Poor = 59,
        Pink = 60,
        Big = 63,
        Small = 64,
        Fishy = 74,

        //UNCOMMON
        Accelerating = 19, // Constantly gains speed up to 4x normal speed. If they hit you or you hit them with a melee attack, their speed resets
        Bloodthirsty = 20, //Gains 25% damage every time they hit you
        Bulletproof = 21, //Gains 80% resistance to ranged damage
        Flaming = 22, //spawns on fire, moves at 2x speed, leaves trail of fire - uncommon
        Fortifying = 23, // Defense decreased by 50%. Gains an aura that gives other enemies 50% damage resistance
        Hexproof = 24, // Damage over time does not work on them
        Huge = 25, //Size increased by 50%, HP increased by 25%, speed decreased by 15%
        Infested = 26, // Periodically spawns Baby Spiders
        Metallic = 27, //Increased DR, drops a random metal based on progression, also change hit sound - uncommon
        Miniature = 28,//Size decreased by 50%, HP reduced by 25%, speed increased by 20%
        Possessed = 29, // Replaces its normal AI with Ghost AI
        Rallying = 30, //Gains an aura that buffs other enemies: +25% damage and +25% speed
        Shielded = 31, // Gains 80% resistance to melee damage
        Stealthy = 32, // Slowly turns nearly invsible, becomes fully visible when hit
        Vaccinated = 33, //vaccinated - immune to all debuffs - uncommon
        Vampiric = 34, //life steal on hit - uncommon
        Veiled = 35, //Gains 80% resistance to magic and summon damage
        Reflective = 51,
        Volatile = 55,
        Frenzied = 61,
        Scary = 65,
        Sleepy = 67,
        Polite = 73,
        Smoky = 75,

        //RARE
        Chained = 36, // They spawn chained to you and cannot leave a radius around you
        Devouring = 37, //Devours any enemy it comes across with lesser or equal HP. When it does so, it heals itself and gains max HP and damage
        Hyper = 38, //Performs all actions twice as fast
        Imaginary = 39, //slightly transparent, when attacked or contacts you it vanishes … - RARE
        Juggernaut = 40, // HP increased by 150% and damage, defense, size increased by 25%. But has Sluggish built in (performs actions half as fast)
        Merchant = 41, // Replaces its usual AI with Town NPC AI. It will give you one random free item from the Travelling Merchant's current shop then vanish
        Rainbow = 42, // It's rainbow and Drops 5x more drops not including coins
        Showman = 43, //explodes into smoke and birds on death
        Solidified = 44, // tile collide - only on things that dont already - rare
        Undying = 45,//hangs on at 1 HP for awhile - rare
        //Warping, // When they reach half HP, you switch places with them
        Worm = 46, // Spawns with several body segments that lag behind it. Gains knockback immunity and 2x HP
        Swarming = 47, // Upon spawning, creates 8 copies of itself (without modifiers). Has 4x the HP and 2x the size
        Aquarian = 50,
        Fluctuating = 52,
        Skeletal = 54,
        Disguised = 56,
        Shimmering = 62,
        Celebratory = 66,
        Sticky = 68,
        Medic = 69,
        Sorcerous = 70, //Occasionally shoots Chaos Balls towards the player (ala Goblin Sorcerers or Tim) whose damage scales with the enemy’s stats (only post goblins)
        Creepy = 71,
        Shy = 72,
        Dramatic = 76,
        Buzzing = 77,

        //MYTHIC
        //Lethal,
        //Nested,// starts big, kill several times keeps getting smaller - mythic
        Royal = 48, // 200% HP, 200% size, 60% move speed, spawns mini versions of itself every so oftrn when hurt
        Modifying = 49,
        //Monumental = 78,


        // Helper modifiers
        WormBody = 201




       
    }

    // Apparently you can't filter values out of an enum, so this is used for config
    public enum ConfigModifierID : int
    {
        //Sorted by rarity
        //COMMON
        Armored = 0, //Defense is increased by 50%
        Blue = 1,
        Green = 2,
        Healthy = 3, // HP buff
        Impenetrable = 4, // Piercing projectiles are destroyed on hit
        Light = 5, // Takes increased knockback
        Menacing = 6, //Damage is increased by 50%
        Orange = 7,
        Purple = 8,
        Rampaging = 9, // Deals extremely high knockback to you
        Red = 10,
        Regenerating = 11, // Regenerates 10% HP every 5 seconds
        Slow = 12, // Speed is decreased by 50%
        Sluggish = 13, //Performs all actions half as fast
        Splitting = 14, // Spawns 2-3 smaller versions of itself with reduced stats on death
        Swift = 15, // Speed is increased by 50%
        Unrelenting = 16, //Gains knockback immunity
        Wealthy = 17, // Drops 5x more coins
        Yellow = 18,
        Shiny = 53,
        Stinky = 57,
        Lovestruck = 58,
        Poor = 59,
        Pink = 60,
        Big = 63,
        Small = 64,
        Fishy = 74,

        //UNCOMMON
        Accelerating = 19, // Constantly gains speed up to 4x normal speed. If they hit you or you hit them with a melee attack, their speed resets
        Bloodthirsty = 20, //Gains 25% damage every time they hit you
        Bulletproof = 21, //Gains 80% resistance to ranged damage
        Flaming = 22, //spawns on fire, moves at 2x speed, leaves trail of fire - uncommon
        Fortifying = 23, // Defense decreased by 50%. Gains an aura that gives other enemies 50% damage resistance
        Hexproof = 24, // Damage over time does not work on them
        Huge = 25, //Size increased by 50%, HP increased by 25%, speed decreased by 15%
        Infested = 26, // Periodically spawns Baby Spiders
        Metallic = 27, //Increased DR, drops a random metal based on progression, also change hit sound - uncommon
        Miniature = 28,//Size decreased by 50%, HP reduced by 25%, speed increased by 20%
        Possessed = 29, // Replaces its normal AI with Ghost AI
        Rallying = 30, //Gains an aura that buffs other enemies: +25% damage and +25% speed
        Shielded = 31, // Gains 80% resistance to melee damage
        Stealthy = 32, // Slowly turns nearly invsible, becomes fully visible when hit
        Vaccinated = 33, //vaccinated - immune to all debuffs - uncommon
        Vampiric = 34, //life steal on hit - uncommon
        Veiled = 35, //Gains 80% resistance to magic and summon damage
        Reflective = 51,
        Volatile = 55,
        Frenzied = 61,
        Scary = 65,
        Sleepy = 67,
        Polite = 73,
        Smoky = 75,

        //RARE
        Chained = 36, // They spawn chained to you and cannot leave a radius around you
        Devouring = 37, //Devours any enemy it comes across with lesser or equal HP. When it does so, it heals itself and gains max HP and damage
        Hyper = 38, //Performs all actions twice as fast
        Imaginary = 39, //slightly transparent, when attacked or contacts you it vanishes … - RARE
        Juggernaut = 40, // HP increased by 150% and damage, defense, size increased by 25%. But has Sluggish built in (performs actions half as fast)
        Merchant = 41, // Replaces its usual AI with Town NPC AI. It will give you one random free item from the Travelling Merchant's current shop then vanish
        Rainbow = 42, // It's rainbow and Drops 5x more drops not including coins
        Showman = 43, //explodes into smoke and birds on death
        Solidified = 44, // tile collide - only on things that dont already - rare
        Undying = 45,//hangs on at 1 HP for awhile - rare
        //Warping, // When they reach half HP, you switch places with them
        Worm = 46, // Spawns with several body segments that lag behind it. Gains knockback immunity and 2x HP
        Swarming = 47, // Spawns with several copies of itself. 4x the HP and shared health pool
        Aquarian = 50,
        Fluctuating = 52,
        Skeletal = 54,
        Disguised = 56,
        Shimmering = 62,
        Celebratory = 66,
        Sticky = 68,
        Medic = 69,
        Sorcerous = 70,
        Creepy = 71,
        Shy = 72,
        Dramatic = 76,
        Buzzing = 77,


        //MYTHIC
        //Lethal,
        //Nested,// starts big, kill several times keeps getting smaller - mythic
        Royal = 48, // 200% HP, 200% size, 60% move speed, spawns mini versions of itself every so oftrn when hurt
        Modifying = 49,
        //Monumental = 78,
        
    }
}

//COMMON
//Extroverted: Gains increased damage/speed the more enemies are alive
//Introverted: Gains increased damage/speed the less enemies are alive

//UNCOMMON
//Valuable, counts for 10x kills for banners

//RARE
//Popular: Spawns with around 10 to 20 of the same entity (not on bosses)

//engineer: spawns the martian turrets every few seconds

//MYTHIC
//Monumental: huge size, huge HP, never moves (can fire projectiles), spawns whatever the original npc was around it, undamageable until you kill 25 of the same enemy. drops 20x loot. 









