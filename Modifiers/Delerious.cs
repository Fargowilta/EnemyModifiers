using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargoEnemyModifiers.Modifiers
{
    public class Delerious : Modifier
    {
        public Delerious()
        {
            name = "Delerious";
        }

        private int counter = 0;

        private int aiStyle;
        public override bool PreAI(NPC npc)
        {
            if (++counter > Main.rand.Next(600))
            {
                aiStyle = npc.aiStyle;

                //int numAiStyles = 111; // update in 1.4
                //npc.aiStyle = Main.rand.Next(1, numAiStyles + 1);

                //Main.NewText("aistyle: " + aiStyle);
                //npc.aiStyle = aiStyle++;

                counter = -Main.rand.Next(600);

                switch (Main.rand.Next(18))
				{
					case 0: npc.aiStyle = 15; npc.noTileCollide = false; npc.noGravity = false; break; //king slime
					case 1: npc.aiStyle = 43; npc.noTileCollide = true; npc.noGravity = true; break; //queen bee
					case 2: npc.aiStyle = 96; npc.noTileCollide = false; npc.noGravity = true; break; //flow invader
					case 3: npc.aiStyle = 6; npc.noTileCollide = true; npc.noGravity = true; break; //worm
					case 4: npc.aiStyle = 4; npc.noTileCollide = true; npc.noGravity = true; break; //eye of cthulhu
					case 5: npc.aiStyle = 11; npc.noTileCollide = true; npc.noGravity = true; break; //skeletron
					case 6: npc.aiStyle = 30; npc.noTileCollide = true; npc.noGravity = true; break; //retinazer
					case 7: npc.aiStyle = 31; npc.noTileCollide = true; npc.noGravity = true; break; //spazmatism
					case 8: npc.aiStyle = 32; npc.noTileCollide = true; npc.noGravity = true; break; //prime
                    case 9: npc.aiStyle = 3; npc.noTileCollide = false; npc.noGravity = false; break; //fighter ai
                    case 10: npc.aiStyle = 54; npc.noTileCollide = true; npc.noGravity = true; break; //boc
                    case 11: npc.aiStyle = 60; npc.noTileCollide = true; npc.noGravity = true; break; //ice queen
                    case 12: npc.aiStyle = 61; npc.noTileCollide = true; npc.noGravity = false; break; //santank
                    case 13: npc.aiStyle = 69; npc.noTileCollide = true; npc.noGravity = true; break; //fishron, nice
                    case 14: npc.aiStyle = 97; npc.noTileCollide = true; npc.noGravity = true; break; //nebula floater
                    case 15: npc.aiStyle = 86; npc.noTileCollide = true; npc.noGravity = true; break; //ancient vision
                    case 16: npc.aiStyle = 87; npc.noTileCollide = false; npc.noGravity = false; break; //mimic
                    case 17: npc.aiStyle = 110; npc.noTileCollide = true; npc.noGravity = true; break; //betsy
				}

                if (npc.HasValidTarget)
                    npc.Center = Main.player[npc.target].Center + Main.rand.NextFloat(2f) * npc.Distance(Main.player[npc.target].Center) * Vector2.UnitX.RotatedByRandom(MathHelper.TwoPi);

                npc.knockBackResist = 0f;
                npc.dontTakeDamage = false;
			    npc.localAI[0] = 0f;
			    npc.localAI[1] = 0f;
			    npc.localAI[2] = 0f;
			    npc.localAI[3] = 0f;
			    npc.ai[0] = 0f;
			    npc.ai[1] = 0f;
			    npc.ai[2] = 0f;
			    npc.ai[3] = 0f;

                if (Main.netMode != 1)
                {
                    float speed = Main.rand.NextFloat(6f, 24f);
                    float baseRotation = Main.rand.NextFloat(MathHelper.TwoPi);
                    int type = Main.rand.Next(new int[] {
                        ProjectileID.DeathLaser,
                        ProjectileID.CursedFlameHostile,
                        ProjectileID.PhantasmalBolt,
                        ProjectileID.DD2DarkMageBolt,
                        ProjectileID.EyeLaser,
                        ProjectileID.PinkLaser,
                        ProjectileID.FrostBlastHostile,
                        ProjectileID.RuneBlast,
                        ProjectileID.BulletDeadeye,
                        ProjectileID.PoisonDartTrap,
                        ProjectileID.FrostBeam,
                        ProjectileID.PoisonSeedPlantera,
                        ProjectileID.ShadowBeamHostile,
                        ProjectileID.SniperBullet,
                        ProjectileID.RocketSkeleton,
                        ProjectileID.FlamingWood,
                        ProjectileID.FrostWave,
                        ProjectileID.Missile,
                        ProjectileID.BrainScramblerBolt,
                        ProjectileID.RayGunnerLaser,
                        ProjectileID.CultistBossLightningOrb,
                        ProjectileID.NebulaBolt,
                        ProjectileID.AncientDoomProjectile,
                        ProjectileID.ThornBall
                    });
                    int max = Main.rand.Next(64);
                    for (int i = 0; i < max; i++)
                    {
                        Projectile.NewProjectile(npc.Center, baseRotation.ToRotationVector2().RotatedBy(Math.PI * 2 / max * i) * speed, type, npc.damage / 4, 0, Main.myPlayer);
                    }
                }
            }

            /*if (counter < 0)
                return false;

            if (counter == 0)
            {
                npc.aiStyle = aiStyle;

                npc.dontTakeDamage = false;
                npc.knockBackResist = 0f;
                npc.localAI[0] = 0f;
			    npc.localAI[1] = 0f;
			    npc.localAI[2] = 0f;
			    npc.localAI[3] = 0f;
			    npc.ai[0] = 0f;
			    npc.ai[1] = 0f;
			    npc.ai[2] = 0f;
			    npc.ai[3] = 0f;
            }*/

            return base.PreAI(npc);
        }
    }
}
