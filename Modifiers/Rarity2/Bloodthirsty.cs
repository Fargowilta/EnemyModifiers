using Terraria;

namespace FargoEnemyModifiers.Modifiers
{
    public class Bloodthirsty : Modifier
    {
        public override string Name => "Bloodthirsty";
        public override string Description => "Gains 25% damage every time they hit you";
        public override int Rarity => 2;

        public override void OnHitPlayer(NPC npc, Player target)
        {
            npc.damage = (int) (npc.damage * 1.25f);
        }
    }
}