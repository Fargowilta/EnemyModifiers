using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargoEnemyModifiers.NPCs
{
    public class BabySpider : ModNPC
    {
        public override string Texture => "Terraria/Images/Projectile_379";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Baby Spider");
            Main.npcFrameCount[NPC.type] = 4;
        }

        public override void SetDefaults()
        {
            NPC.width = 14;
            NPC.height = 10;
            NPC.damage = 10;
            NPC.defense = 5;
            NPC.lifeMax = 20;
            NPC.HitSound = SoundID.NPCHit29;
            NPC.DeathSound = SoundID.NPCDeath31;
            NPC.value = 0f;
            NPC.knockBackResist = 0.5f;
            NPC.aiStyle = -1;
        }

        //public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        //{
        //    bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
        //        BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.SpiderNest,
        //        new FlavorTextBestiaryInfoElement("Mods.FargoEnemyModifiers.Bestiary.BabySpider")
        //    });

        //}

        public override void AI()
        {
            Vector2 value14 = NPC.position;
            bool flag20 = false;
            float num593 = 500f;

            int player = NPC.FindClosestPlayer();
            if (player == -1)
            {
                return; // This scenario can occur when the last player disconnects
            }
            Player target = Main.player[player];

            float num595 = Vector2.Distance(target.Center, NPC.Center);
            if (((Vector2.Distance(NPC.Center, value14) > num595 && num595 < num593) || !flag20) && Collision.CanHit(NPC.position, NPC.width, NPC.height, target.position, target.width, target.height))
            {
                value14 = target.Center;
                flag20 = true;
            }

            if (!flag20)
            {
                NPC.velocity.X = NPC.velocity.X * 0.95f;
            }
            else
            {
                float num596 = 5f;
                float num597 = 0.08f;
                if (NPC.velocity.Y == 0f)
                {
                    bool flag21 = false;
                    if (NPC.Center.Y - 50f > value14.Y)
                    {
                        flag21 = true;
                    }
                    if (flag21)
                    {
                        NPC.velocity.Y = -6f;
                    }
                }
                else
                {
                    num596 = 8f;
                    num597 = 0.12f;
                }
                NPC.velocity.X = NPC.velocity.X + (float)Math.Sign(value14.X - NPC.Center.X) * num597;
                if (NPC.velocity.X < -num596)
                {
                    NPC.velocity.X = -num596;
                }
                if (NPC.velocity.X > num596)
                {
                    NPC.velocity.X = num596;
                }
            }
            float num598 = 0f;
            if (!(NPC.Center.X < 50 || NPC.Center.Y < 50 //oob checks
                || NPC.Center.X > Main.maxTilesX * 16 - 50 || NPC.Center.Y > Main.maxTilesY * 16 - 50))
            {
                try
                {
                    Collision.StepUp(ref NPC.position, ref NPC.velocity, NPC.width, NPC.height, ref num598, ref NPC.gfxOffY,
                        1, false, 0);
                }
                catch
                {
                    // ignore
                }
            }

            if (NPC.velocity.X != 0f)
            {
                NPC.direction = Math.Sign(NPC.velocity.X);
            }
            NPC.spriteDirection = NPC.direction;
            NPC.velocity.Y = NPC.velocity.Y + 0.2f;
            if (NPC.velocity.Y > 16f)
            {
                NPC.velocity.Y = 16f;
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
            if (NPC.velocity.Y != 0f)
            {
                NPC.frame.Y = 3 * frameHeight;
            }
            else
            {
                if (Math.Abs(NPC.velocity.X) > 0.2f)
                {
                    NPC.frameCounter++;
                }
                if (NPC.frameCounter >= 9)
                {
                    NPC.frameCounter = 0;
                }
                if (NPC.frameCounter >= 6)
                {
                    NPC.frame.Y = 2 * frameHeight;
                }
                else if (NPC.frameCounter >= 3)
                {
                    NPC.frame.Y = 1 * frameHeight;
                }
                else
                {
                    NPC.frame.Y = 0 * frameHeight;
                }
            }
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life == 0)
            {
                for (int num512 = 0; num512 < 5; num512++)
                {
                    Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, 171, 0f, 0f, 100);
                    dust.scale = Main.rand.Next(1, 10) * 0.1f;
                    dust.noGravity = true;
                    dust.fadeIn = 1.5f;
                    dust.velocity *= 0.75f;
                }
            }
        }
    }
}