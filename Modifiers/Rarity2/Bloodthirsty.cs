using FargoEnemyModifiers.Utilities;
using Terraria;

namespace FargoEnemyModifiers.Modifiers
{
    public class Bloodthirsty : Modifier
    {
        public override ModifierID ModifierID => ModifierID.Bloodthirsty;
        public override string Key => "Bloodthirsty";
        public override RarityID Rarity => RarityID.Uncommon;

        public override void OnHitPlayer(NPC npc, Player target)
        {
            npc.damage = (int) (npc.damage * 1.25f);
        }
    }
}