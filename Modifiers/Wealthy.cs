using Terraria;

namespace FargoEnemyModifiers.Modifiers
{
    public class Wealthy : Modifier
    {
        public override string Name => "Wealthy";

        public override bool PreNPCLoot(NPC npc)
        {
            npc.value *= 5;
            return true;
        }
    }
}