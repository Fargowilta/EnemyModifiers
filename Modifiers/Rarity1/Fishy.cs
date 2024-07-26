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
    public class Fishy : Modifier
    {
        public override ModifierID ModifierID => ModifierID.Fishy;
        public override string Key => "Fishy";
        public override RarityID Rarity => RarityID.Common;

        public override AiOverrideStyle AiOverride => AiOverrideStyle.PreVanilla;

        public override bool ExtraCondition(NPC npc)
        {
            return NPC.AnyNPCs(NPCID.Angler);
        }

        public override void AI(NPC npc)
        {
            npc.AddBuff(BuffID.Wet, 2);
        }

        public override void OnKill(NPC npc)
        {
            int itemDropped = Main.anglerQuestItemNetIDs[Main.anglerQuest];
            Item.NewItem(npc.GetSource_DropAsItem(), npc.Center, itemDropped, 1);
        }
    }
}
