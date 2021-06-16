using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace FargoEnemyModifiers.Modifiers
{
    public class Hyper : Modifier
    {
        public Hyper()
        {
            name = "Hyper";
            speedMultiplier = 1.5f;
        }

        private bool hyperGo = true;
        public override bool PreAI(NPC npc)
        {
            if (hyperGo)
            {
                hyperGo = false;
                npc.AI();
            }
            return true;
        }

        public override void PostAI(NPC npc)
        {
            if (!hyperGo)
            {
                hyperGo = true;
            }
        }
    }
}
