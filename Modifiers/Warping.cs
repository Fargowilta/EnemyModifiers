using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace FargoEnemyModifiers.Modifiers
{
    public class Warping : Modifier
    {
        public Warping()
        {
            name = "Warping";
        }

        private bool Warped = false;

        public override void ModifyHitByItem(NPC npc, Player player, Item item, ref int damage, ref float knockback)
        {
            if (!Warped && (npc.life - damage) < npc.lifeMax / 2)
            {
                SwitchPlaces(npc, player);
                knockback = 0;
            }
        }

        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref int damage, ref float knockback)
        {
            if (!Warped && (npc.life - damage) < npc.lifeMax / 2)
            {
                SwitchPlaces(npc, Main.player[projectile.owner]);
                knockback = 0;
            }
        }

        private void SwitchPlaces(NPC npc, Player player)
        {
            Vector2 npcPos = npc.position;
            npcPos.Y -= 10;
            Vector2 playerPos = player.position;
            playerPos.Y -= 10;

            if (Vector2.Distance(npcPos, playerPos) > 1000 || !Collision.CanHitLine(npcPos, npc.width, npc.height, playerPos, player.width, player.height))
            {
                return;
            }

            if (player.whoAmI != Main.myPlayer || !player.active || player.dead || player.ghost)
            {
                return;
            }

            npc.Teleport(playerPos);
            player.Teleport(npcPos);

            player.immune = true;
            if (player.immuneTime < 30)
                player.immuneTime = 30;
            if (player.hurtCooldown[0] < 30)
                player.hurtCooldown[0] = 30;
            if (player.hurtCooldown[1] < 30)
                player.hurtCooldown[1] = 30;

            Warped = true;
        }
    }
}
