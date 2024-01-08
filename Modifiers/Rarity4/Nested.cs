//using Microsoft.Xna.Framework;
//using System;
//using Terraria;
//using Terraria.ID;
//using Terraria.ModLoader;

//namespace FargoEnemyModifiers.Modifiers
//{
//    public class Nested : Modifier
//    {
//        public override string Name => "Nested";
//        public override string Description => "200% HP, 200% size, 60% move speed, immune to knockback, spawns mini versions of itself every so often when hurt";
//        public override int Rarity => 4;

//        public override float HealthMultiplier => 2f;
//        public override float SizeMultiplier => 3f;
//        public override float SpeedMultiplier => 0.6f;
//        public override float KnockBackMultiplier => 0;



//        public override void Setup(NPC npc)
//        {
//            npc.GetGlobalNPC<EnemyModifiersGlobalNPC>().DropLoot = false;
//        }
//    }
//}