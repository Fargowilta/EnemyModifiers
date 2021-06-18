using Terraria;

namespace FargoEnemyModifiers.Modifiers
{
    public class Impenetrable : Modifier
    {
        public override string Name => "Impenetrable";

        public override void OnHitByProjectile(NPC npc, Projectile projectile)
        {
            if (!projectile.minion)
                projectile.Kill();
        }
    }
}
