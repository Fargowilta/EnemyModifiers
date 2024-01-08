using FargoEnemyModifiers.Modifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace FargoEnemyModifiers
{
    public class EnemyModifiersGlobalProj : GlobalProjectile
    {
        NPC owner = null;
        private bool imaginary = false;

        public override bool InstancePerEntity => true;

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if (source is EntitySource_Parent parent && parent.Entity is NPC parentNPC)
            {
                foreach (Modifier modifier in parentNPC.GetGlobalNPC<EnemyModifiersGlobalNPC>().Modifiers)
                {
                    if (modifier.SizeMultiplier != 1)
                    {
                        projectile.scale = parentNPC.scale;
                    }

                    if (modifier.Name == "Imaginary")
                    {
                        imaginary = true;
                        projectile.alpha = 150;
                        owner = parentNPC;
                    }
                }
            }
        }

        public override void OnHitPlayer(Projectile projectile, Player target, Player.HurtInfo info)
        {
            if (imaginary)
            {
                Imaginary.ceaseToExist(owner);
            }
        }
    }
}
