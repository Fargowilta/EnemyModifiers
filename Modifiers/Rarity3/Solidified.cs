using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargoEnemyModifiers.Modifiers
{
    public class Solidified : Modifier
    {
        public override string Name => "Solidified";
        public override string Description => "Can no longer pass through tiles";
        public override int Rarity => 3;

        private bool firstTick = true;

        public override void AI(NPC npc)
        {
            if (firstTick)
            {
                npc.noTileCollide = false;

                firstTick = false;
            }
        }

        public override bool ExtraCondition(NPC npc)
        {
            return npc.noTileCollide;
        }
    }
}