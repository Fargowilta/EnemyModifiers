using FargoEnemyModifiers.Utilities;
using Microsoft.Xna.Framework;

namespace FargoEnemyModifiers.Modifiers
{
    public class Yellow : Modifier
    {
        public override ModifierID ModifierID => ModifierID.Yellow;
        public override string Key => "Yellow";
        public override RarityID Rarity => RarityID.Common;
        public override bool ColorChanger => true;

        public override Color? GetAlpha()
        {
            return Color.Yellow;
        }
    }
}
