using System;
using FargoEnemyModifiers.Utilities;
using Microsoft.Xna.Framework;
using Terraria;

namespace FargoEnemyModifiers.Modifiers.Rarity3
{
    public class Aquarian : Modifier
    {
        public override string Key => "Aquarian";
        public override RarityID Rarity => RarityID.Rare;
        public override ModifierID ModifierID => ModifierID.Aquarian;

        public override bool ExtraCondition(NPC npc)
        {
            return Main.hardMode;
        }

        private int counter = 0;

        public override bool PreAI(NPC npc)
        {
            //let worms spawn their segments first
            if (++counter > 600)
            {
                DukeDash(npc);

                return false;
            }

            return true;
        }

        bool dashing = false; //ai 0
        float ai1 = 0; //yea idk what it actually is lol
        float counter2 = 0; //ai2

        private void DukeDash(NPC npc)
        {
            int framesToWindup = 60;
            int framesToDash = 30;
            float moveSpeed = 0.45f;
            float windUpSpeed = 7.5f;
            float dashSpeed = 16f;

            Vector2 center1 = npc.Center;
            Player player = Main.player[npc.target];
            if (npc.target < 0 || npc.target == byte.MaxValue || player.dead || !player.active)
            {
                npc.TargetClosest();
                player = Main.player[npc.target];
                npc.netUpdate = true;
            }

            if (!dashing && !player.dead)
            {
                if (ai1 == 0.0)
                    ai1 = 300 * Math.Sign((center1 - player.Center).X);
                Vector2 vector2 = Vector2.Normalize(player.Center + new Vector2(ai1, -200f) - center1 - npc.velocity) * windUpSpeed;
                if (npc.velocity.X < vector2.X)
                {
                    npc.velocity.X += moveSpeed;
                    if (npc.velocity.X < 0.0 && vector2.X > 0.0)
                        npc.velocity.X += moveSpeed;
                }
                else if (npc.velocity.X > vector2.X)
                {
                    npc.velocity.X -= moveSpeed;
                    if (npc.velocity.X > 0.0 && vector2.X < 0.0)
                        npc.velocity.X -= moveSpeed;
                }
                if (npc.velocity.Y < vector2.Y)
                {
                    npc.velocity.Y += moveSpeed;
                    if (npc.velocity.Y < 0.0 && vector2.Y > 0.0)
                        npc.velocity.Y += moveSpeed;
                }
                else if (npc.velocity.Y > vector2.Y)
                {
                    npc.velocity.Y -= moveSpeed;
                    if (npc.velocity.Y > 0.0 && vector2.Y < 0.0)
                        npc.velocity.Y -= moveSpeed;
                }

                ++counter2;
                if (counter2 < (double)framesToWindup)
                    return;

                dashing = true;
                counter2 = 0.0f;
                npc.velocity = Vector2.Normalize(player.Center - center1) * dashSpeed;

                npc.netUpdate = true;
            }
            else if (dashing)
            {
                int num20 = 7;
                for (int index1 = 0; index1 < num20; ++index1)
                {
                    Vector2 vector2_1 = (Vector2.Normalize(npc.velocity) * new Vector2((npc.width + 50) / 2f, npc.height) * 0.75f).RotatedBy((index1 - (num20 / 2 - 1)) * Math.PI / num20) + center1;
                    Vector2 vector2_2 = ((float)(Main.rand.NextDouble() * 3.14159274101257) - 1.570796f).ToRotationVector2() * Main.rand.Next(3, 8);
                    Vector2 vector2_3 = vector2_2;
                    int index2 = Dust.NewDust(vector2_1 + vector2_3, 0, 0, 172, (float)(vector2_2.X * 2.0), (float)(vector2_2.Y * 2.0), 100, default, 1.4f);
                    Main.dust[index2].noGravity = true;
                    Main.dust[index2].noLight = true;
                    Dust dust1 = Main.dust[index2];
                    Vector2 vector2_4 = dust1.velocity / 4f;
                    dust1.velocity = vector2_4;
                    Dust dust2 = Main.dust[index2];
                    Vector2 vector2_5 = dust2.velocity - npc.velocity;
                    dust2.velocity = vector2_5;
                }
                ++counter2;
                if (counter2 < (double)framesToDash)
                    return;

                npc.velocity *= 0.3f;
                counter = 0;
                dashing = false;
                counter2 = 0;
            }
        }
    }
}
