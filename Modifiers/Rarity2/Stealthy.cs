﻿using FargoEnemyModifiers.Utilities;
using Terraria;

namespace FargoEnemyModifiers.Modifiers
{
    public class Stealthy : Modifier
    {
        public override ModifierID ModifierID => ModifierID.Stealthy;
        public override string Key => "Stealthy";
        public override RarityID Rarity => RarityID.Uncommon;
        public override AiOverrideStyle AiOverride => AiOverrideStyle.PreVanilla;

        public override bool AllowAnnounceModifier()
        {
            return false;
        }

        public override void AI(NPC npc)
        {
            if (npc.alpha < 200)
                npc.alpha++;
        }

        public override void OnHitByItem(NPC npc, Player player)
        {
            npc.alpha = 0;
        }

        public override void OnHitByProjectile(NPC npc, Projectile projectile)
        {
            npc.alpha = 0;
        }
    }
}