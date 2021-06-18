using Terraria;

namespace FargoEnemyModifiers.Modifiers
{
    public class Inpenetrable : Modifier
    {
        public Inpenetrable()
        {
            name = "Inpenetrable";
        }

        public override void OnHitByProjectile(NPC npc, Projectile projectile)
        {
            if (!projectile.minion)
            {
                projectile.Kill();
            }
            
        }
    }
}
