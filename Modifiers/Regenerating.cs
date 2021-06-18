using Terraria;

namespace FargoEnemyModifiers.Modifiers
{
    public class Regenerating : Modifier
    {
        public Regenerating()
        {
            name = "Regenerating";
        }

        private int counter;
        public override void AI(NPC npc)
        {
            if (npc.life < npc.lifeMax)
            {
                counter++;

                if (counter >= 300)
                {
                    int amtHealed = npc.lifeMax / 10;
                    npc.life += amtHealed;
                    npc.HealEffect(amtHealed);
                    counter = 0;
                }
            }
        }
    }
}
