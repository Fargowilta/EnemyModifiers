using FargoEnemyModifiers.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace FargoEnemyModifiers.Modifiers
{
    public class Shielded : Modifier
    {
        public override string Key => "Shielded";
        public override RarityID Rarity => RarityID.Uncommon;

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