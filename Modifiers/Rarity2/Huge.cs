using FargoEnemyModifiers.Utilities;
namespace FargoEnemyModifiers.Modifiers
{
    public class Huge : Modifier
    {
        public override ModifierID ModifierID => ModifierID.Huge;
        public override string Key => "Huge";
        public override RarityID Rarity => RarityID.Uncommon;

        public override float HealthMultiplier => 1.5f;
        public override float DamageMultiplier => 1.4f;
        public override float SizeMultiplier => 1.4f;
        public override float SpeedMultiplier => 0.75f;
    }
}