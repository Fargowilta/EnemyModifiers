using FargoEnemyModifiers.Utilities;
using Microsoft.Xna.Framework;

namespace FargoEnemyModifiers.Modifiers
{
    public class Purple : Modifier
    {
        public override string Key => "Purple";
        public override RarityID Rarity => RarityID.Common;
        public override bool ColorChanger => true;

        public override Color? GetAlpha()
        {
            return Color.Purple;
        }
    }
}
