using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace FargoEnemyModifiers.Modifiers
{
    public class Sluggish : Modifier
    {
        public Sluggish()
        {
            name = "Sluggish";
        }

        private bool sluggishGo = false;
        public override bool PreAI(NPC npc)
        {
            if (sluggishGo)
            {
                sluggishGo = false;
                return true;
            }
            else
            {
                sluggishGo = true;
                return false;
            }
        }
    }
}
