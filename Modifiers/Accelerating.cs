using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace FargoEnemyModifiers.Modifiers
{
    public class Accelerating : Modifier
    {
        public Accelerating()
        {
            name = "Accelerating";
        }

        private int counter = 0;
        public override bool PreAI(NPC npc)
        {
            if (++counter > 30 && speedMultiplier < 4f)
            {
                speedMultiplier *= 1.05f;
                counter = 0;
            }

            return true;
        }

        public override void OnHitByItem(NPC npc, Player player)
        {
            speedMultiplier = 0.5f;
        }

        public override void OnHitPlayer(NPC npc, Player target)
        {
            speedMultiplier = 0.5f;
        }
    }
}
