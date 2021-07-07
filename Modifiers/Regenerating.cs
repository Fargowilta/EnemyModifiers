using Terraria;

namespace FargoEnemyModifiers.Modifiers
{
    public class Regenerating : Modifier
    {
        public override string Name => "Regenerating";

        private int counter;

        public override void AI(NPC npc)
        {
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
                    if (npc.life > npc.lifeMax)
                        npc.life = npc.lifeMax;
                    counter = 0;
                }
            }
        }
    }
}