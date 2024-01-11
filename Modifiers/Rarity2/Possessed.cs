using Terraria;
using Terraria.ID;

namespace FargoEnemyModifiers.Modifiers
{
    public class Possessed : Modifier
    {
        public override string Name => "Possessed";
        public override string Description => "Replaces its normal AI with Ghost AI";
        public override int Rarity => 2;

        private int counter;

        public override bool PreAI(NPC npc)
        {
            npc.alpha = 100;
            npc.aiStyle = NPCAIStyleID.HoveringFighter;
            npc.noTileCollide = true;
            npc.noGravity = true;

            // PossessedAI(npc);
            return true;
        }
    }
}