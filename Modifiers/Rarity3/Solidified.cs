using FargoEnemyModifiers.NetCode;
using Terraria;

namespace FargoEnemyModifiers.Modifiers
{
    public class Solidified : Modifier
    {
        public override string Name => "Solidified";
        public override string Description => "Can no longer pass through tiles";
        public override int Rarity => 3;
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