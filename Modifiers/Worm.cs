using Terraria;

namespace FargoEnemyModifiers.Modifiers
{
    public class Worm : Modifier
    {
        public Worm()
        {
            name = "Worm";
            HealthMultiplier = 2f;
            KnockBackMultiplier = 0;
        }

        private bool BodySpawned;

        public override bool PreAI(NPC npc)
        {
            if (!BodySpawned)
            {
				int prevIndex = npc.whoAmI;

                for (int i = 0; i < 7; i++)
                {
                    int index = NPC.NewNPC((int)(npc.position.X + npc.width / 2), (int)(npc.position.Y + npc.height), npc.type, prevIndex);

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
