using FargoEnemyModifiers.Utilities;
using Terraria;

namespace FargoEnemyModifiers.Modifiers
{
    public class Vampiric : Modifier
    {
        public override ModifierID ModifierID => ModifierID.Vampiric;
        public override string Key => "Vampiric";
        public override RarityID Rarity => RarityID.Uncommon;

        public override void OnHitPlayer(NPC npc, Player target)
        {
            int amtHealed = npc.damage / 2;
            npc.life += amtHealed;
            npc.HealEffect(amtHealed);
        }
    }
}