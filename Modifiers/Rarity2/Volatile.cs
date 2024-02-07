using FargoEnemyModifiers.Projectiles;
using FargoEnemyModifiers.Utilities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargoEnemyModifiers.Modifiers.Rarity2
{
    public class Volatile : Modifier
    {
        public override ModifierID ModifierID => ModifierID.Volatile;
        public override string Key => "Volatile";
        public override RarityID Rarity => RarityID.Uncommon;

        public override void OnKill(NPC npc)
        {
            int damage = npc.lifeMax / 2;

            Projectile.NewProjectile(npc.GetSource_Death(), npc.Center, Vector2.Zero, ModContent.ProjectileType<Explosion>(), damage, 3f, npc.whoAmI);
        }
    }
}
