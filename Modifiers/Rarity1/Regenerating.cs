using FargoEnemyModifiers.NetCode;
using Terraria;
using Terraria.ID;

namespace FargoEnemyModifiers.Modifiers
{
    public class Regenerating : Modifier
    {
        public override string Name => "Regenerating";
        public override string Description => "Regenerates 2% HP every second";
        public override int Rarity => 1;
        public override AiOverrideStyle AiOverride => AiOverrideStyle.PreVanilla;

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
                        amtHealed /= 4;
                    npc.life += amtHealed;
                    if (Main.netMode != NetmodeID.Server)
                    {
                        npc.HealEffect(amtHealed);
                    }
                    if (npc.life > npc.lifeMax)
                        npc.life = npc.lifeMax;
                    counter = 0;
                }
            }
        }
    }
}