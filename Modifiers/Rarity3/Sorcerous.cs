using FargoEnemyModifiers.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;

namespace FargoEnemyModifiers.Modifiers.Rarity3
{
    public class Sorcerous : Modifier
    {
        public override ModifierID ModifierID => ModifierID.Sorcerous;
        public override string Key => "Sorcerous";
        public override RarityID Rarity => RarityID.Rare;

        public override bool ExtraCondition(NPC npc)
        {
            return NPC.downedGoblins;
        }

        public override AiOverrideStyle AiOverride => AiOverrideStyle.PostVanilla;

        private int counter = 0;

        public override void AI(NPC npc)
        {
            if (counter++ >= 300)
            {
                NPC.NewNPC(npc.GetSource_FromAI(), (int)npc.position.X + npc.width / 2, (int)npc.position.Y - 8, NPCID.ChaosBall);

                counter = 0;
            }
        }
    }
}
