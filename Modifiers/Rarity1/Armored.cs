using FargoEnemyModifiers.Utilities;

namespace FargoEnemyModifiers.Modifiers
{
    public class Armored : Modifier
    {
        public override ModifierID ModifierID => ModifierID.Armored;
        public override string Key => "Armored";
        public override RarityID Rarity => RarityID.Common;

        public override float DefenseMultiplier => 1.5f;
    }
}