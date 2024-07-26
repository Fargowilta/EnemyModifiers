//using FargoEnemyModifiers.Utilities;
//using Microsoft.Xna.Framework;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Terraria;

//namespace FargoEnemyModifiers.Modifiers.Rarity4
//{
//    public class Monumental : Modifier
//    {
//        public override ModifierID ModifierID => ModifierID.Monumental;
//        public override string Key => "Monumental";
//        public override RarityID Rarity => RarityID.Mythic;

//        public override float HealthMultiplier => 5f;
//        public override float SizeMultiplier => 3f;
//        public override float SpeedMultiplier => 0f;
//        public override float KnockBackMultiplier => 0;

//        public override int LootMultiplier => 20;

//        public override bool ExtraCondition(NPC npc)
//        {
//            return Main.hardMode;
//        }

//        public override AiOverrideStyle AiOverride => AiOverrideStyle.PreVanilla;

//        public override void AI(NPC npc)
//        {
//            //pillar hovering
//            npc.velocity = Vector2.UnitY * npc.velocity.Length();
//            if (npc.velocity.Y < 0.25f)
//            {
//                npc.velocity.Y += 0.02f;
//            }
//            if (npc.velocity.Y > 0.25f)
//            {
//                npc.velocity.Y -= 0.02f;
//            }

//            npc.dontTakeDamage = true;


//        }


//    }
//}
