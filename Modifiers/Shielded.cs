using Terraria;

namespace FargoEnemyModifiers.Modifiers
{
    public class Shielded : Modifier
    {
        public override string Name => "Shielded";

        public override void ModifyHitByItem(NPC npc, Player player, Item item, ref int damage, ref float knockback)
        {
            if (item.melee)
                damage = (int) (damage * .2f);
        }

        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref int damage, ref float knockback)
        {
            if (projectile.melee)
                damage = (int) (damage * .2f);
        }
    }
}