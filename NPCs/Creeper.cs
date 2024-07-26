using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace FargoEnemyModifiers.NPCs
{
    public class Creeper : ModNPC
    {
        public override string Texture => "Terraria/Images/NPC_267";


        public override void SetDefaults()
        {
            NPC.width = 30;
            NPC.height = 30;
            NPC.damage = 20;
            NPC.defense = 10;
            NPC.lifeMax = 100;
            NPC.HitSound = SoundID.NPCHit9;
            NPC.DeathSound = SoundID.NPCDeath11;
            NPC.value = 0f;
            NPC.knockBackResist = 0.8f;
            NPC.aiStyle = -1;
            NPC.noGravity = true;
            NPC.noTileCollide = true;

        }

        

        public override void AI()
        {
            NPC leaderNpc = Main.npc[(int)NPC.ai[2]];

            if (!leaderNpc.active)
            {
                NPC.active = false;
                NPC.netUpdate = true;
            }

            if (NPC.ai[0] == 0f)
            {
                NPC.ai[1] = 0f;
                Vector2 vector106 = new Vector2(NPC.Center.X, NPC.Center.Y);
                float num866 = leaderNpc.Center.X - vector106.X;
                float num867 = leaderNpc.Center.Y - vector106.Y;
                float num868 = (float)Math.Sqrt(num866 * num866 + num867 * num867);
                if (num868 > 50f) //distance before going back
                {
                    num868 = 8f / num868;
                    num866 *= num868;
                    num867 *= num868;
                    NPC.velocity.X = (NPC.velocity.X * 15f + num866) / 16f;
                    NPC.velocity.Y = (NPC.velocity.Y * 15f + num867) / 16f;
                    return;
                }
                if (Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y) < 8f)
                {
                    NPC.velocity.Y *= 1.05f;
                    NPC.velocity.X *= 1.05f;
                }
                if (Main.netMode != 1 && ((Main.expertMode && Main.rand.Next(100) == 0) || Main.rand.Next(200) == 0))
                {
                    NPC.TargetClosest();
                    vector106 = new Vector2(NPC.Center.X, NPC.Center.Y);
                    num866 = Main.player[NPC.target].Center.X - vector106.X;
                    num867 = Main.player[NPC.target].Center.Y - vector106.Y;
                    num868 = (float)Math.Sqrt(num866 * num866 + num867 * num867);
                    num868 = 8f / num868;
                    NPC.velocity.X = num866 * num868;
                    NPC.velocity.Y = num867 * num868;
                    NPC.ai[0] = 1f;
                    NPC.netUpdate = true;
                }
                return;
            }
            if (Main.expertMode)
            {
                Vector2 vector107 = Main.player[NPC.target].Center - NPC.Center;
                vector107.Normalize();
                if (Main.getGoodWorld)
                {
                    vector107 *= 12f;
                    NPC.velocity = (NPC.velocity * 49f + vector107) / 50f;
                }
                else
                {
                    vector107 *= 9f;
                    NPC.velocity = (NPC.velocity * 99f + vector107) / 100f;
                }
            }
            Vector2 vector108 = new Vector2(NPC.Center.X, NPC.Center.Y);
            float num869 = leaderNpc.Center.X - vector108.X;
            float num870 = leaderNpc.Center.Y - vector108.Y;
            float num871 = (float)Math.Sqrt(num869 * num869 + num870 * num870);
            if (num871 > 700f)
            {
                NPC.ai[0] = 0f;
            }
            else
            {
                if (!NPC.justHit)
                {
                    return;
                }
                if (NPC.knockBackResist == 0f)
                {
                    NPC.ai[1] += 1f;
                    if (NPC.ai[1] > 5f)
                    {
                        NPC.ai[0] = 0f;
                    }
                }
                else
                {
                    NPC.ai[0] = 0f;
                }
            }
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life > 0)
            {
                for (int num551 = 0; (double)num551 < hit.Damage / (double)NPC.lifeMax * 50.0; num551++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, hit.HitDirection, -1f);
                }
                return;
            }
            for (int num552 = 0; num552 < 20; num552++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, 2 * hit.HitDirection, -2f);
            }
            Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, 402);
        }
    }
}
