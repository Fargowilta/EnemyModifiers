using FargoEnemyModifiers.NPCs;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargoEnemyModifiers.Modifiers
{
    public class Infested : Modifier
    {
        public Infested()
        {
            name = "Infested";
        }

        private int counter = 0;
        public override void AI(NPC npc)
        {
            if (NPC.CountNPCS(ModContent.NPCType<BabySpider>()) < 10 && ++counter > 120)
            {
                if (Collision.SolidCollision(npc.Center, npc.width, npc.height))
                {
                    return;
                }

                int index = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<BabySpider>());
                NPC spider = Main.npc[index];
                spider.GetGlobalNPC<EnemyModifiersGlobalNPC>().firstTick = false;
                spider.velocity = new Vector2(Main.rand.Next(-2, 3), -2);

                if (Main.netMode == NetmodeID.Server)
                    NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, index);

                counter = 0;
            }
        }
    }
}
