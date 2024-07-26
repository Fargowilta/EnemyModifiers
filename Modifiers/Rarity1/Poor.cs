using FargoEnemyModifiers.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace FargoEnemyModifiers.Modifiers.Rarity1
{
    public class Poor : Modifier
    {
        public override ModifierID ModifierID => ModifierID.Poor;
        public override string Key => "Poor";
        public override RarityID Rarity => RarityID.Common;
        public override float SpeedMultiplier => 0.75f;
        public override float HealthMultiplier => 0.75f;

        public override bool ExtraCondition(NPC npc)
        {
            return !npc.boss;
        }

        public override bool PreNPCLoot(NPC npc)
        {
            npc.value = 0;
            return false;
        }
    }
}
