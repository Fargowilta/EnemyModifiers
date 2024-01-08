using Microsoft.Xna.Framework;
using Terraria;

namespace FargoEnemyModifiers.Modifiers
{
    public class Wealthy : Modifier
    {
        public override string Name => "Wealthy";
        public override string Description => "Drops 5x more coins";
        public override int Rarity => 1;

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