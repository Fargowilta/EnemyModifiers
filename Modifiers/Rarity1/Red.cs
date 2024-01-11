using Microsoft.Xna.Framework;

namespace FargoEnemyModifiers.Modifiers
{
    public class Red : Modifier
    {
        public override string Name => "Red";
        public override string Description => "Red";
        public override int Rarity => 1;
        public override bool ColorChanger => true;

        public override Color? GetAlpha()
        {
            return Color.Red;
        }
    }
}
