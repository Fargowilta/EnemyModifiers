using FargoEnemyModifiers.Utilities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;

namespace FargoEnemyModifiers.Modifiers.Rarity3
{
    public class Celebratory : Modifier
    {
        public override ModifierID ModifierID => ModifierID.Celebratory;
        public override string Key => "Celebratory";
        public override RarityID Rarity => RarityID.Rare;
        public override int LootMultiplier => 3;
        public override AiOverrideStyle AiOverride => AiOverrideStyle.PreVanilla;

        private int counter = 0;

        public override void AI(NPC npc)
        {
            if (++counter >= 300)
            {
                Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center, new Vector2(0,-2), ProjectileID.ConfettiGun, 0, 0, Main.myPlayer);

                /*Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center, new Vector2(0, -2), ProjectileID.Bubble, 0, 0, npc.whoAmI)*/;

                counter = 0;
            }
        }


        public override void OnKill(NPC npc)
        {
            for (int i = 0; i < 1; i++)
            {
                Projectile.NewProjectile(NPC.GetSource_None(), npc.Center, new Vector2(Main.rand.Next(-20, 20), -15), Main.rand.Next(ProjectileID.RocketFireworksBoxRed, ProjectileID.RocketFireworksBoxYellow + 1), 0, 0, Main.myPlayer);
            }

            
        }
    }
}
