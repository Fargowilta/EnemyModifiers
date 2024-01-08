namespace FargoEnemyModifiers.Modifiers
{
    public class Huge : Modifier
    {
        public override string Name => "Huge";
        public override string Description => "Size increased by 50%, HP increased by 25%, speed decreased by 15%";
        public override int Rarity => 2;

        public override float HealthMultiplier => 1.25f;

        public override float SizeMultiplier => 1.5f;

        public override float SpeedMultiplier => 0.85f;
    }
}