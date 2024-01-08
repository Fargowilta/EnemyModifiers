//using System;
//using Microsoft.Xna.Framework;
//using Terraria;
//using Terraria.Audio;

//namespace FargoEnemyModifiers.Modifiers
//{
//    public class Duke : Modifier
//    {
//        public override string Name => "Duke";
//        public override string Description => "Replaces its normal AI with Duke Fishron AI";
//        public override int Rarity => 4;

//        public override bool ExtraCondition(NPC npc)
//        {
//            return Main.hardMode;
//        }

//        private int counter;

//        public override bool PreAI(NPC npc)
//        {
//            //let worms spawn their segments first
//            if (npc.aiStyle != 6 || ++counter > 2)
//            {
//                npc.noGravity = true;
//                npc.noTileCollide = true;
//                npc.alpha = 100;
//                npc.realLife = npc.whoAmI;

//                DukeAI(npc);
//                return false;
//            }

//            return true;
//        }

//        private void DukeAI(NPC npc) //literally the entire vanilla fishron ai
//        {
//            bool expertMode = Main.expertMode;
//            float num1 = 1f;//expertMode ? 0.6f * Main.damageMultiplier : 1f;
//            bool flag1 = npc.life <= npc.lifeMax * 0.5;
//            bool flag2 = expertMode && npc.life <= npc.lifeMax * 0.15;
//            bool flag3 = npc.ai[0] > 4.0;
//            bool flag4 = npc.ai[0] > 9.0;
//            bool flag5 = npc.ai[3] < 10.0;
//            if (flag4)
//            {
//                npc.damage = (int)(npc.defDamage * 1.10000002384186 * num1);
//                npc.defense = 0;
//            }
//            else if (flag3)
//            {
//                npc.damage = (int)(npc.defDamage * 1.20000004768372 * num1);
//                npc.defense = (int)(npc.defDefense * 0.800000011920929);
//            }
//            else
//            {
//                npc.damage = npc.defDamage;
//                npc.defense = npc.defDefense;
//            }
//            int num2 = expertMode ? 40 : 60;
//            float moveSpeed = expertMode ? 0.55f : 0.45f;
//            float num3 = expertMode ? 8.5f : 7.5f;
//            if (flag4)
//            {
//                moveSpeed = 0.7f;
//                num3 = 12f;
//                num2 = 30;
//            }
//            else if (flag3 & flag5)
//            {
//                moveSpeed = expertMode ? 0.6f : 0.5f;
//                num3 = expertMode ? 10f : 8f;
//                num2 = expertMode ? 40 : 20;
//            }
//            else if (flag5 && !flag3 && !flag4)
//                num2 = 30;
//            int num4 = expertMode ? 28 : 30;
//            float num5 = expertMode ? 17f : 16f;
//            if (flag4)
//            {
//                num4 = 25;
//                num5 = 27f;
//            }
//            else if (flag5 & flag3)
//            {
//                num4 = expertMode ? 27 : 30;
//                if (expertMode)
//                    num5 = 21f;
//            }
//            int num6 = 80;
//            int num7 = 4;
//            float num8 = 0.3f;
//            float num9 = 5f;
//            int num10 = 90;
//            int num11 = 180;
//            int num12 = 180;
//            int num13 = 30;
//            int num14 = 120;
//            int num15 = 4;
//            float num16 = 6f;
//            float num17 = 20f;
//            float num18 = 6.283185f / (num14 / 2);
//            int num19 = 75;
//            Vector2 center1 = npc.Center;
//            Player player = Main.player[npc.target];
//            if (npc.target < 0 || npc.target == byte.MaxValue || player.dead || !player.active)
//            {
//                npc.TargetClosest();
//                player = Main.player[npc.target];
//                npc.netUpdate = true;
//            }
//            if (player.dead || Vector2.Distance(player.Center, center1) > 5600.0)
//            {
//                npc.velocity.Y -= 0.4f;
//                if (npc.timeLeft > 10)
//                    npc.timeLeft = 10;
//                npc.ai[0] = npc.ai[0] <= 4.0 ? 0.0f : 5f;
//                npc.ai[2] = 0.0f;
//            }
//            if ((player.position.Y < 800.0 || player.position.Y > Main.worldSurface * 16.0 ? 1 : player.position.X <= 6400.0 ? 0 : player.position.X < (double)(Main.maxTilesX * 16 - 6400) ? 1 : 0) != 0)
//            {
//                num2 = 20;
//                npc.damage = npc.defDamage * 2;
//                npc.defense = npc.defDefense * 2;
//                npc.ai[3] = 0.0f;
//                num5 += 6f;
//            }
//            if (npc.localAI[0] == 0.0)
//            {
//                npc.localAI[0] = 1f;
//                npc.alpha = byte.MaxValue;
//                npc.rotation = 0.0f;
//                if (Main.netMode != 1)
//                {
//                    npc.ai[0] = -1f;
//                    npc.netUpdate = true;
//                }
//            }
//            float num21 = (float)Math.Atan2(player.Center.Y - center1.Y, player.Center.X - center1.X);
//            if (npc.spriteDirection == 1)
//                num21 += 3.141593f;
//            if (num21 < 0.0)
//                num21 += 6.283185f;
//            if (num21 > 6.28318548202515)
//                num21 -= 6.283185f;
//            if (npc.ai[0] == -1.0)
//                num21 = 0.0f;
//            if (npc.ai[0] == 3.0)
//                num21 = 0.0f;
//            if (npc.ai[0] == 4.0)
//                num21 = 0.0f;
//            if (npc.ai[0] == 8.0)
//                num21 = 0.0f;
//            float num22 = 0.04f;
//            if (npc.ai[0] == 1.0 || npc.ai[0] == 6.0)
//                num22 = 0.0f;
//            if (npc.ai[0] == 7.0)
//                num22 = 0.0f;
//            if (npc.ai[0] == 3.0)
//                num22 = 0.01f;
//            if (npc.ai[0] == 4.0)
//                num22 = 0.01f;
//            if (npc.ai[0] == 8.0)
//                num22 = 0.01f;
//            if (npc.rotation < (double)num21)
//                npc.rotation = num21 - (double)npc.rotation <= Math.PI ? npc.rotation + num22 : npc.rotation - num22;
//            if (npc.rotation > (double)num21)
//                npc.rotation = npc.rotation - (double)num21 <= Math.PI ? npc.rotation - num22 : npc.rotation + num22;
//            if (npc.rotation > num21 - (double)num22 && npc.rotation < num21 + (double)num22)
//                npc.rotation = num21;
//            if (npc.rotation < 0.0)
//                npc.rotation = npc.rotation + 6.283185f;
//            if (npc.rotation > 6.28318548202515)
//                npc.rotation = npc.rotation - 6.283185f;
//            if (npc.rotation > num21 - (double)num22 && npc.rotation < num21 + (double)num22)
//                npc.rotation = num21;
//            if (npc.ai[0] != -1.0 && npc.ai[0] < 9.0)
//            {
//                npc.alpha = !Collision.SolidCollision(npc.position, npc.width, npc.height) ? npc.alpha - 15 : npc.alpha + 15;
//                if (npc.alpha < 0)
//                    npc.alpha = 0;
//                if (npc.alpha > 150)
//                    npc.alpha = 150;
//            }
//            if (npc.ai[0] == -1.0)
//            {
//                npc.velocity *= 0.98f;
//                int num20 = Math.Sign(player.Center.X - center1.X);
//                if (num20 != 0)
//                {
//                    npc.direction = num20;
//                    npc.spriteDirection = -npc.direction;
//                }
//                if (npc.ai[2] > 20.0)
//                {
//                    npc.velocity.Y = -2f;
//                    npc.alpha = npc.alpha - 5;
//                    if (Collision.SolidCollision(npc.position, npc.width, npc.height))
//                        npc.alpha = npc.alpha + 15;
//                    if (npc.alpha < 0)
//                        npc.alpha = 0;
//                    if (npc.alpha > 150)
//                        npc.alpha = 150;
//                }
//                if (npc.ai[2] == (double)(num10 - 30))
//                {
//                    int num23 = 36;
//                    for (int index1 = 0; index1 < num23; ++index1)
//                    {
//                        Vector2 vector2_1 = (Vector2.Normalize(npc.velocity) * new Vector2(npc.width / 2f, npc.height) * 0.75f * 0.5f).RotatedBy((index1 - (num23 / 2 - 1)) * 6.28318548202515 / num23) + npc.Center;
//                        Vector2 center2 = npc.Center;
//                        Vector2 vector2_2 = vector2_1 - center2;
//                        Vector2 vector2_3 = vector2_2;
//                        int index2 = Dust.NewDust(vector2_1 + vector2_3, 0, 0, 172, (float)(vector2_2.X * 2.0), (float)(vector2_2.Y * 2.0), 100, default, 1.4f);
//                        Main.dust[index2].noGravity = true;
//                        Main.dust[index2].noLight = true;
//                        Main.dust[index2].velocity = Vector2.Normalize(vector2_2) * 3f;
//                    }
//                    //SoundEngine.PlaySound(29, (int)center1.X, (int)center1.Y, 20);
//                }
//                ++npc.ai[2];
//                if (npc.ai[2] < (double)num19)
//                    return;
//                npc.ai[0] = 0.0f;
//                npc.ai[1] = 0.0f;
//                npc.ai[2] = 0.0f;
//                npc.netUpdate = true;
//            }
//            else if (npc.ai[0] == 0.0 && !player.dead)
//            {
//                if (npc.ai[1] == 0.0)
//                    npc.ai[1] = 300 * Math.Sign((center1 - player.Center).X);
//                Vector2 vector2 = Vector2.Normalize(player.Center + new Vector2(npc.ai[1], -200f) - center1 - npc.velocity) * num3;
//                if (npc.velocity.X < vector2.X)
//                {
//                    npc.velocity.X += moveSpeed;
//                    if (npc.velocity.X < 0.0 && vector2.X > 0.0)
//                        npc.velocity.X += moveSpeed;
//                }
//                else if (npc.velocity.X > vector2.X)
//                {
//                    npc.velocity.X -= moveSpeed;
//                    if (npc.velocity.X > 0.0 && vector2.X < 0.0)
//                        npc.velocity.X -= moveSpeed;
//                }
//                if (npc.velocity.Y < vector2.Y)
//                {
//                    npc.velocity.Y += moveSpeed;
//                    if (npc.velocity.Y < 0.0 && vector2.Y > 0.0)
//                        npc.velocity.Y += moveSpeed;
//                }
//                else if (npc.velocity.Y > vector2.Y)
//                {
//                    npc.velocity.Y -= moveSpeed;
//                    if (npc.velocity.Y > 0.0 && vector2.Y < 0.0)
//                        npc.velocity.Y -= moveSpeed;
//                }
//                int num24 = Math.Sign(player.Center.X - center1.X);
//                if (num24 != 0)
//                {
//                    if (npc.ai[2] == 0.0 && num24 != npc.direction)
//                        npc.rotation = npc.rotation + 3.141593f;
//                    npc.direction = num24;
//                    if (npc.spriteDirection != -npc.direction)
//                        npc.rotation = npc.rotation + 3.141593f;
//                    npc.spriteDirection = -npc.direction;
//                }
//                ++npc.ai[2];
//                if (npc.ai[2] < (double)num2)
//                    return;
//                int num25 = 0;
//                switch ((int)npc.ai[3])
//                {
//                    case 0:
//                    case 1:
//                    case 2:
//                    case 3:
//                    case 4:
//                    case 5:
//                    case 6:
//                    case 7:
//                    case 8:
//                    case 9:
//                        num25 = 1;
//                        break;
//                    case 10:
//                        npc.ai[3] = 1f;
//                        num25 = 2;
//                        break;
//                    case 11:
//                        npc.ai[3] = 0.0f;
//                        num25 = 3;
//                        break;
//                }
//                if (flag1)
//                    num25 = 4;
//                if (num25 == 1)
//                {
//                    npc.ai[0] = 1f;
//                    npc.ai[1] = 0.0f;
//                    npc.ai[2] = 0.0f;
//                    npc.velocity = Vector2.Normalize(player.Center - center1) * num5;
//                    npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X);
//                    if (num24 != 0)
//                    {
//                        npc.direction = num24;
//                        if (npc.spriteDirection == 1)
//                            npc.rotation = npc.rotation + 3.141593f;
//                        npc.spriteDirection = -npc.direction;
//                    }
//                }
//                else if (num25 == 2)
//                {
//                    npc.ai[0] = 2f;
//                    npc.ai[1] = 0.0f;
//                    npc.ai[2] = 0.0f;
//                }
//                else if (num25 == 3)
//                {
//                    npc.ai[0] = 3f;
//                    npc.ai[1] = 0.0f;
//                    npc.ai[2] = 0.0f;
//                }
//                else if (num25 == 4)
//                {
//                    npc.ai[0] = 4f;
//                    npc.ai[1] = 0.0f;
//                    npc.ai[2] = 0.0f;
//                }
//                npc.netUpdate = true;
//            }
//            else if (npc.ai[0] == 1.0)
//            {
//                int num20 = 7;
//                for (int index1 = 0; index1 < num20; ++index1)
//                {
//                    Vector2 vector2_1 = (Vector2.Normalize(npc.velocity) * new Vector2((npc.width + 50) / 2f, npc.height) * 0.75f).RotatedBy((index1 - (num20 / 2 - 1)) * Math.PI / num20) + center1;
//                    Vector2 vector2_2 = ((float)(Main.rand.NextDouble() * 3.14159274101257) - 1.570796f).ToRotationVector2() * Main.rand.Next(3, 8);
//                    Vector2 vector2_3 = vector2_2;
//                    int index2 = Dust.NewDust(vector2_1 + vector2_3, 0, 0, 172, (float)(vector2_2.X * 2.0), (float)(vector2_2.Y * 2.0), 100, default, 1.4f);
//                    Main.dust[index2].noGravity = true;
//                    Main.dust[index2].noLight = true;
//                    Dust dust1 = Main.dust[index2];
//                    Vector2 vector2_4 = dust1.velocity / 4f;
//                    dust1.velocity = vector2_4;
//                    Dust dust2 = Main.dust[index2];
//                    Vector2 vector2_5 = dust2.velocity - npc.velocity;
//                    dust2.velocity = vector2_5;
//                }
//                ++npc.ai[2];
//                if (npc.ai[2] < (double)num4)
//                    return;
//                npc.ai[0] = 0.0f;
//                npc.ai[1] = 0.0f;
//                npc.ai[2] = 0.0f;
//                npc.ai[3] += 2f;
//                npc.netUpdate = true;
//            }
//            else if (npc.ai[0] == 2.0)
//            {
//                if (npc.ai[1] == 0.0)
//                    npc.ai[1] = 300 * Math.Sign((center1 - player.Center).X);
//                Vector2 vector2_1 = Vector2.Normalize(player.Center + new Vector2(npc.ai[1], -200f) - center1 - npc.velocity) * num9;
//                if (npc.velocity.X < vector2_1.X)
//                {
//                    npc.velocity.X += num8;
//                    if (npc.velocity.X < 0.0 && vector2_1.X > 0.0)
//                        npc.velocity.X += num8;
//                }
//                else if (npc.velocity.X > vector2_1.X)
//                {
//                    npc.velocity.X -= num8;
//                    if (npc.velocity.X > 0.0 && vector2_1.X < 0.0)
//                        npc.velocity.X -= num8;
//                }
//                if (npc.velocity.Y < vector2_1.Y)
//                {
//                    npc.velocity.Y += num8;
//                    if (npc.velocity.Y < 0.0 && vector2_1.Y > 0.0)
//                        npc.velocity.Y += num8;
//                }
//                else if (npc.velocity.Y > vector2_1.Y)
//                {
//                    npc.velocity.Y -= num8;
//                    if (npc.velocity.Y > 0.0 && vector2_1.Y < 0.0)
//                        npc.velocity.Y -= num8;
//                }
//                if (npc.ai[2] == 0.0)
//                    //SoundEngine.PlaySound(29, (int)center1.X, (int)center1.Y, 20);
//                if (npc.ai[2] % (double)num7 == 0.0)
//                {
//                    //SoundEngine.PlaySound(4, (int)npc.Center.X, (int)npc.Center.Y, 19);
//                    if (Main.netMode != 1)
//                    {
//                        Vector2 vector2_2 = Vector2.Normalize(player.Center - center1) * (npc.width + 20) / 2f + center1;
//                        NPC.NewNPC(npc.GetSource_FromAI(), (int)vector2_2.X, (int)vector2_2.Y + 45, 371);
//                    }
//                }
//                int num24 = Math.Sign(player.Center.X - center1.X);
//                if (num24 != 0)
//                {
//                    npc.direction = num24;
//                    if (npc.spriteDirection != -npc.direction)
//                        npc.rotation = npc.rotation + 3.141593f;
//                    npc.spriteDirection = -npc.direction;
//                }
//                ++npc.ai[2];
//                if (npc.ai[2] < (double)num6)
//                    return;
//                npc.ai[0] = 0.0f;
//                npc.ai[1] = 0.0f;
//                npc.ai[2] = 0.0f;
//                npc.netUpdate = true;
//            }
//            else if (npc.ai[0] == 3.0)
//            {
//                npc.velocity *= 0.98f;
//                npc.velocity.Y = MathHelper.Lerp(npc.velocity.Y, 0.0f, 0.02f);
//                if (npc.ai[2] == (double)(num10 - 30))
//                    //SoundEngine.PlaySound(29, (int)center1.X, (int)center1.Y, 9);
//                if (Main.netMode != 1 && npc.ai[2] == (double)(num10 - 30))
//                {
//                    Vector2 vector2 = npc.rotation.ToRotationVector2() * Vector2.UnitX * npc.direction * (npc.width + 20) / 2f + center1;
//                    Projectile.NewProjectile(npc.GetSource_FromAI(), vector2.X, vector2.Y, npc.direction * 2, 8f, 385, 0, 0.0f, Main.myPlayer);
//                    Projectile.NewProjectile(npc.GetSource_FromAI(), vector2.X, vector2.Y, -npc.direction * 2, 8f, 385, 0, 0.0f, Main.myPlayer);
//                }
//                ++npc.ai[2];
//                if (npc.ai[2] < (double)num10)
//                    return;
//                npc.ai[0] = 0.0f;
//                npc.ai[1] = 0.0f;
//                npc.ai[2] = 0.0f;
//                npc.netUpdate = true;
//            }
//            else if (npc.ai[0] == 4.0)
//            {
//                npc.velocity *= 0.98f;
//                npc.velocity.Y = MathHelper.Lerp(npc.velocity.Y, 0.0f, 0.02f);
//                if (npc.ai[2] == (double)(num11 - 60))
//                    //SoundEngine.PlaySound(29, (int)center1.X, (int)center1.Y, 20);
//                ++npc.ai[2];
//                if (npc.ai[2] < (double)num11)
//                    return;
//                npc.ai[0] = 5f;
//                npc.ai[1] = 0.0f;
//                npc.ai[2] = 0.0f;
//                npc.ai[3] = 0.0f;
//                npc.netUpdate = true;
//            }
//            else if (npc.ai[0] == 5.0 && !player.dead)
//            {
//                if (npc.ai[1] == 0.0)
//                    npc.ai[1] = 300 * Math.Sign((center1 - player.Center).X);
//                Vector2 vector2 = Vector2.Normalize(player.Center + new Vector2(npc.ai[1], -200f) - center1 - npc.velocity) * num3;
//                if (npc.velocity.X < vector2.X)
//                {
//                    npc.velocity.X += moveSpeed;
//                    if (npc.velocity.X < 0.0 && vector2.X > 0.0)
//                        npc.velocity.X += moveSpeed;
//                }
//                else if (npc.velocity.X > vector2.X)
//                {
//                    npc.velocity.X -= moveSpeed;
//                    if (npc.velocity.X > 0.0 && vector2.X < 0.0)
//                        npc.velocity.X -= moveSpeed;
//                }
//                if (npc.velocity.Y < vector2.Y)
//                {
//                    npc.velocity.Y += moveSpeed;
//                    if (npc.velocity.Y < 0.0 && vector2.Y > 0.0)
//                        npc.velocity.Y += moveSpeed;
//                }
//                else if (npc.velocity.Y > vector2.Y)
//                {
//                    npc.velocity.Y -= moveSpeed;
//                    if (npc.velocity.Y > 0.0 && vector2.Y < 0.0)
//                        npc.velocity.Y -= moveSpeed;
//                }
//                int num24 = Math.Sign(player.Center.X - center1.X);
//                if (num24 != 0)
//                {
//                    if (npc.ai[2] == 0.0 && num24 != npc.direction)
//                        npc.rotation = npc.rotation + 3.141593f;
//                    npc.direction = num24;
//                    if (npc.spriteDirection != -npc.direction)
//                        npc.rotation = npc.rotation + 3.141593f;
//                    npc.spriteDirection = -npc.direction;
//                }
//                ++npc.ai[2];
//                if (npc.ai[2] < (double)num2)
//                    return;
//                int num25 = 0;
//                switch ((int)npc.ai[3])
//                {
//                    case 0:
//                    case 1:
//                    case 2:
//                    case 3:
//                    case 4:
//                    case 5:
//                        num25 = 1;
//                        break;
//                    case 6:
//                        npc.ai[3] = 1f;
//                        num25 = 2;
//                        break;
//                    case 7:
//                        npc.ai[3] = 0.0f;
//                        num25 = 3;
//                        break;
//                }
//                if (flag2)
//                    num25 = 4;
//                if (num25 == 1)
//                {
//                    npc.ai[0] = 6f;
//                    npc.ai[1] = 0.0f;
//                    npc.ai[2] = 0.0f;
//                    npc.velocity = Vector2.Normalize(player.Center - center1) * num5;
//                    npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X);
//                    if (num24 != 0)
//                    {
//                        npc.direction = num24;
//                        if (npc.spriteDirection == 1)
//                            npc.rotation = npc.rotation + 3.141593f;
//                        npc.spriteDirection = -npc.direction;
//                    }
//                }
//                else if (num25 == 2)
//                {
//                    npc.velocity = Vector2.Normalize(player.Center - center1) * num17;
//                    npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X);
//                    if (num24 != 0)
//                    {
//                        npc.direction = num24;
//                        if (npc.spriteDirection == 1)
//                            npc.rotation = npc.rotation + 3.141593f;
//                        npc.spriteDirection = -npc.direction;
//                    }
//                    npc.ai[0] = 7f;
//                    npc.ai[1] = 0.0f;
//                    npc.ai[2] = 0.0f;
//                }
//                else if (num25 == 3)
//                {
//                    npc.ai[0] = 8f;
//                    npc.ai[1] = 0.0f;
//                    npc.ai[2] = 0.0f;
//                }
//                else if (num25 == 4)
//                {
//                    npc.ai[0] = 9f;
//                    npc.ai[1] = 0.0f;
//                    npc.ai[2] = 0.0f;
//                }
//                npc.netUpdate = true;
//            }
//            else if (npc.ai[0] == 6.0)
//            {
//                int num20 = 7;
//                for (int index1 = 0; index1 < num20; ++index1)
//                {
//                    Vector2 vector2_1 = (Vector2.Normalize(npc.velocity) * new Vector2((npc.width + 50) / 2f, npc.height) * 0.75f).RotatedBy((index1 - (num20 / 2 - 1)) * Math.PI / num20) + center1;
//                    Vector2 vector2_2 = ((float)(Main.rand.NextDouble() * 3.14159274101257) - 1.570796f).ToRotationVector2() * Main.rand.Next(3, 8);
//                    Vector2 vector2_3 = vector2_2;
//                    int index2 = Dust.NewDust(vector2_1 + vector2_3, 0, 0, 172, (float)(vector2_2.X * 2.0), (float)(vector2_2.Y * 2.0), 100, default, 1.4f);
//                    Main.dust[index2].noGravity = true;
//                    Main.dust[index2].noLight = true;
//                    Dust dust1 = Main.dust[index2];
//                    Vector2 vector2_4 = dust1.velocity / 4f;
//                    dust1.velocity = vector2_4;
//                    Dust dust2 = Main.dust[index2];
//                    Vector2 vector2_5 = dust2.velocity - npc.velocity;
//                    dust2.velocity = vector2_5;
//                }
//                ++npc.ai[2];
//                if (npc.ai[2] < (double)num4)
//                    return;
//                npc.ai[0] = 5f;
//                npc.ai[1] = 0.0f;
//                npc.ai[2] = 0.0f;
//                npc.ai[3] += 2f;
//                npc.netUpdate = true;
//            }
//            else if (npc.ai[0] == 7.0)
//            {
//                if (npc.ai[2] == 0.0)
//                    //SoundEngine.PlaySound(29, (int)center1.X, (int)center1.Y, 20);
//                if (npc.ai[2] % (double)num15 == 0.0)
//                {
//                    //SoundEngine.PlaySound(4, (int)npc.Center.X, (int)npc.Center.Y, 19);
//                    if (Main.netMode != 1)
//                    {
//                        Vector2 vector2 = Vector2.Normalize(npc.velocity) * (npc.width + 20) / 2f + center1;
//                        int index = NPC.NewNPC(npc.GetSource_FromAI(), (int)vector2.X, (int)vector2.Y + 45, 371);
//                        Main.npc[index].target = npc.target;
//                        Main.npc[index].velocity = Vector2.Normalize(npc.velocity).RotatedBy(1.57079637050629 * npc.direction) * num16;
//                        Main.npc[index].netUpdate = true;
//                        Main.npc[index].ai[3] = Main.rand.Next(80, 121) / 100f;
//                    }
//                }
//                npc.velocity = npc.velocity.RotatedBy(-(double)num18 * npc.direction);
//                npc.rotation = npc.rotation - num18 * npc.direction;
//                ++npc.ai[2];
//                if (npc.ai[2] < (double)num14)
//                    return;
//                npc.ai[0] = 5f;
//                npc.ai[1] = 0.0f;
//                npc.ai[2] = 0.0f;
//                npc.netUpdate = true;
//            }
//            else if (npc.ai[0] == 8.0)
//            {
//                npc.velocity *= 0.98f;
//                npc.velocity.Y = MathHelper.Lerp(npc.velocity.Y, 0.0f, 0.02f);
//                if (npc.ai[2] == (double)(num10 - 30))
//                    //SoundEngine.PlaySound(29, (int)center1.X, (int)center1.Y, 20);
//                if (Main.netMode != 1 && npc.ai[2] == (double)(num10 - 30))
//                    Projectile.NewProjectile(npc.GetSource_FromAI(), center1.X, center1.Y, 0.0f, 0.0f, 385, 0, 0.0f, Main.myPlayer, 1f, npc.target + 1);
//                ++npc.ai[2];
//                if (npc.ai[2] < (double)num10)
//                    return;
//                npc.ai[0] = 5f;
//                npc.ai[1] = 0.0f;
//                npc.ai[2] = 0.0f;
//                npc.netUpdate = true;
//            }
//            else if (npc.ai[0] == 9.0)
//            {
//                if (npc.ai[2] < (double)(num12 - 90))
//                {
//                    npc.alpha = !Collision.SolidCollision(npc.position, npc.width, npc.height) ? npc.alpha - 15 : npc.alpha + 15;
//                    if (npc.alpha < 0)
//                        npc.alpha = 0;
//                    if (npc.alpha > 150)
//                        npc.alpha = 150;
//                }
//                else if (npc.alpha < byte.MaxValue)
//                {
//                    npc.alpha = npc.alpha + 4;
//                    if (npc.alpha > byte.MaxValue)
//                        npc.alpha = byte.MaxValue;
//                }
//                npc.velocity *= 0.98f;
//                npc.velocity.Y = MathHelper.Lerp(npc.velocity.Y, 0.0f, 0.02f);
//                if (npc.ai[2] == (double)(num12 - 60))
//                    //SoundEngine.PlaySound(29, (int)center1.X, (int)center1.Y, 20);
//                ++npc.ai[2];
//                if (npc.ai[2] < (double)num12)
//                    return;
//                npc.ai[0] = 10f;
//                npc.ai[1] = 0.0f;
//                npc.ai[2] = 0.0f;
//                npc.ai[3] = 0.0f;
//                npc.netUpdate = true;
//            }
//            else if (npc.ai[0] == 10.0 && !player.dead)
//            {
//                npc.dontTakeDamage = false;
//                npc.chaseable = false;
//                if (npc.alpha < byte.MaxValue)
//                {
//                    npc.alpha = npc.alpha + 25;
//                    if (npc.alpha > byte.MaxValue)
//                        npc.alpha = byte.MaxValue;
//                }
//                if (npc.ai[1] == 0.0)
//                    npc.ai[1] = 360 * Math.Sign((center1 - player.Center).X);
//                npc.SimpleFlyMovement(Vector2.Normalize(player.Center + new Vector2(npc.ai[1], -200f) - center1 - npc.velocity) * num3, moveSpeed);
//                int num20 = Math.Sign(player.Center.X - center1.X);
//                if (num20 != 0)
//                {
//                    if (npc.ai[2] == 0.0 && num20 != npc.direction)
//                    {
//                        npc.rotation = npc.rotation + 3.141593f;
//                        for (int index = 0; index < npc.oldPos.Length; ++index)
//                            npc.oldPos[index] = Vector2.Zero;
//                    }
//                    npc.direction = num20;
//                    if (npc.spriteDirection != -npc.direction)
//                        npc.rotation = npc.rotation + 3.141593f;
//                    npc.spriteDirection = -npc.direction;
//                }
//                ++npc.ai[2];
//                if (npc.ai[2] < (double)num2)
//                    return;
//                int num23 = 0;
//                switch ((int)npc.ai[3])
//                {
//                    case 0:
//                    case 2:
//                    case 3:
//                    case 5:
//                    case 6:
//                    case 7:
//                        num23 = 1;
//                        break;
//                    case 1:
//                    case 4:
//                    case 8:
//                        num23 = 2;
//                        break;
//                }
//                if (num23 == 1)
//                {
//                    npc.ai[0] = 11f;
//                    npc.ai[1] = 0.0f;
//                    npc.ai[2] = 0.0f;
//                    npc.velocity = Vector2.Normalize(player.Center - center1) * num5;
//                    npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X);
//                    if (num20 != 0)
//                    {
//                        npc.direction = num20;
//                        if (npc.spriteDirection == 1)
//                            npc.rotation = npc.rotation + 3.141593f;
//                        npc.spriteDirection = -npc.direction;
//                    }
//                }
//                else if (num23 == 2)
//                {
//                    npc.ai[0] = 12f;
//                    npc.ai[1] = 0.0f;
//                    npc.ai[2] = 0.0f;
//                }
//                else if (num23 == 3)
//                {
//                    npc.ai[0] = 13f;
//                    npc.ai[1] = 0.0f;
//                    npc.ai[2] = 0.0f;
//                }
//                npc.netUpdate = true;
//            }
//            else if (npc.ai[0] == 11.0)
//            {
//                npc.dontTakeDamage = false;
//                npc.chaseable = true;
//                npc.alpha = npc.alpha - 25;
//                if (npc.alpha < 0)
//                    npc.alpha = 0;
//                int num20 = 7;
//                for (int index1 = 0; index1 < num20; ++index1)
//                {
//                    Vector2 vector2_1 = (Vector2.Normalize(npc.velocity) * new Vector2((npc.width + 50) / 2f, npc.height) * 0.75f).RotatedBy((index1 - (num20 / 2 - 1)) * Math.PI / num20) + center1;
//                    Vector2 vector2_2 = ((float)(Main.rand.NextDouble() * 3.14159274101257) - 1.570796f).ToRotationVector2() * Main.rand.Next(3, 8);
//                    Vector2 vector2_3 = vector2_2;
//                    int index2 = Dust.NewDust(vector2_1 + vector2_3, 0, 0, 172, (float)(vector2_2.X * 2.0), (float)(vector2_2.Y * 2.0), 100, default, 1.4f);
//                    Main.dust[index2].noGravity = true;
//                    Main.dust[index2].noLight = true;
//                    Dust dust1 = Main.dust[index2];
//                    Vector2 vector2_4 = dust1.velocity / 4f;
//                    dust1.velocity = vector2_4;
//                    Dust dust2 = Main.dust[index2];
//                    Vector2 vector2_5 = dust2.velocity - npc.velocity;
//                    dust2.velocity = vector2_5;
//                }
//                ++npc.ai[2];
//                if (npc.ai[2] < (double)num4)
//                    return;
//                npc.ai[0] = 10f;
//                npc.ai[1] = 0.0f;
//                npc.ai[2] = 0.0f;
//                ++npc.ai[3];
//                npc.netUpdate = true;
//            }
//            else if (npc.ai[0] == 12.0)
//            {
//                npc.dontTakeDamage = true;
//                npc.chaseable = false;
//                if (npc.alpha < byte.MaxValue)
//                {
//                    npc.alpha = npc.alpha + 17;
//                    if (npc.alpha > byte.MaxValue)
//                        npc.alpha = byte.MaxValue;
//                }
//                npc.velocity *= 0.98f;
//                npc.velocity.Y = MathHelper.Lerp(npc.velocity.Y, 0.0f, 0.02f);
//                if (npc.ai[2] == (double)(num13 / 2))
//                    //SoundEngine.PlaySound(29, (int)center1.X, (int)center1.Y, 20);
//                if (Main.netMode != 1 && npc.ai[2] == (double)(num13 / 2))
//                {
//                    if (npc.ai[1] == 0.0)
//                        npc.ai[1] = 300 * Math.Sign((center1 - player.Center).X);
//                    Vector2 vector2 = npc.Center = player.Center + new Vector2(-npc.ai[1], -200f);
//                    int num20 = Math.Sign(player.Center.X - vector2.X);
//                    if (num20 != 0)
//                    {
//                        if (npc.ai[2] == 0.0 && num20 != npc.direction)
//                        {
//                            npc.rotation = npc.rotation + 3.141593f;
//                            for (int index = 0; index < npc.oldPos.Length; ++index)
//                                npc.oldPos[index] = Vector2.Zero;
//                        }
//                        npc.direction = num20;
//                        if (npc.spriteDirection != -npc.direction)
//                            npc.rotation = npc.rotation + 3.141593f;
//                        npc.spriteDirection = -npc.direction;
//                    }
//                }
//                ++npc.ai[2];
//                if (npc.ai[2] < (double)num13)
//                    return;
//                npc.ai[0] = 10f;
//                npc.ai[1] = 0.0f;
//                npc.ai[2] = 0.0f;
//                ++npc.ai[3];
//                if (npc.ai[3] >= 9.0)
//                    npc.ai[3] = 0.0f;
//                npc.netUpdate = true;
//            }
//            else
//            {
//                if (npc.ai[0] != 13.0)
//                    return;
//                if (npc.ai[2] == 0.0)
//                    //SoundEngine.PlaySound(29, (int)center1.X, (int)center1.Y, 20);
//                npc.velocity = npc.velocity.RotatedBy(-(double)num18 * npc.direction);
//                npc.rotation = npc.rotation - num18 * npc.direction;
//                ++npc.ai[2];
//                if (npc.ai[2] < (double)num14)
//                    return;
//                npc.ai[0] = 10f;
//                npc.ai[1] = 0.0f;
//                npc.ai[2] = 0.0f;
//                ++npc.ai[3];
//                npc.netUpdate = true;
//            }
//        }
//    }
//}
