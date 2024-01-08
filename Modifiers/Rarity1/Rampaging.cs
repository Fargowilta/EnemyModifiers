using Microsoft.Xna.Framework;
using Terraria;

namespace FargoEnemyModifiers.Modifiers
{
    public class Rampaging : Modifier
    {
        public override string Name => "Rampaging";
        public override string Description => "Deals extremely high knockback to you";
        public override int Rarity => 1;

        public override void OnHitPlayer(NPC npc, Player target)
        {
            Vector2 velocity = Vector2.Normalize(target.Center - npc.Center) * 20;
            target.velocity = velocity;
        }
    }
}