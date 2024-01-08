namespace FargoEnemyModifiers.Modifiers
{
    public class Healthy : Modifier
    {
        public override string Name => "Healthy";
        public override string Description => "HP is increased by 50%";
        public override int Rarity => 1;

        public override float HealthMultiplier => 1.5f;
    }
}