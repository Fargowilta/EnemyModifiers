using FargoEnemyModifiers.Utilities;
using Microsoft.Xna.Framework;
using Terraria;

namespace FargoEnemyModifiers.Modifiers
{
    public class Wealthy : Modifier
    {
        public override string Key => "Wealthy";
        public override RarityID Rarity => RarityID.Common;

        public override Color? GetAlpha()
        {
            return Color.Gold;
        }

        public override bool PreNPCLoot(NPC npc)
        {
            npc.value *= 5;
            return true;
        }
    }
}