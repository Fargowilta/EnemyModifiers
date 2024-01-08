using Terraria;

namespace FargoEnemyModifiers.Modifiers
{
    public class Impenetrable : Modifier
    {
        public override string Name => "Impenetrable";
        public override string Description => "Piercing projectiles are destroyed on hit";
        public override int Rarity => 1;

        public override void OnHitByProjectile(NPC npc, Projectile projectile)
        {
            if (!projectile.minion)
                projectile.Kill();
        }
    }
}
