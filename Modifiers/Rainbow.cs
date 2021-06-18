using Microsoft.Xna.Framework;
using Terraria;

namespace FargoEnemyModifiers.Modifiers
{
    public class Rainbow : Modifier
    {
        public Rainbow()
        {
            name = "Rainbow";
        }

        public override Color? GetAlpha()
        {
            return new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);
        }
    }
}
