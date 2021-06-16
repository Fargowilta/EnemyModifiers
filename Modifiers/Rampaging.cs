using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace FargoEnemyModifiers.Modifiers
{
    public class Rampaging : Modifier
    {
        public Rampaging()
        {
            name = "Rampaging";
        }

        public override void OnHitPlayer(NPC npc, Player target)
        {
            Vector2 velocity = Vector2.Normalize(target.Center - npc.Center) * 20;
            target.velocity = velocity;
        }
    }
}
