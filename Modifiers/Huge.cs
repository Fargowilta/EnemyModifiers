namespace FargoEnemyModifiers.Modifiers
{
    public class Huge : Modifier
    {
        public override string Name => "Huge";

        public override float HealthMultiplier => 1.25f;

        public override float SizeMultiplier => 1.5f;

        public override float SpeedMultiplier => 0.85f;
    }
}