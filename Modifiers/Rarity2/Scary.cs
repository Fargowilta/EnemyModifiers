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
    public class Scary : Modifier
    {
        public override ModifierID ModifierID => ModifierID.Scary;
        public override string Key => "Scary";
        public override RarityID Rarity => RarityID.Uncommon;
        public override AiOverrideStyle AiOverride => AiOverrideStyle.PostVanilla;

        public override void AI(NPC npc)
        {
            //when player near, fear them (they can only move away)
            Player player = Main.player[npc.target];

            if (!player.active || player.dead)
            {
                return;
            }

            int auraDist = 200;
            CreateAura(npc, auraDist, DustID.DarkCelestial, Color.Black);

            float distance = Vector2.Distance(npc.Center, player.Center);

            if (distance <= auraDist && Collision.CanHitLine(npc.Center, 1, 1, player.Center, 1, 1))
            {
                player.AddBuff(BuffID.Confused, 60);
            }
        }
    }
}
