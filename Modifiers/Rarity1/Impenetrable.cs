using FargoEnemyModifiers.Utilities;
using Terraria;

namespace FargoEnemyModifiers.Modifiers
{
    public class Impenetrable : Modifier
    {
        public override ModifierID ModifierID => ModifierID.Impenetrable;
        public override string Key => "Impenetrable";
        public override RarityID Rarity => RarityID.Common;

        public override void OnHitByProjectile(NPC npc, Projectile projectile)
        {
            if (!projectile.minion)
                projectile.Kill();
        }
    }
}
