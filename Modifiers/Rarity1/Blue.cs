using FargoEnemyModifiers.Utilities;
using Microsoft.Xna.Framework;

namespace FargoEnemyModifiers.Modifiers
{
    public class Blue : Modifier
    {
        public override ModifierID ModifierID => ModifierID.Blue;
        public override string Key => "Blue";
        public override RarityID Rarity => RarityID.Common;
        public override bool ColorChanger => true;

        public override Color? GetAlpha()
        {
            return Color.Blue;
        }
    }
}
