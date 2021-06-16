using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace FargoEnemyModifiers.Modifiers
{
    public class Possessed : Modifier
    {
        public Possessed()
        {
            name = "Possessed";
        }

        private int counter = 0;

        public override bool PreAI(NPC npc)
        {
            //let worms spawn their segements first
            if (npc.aiStyle != 6 || ++counter > 2)
            {
                npc.noGravity = true;
                npc.noTileCollide = true;
                npc.alpha = 100;
                npc.realLife = npc.whoAmI;

                PossessedAI(npc);
                return false;
            }

            return true;
        }

        //vanilla AI 22
        private void PossessedAI(NPC npc)
        {
            bool flag19 = false;

            if (npc.justHit)
            {
                npc.ai[2] = 0f;
            }
            if (npc.ai[2] >= 0f)
            {
                int num282 = 16;
                bool flag21 = false;
                bool flag22 = false;
                if (npc.position.X > npc.ai[0] - (float)num282 && npc.position.X < npc.ai[0] + (float)num282)
                {
                    flag21 = true;
                }
                else if ((npc.velocity.X < 0f && npc.direction > 0) || (npc.velocity.X > 0f && npc.direction < 0))
                {
                    flag21 = true;
                }
                num282 += 24;
                if (npc.position.Y > npc.ai[1] - (float)num282 && npc.position.Y < npc.ai[1] + (float)num282)
                {
                    flag22 = true;
                }
                if (flag21 && flag22)
                {
                    npc.ai[2] += 1f;
                    if (npc.ai[2] >= 30f && num282 == 16)
                    {
                        flag19 = true;
                    }
                    if (npc.ai[2] >= 60f)
                    {
                        npc.ai[2] = -200f;
                        npc.direction *= -1;
                        npc.velocity.X = npc.velocity.X * -1f;
                        npc.collideX = false;
                    }
                }
                else
                {
                    //        npc.ai[0] = npc.position.X;
                    //        npc.ai[1] = npc.position.Y;
                    //        npc.ai[2] = 0f;
                }
                npc.TargetClosest(true);
            }
            else
            {
                npc.ai[2] += 1f;

                if (Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) > npc.position.X + (float)(npc.width / 2))
                {
                    npc.direction = -1;
                }
                else
                {
                    npc.direction = 1;
                }
            }
            int num283 = (int)((npc.position.X + (float)(npc.width / 2)) / 16f) + npc.direction * 2;
            int num284 = (int)((npc.position.Y + (float)npc.height) / 16f);
            bool flag23 = true;
            bool flag24 = false;
            int num285 = 3;

            for (int num308 = num284; num308 < num284 + num285; num308++)
            {
                if (Main.tile[num283, num308] == null)
                {
                    Main.tile[num283, num308] = new Tile();
                }
                if ((Main.tile[num283, num308].nactive() && Main.tileSolid[(int)Main.tile[num283, num308].type]) || Main.tile[num283, num308].liquid > 0)
                {
                    if (num308 <= num284 + 1)
                    {
                        flag24 = true;
                    }
                    flag23 = false;
                    break;
                }
            }
            if (Main.player[npc.target].npcTypeNoAggro[npc.type])
            {
                bool flag25 = false;
                for (int num309 = num284; num309 < num284 + num285 - 2; num309++)
                {
                    if (Main.tile[num283, num309] == null)
                    {
                        Main.tile[num283, num309] = new Tile();
                    }
                    if ((Main.tile[num283, num309].nactive() && Main.tileSolid[(int)Main.tile[num283, num309].type]) || Main.tile[num283, num309].liquid > 0)
                    {
                        flag25 = true;
                        break;
                    }
                }
                npc.directionY = (!flag25).ToDirectionInt();
            }

            if (flag19)
            {
                flag24 = false;
                flag23 = true;
            }
            if (flag23)
            {
                npc.velocity.Y = npc.velocity.Y + 0.1f;
                if (npc.velocity.Y > 3f)
                {
                    npc.velocity.Y = 3f;
                }
            }
            else
            {
                if (npc.directionY < 0 && npc.velocity.Y > 0f)
                {
                    npc.velocity.Y = npc.velocity.Y - 0.1f;
                }
                if (npc.velocity.Y < -4f)
                {
                    npc.velocity.Y = -4f;
                }
            }

            if (npc.collideX)
            {
                npc.velocity.X = npc.oldVelocity.X * -0.4f;
                if (npc.direction == -1 && npc.velocity.X > 0f && npc.velocity.X < 1f)
                {
                    npc.velocity.X = 1f;
                }
                if (npc.direction == 1 && npc.velocity.X < 0f && npc.velocity.X > -1f)
                {
                    npc.velocity.X = -1f;
                }
            }
            if (npc.collideY)
            {
                npc.velocity.Y = npc.oldVelocity.Y * -0.25f;
                if (npc.velocity.Y > 0f && npc.velocity.Y < 1f)
                {
                    npc.velocity.Y = 1f;
                }
                if (npc.velocity.Y < 0f && npc.velocity.Y > -1f)
                {
                    npc.velocity.Y = -1f;
                }
            }
            float num311 = 2f;

            if (npc.direction == -1 && npc.velocity.X > -num311)
            {
                npc.velocity.X = npc.velocity.X - 0.1f;
                if (npc.velocity.X > num311)
                {
                    npc.velocity.X = npc.velocity.X - 0.1f;
                }
                else if (npc.velocity.X > 0f)
                {
                    npc.velocity.X = npc.velocity.X + 0.05f;
                }
                if (npc.velocity.X < -num311)
                {
                    npc.velocity.X = -num311;
                }
            }
            else if (npc.direction == 1 && npc.velocity.X < num311)
            {
                npc.velocity.X = npc.velocity.X + 0.1f;
                if (npc.velocity.X < -num311)
                {
                    npc.velocity.X = npc.velocity.X + 0.1f;
                }
                else if (npc.velocity.X < 0f)
                {
                    npc.velocity.X = npc.velocity.X - 0.05f;
                }
                if (npc.velocity.X > num311)
                {
                    npc.velocity.X = num311;
                }
            }

            num311 = 1.5f;

            if (npc.directionY == -1 && npc.velocity.Y > -num311)
            {
                npc.velocity.Y = npc.velocity.Y - 0.04f;
                if (npc.velocity.Y > num311)
                {
                    npc.velocity.Y = npc.velocity.Y - 0.05f;
                }
                else if (npc.velocity.Y > 0f)
                {
                    npc.velocity.Y = npc.velocity.Y + 0.03f;
                }
                if (npc.velocity.Y < -num311)
                {
                    npc.velocity.Y = -num311;
                }
            }
            else if (npc.directionY == 1 && npc.velocity.Y < num311)
            {
                npc.velocity.Y = npc.velocity.Y + 0.04f;
                if (npc.velocity.Y < -num311)
                {
                    npc.velocity.Y = npc.velocity.Y + 0.05f;
                }
                else if (npc.velocity.Y < 0f)
                {
                    npc.velocity.Y = npc.velocity.Y - 0.03f;
                }
                if (npc.velocity.Y > num311)
                {
                    npc.velocity.Y = num311;
                }
            }
        }
    }
}
