using FargoEnemyModifiers.Utilities;
using Microsoft.Xna.Framework;

namespace FargoEnemyModifiers.Modifiers
{
    public class Green : Modifier
    {
        public override ModifierID ModifierID => ModifierID.Green;
        public override string Key => "Green";
        public override RarityID Rarity => RarityID.Common;
        public override bool ColorChanger => true;

        public override Color? GetAlpha()
        {
            return Color.Green;
        }
    }
}
