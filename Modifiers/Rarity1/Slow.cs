using FargoEnemyModifiers.Utilities;

namespace FargoEnemyModifiers.Modifiers
{
    public class Slow : Modifier
    {
        public override ModifierID ModifierID => ModifierID.Slow;
        public override string Key => "Slow";
        public override RarityID Rarity => RarityID.Common;

        public override float SpeedMultiplier => 0.5f;
    }
}