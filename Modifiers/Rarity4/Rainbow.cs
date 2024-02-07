using FargoEnemyModifiers.Utilities;
using Microsoft.Xna.Framework;
using Terraria;

namespace FargoEnemyModifiers.Modifiers.Rarity4
{
    public class Rainbow : Modifier
    {
        public override ModifierID ModifierID => ModifierID.Rainbow;
        public override string Key => "Rainbow";
        public override RarityID Rarity => RarityID.Mythic;
        public override float HealthMultiplier => 2f;
        public override int LootMultiplier => 9;

        public override Color? GetAlpha()
        {
            return Main.DiscoColor;
        }
    }
}