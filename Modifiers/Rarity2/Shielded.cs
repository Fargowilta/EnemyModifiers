using Terraria;
using Terraria.ModLoader;

namespace FargoEnemyModifiers.Modifiers
{
    public class Shielded : Modifier
    {
        public override string Name => "Shielded";
        public override string Description => "Gains 80% resistance to melee damage";
        public override int Rarity => 2;

        public override void ModifyHitByItem(NPC npc, Player player, Item item, ref NPC.HitModifiers modifiers)
        {
            if (item.CountsAsClass(DamageClass.Melee))
                modifiers.FinalDamage *= 0.2f;
        }

        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            if (projectile.CountsAsClass(DamageClass.Melee))
                modifiers.FinalDamage *= 0.2f;
        }
    }
}