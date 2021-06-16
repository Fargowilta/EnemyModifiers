using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace FargoEnemyModifiers.Modifiers
{
    public class Bloodthirsty : Modifier
    {
        public Bloodthirsty()
        {
            name = "Bloodthirsty";
        }

        public override void OnHitPlayer(NPC npc, Player target)
        {
            npc.damage = (int)(npc.damage * 1.25f);
        }
    }
}
