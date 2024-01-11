using Microsoft.Xna.Framework;

namespace FargoEnemyModifiers.Modifiers
{
    public class Purple : Modifier
    {
        public override string Name => "Purple";
        public override string Description => "Purple";
        public override int Rarity => 1;
        public override bool ColorChanger => true;

        public override Color? GetAlpha()
        {
            return Color.Purple;
        }
    }
}
