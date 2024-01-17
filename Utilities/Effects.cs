using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace FargoEnemyModifiers.Utilities
{
    public static class Effects
    {
        public static void PuffOfSmoke(Entity entity)
        {
            if (Main.netMode == NetmodeID.Server) return;

            for (int i = 0; i < 100; ++i)
            {
                int dustId = Dust.NewDust(entity.position, entity.width, entity.height, DustID.Smoke, 0.0f, 0.0f, 100, new Color(), 2f);
                Main.dust[dustId].position.X += Main.rand.Next(-20, 21);
                Main.dust[dustId].position.Y += Main.rand.Next(-20, 21);
                Dust dust = Main.dust[dustId];
                dust.velocity *= 0.4f;
                Main.dust[dustId].scale *= 1f + Main.rand.Next(40) * 0.01f;
                if (Main.rand.NextBool())
                {
                    Main.dust[dustId].scale *= 1f + Main.rand.Next(40) * 0.01f;
                    Main.dust[dustId].noGravity = true;
                }
            }

            int index3 = Gore.NewGore(entity.GetSource_FromThis(), new Vector2(entity.Center.X - 24, entity.Center.Y - 24), new Vector2(), Main.rand.Next(61, 64));
            Main.gore[index3].scale = 1.5f;
            Main.gore[index3].velocity.X = Main.rand.Next(-50, 51) * 0.01f;
            Main.gore[index3].velocity.Y = Main.rand.Next(-50, 51) * 0.01f;
            Main.gore[index3].velocity *= 0.4f;

            int index4 = Gore.NewGore(entity.GetSource_FromThis(), new Vector2(entity.Center.X - 24, entity.Center.Y - 24), new Vector2(), Main.rand.Next(61, 64));
            Main.gore[index4].scale = 1.5f;
            Main.gore[index4].velocity.X = 1.5f + Main.rand.Next(-50, 51) * 0.01f;
            Main.gore[index4].velocity.Y = 1.5f + Main.rand.Next(-50, 51) * 0.01f;
            Main.gore[index4].velocity *= 0.4f;

            int index5 = Gore.NewGore(entity.GetSource_FromThis(), new Vector2(entity.Center.X - 24, entity.Center.Y - 24), new Vector2(), Main.rand.Next(61, 64));
            Main.gore[index5].scale = 1.5f;
            Main.gore[index5].velocity.X = -1.5f - Main.rand.Next(-50, 51) * 0.01f;
            Main.gore[index5].velocity.Y = 1.5f + Main.rand.Next(-50, 51) * 0.01f;
            Main.gore[index5].velocity *= 0.4f;

            int index6 = Gore.NewGore(entity.GetSource_FromThis(), new Vector2(entity.Center.X - 24, entity.Center.Y - 24), new Vector2(), Main.rand.Next(61, 64));
            Main.gore[index6].scale = 1.5f;
            Main.gore[index6].velocity.X = 1.5f - Main.rand.Next(-50, 51) * 0.01f;
            Main.gore[index6].velocity.Y = -1.5f + Main.rand.Next(-50, 51) * 0.01f;
            Main.gore[index6].velocity *= 0.4f;

            int index7 = Gore.NewGore(entity.GetSource_FromThis(), new Vector2(entity.Center.X - 24, entity.Center.Y - 24), new Vector2(), Main.rand.Next(61, 64));
            Main.gore[index7].scale = 1.5f;
            Main.gore[index7].velocity.X = -1.5f - Main.rand.Next(-50, 51) * 0.01f;
            Main.gore[index7].velocity.Y = -1.5f + Main.rand.Next(-50, 51) * 0.01f;
            Main.gore[index7].velocity *= 0.4f;
        }
    }
}