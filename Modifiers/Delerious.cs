using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
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

        private int aiStyle = 1;
        public override bool PreAI(NPC npc)
        {
            if (++counter > Main.rand.Next(600))
            {
                int numAiStyles = 111; // update in 1.4


                npc.aiStyle = Main.rand.Next(1, numAiStyles + 1);

                //Main.NewText("aistyle: " + aiStyle);
                //npc.aiStyle = aiStyle++;

                counter = 0;

                if (Main.netMode != 1)
                {
                    float speed = Main.rand.NextFloat(3f, 18f);
                    float baseRotation = Main.rand.NextFloat(MathHelper.TwoPi);
                    int type = Main.rand.Next(new int[] {
                        ProjectileID.DeathLaser,
                        ProjectileID.CursedFlameHostile,
                        ProjectileID.PhantasmalBolt,
                        ProjectileID.DD2DarkMageBolt,
                        ProjectileID.DD2BetsyFireball,
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
                        ProjectileID.JavelinHostile,
                        ProjectileID.NebulaBolt,
                        ProjectileID.AncientDoomProjectile
                    });
                    for (int i = 0; i < 16; i++)
                    {
                        Projectile.NewProjectile(npc.Center, baseRotation.ToRotationVector2().RotatedBy(Math.PI / 8 * i) * speed, type, npc.damage / 4, 0, Main.myPlayer);
                    }
            }
            

            return base.PreAI(npc);
        }
    }
}
