using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;

namespace FargoEnemyModifiers.Modifiers
{
    public class Fortifying : Modifier
    {
        public Fortifying()
        {
            name = "Fortifying";
            defenseMultiplier = 0.5f;
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
                    buffedNPC.GetGlobalNPC<EnemyModifiersGlobalNPC>().Fortified = true;
                    buffedNPC.GetGlobalNPC<EnemyModifiersGlobalNPC>().FortTimer = 30;

                    if (Main.rand.Next(2) == 0)
                    {
                        int d = Dust.NewDust(buffedNPC.position, buffedNPC.width, buffedNPC.height, DustID.Iron, 0f, -1.5f, 0, new Color());
                        Main.dust[d].velocity *= 0.5f;
                        Main.dust[d].noLight = true;
                    }
                }
            }
        }
    }
}
