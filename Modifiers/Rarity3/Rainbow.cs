using FargoEnemyModifiers.Utilities;
using Microsoft.Xna.Framework;
using Terraria;

namespace FargoEnemyModifiers.Modifiers
{
    public class Rainbow : Modifier
    {
        public override ModifierID ModifierID => ModifierID.Rainbow;
        public override string Key => "Rainbow";
        public override RarityID Rarity => RarityID.Rare;

        public override int LootMultiplier => 4;

        public override Color? GetAlpha()
        {
            return Main.DiscoColor;
        }
    }
}