using FargoEnemyModifiers.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;

namespace FargoEnemyModifiers.Modifiers.Rarity1
{
    public class Stinky : Modifier
    {
        public override ModifierID ModifierID => ModifierID.Stinky;
        public override string Key => "Stinky";
        public override RarityID Rarity => RarityID.Common;
        public override AiOverrideStyle AiOverride => AiOverrideStyle.PreVanilla;

        public override float DamageMultiplier => 1.25f;

        public override void AI(NPC npc)
        {
            npc.AddBuff(BuffID.Stinky, 2);
        }

        public override void OnHitPlayer(NPC npc, Player target)
        {
            target.AddBuff(BuffID.Stinky, 1500);
        }
    }
}
