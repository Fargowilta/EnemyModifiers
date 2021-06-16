using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace FargoEnemyModifiers.Modifiers
{
    public class Delerious : Modifier
    {
        public Delerious()
        {
            name = "Delerious";
        }

        private int counter = 0;

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
