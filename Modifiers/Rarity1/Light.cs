using FargoEnemyModifiers.NetCode;
using Terraria;

namespace FargoEnemyModifiers.Modifiers
{
    public class Light : Modifier
    {
        public override string Name => "Light";
        public override string Description => "Takes increased knockback and has redued gravity";
        public override int Rarity => 1;
        public override AiOverrideStyle AiOverride => AiOverrideStyle.PreVanilla;

        public override float KnockBackMultiplier => 1.4f;

        public override void AI(NPC npc)
        {
            npc.GravityMultiplier *= 0.5f;
        }
    }
}