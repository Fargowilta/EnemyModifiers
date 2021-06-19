using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargoEnemyModifiers.NPCs
{
    public class BabySpider : ModNPC
    {
        public override string Texture => "Terraria/Projectile_379";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Baby Spider");
            Main.npcFrameCount[npc.type] = 4;
        }

        public override void SetDefaults()
        {
            npc.width = 14;
            npc.height = 10;
            npc.damage = 10;
            npc.defense = 5;
            npc.lifeMax = 20;
            npc.HitSound = SoundID.NPCHit29;
            npc.DeathSound = SoundID.NPCDeath31;
            npc.value = 0f;
            npc.knockBackResist = 0.5f;
            npc.aiStyle = -1;
        }

        public override void AI()
        {
            Vector2 value14 = npc.position;
            bool flag20 = false;
            float num593 = 500f;

            Player target = Main.player[npc.FindClosestPlayer()];

            float num595 = Vector2.Distance(target.Center, npc.Center);
            if (((Vector2.Distance(npc.Center, value14) > num595 && num595 < num593) || !flag20) && Collision.CanHit(npc.position, npc.width, npc.height, target.position, target.width, target.height))
            {
                value14 = target.Center;
                flag20 = true;
            }

            if (!flag20)
            {
                npc.velocity.X = npc.velocity.X * 0.95f;
            }
            else
            {
                float num596 = 5f;
                float num597 = 0.08f;
                if (npc.velocity.Y == 0f)
                {
                    bool flag21 = false;
                    if (npc.Center.Y - 50f > value14.Y)
                    {
                        flag21 = true;
                    }
                    if (flag21)
                    {
                        npc.velocity.Y = -6f;
                    }
                }
                else
                {
                    num596 = 8f;
                    num597 = 0.12f;
                }
                npc.velocity.X = npc.velocity.X + (float)Math.Sign(value14.X - npc.Center.X) * num597;
                if (npc.velocity.X < -num596)
                {
                    npc.velocity.X = -num596;
                }
                if (npc.velocity.X > num596)
                {
                    npc.velocity.X = num596;
                }
            }
            float num598 = 0f;
            try
            {
                Collision.StepUp(ref npc.position, ref npc.velocity, npc.width, npc.height, ref num598, ref npc.gfxOffY,
                    1, false, 0);
            }
            catch
            {
                // ignore
            }

            if (npc.velocity.X != 0f)
            {
                npc.direction = Math.Sign(npc.velocity.X);
            }
            npc.spriteDirection = npc.direction;
            npc.velocity.Y = npc.velocity.Y + 0.2f;
            if (npc.velocity.Y > 16f)
            {
                npc.velocity.Y = 16f;
                return;
            }

            //if (this.velocity.X != velocity.X)
            //{
            //	this.velocity.X = velocity.X * -0.6f;
            //}
            //if (this.velocity.Y != velocity.Y && velocity.Y > 2f)
            //{
            //	this.velocity.Y = velocity.Y * -0.6f;
            //}
        }

        public override void FindFrame(int frameHeight)
        {
            if (npc.velocity.Y != 0f)
            {
                npc.frame.Y = 3 * frameHeight;
            }
            else
            {
                if (Math.Abs(npc.velocity.X) > 0.2f)
                {
                    npc.frameCounter++;
                }
                if (npc.frameCounter >= 9)
                {
                    npc.frameCounter = 0;
                }
                if (npc.frameCounter >= 6)
                {
                    npc.frame.Y = 2 * frameHeight;
                }
                else if (npc.frameCounter >= 3)
                {
                    npc.frame.Y = 1 * frameHeight;
                }
                else
                {
                    npc.frame.Y = 0 * frameHeight;
                }
            }
        }

        public override void NPCLoot()
        {
            for (int num512 = 0; num512 < 5; num512++)
            {
                Dust dust = Dust.NewDustDirect(npc.position, npc.width, npc.height, 171, 0f, 0f, 100);
                dust.scale = Main.rand.Next(1, 10) * 0.1f;
                dust.noGravity = true;
                dust.fadeIn = 1.5f;
                dust.velocity *= 0.75f;
            }
        }
    }
}