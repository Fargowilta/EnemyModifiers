using Terraria;
using Terraria.ModLoader;

namespace FargoEnemyModifiers.Modifiers
{
    public class Veiled : Modifier
    {
        public override string Name => "Veiled";
        public override string Description => "Gains 80% resistance to magic and summon damage";
        public override int Rarity => 2;

        public override void ModifyHitByItem(NPC npc, Player player, Item item, ref NPC.HitModifiers modifiers)
        {
            if (item.CountsAsClass(DamageClass.Magic) || item.CountsAsClass(DamageClass.Summon))
                modifiers.FinalDamage *= 0.2f;
        }

        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            if (projectile.CountsAsClass(DamageClass.Magic) || projectile.CountsAsClass(DamageClass.Summon))
                modifiers.FinalDamage *= 0.2f;
        }
    }
}