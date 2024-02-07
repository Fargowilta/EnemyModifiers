using FargoEnemyModifiers.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;

namespace FargoEnemyModifiers.Modifiers.Rarity3
{
    public class Shimmering : Modifier
    {
        public override ModifierID ModifierID => ModifierID.Shimmering;
        public override string Key => "Shimmering";
        public override RarityID Rarity => RarityID.Rare;
        public override AiOverrideStyle AiOverride => AiOverrideStyle.PreVanilla;
        public override int LootMultiplier => 2;
        public override float SpeedMultiplier => 1.25f;

        private int counter = 0;

        public override void AI(NPC npc)
        {
            if (--counter <= 0)
            {
                npc.AddBuff(BuffID.Shimmer, 90);
                counter = 900;
            }
        }
    }
}
