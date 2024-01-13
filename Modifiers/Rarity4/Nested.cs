//using Microsoft.Xna.Framework;
//using System;
//using FargoEnemyModifiers.Utilities;
//using Terraria;
//using Terraria.ID;
//using Terraria.ModLoader;

//namespace FargoEnemyModifiers.Modifiers
//{
//    public class Nested : Modifier
//    {
//        public override string Key => "Nested";
//        public override RarityID Rarity => RarityID.Mythic;
//
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