using Terraria;

namespace FargoEnemyModifiers.Modifiers
{
    public class Delirious : Modifier
    {
        public override string Name => "Delirious";

        protected int counter;

        public override bool PreAI(NPC npc)
        {
            if (++counter <= 300)
                return base.PreAI(npc);

            const int numAiStyles = 111; // update in 1.4

            npc.aiStyle = Main.rand.Next(1, numAiStyles + 1);

            counter = 0;
            return base.PreAI(npc);
        }
    }
}