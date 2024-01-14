using FargoEnemyModifiers.Utilities;
using Microsoft.Xna.Framework;

namespace FargoEnemyModifiers.Modifiers
{
    public class Red : Modifier
    {
        public override ModifierID ModifierID => ModifierID.Red;
        public override string Key => "Red";
        public override RarityID Rarity => RarityID.Common;
        public override bool ColorChanger => true;

        public override Color? GetAlpha()
        {
            return Color.Red;
        }
    }
}
