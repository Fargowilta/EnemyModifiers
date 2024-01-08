namespace FargoEnemyModifiers.Modifiers
{
    public class Swift : Modifier
    {
        public override string Name => "Swift";
        public override string Description => "Speed is increased by 50%";
        public override int Rarity => 1;

        public override float SpeedMultiplier => 1.5f;
    }
}