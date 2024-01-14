using FargoEnemyModifiers.Utilities;

namespace FargoEnemyModifiers.Modifiers
{
    public class Healthy : Modifier
    {
        public override ModifierID ModifierID => ModifierID.Healthy;
        public override string Key => "Healthy";
        public override RarityID Rarity => RarityID.Common;

        public override float HealthMultiplier => 1.5f;
    }
}