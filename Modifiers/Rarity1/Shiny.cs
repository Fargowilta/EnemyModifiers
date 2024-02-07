using FargoEnemyModifiers.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;

namespace FargoEnemyModifiers.Modifiers.Rarity1
{
    public class Shiny : Modifier
    {
        public override ModifierID ModifierID => ModifierID.Shiny;
        public override string Key => "Shiny";
        public override RarityID Rarity => RarityID.Common;

        public override AiOverrideStyle AiOverride => AiOverrideStyle.PostVanilla;

        public override void AI(NPC npc)
        {
            Lighting.AddLight((int)(npc.position.X + (float)(npc.width / 2)) / 16, (int)(npc.position.Y + 4f) / 16, 0.9f, 0.75f, 0.5f);
        }

        public override void OnKill(NPC npc)
        {
            int amountDropped = Main.rand.Next(5, 15);

            Item.NewItem(npc.GetSource_DropAsItem(), npc.Center, ItemID.Torch, amountDropped);
        }
    }
}
