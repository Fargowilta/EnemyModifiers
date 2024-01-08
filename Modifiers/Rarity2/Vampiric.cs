using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargoEnemyModifiers.Modifiers
{
    public class Vampiric : Modifier
    {
        public override string Name => "Vampiric";
        public override string Description => "Heals some HP when they deal damage";
        public override int Rarity => 2;

        public override void OnHitPlayer(NPC npc, Player target)
        {
            int amtHealed = npc.damage / 2;
            npc.life += amtHealed;
            npc.HealEffect(amtHealed);
        }
    }
}