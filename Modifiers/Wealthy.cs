using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace FargoEnemyModifiers.Modifiers
{
    public class Wealthy : Modifier
    {
        public Wealthy()
        {
            name = "Wealthy";
        }

        public override bool PreNPCLoot(NPC npc)
        {
            npc.value *= 5;
            return true;
        }
    }
}
