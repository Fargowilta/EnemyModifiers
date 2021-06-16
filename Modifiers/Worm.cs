using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace FargoEnemyModifiers.Modifiers
{
    public class Worm : Modifier
    {
        public Worm()
        {
            name = "Worm";
            healthMultiplier = 2f;
            kbMultiplier = 0;
        }

        private bool BodySpawned = false;

        public override bool PreAI(NPC npc)
        {
            if (!BodySpawned)
            {
				int prevIndex = npc.whoAmI;

                for (int i = 0; i < 7; i++)
                {
                    int index = NPC.NewNPC((int)(npc.position.X + (float)(npc.width / 2)), (int)(npc.position.Y + (float)npc.height), npc.type, prevIndex, 0f, 0f, 0f, 0f, 255);

                    NPC newNPC = Main.npc[index];
					newNPC.GetGlobalNPC<EnemyModifiersGlobalNPC>().firstTick = false;
					newNPC.GetGlobalNPC<EnemyModifiersGlobalNPC>().modifier = new WormBody();
					newNPC.realLife = npc.whoAmI;

					if (i != 0)
					{
						Main.npc[prevIndex].localAI[0] = index;
					}

                    newNPC.localAI[1] = prevIndex;
                    npc.netUpdate = true;

					prevIndex = index;
				}

                BodySpawned = true;
            }           

            return true;
        }
	}
}
