using Microsoft.Xna.Framework;
using Terraria;

namespace FargoEnemyModifiers.Modifiers
{
    public class Rainbow : Modifier
    {
        public override string Name => "Rainbow";

        public override Color? GetAlpha()
        {
            return Main.DiscoColor;
        }
    }
}