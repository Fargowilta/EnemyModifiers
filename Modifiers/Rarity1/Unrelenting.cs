using FargoEnemyModifiers.Utilities;

namespace FargoEnemyModifiers.Modifiers
{
    public class Unrelenting : Modifier
    {
        public override string Key => "Unrelenting";
        public override RarityID Rarity => RarityID.Common;

        public override float KnockBackMultiplier => 0f;
    }
}