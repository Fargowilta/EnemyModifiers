using FargoEnemyModifiers.Utilities;
using Terraria;
using Terraria.ID;

namespace FargoEnemyModifiers.Modifiers
{
    public class Regenerating : Modifier
    {
        public override ModifierID ModifierID => ModifierID.Regenerating;
        public override string Key => "Regenerating";
        public override RarityID Rarity => RarityID.Common;
        public override AiOverrideStyle AiOverride => AiOverrideStyle.PreVanilla;

        private int counter;

        public override void AI(NPC npc)
        {
            if (npc.life < npc.lifeMax && npc.realLife == -1)
            {
                counter++;

                if (counter >= 300)
                {
                    RegeneratingHeal(npc);
                    counter = 0;
                }
            }
        }

        public static void RegeneratingHeal(NPC npc)
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
        }
    }
}