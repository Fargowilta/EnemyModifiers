using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace FargoEnemyModifiers.Modifiers
{
    public class Splitting : Modifier
    {
        public Splitting()
        {
            name = "Splitting";
        }

        public override bool PreNPCLoot(NPC npc)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                for (int i = 0; i < Main.rand.Next(2, 4); i++)
                {
                    int index = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, npc.type);
                    NPC baby = Main.npc[index];
                    baby.lifeMax /= 2;
                    baby.life = baby.lifeMax;
                    baby.defense /= 2;
                    baby.damage /= 2;
                    baby.scale = .75f;

                    baby.velocity = new Vector2(Main.rand.Next(-2, 3), -2);

                    baby.GetGlobalNPC<EnemyModifiersGlobalNPC>().firstTick = false;

                    if (Main.netMode == NetmodeID.Server)
                        NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, index);
                }
            }

            return false;
        }
    }
}
