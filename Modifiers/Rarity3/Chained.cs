using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace FargoEnemyModifiers.Modifiers
{
    public class Chained : Modifier
    {
        public override string Name => "Chained";
        public override string Description => "They spawn chained to you and cannot leave a radius around you";
        public override int Rarity => 3;

        public virtual int MaxDistance => 500;

        public virtual Asset<Texture2D> ChainTexture => ModContent.Request<Texture2D>("Terraria/Images/Chain40");

        protected bool firstTick = true;
        protected int target;
        protected bool tileCollide;

        public override bool PreAI(NPC npc)
        {
            if (firstTick)
            {
                target = npc.FindClosestPlayer();
                tileCollide = npc.noTileCollide;

                firstTick = false;
            }

            target = npc.HasValidTarget ? npc.target : npc.FindClosestPlayer();

            //if (target != -1 && Main.player[target].active && !Main.player[target].dead && !Main.player[target].ghost && npc.Distance(Main.player[target].Center) < MaxDistance)
            //{
            //    const float lerp = 0.00025f;
            //    Vector2 playerVelocity = Vector2.Lerp(Main.player[target].Center, npc.Center, lerp) - Main.player[target].Center;
            //    playerVelocity.Y *= 2f; //compensation, x drag is generally a lot stronger than y drag

            //    //i.e. don't y velocity when it's weak, this avoids an interaction where player can't jump despite being grounded
            //    if (Main.player[target].velocity.Y == 0 && Math.Abs(playerVelocity.Y) < Math.Abs(Main.player[target].gravity))
            //        playerVelocity.Y = 0;

            //    Main.player[target].velocity += playerVelocity;

            //    Vector2 npcVelocity = Vector2.Lerp(npc.Center, Main.player[target].Center, lerp) - npc.Center;
            //    if (!npc.noGravity)
            //        npcVelocity.Y *= 2f;
            //    npc.velocity += npcVelocity;
            //}

            if (target < 0)
            {
                return true;
            }

            Player player = Main.player[target];
            int distance = (int) Vector2.Distance(npc.Center, player.Center);
            if (distance > MaxDistance)
            {
                //npc.velocity = player.velocity;
                Vector2 velocity = Vector2.Normalize(player.Center - npc.Center) * (2 + player.velocity.Length());
                npc.velocity = velocity;
                npc.noTileCollide = true;
            }
            else
                npc.noTileCollide = tileCollide;

            return true;
        }

        // chain voodoo
        public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Color drawColor)
        {
            Vector2 position = npc.Center;
            Vector2 mountedCenter = Main.player[target].MountedCenter;
            Vector2 origin = new Vector2(ChainTexture.Width() * 0.5f, ChainTexture.Height() * 0.5f);
            float textureHeight = ChainTexture.Height();
            Vector2 mountedPosition = mountedCenter - position;
            float rotation = (float) Math.Atan2(mountedPosition.Y, mountedPosition.X) - 1.57f;
            bool invalidPosition = !(float.IsNaN(position.X) && float.IsNaN(position.Y) ||
                                     float.IsNaN(mountedPosition.X) && float.IsNaN(mountedPosition.Y));

            while (invalidPosition)
                if (mountedPosition.Length() < textureHeight + 1.0)
                    invalidPosition = false;
                else
                {
                    Vector2 mountedClone = mountedPosition;
                    mountedClone.Normalize();
                    position += mountedClone * textureHeight;
                    mountedPosition = mountedCenter - position;
                    Color color2 = Lighting.GetColor((int) position.X / 16, (int) (position.Y / 16.0));
                    color2 = npc.GetAlpha(color2);

                    Main.spriteBatch.Draw(((Texture2D)ChainTexture), position - Main.screenPosition, null, color2, rotation, origin,
                        1f, SpriteEffects.None, 0.0f);
                }

            return true;
        }
    }
}