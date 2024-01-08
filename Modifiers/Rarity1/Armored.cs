namespace FargoEnemyModifiers.Modifiers
{
    public class Armored : Modifier
    {
        public override string Name => "Armored";
        public override string Description => "Defense is increased by 50%";
        public override int Rarity => 1;

        public override float DefenseMultiplier => 1.5f;
    }
}