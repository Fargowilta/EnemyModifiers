using FargoEnemyModifiers.Utilities;
using Terraria;
using Terraria.ID;

namespace FargoEnemyModifiers.Modifiers
{
    public class Possessed : Modifier
    {
        public override string Key => "Possessed";
        public override RarityID Rarity => RarityID.Uncommon;

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