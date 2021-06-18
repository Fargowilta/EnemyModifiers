using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace FargoEnemyModifiers.Modifiers
{
    public class Regenerating : Modifier
    {
        public Regenerating()
        {
            name = "Regenerating";
        }

        private int counter = 0;
        public override void AI(NPC npc)
        {
            if (npc.life < npc.lifeMax && npc.realLife == -1)
            {
                counter++;

                if (counter >= 300)
                {
                    int amtHealed = npc.lifeMax / 10;
                    if (npc.boss)
                        amtHealed /= 2;
                    npc.life += amtHealed;
                    npc.HealEffect(amtHealed);
                    counter = 0;
                }
            }
        }
    }
}
