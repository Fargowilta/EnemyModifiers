using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace FargoEnemyModifiers.Modifiers
{
    public class Stealthy : Modifier
    {
        public Stealthy()
        {
            name = "Stealthy";
        }

        public override void AI(NPC npc)
        {
            if (npc.alpha < 200)
            {
                npc.alpha++;
            }
        }

        public override void OnHitByItem(NPC npc, Player player)
        {
            npc.alpha = 0;
        }

        public override void OnHitByProjectile(NPC npc, Projectile projectile)
        {
            npc.alpha = 0;
        }
    }
}
