using FargoEnemyModifiers.Utilities;
using Terraria;

namespace FargoEnemyModifiers.Modifiers
{
    public class Light : Modifier
    {
        public override ModifierID ModifierID => ModifierID.Light;
        public override string Key => "Light";
        public override RarityID Rarity => RarityID.Common;
        public override AiOverrideStyle AiOverride => AiOverrideStyle.PreVanilla;

        public override float KnockBackMultiplier => 1.4f;

        public override void AI(NPC npc)
        {
            npc.GravityMultiplier *= 0.5f;
        }
    }
}