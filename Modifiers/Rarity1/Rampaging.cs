using FargoEnemyModifiers.Utilities;
using Microsoft.Xna.Framework;
using Terraria;

namespace FargoEnemyModifiers.Modifiers
{
    public class Rampaging : Modifier
    {
        public override ModifierID ModifierID => ModifierID.Rampaging;
        public override string Key => "Rampaging";
        public override RarityID Rarity => RarityID.Common;

        public override void OnHitPlayer(NPC npc, Player target)
        {
            if (target.noKnockback) return;
            Vector2 velocity = Vector2.Normalize(target.Center - npc.Center) * 20;
            target.velocity = velocity;
        }
    }
}