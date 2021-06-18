namespace FargoEnemyModifiers.Modifiers
{
    public class Miniature : Modifier
    {
        public override string Name => "Miniature";

        public override float HealthMultiplier => 0.75f;

        public override float SizeMultiplier => 0.5f;

        public override float SpeedMultiplier => 1.2f;
    }
}