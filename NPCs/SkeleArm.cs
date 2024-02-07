using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargoEnemyModifiers.NPCs
{
    public class SkeleArm : ModNPC
    {
        public override string Texture => "Terraria/Images/NPC_36";

        public override void SetDefaults()
        {
            NPC.width = 52;
            NPC.height = 52;
            //NPC.aiStyle = 12;
            NPC.damage = 20;
            NPC.defense = 14;
            NPC.lifeMax = 600;
            NPC.HitSound = SoundID.NPCHit2;
            NPC.DeathSound = SoundID.NPCDeath2;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.knockBackResist = 0f;



        }

        public override void AI()
        {
            NPC.spriteDirection = -(int)NPC.ai[0];
            if (!Main.npc[(int)NPC.ai[1]].active)
            {
                NPC.ai[2] += 10f;
                if (NPC.ai[2] > 50f || Main.netMode != 2)
                {
                    NPC.life = -1;
                    NPC.HitEffect();
                    NPC.active = false;
                }
            }
            if (NPC.ai[2] == 0f || NPC.ai[2] == 3f)
            {
                if (Main.npc[(int)NPC.ai[1]].ai[1] == 3f)
                {
                    NPC.EncourageDespawn(10);
                }
                if (Main.npc[(int)NPC.ai[1]].ai[1] != 0f)
                {
                    if (NPC.position.Y > Main.npc[(int)NPC.ai[1]].position.Y - 100f)
                    {
                        if (NPC.velocity.Y > 0f)
                        {
                            NPC.velocity.Y *= 0.96f;
                        }
                        NPC.velocity.Y -= 0.07f;
                        if (NPC.velocity.Y > 6f)
                        {
                            NPC.velocity.Y = 6f;
                        }
                    }
                    else if (NPC.position.Y < Main.npc[(int)NPC.ai[1]].position.Y - 100f)
                    {
                        if (NPC.velocity.Y < 0f)
                        {
                            NPC.velocity.Y *= 0.96f;
                        }
                        NPC.velocity.Y += 0.07f;
                        if (NPC.velocity.Y < -6f)
                        {
                            NPC.velocity.Y = -6f;
                        }
                    }
                    if (NPC.position.X + (float)(NPC.width / 2) > Main.npc[(int)NPC.ai[1]].position.X + (float)(Main.npc[(int)NPC.ai[1]].width / 2) - 120f * NPC.ai[0])
                    {
                        if (NPC.velocity.X > 0f)
                        {
                            NPC.velocity.X *= 0.96f;
                        }
                        NPC.velocity.X -= 0.1f;
                        if (NPC.velocity.X > 8f)
                        {
                            NPC.velocity.X = 8f;
                        }
                    }
                    if (NPC.position.X + (float)(NPC.width / 2) < Main.npc[(int)NPC.ai[1]].position.X + (float)(Main.npc[(int)NPC.ai[1]].width / 2) - 120f * NPC.ai[0])
                    {
                        if (NPC.velocity.X < 0f)
                        {
                            NPC.velocity.X *= 0.96f;
                        }
                        NPC.velocity.X += 0.1f;
                        if (NPC.velocity.X < -8f)
                        {
                            NPC.velocity.X = -8f;
                        }
                    }
                }
                else
                {
                    NPC.ai[3] += 1f;
                    if (Main.expertMode)
                    {
                        NPC.ai[3] += 0.5f;
                    }
                    if (NPC.ai[3] >= 300f)
                    {
                        NPC.ai[2] += 1f;
                        NPC.ai[3] = 0f;
                        NPC.netUpdate = true;
                    }
                    if (Main.expertMode)
                    {
                        if (NPC.position.Y > Main.npc[(int)NPC.ai[1]].position.Y + 230f)
                        {
                            if (NPC.velocity.Y > 0f)
                            {
                                NPC.velocity.Y *= 0.96f;
                            }
                            NPC.velocity.Y -= 0.04f;
                            if (NPC.velocity.Y > 3f)
                            {
                                NPC.velocity.Y = 3f;
                            }
                        }
                        else if (NPC.position.Y < Main.npc[(int)NPC.ai[1]].position.Y + 230f)
                        {
                            if (NPC.velocity.Y < 0f)
                            {
                                NPC.velocity.Y *= 0.96f;
                            }
                            NPC.velocity.Y += 0.04f;
                            if (NPC.velocity.Y < -3f)
                            {
                                NPC.velocity.Y = -3f;
                            }
                        }
                        if (NPC.position.X + (float)(NPC.width / 2) > Main.npc[(int)NPC.ai[1]].position.X + (float)(Main.npc[(int)NPC.ai[1]].width / 2) - 200f * NPC.ai[0])
                        {
                            if (NPC.velocity.X > 0f)
                            {
                                NPC.velocity.X *= 0.96f;
                            }
                            NPC.velocity.X -= 0.07f;
                            if (NPC.velocity.X > 8f)
                            {
                                NPC.velocity.X = 8f;
                            }
                        }
                        if (NPC.position.X + (float)(NPC.width / 2) < Main.npc[(int)NPC.ai[1]].position.X + (float)(Main.npc[(int)NPC.ai[1]].width / 2) - 200f * NPC.ai[0])
                        {
                            if (NPC.velocity.X < 0f)
                            {
                                NPC.velocity.X *= 0.96f;
                            }
                            NPC.velocity.X += 0.07f;
                            if (NPC.velocity.X < -8f)
                            {
                                NPC.velocity.X = -8f;
                            }
                        }
                    }
                    if (NPC.position.Y > Main.npc[(int)NPC.ai[1]].position.Y + 230f)
                    {
                        if (NPC.velocity.Y > 0f)
                        {
                            NPC.velocity.Y *= 0.96f;
                        }
                        NPC.velocity.Y -= 0.04f;
                        if (NPC.velocity.Y > 3f)
                        {
                            NPC.velocity.Y = 3f;
                        }
                    }
                    else if (NPC.position.Y < Main.npc[(int)NPC.ai[1]].position.Y + 230f)
                    {
                        if (NPC.velocity.Y < 0f)
                        {
                            NPC.velocity.Y *= 0.96f;
                        }
                        NPC.velocity.Y += 0.04f;
                        if (NPC.velocity.Y < -3f)
                        {
                            NPC.velocity.Y = -3f;
                        }
                    }
                    if (NPC.position.X + (float)(NPC.width / 2) > Main.npc[(int)NPC.ai[1]].position.X + (float)(Main.npc[(int)NPC.ai[1]].width / 2) - 200f * NPC.ai[0])
                    {
                        if (NPC.velocity.X > 0f)
                        {
                            NPC.velocity.X *= 0.96f;
                        }
                        NPC.velocity.X -= 0.07f;
                        if (NPC.velocity.X > 8f)
                        {
                            NPC.velocity.X = 8f;
                        }
                    }
                    if (NPC.position.X + (float)(NPC.width / 2) < Main.npc[(int)NPC.ai[1]].position.X + (float)(Main.npc[(int)NPC.ai[1]].width / 2) - 200f * NPC.ai[0])
                    {
                        if (NPC.velocity.X < 0f)
                        {
                            NPC.velocity.X *= 0.96f;
                        }
                        NPC.velocity.X += 0.07f;
                        if (NPC.velocity.X < -8f)
                        {
                            NPC.velocity.X = -8f;
                        }
                    }
                }
                Vector2 vector22 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
                float num181 = Main.npc[(int)NPC.ai[1]].position.X + (float)(Main.npc[(int)NPC.ai[1]].width / 2) - 200f * NPC.ai[0] - vector22.X;
                float num182 = Main.npc[(int)NPC.ai[1]].position.Y + 230f - vector22.Y;
                float num183 = (float)Math.Sqrt(num181 * num181 + num182 * num182);
                NPC.rotation = (float)Math.Atan2(num182, num181) + 1.57f;
            }
            else if (NPC.ai[2] == 1f)
            {
                Vector2 vector23 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
                float num184 = Main.npc[(int)NPC.ai[1]].position.X + (float)(Main.npc[(int)NPC.ai[1]].width / 2) - 200f * NPC.ai[0] - vector23.X;
                float num185 = Main.npc[(int)NPC.ai[1]].position.Y + 230f - vector23.Y;
                float num186 = (float)Math.Sqrt(num184 * num184 + num185 * num185);
                NPC.rotation = (float)Math.Atan2(num185, num184) + 1.57f;
                NPC.velocity.X *= 0.95f;
                NPC.velocity.Y -= 0.1f;
                if (Main.expertMode)
                {
                    NPC.velocity.Y -= 0.06f;
                    if (NPC.velocity.Y < -13f)
                    {
                        NPC.velocity.Y = -13f;
                    }
                }
                else if (NPC.velocity.Y < -8f)
                {
                    NPC.velocity.Y = -8f;
                }
                if (NPC.position.Y < Main.npc[(int)NPC.ai[1]].position.Y - 200f)
                {
                    NPC.TargetClosest();
                    NPC.ai[2] = 2f;
                    vector23 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
                    num184 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector23.X;
                    num185 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - vector23.Y;
                    num186 = (float)Math.Sqrt(num184 * num184 + num185 * num185);
                    num186 = ((!Main.expertMode) ? (18f / num186) : (21f / num186));
                    NPC.velocity.X = num184 * num186;
                    NPC.velocity.Y = num185 * num186;
                    NPC.netUpdate = true;
                }
            }
            else if (NPC.ai[2] == 2f)
            {
                if (NPC.position.Y > Main.player[NPC.target].position.Y || NPC.velocity.Y < 0f)
                {
                    NPC.ai[2] = 3f;
                }
            }
            else if (NPC.ai[2] == 4f)
            {
                Vector2 vector24 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
                float num187 = Main.npc[(int)NPC.ai[1]].position.X + (float)(Main.npc[(int)NPC.ai[1]].width / 2) - 200f * NPC.ai[0] - vector24.X;
                float num188 = Main.npc[(int)NPC.ai[1]].position.Y + 230f - vector24.Y;
                float num189 = (float)Math.Sqrt(num187 * num187 + num188 * num188);
                NPC.rotation = (float)Math.Atan2(num188, num187) + 1.57f;
                NPC.velocity.Y *= 0.95f;
                NPC.velocity.X += 0.1f * (0f - NPC.ai[0]);
                if (Main.expertMode)
                {
                    NPC.velocity.X += 0.07f * (0f - NPC.ai[0]);
                    if (NPC.velocity.X < -12f)
                    {
                        NPC.velocity.X = -12f;
                    }
                    else if (NPC.velocity.X > 12f)
                    {
                        NPC.velocity.X = 12f;
                    }
                }
                else if (NPC.velocity.X < -8f)
                {
                    NPC.velocity.X = -8f;
                }
                else if (NPC.velocity.X > 8f)
                {
                    NPC.velocity.X = 8f;
                }
                if (NPC.position.X + (float)(NPC.width / 2) < Main.npc[(int)NPC.ai[1]].position.X + (float)(Main.npc[(int)NPC.ai[1]].width / 2) - 500f || NPC.position.X + (float)(NPC.width / 2) > Main.npc[(int)NPC.ai[1]].position.X + (float)(Main.npc[(int)NPC.ai[1]].width / 2) + 500f)
                {
                    NPC.TargetClosest();
                    NPC.ai[2] = 5f;
                    vector24 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
                    num187 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector24.X;
                    num188 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - vector24.Y;
                    num189 = (float)Math.Sqrt(num187 * num187 + num188 * num188);
                    num189 = ((!Main.expertMode) ? (17f / num189) : (22f / num189));
                    NPC.velocity.X = num187 * num189;
                    NPC.velocity.Y = num188 * num189;
                    NPC.netUpdate = true;
                }
            }
            else if (NPC.ai[2] == 5f && ((NPC.velocity.X > 0f && NPC.position.X + (float)(NPC.width / 2) > Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2)) || (NPC.velocity.X < 0f && NPC.position.X + (float)(NPC.width / 2) < Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2))))
            {
                NPC.ai[2] = 0f;
            }
        }

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Vector2 vector5 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f - 5f * NPC.ai[0], NPC.position.Y + 20f);
            for (int j = 0; j < 2; j++)
            {
                float num14 = Main.npc[(int)NPC.ai[1]].position.X + (float)(Main.npc[(int)NPC.ai[1]].width / 2) - vector5.X;
                float num15 = Main.npc[(int)NPC.ai[1]].position.Y + (float)(Main.npc[(int)NPC.ai[1]].height / 2) - vector5.Y;
                float num16 = 0f;
                if (j == 0)
                {
                    num14 -= 200f * NPC.ai[0];
                    num15 += 130f;
                    num16 = (float)Math.Sqrt(num14 * num14 + num15 * num15);
                    num16 = 92f / num16;
                    vector5.X += num14 * num16;
                    vector5.Y += num15 * num16;
                }
                else
                {
                    num14 -= 50f * NPC.ai[0];
                    num15 += 80f;
                    num16 = (float)Math.Sqrt(num14 * num14 + num15 * num15);
                    num16 = 60f / num16;
                    vector5.X += num14 * num16;
                    vector5.Y += num15 * num16;
                }
                float rotation5 = (float)Math.Atan2(num15, num14) - 1.57f;
                Microsoft.Xna.Framework.Color color5 = Lighting.GetColor((int)vector5.X / 16, (int)(vector5.Y / 16f));
                spriteBatch.Draw(((Texture2D)TextureAssets.BoneArm), new Vector2(vector5.X - screenPos.X, vector5.Y - screenPos.Y), new Microsoft.Xna.Framework.Rectangle(0, 0, TextureAssets.BoneArm.Width(), TextureAssets.BoneArm.Height()), color5, rotation5, new Vector2((float)TextureAssets.BoneArm.Width() * 0.5f, (float)TextureAssets.BoneArm.Height() * 0.5f), 1f, SpriteEffects.None, 0f);
                if (j == 0)
                {
                    vector5.X += num14 * num16 / 2f;
                    vector5.Y += num15 * num16 / 2f;
                }
                else if ( !Main.gameInactive)// base.IsActive)
                {
                    vector5.X += num14 * num16 - 16f;
                    vector5.Y += num15 * num16 - 6f;
                    int num17 = Dust.NewDust(new Vector2(vector5.X, vector5.Y), 30, 10, 5, num14 * 0.02f, num15 * 0.02f, 0, default(Microsoft.Xna.Framework.Color), 2f);
                    Main.dust[num17].noGravity = true;
                }
            }
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life > 0)
            {
                for (int num771 = 0; (double)num771 < hit.Damage / (double)NPC.lifeMax * 100.0; num771++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, 26,hit.HitDirection, -1f);
                }
                return;
            }
            for (int num772 = 0; num772 < 150; num772++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, 26, 2.5f * (float)hit.HitDirection, -2.5f);
            }

            Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, 56);
            Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, 57);
            Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, 57);
            Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, 57);
        }

    }
}
