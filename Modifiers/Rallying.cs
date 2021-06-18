using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace FargoEnemyModifiers.Modifiers
{
    public class Rallying : Modifier
    {
        public Rallying()
        {
            name = "Rallying";
        }

        public override void AI(NPC npc)
        {
            const int range = 400;

            Aura(npc, range);

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC buffedNPC = Main.npc[i];

                if (buffedNPC.active && buffedNPC.whoAmI != npc.whoAmI && Vector2.Distance(npc.Center, buffedNPC.Center) < range)
                {
                    buffedNPC.GetGlobalNPC<EnemyModifiersGlobalNPC>().Rallied = true;
                    buffedNPC.GetGlobalNPC<EnemyModifiersGlobalNPC>().RallyTimer = 30;

                    if (Main.rand.Next(2) == 0)
                    {
                        int d = Dust.NewDust(buffedNPC.position, buffedNPC.width, buffedNPC.height, DustID.GoldFlame, 0f, -1.5f);
                        Main.dust[d].velocity *= 0.5f;
                        Main.dust[d].noLight = true;
                    }
                }
            }
        }
    }
}
