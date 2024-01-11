using Microsoft.Xna.Framework;

namespace FargoEnemyModifiers.Modifiers
{
    public class Orange : Modifier
    {
        public override string Name => "Orange";
        public override string Description => "Orange";
        public override int Rarity => 1;
        public override bool ColorChanger => true;

        public override Color? GetAlpha()
        {
            return Color.Orange;
        }
    }
}
