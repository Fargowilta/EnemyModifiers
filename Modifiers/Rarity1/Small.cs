using FargoEnemyModifiers.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FargoEnemyModifiers.Modifiers.Rarity1
{
    public class Small : Modifier
    {
        public override ModifierID ModifierID => ModifierID.Small;
        public override string Key => "Small";
        public override RarityID Rarity => RarityID.Common;

        public override float HealthMultiplier => 0.75f;
        public override float DamageMultiplier => 0.75f;
        public override float SizeMultiplier => 0.75f;
        public override float SpeedMultiplier => 1.2f;
    }
}
