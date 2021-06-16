using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace FargoEnemyModifiers.Modifiers
{
    public class Juggernaut : Modifier
    {
        public Juggernaut()
        {
            name = "Juggernaut";

            healthMultiplier = 2.5f;
            defenseMultiplier = 1.25f;
            damageMultiplier = 1.25f;
            sizeMultiplier = 1.25f;
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
