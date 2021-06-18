using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace FargoEnemyModifiers.Modifiers
{
    public class Chained : Modifier
    {
        public Chained()
        {
            name = "Chained";
        }

        const int maxDistance = 500;
        private bool firstTick = true;
        private int target;
        private bool tileCollide;

        public override bool PreAI(NPC npc)
        {
            if (firstTick)
            {
                target = npc.FindClosestPlayer();
                tileCollide = npc.noTileCollide;

                firstTick = false;
            }

            Player player = Main.player[target];
            int distance = (int)Vector2.Distance(npc.Center, player.Center);

            if (distance > maxDistance)
            {
                //npc.velocity = player.velocity;
                Vector2 velocity = Vector2.Normalize(player.Center - npc.Center) * (2 + player.velocity.Length());
                npc.velocity = velocity;

                npc.noTileCollide = true;
            }
            else
            {
                npc.noTileCollide = tileCollide;
            }

            return true;
        }

        // chain voodoo
        public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture =  ModContent.GetTexture("Terraria/Chain40");

            Vector2 position = npc.Center;
            Vector2 mountedCenter = Main.player[target].MountedCenter;
            Rectangle? sourceRectangle = new Rectangle?();
            Vector2 origin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);
            float num1 = texture.Height;
            Vector2 vector24 = mountedCenter - position;
            float rotation = (float)Math.Atan2(vector24.Y, vector24.X) - 1.57f;
            bool flag = true;
            if (float.IsNaN(position.X) && float.IsNaN(position.Y))
                flag = false;
            if (float.IsNaN(vector24.X) && float.IsNaN(vector24.Y))
                flag = false;
            while (flag)
                if (vector24.Length() < num1 + 1.0)
                {
                    flag = false;
                }
                else
                {
                    Vector2 vector21 = vector24;
                    vector21.Normalize();
                    position += vector21 * num1;
                    vector24 = mountedCenter - position;
                    Color color2 = Lighting.GetColor((int)position.X / 16, (int)(position.Y / 16.0));
                    color2 = npc.GetAlpha(color2);
                    Main.spriteBatch.Draw(texture, position - Main.screenPosition, sourceRectangle, color2, rotation, origin, 1f, SpriteEffects.None, 0.0f);
                }

            return true;
        }
    }
}
