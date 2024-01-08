using Microsoft.Xna.Framework;
using Terraria;

namespace FargoEnemyModifiers.Modifiers
{
    public class Rainbow : Modifier
    {
        public override string Name => "Rainbow";
        public override string Description => "It's rainbow and drops 5x more drops";
        public override int Rarity => 3;

        public override int LootMultiplier => 4;

        public override Color? GetAlpha()
        {
            return Main.DiscoColor;
        }
    }
}