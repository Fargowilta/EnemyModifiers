namespace FargoEnemyModifiers.Modifiers
{
    public class Unrelenting : Modifier
    {
        public override string Name => "Unrelenting";
        public override string Description => "Gains knockback immunity";
        public override int Rarity => 1;

        public override float KnockBackMultiplier => 0f;
    }
}