using FargoEnemyModifiers.Utilities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;

namespace FargoEnemyModifiers.Modifiers.Rarity2
{
    public class Lovestruck : Modifier
    {
        public override ModifierID ModifierID => ModifierID.Lovestruck;
        public override string Key => "Lovestruck";
        public override RarityID Rarity => RarityID.Common;
        public override AiOverrideStyle AiOverride => AiOverrideStyle.PostVanilla;
        public override int LootMultiplier => 2;
        public override float DamageMultiplier => 0.5f;
        
        public override void AI(NPC npc)
        {
            npc.AddBuff(BuffID.Lovestruck, 2);
        }

        public override void OnHitByItem(NPC npc, Player player)
        {
            heartBurst(npc);
        }

        public override void OnHitByProjectile(NPC npc, Projectile projectile)
        {
            heartBurst(npc);
        }

        public override void OnKill(NPC npc)
        {
            heartBurst(npc);
        }

        private void heartBurst(NPC npc)
        {
            for (int i = 0; i < Main.rand.Next(3, 7); i++)
            {
                int itemIndex = Item.NewItem(npc.GetSource_Loot(), npc.Center, ItemID.Heart);

                Main.item[itemIndex].velocity = new Vector2(Main.rand.NextFloat(-1, 1) * 25, Main.rand.NextFloat(-1, 1) * 25);
            }
        }


    }
}
