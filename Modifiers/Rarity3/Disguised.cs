using FargoEnemyModifiers.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;

namespace FargoEnemyModifiers.Modifiers.Rarity3
{
    public class Disguised : Modifier
    {
        public override ModifierID ModifierID => ModifierID.Disguised;
        public override string Key => "Disguised";
        public override RarityID Rarity => RarityID.Rare;

        public override AiOverrideStyle AiOverride => AiOverrideStyle.PreVanilla;

        private bool firstTick = true;

        public override void AI(NPC npc)
        {
            if (firstTick)
            {
                firstTick= false;

                npc.width = 24;
                npc.height = 18;

                if (npc.boss)
                {
                    npc.width*= 2;
                    npc.height*= 2;
                }
            }
        }

        public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D slimeSprite = (Texture2D)TextureAssets.Npc[1];

            Color color = Color.MediumBlue;
            color.A = 175;

            float scale = 1f;

            if (npc.boss)
            {
                scale = 2f;
            }

            spriteBatch.Draw(slimeSprite, new Vector2(npc.Center.X - npc.width / 2 - screenPos.X, npc.Center.Y - (npc.height / 2) - screenPos.Y), new Rectangle(0, 0, slimeSprite.Width, slimeSprite.Height / 2), color, npc.rotation, new Vector2(0,0), scale, SpriteEffects.None, 0f);

            return false;
        }
    }
}
