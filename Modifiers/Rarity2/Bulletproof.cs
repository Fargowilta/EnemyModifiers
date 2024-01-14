using FargoEnemyModifiers.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace FargoEnemyModifiers.Modifiers
{
    public class Bulletproof : Modifier
    {
        public override ModifierID ModifierID => ModifierID.Bulletproof;
        public override string Key => "Bulletproof";
        public override RarityID Rarity => RarityID.Uncommon;

        public override void ModifyHitByItem(NPC npc, Player player, Item item, ref NPC.HitModifiers modifiers)
        {
            if (item.CountsAsClass(DamageClass.Ranged))
                modifiers.FinalDamage *= 0.2f;
        }

        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            if (projectile.CountsAsClass(DamageClass.Ranged))
                modifiers.FinalDamage *= 0.2f;
        }
    }
}