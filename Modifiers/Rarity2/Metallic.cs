using Microsoft.Xna.Framework;
using System.Collections.Generic;
using FargoEnemyModifiers.Utilities;
using Terraria;
using Terraria.ID;

namespace FargoEnemyModifiers.Modifiers
{
    public class Metallic : Modifier
    {
        public override ModifierID ModifierID => ModifierID.Metallic;
        public override string Key => "Metallic";
        public override RarityID Rarity => RarityID.Uncommon;

        public override float KnockBackMultiplier => 0.8f;
        public override float DefenseMultiplier => 2f;

        public override void Setup(NPC npc)
        {
            npc.HitSound = SoundID.NPCHit4;
        }

        public override Color? GetAlpha()
        {
            return Color.Gray;
        }

        public override void OnKill(NPC npc)
        {
            List<int> possibleItems = new List<int>();
            possibleItems.Add(ItemID.CopperOre);
            possibleItems.Add(ItemID.TinOre);
            possibleItems.Add(ItemID.IronOre);
            possibleItems.Add(ItemID.LeadOre);
            possibleItems.Add(ItemID.SilverOre);
            possibleItems.Add(ItemID.GoldOre);
            possibleItems.Add(ItemID.TungstenOre);
            possibleItems.Add(ItemID.PlatinumOre);

            if (Main.hardMode)
            {
                possibleItems.Clear();
                possibleItems.Add(ItemID.CobaltOre);
                possibleItems.Add(ItemID.PalladiumOre);
                possibleItems.Add(ItemID.MythrilOre);
                possibleItems.Add(ItemID.OrichalcumOre);
                possibleItems.Add(ItemID.AdamantiteOre);
                possibleItems.Add(ItemID.TitaniumOre);
            }

            int itemDropped = Main.rand.NextFromCollection(possibleItems);

            Item.NewItem(npc.GetSource_DropAsItem(), npc.Center, itemDropped, 22);
        }
    }
}