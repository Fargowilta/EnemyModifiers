using Terraria;

namespace FargoEnemyModifiers.Modifiers
{
    public class Hexproof : Modifier
    {
        public Hexproof()
        {
            name = "Hexproof";
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (npc.lifeRegen < 0)
            {
                npc.lifeRegen = 0;
                damage = 0;
            }
        }
    }
}
