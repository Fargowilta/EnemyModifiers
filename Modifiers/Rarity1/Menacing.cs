namespace FargoEnemyModifiers.Modifiers
{
    public class Menacing : Modifier
    {
        public override string Name => "Menacing";
        public override string Description => "Damage is increased by 50%";
        public override int Rarity => 1;

        public override float DamageMultiplier => 1.5f;
    }
}