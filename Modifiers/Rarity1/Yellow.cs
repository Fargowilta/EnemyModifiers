using Microsoft.Xna.Framework;

namespace FargoEnemyModifiers.Modifiers
{
    public class Yellow : Modifier
    {
        public override string Name => "Yellow";
        public override string Description => "Yellow";
        public override int Rarity => 1;
        public override bool ColorChanger => true;

        public override Color? GetAlpha()
        {
            return Color.Yellow;
        }
    }
}
