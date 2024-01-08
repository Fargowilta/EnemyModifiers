namespace FargoEnemyModifiers.Modifiers
{
    public class Miniature : Modifier
    {
        public override string Name => "Miniature";
        public override string Description => "Size decreased by 50%, HP reduced by 25%, speed increased by 20%";
        public override int Rarity => 2;

        public override float HealthMultiplier => 0.75f;

        public override float SizeMultiplier => 0.5f;

        public override float SpeedMultiplier => 1.2f;
    }
}