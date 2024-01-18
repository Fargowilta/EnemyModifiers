using FargoEnemyModifiers.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace FargoEnemyModifiers.Modifiers
{
    public class Vaccinated : Modifier
    {
        public override ModifierID ModifierID => ModifierID.Vaccinated;
        public override string Key => "Vaccinated";
        public override RarityID Rarity => RarityID.Uncommon;
        public override AiOverrideStyle AiOverride => AiOverrideStyle.PreVanilla;

        private bool firstTick = true;

        public override void AI(NPC npc)
        {
            if (firstTick)
            {
                for (int i = 0; i < BuffLoader.BuffCount; i++)
                {
                    npc.buffImmune[i] = true;
                }
            
                firstTick = false;
            }
        }
    }
}