namespace FargoEnemyModifiers.Modifiers
{
    public enum ModifierID
    {
        //Sorted by rarity
        //COMMON
        Armored, //Defense is increased by 50%
        Blue,
        Green,
        Healthy, // HP buff
        Impenetrable, // Piercing projectiles are destroyed on hit
        Light, // Takes increased knockback
        Menacing, //Damage is increased by 50%
        Orange,
        Purple,
        Rampaging, // Deals extremely high knockback to you
        Red,
        Regenerating, // Regenerates 10% HP every 5 seconds
        Slow, // Speed is decreased by 50%
        Sluggish, //Performs all actions half as fast
        Splitting, // Spawns 2-3 smaller versions of itself with reduced stats on death
        Swift, // Speed is increased by 50%
        Unrelenting, //Gains knockback immunity
        Wealthy, // Drops 5x more coins
        Yellow,

        //UNCOMMON
        Accelerating, // Constantly gains speed up to 4x normal speed. If they hit you or you hit them with a melee attack, their speed resets
        Bloodthirsty, //Gains 25% damage every time they hit you
        Bulletproof, //Gains 80% resistance to ranged damage
        Flaming, //spawns on fire, moves at 2x speed, leaves trail of fire - uncommon
        Fortifying, // Defense decreased by 50%. Gains an aura that gives other enemies 50% damage resistance
        Hexproof, // Damage over time does not work on them
        Huge, //Size increased by 50%, HP increased by 25%, speed decreased by 15%
        Infested, // Periodically spawns Baby Spiders
        Metallic, //Increased DR, drops a random metal based on progression, also change hit sound - uncommon
        Miniature,//Size decreased by 50%, HP reduced by 25%, speed increased by 20%
        Possessed, // Replaces its normal AI with Ghost AI
        Rallying, //Gains an aura that buffs other enemies: +25% damage and +25% speed
        Shielded, // Gains 80% resistance to melee damage
        Stealthy, // Slowly turns nearly invsible, becomes fully visible when hit
        Vaccinated, //vaccinated - immune to all debuffs - uncommon
        Vampiric, //life steal on hit - uncommon
        Veiled, //Gains 80% resistance to magic and summon damage

        //RARE
        Chained, // They spawn chained to you and cannot leave a radius around you
        Devouring, //Devours any enemy it comes across with lesser or equal HP. When it does so, it heals itself and gains max HP and damage
        Hyper, //Performs all actions twice as fast
        Imaginary, //slightly transparent, when attacked or contacts you it vanishes … - RARE
        Juggernaut, // HP increased by 150% and damage, defense, size increased by 25%. But has Sluggish built in (performs actions half as fast)
        Merchant, // Replaces its usual AI with Town NPC AI. It will give you one random free item from the Travelling Merchant's current shop then vanish
        Rainbow, // It's rainbow and Drops 5x more drops not including coins
        Showman, //explodes into smoke and birds on death
        Solidifed, // tile collide - only on things that dont already - rare
        Undying,//hangs on at 1 HP for awhile - rare
        //Warping, // When they reach half HP, you switch places with them
        Worm, // Spawns with several body segments that lag behind it. Gains knockback immunity and 2x HP

        //MYTHIC
        //Lethal,
        //Nested,// starts big, kill several times keeps getting smaller - mythic
        Royal, // 200% HP, 200% size, 60% move speed, spawns mini versions of itself every so oftrn when hurt 



        ////shiny - they glow in the dark and drop torches
        //evasive - dodge attacks
        //










        //Delerious, // Changes its AI periodically and sprays projectiles whenever it does
        //Duke, //Replaces its normal AI with Duke Fishron AI





        //Medic,// Heals all enemies around it like fortifying. (Same as Regenerating, just for enemies around it instead of itself.)

        //Pyro,// immune to lava, heals from fire debuffs - 


        //Sticky, //: leaves cobwebs in its wake

        // Unstable, //teleports randomly ie chaos elemental - mythic rare

        //Shapeshifting, //shapeshifting - randomly becomes a different npc type with the same stats and this modifier





        ////public const int Draining = 62; //draining - life steal aura (derpling) OR drains other enemies

        //Flammable, //Flammable - takes increased dot damage
        //Puppet, //Puppet - hit box is somewhere else controlled by a thing
        //Hydrophobic, //Hydrophobic - takes damage when in water or wet




        //Contagious, //contagious-any debuffs it has will also be applied to you if it hits you. gains a random debuff every 5 seconds

        ////public const int Reflective = 59; //reflective - reflects proj periodically (make clear)

        ////public const int Explosive = 63; //explosive - starts ticking when near, kill it fast or it explodes (no tile damage)

        //public const int Multiplying = 66; //cloning - emode illum bats


        //allergic - increased damage from bees
        //unpredictable - inflict any debuff on hit
        //aggressive - can dash towards you

        //sad - a rain cloud is always above them
        //protected - gains iframes between attacks
        //shroomified - spawn spores on death
        //greedy - collects nearby items, steals money on hit


        /*
         * 

        Transmutating- any other npc it touches becomes a copy of whatever this one is
        
        Dyed: has a random rare dye effect
        Talkative: Says random dialogue lines on hit. (Copied from town NPCs?)
        Greedy: Drops player’s money on hit
        Simulacrum: Has Brain of Cthulhu illusion clones
        
        Glacial: Has a aura around it that massively slows all projectiles in it.
        Driver: Has the Blazing Wheel AI
        
        Decaying: Gains an aura that decays all entities inside it, dealing damage over time. The closer you are to the enemy, the more damage you take
        Vegetative: fully stationary but has the plantera mouth hooks around itself

        Magnetizing (debatable name): slightly sucks everything in
        Repelling: Periodically repels all projectiles away from it
        Draining: sucks stats from you 
        Poor: drops no money, has less HP, picks up any items it comes across 
        
        Reformed: heals you instead of damaging. All it’s attacks also damage any enemies 
        
        Harmless: replaces Ai with critter AI*/
    }
}

