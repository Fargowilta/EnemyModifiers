using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargoEnemyModifiers.Modifiers
{
    public class Vaccinated : Modifier
    {
        public override string Name => "Vaccinated";
        public override string Description => "Immune to all debuffs";
        public override int Rarity => 2;

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