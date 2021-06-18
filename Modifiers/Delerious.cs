using Terraria;

namespace FargoEnemyModifiers.Modifiers
{
    public class Delerious : Modifier
    {
        public Delerious()
        {
            name = "Delerious";
        }

        private int counter;

        private int aiStyle = 1;
        public override bool PreAI(NPC npc)
        {
            if (++counter > 300)
            {
                int numAiStyles = 111; // update in 1.4


                npc.aiStyle = Main.rand.Next(1, numAiStyles + 1);

                //Main.NewText("aistyle: " + aiStyle);
                //npc.aiStyle = aiStyle++;

                counter = 0;
            }
            

            return base.PreAI(npc);
        }
    }
}
