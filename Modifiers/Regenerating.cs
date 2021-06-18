using Terraria;

namespace FargoEnemyModifiers.Modifiers
{
    public class Regenerating : Modifier
    {
        public override string Name => "Regenerating";

        private int counter;

        public override void AI(NPC npc)
        {
<<<<<<< HEAD
            if (npc.life >= npc.lifeMax || ++counter < 300)
                return;

            int amountHealed = npc.lifeMax / 10;
            npc.life += amountHealed;
            npc.HealEffect(amountHealed);
            counter = 0;
=======
            if (npc.life < npc.lifeMax && npc.realLife == -1)
            {
                counter++;

                if (counter >= 300)
                {
                    int amtHealed = npc.lifeMax / 10;
                    if (npc.boss)
                        amtHealed /= 2;
                    npc.life += amtHealed;
                    npc.HealEffect(amtHealed);
                    counter = 0;
                }
            }
>>>>>>> 9e94e3e16eca90b12f41c32c4a5c9fe4b429a9fd
        }
    }
}