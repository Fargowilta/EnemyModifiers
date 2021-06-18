using Terraria;

namespace FargoEnemyModifiers.Modifiers
{
    public class HexProof : Modifier
    {
        public override string Name => "Hex-proof";

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (npc.lifeRegen >= 0)
                return;

            npc.lifeRegen = 0;
            damage = 0;
        }
    }
}