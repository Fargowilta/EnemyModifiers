using FargoEnemyModifiers.Utilities;

namespace FargoEnemyModifiers.Modifiers
{
    public class Swift : Modifier
    {
        public override string Key => "Swift";
        public override RarityID Rarity => RarityID.Common;

        public override float SpeedMultiplier => 1.5f;
    }
}