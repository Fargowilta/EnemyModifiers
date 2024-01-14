using FargoEnemyModifiers.Utilities;

namespace FargoEnemyModifiers.Modifiers
{
    public class Menacing : Modifier
    {
        public override ModifierID ModifierID => ModifierID.Menacing;
        public override string Key => "Menacing";
        public override RarityID Rarity => RarityID.Common;

        public override float DamageMultiplier => 1.5f;
    }
}