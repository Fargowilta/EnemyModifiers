using Microsoft.Xna.Framework;

namespace FargoEnemyModifiers.Modifiers
{
    public class Green : Modifier
    {
        public override string Name => "Green";
        public override string Description => "Green";
        public override int Rarity => 1;
        public override bool ColorChanger => true;

        public override Color? GetAlpha()
        {
            return Color.Green;
        }
    }
}
