using FargoEnemyModifiers.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;

namespace FargoEnemyModifiers.Modifiers.Rarity3
{
    public class Sticky : Modifier
    {
        public override ModifierID ModifierID => ModifierID.Sticky;
        public override string Key => "Sticky";
        public override RarityID Rarity => RarityID.Rare;
        public override AiOverrideStyle AiOverride => AiOverrideStyle.PostVanilla;

        private int counter = 0;

        public override void AI(NPC npc)
        {
            if (counter++ > 15)
            {
                int xPos = (int)npc.Center.X / 16;
                int yPos = (int)npc.Center.Y / 16;

                Tile tile = Main.tile[xPos, yPos];

                if (tile != null && !tile.HasTile)
                {
                    WorldGen.PlaceTile(xPos, yPos, TileID.Cobweb);
                    if (Main.netMode == NetmodeID.Server)
                        NetMessage.SendTileSquare(-1, xPos, yPos, 1);

                    counter = 0;
                }

            }
        }
    }
}
