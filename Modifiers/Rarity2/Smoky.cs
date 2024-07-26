using FargoEnemyModifiers.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace FargoEnemyModifiers.Modifiers.Rarity2
{
    public class Smoky : Modifier
    {
        public override ModifierID ModifierID => ModifierID.Smoky;
        public override string Key => "Smoky";
        public override RarityID Rarity => RarityID.Uncommon;

        public override AiOverrideStyle AiOverride => AiOverrideStyle.PreVanilla;

        public override void AI(NPC npc)
        {
            int num151 = Main.rand.Next(1, 3);
            for (int num152 = 0; num152 < num151; num152++)
            {
                int num153 = Dust.NewDust(npc.position, npc.width, npc.height, 31, 0f, 0f, 100);
                Dust dust2 = Main.dust[num153];
                dust2.alpha += Main.rand.Next(100);
                dust2 = Main.dust[num153];
                dust2.velocity *= 0.3f;
                Main.dust[num153].velocity.X += (float)Main.rand.Next(-10, 11) * 0.025f;
                Main.dust[num153].velocity.Y -= 0.4f + (float)Main.rand.Next(-3, 14) * 0.15f;
                Main.dust[num153].fadeIn = 1.25f + (float)Main.rand.Next(20) * 0.15f;
            }
        }
    }
}
