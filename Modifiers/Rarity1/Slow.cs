namespace FargoEnemyModifiers.Modifiers
{
    public class Slow : Modifier
    {
        public override string Name => "Slow";
        public override string Description => "Speed is decreased by 50%";
        public override int Rarity => 1;

        public override float SpeedMultiplier => 0.5f;
    }
}