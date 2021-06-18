using Terraria;

namespace FargoEnemyModifiers.Modifiers
{
    public class Regenerating : Modifier
    {
        public override string Name => "Regenerating";

        private int counter;

        public override void AI(NPC npc)
        {
            if (npc.life >= npc.lifeMax || ++counter < 300)
                return;

            int amountHealed = npc.lifeMax / 10;
            npc.life += amountHealed;
            npc.HealEffect(amountHealed);
            counter = 0;
        }
    }
}