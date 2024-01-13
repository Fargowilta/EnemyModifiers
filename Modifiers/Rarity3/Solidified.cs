using FargoEnemyModifiers.Utilities;
using Terraria;

namespace FargoEnemyModifiers.Modifiers
{
    public class Solidified : Modifier
    {
        public override string Key => "Solidified";
        public override RarityID Rarity => RarityID.Rare;
        public override AiOverrideStyle AiOverride => AiOverrideStyle.PreVanilla;

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