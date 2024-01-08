using Terraria;

namespace FargoEnemyModifiers.Modifiers
{
    public class HexProof : Modifier
    {
        public override string Name => "Hex-proof";
        public override string Description => "Damage over time does not work on them";
        public override int Rarity => 2;

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (npc.lifeRegen >= 0)
                return;

            npc.lifeRegen = 0;
            damage = 0;
        }
    }
}