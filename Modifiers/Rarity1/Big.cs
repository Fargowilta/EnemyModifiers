using FargoEnemyModifiers.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FargoEnemyModifiers.Modifiers.Rarity1
{
    public class Big : Modifier
    {
        public override ModifierID ModifierID => ModifierID.Big;
        public override string Key => "Big";
        public override RarityID Rarity => RarityID.Common;

        public override float HealthMultiplier => 1.25f;
        public override float DamageMultiplier => 1.2f;
        public override float SizeMultiplier => 1.25f;
        public override float SpeedMultiplier => 0.9f;
    }
}
