using FargoEnemyModifiers.Utilities;
namespace FargoEnemyModifiers.Modifiers
{
    public class Miniature : Modifier
    {
        public override ModifierID ModifierID => ModifierID.Miniature;
        public override string Key => "Miniature";
        public override RarityID Rarity => RarityID.Uncommon;

        public override float HealthMultiplier => 0.5f;
        public override float DamageMultiplier => 0.75f;
        public override float SizeMultiplier => 0.5f;
        public override float SpeedMultiplier => 1.5f;
    }
}