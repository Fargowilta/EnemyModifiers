using FargoEnemyModifiers.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Microsoft.Xna.Framework;

namespace FargoEnemyModifiers.Modifiers.Rarity3
{
    public class Medic : Modifier
    {
        public override ModifierID ModifierID => ModifierID.Medic;
        public override string Key => "Medic";
        public override RarityID Rarity => RarityID.Rare;

        public override AiOverrideStyle AiOverride => AiOverrideStyle.PreVanilla;

        private int counter = 0;

        public override void AI(NPC npc)
        {
            const int range = 300;

            CreateAura(npc, range, DustID.GreenFairy, Color.LightGreen);

            if (++counter >= 300)
            {
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC healedNPC = Main.npc[i];

                    if (!healedNPC.active || healedNPC.whoAmI == npc.whoAmI || npc.townNPC || npc.friendly || NPCID.Sets.CountsAsCritter[npc.type] ||
                        !(Vector2.Distance(npc.Center, healedNPC.Center) < range))
                        continue;

                    Regenerating.RegeneratingHeal(healedNPC);
                }

                counter = 0;
            }

            
        }
    }
}
