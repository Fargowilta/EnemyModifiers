using FargoEnemyModifiers.Utilities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FargoEnemyModifiers.Modifiers.Rarity1
{
    public class Pink : Modifier
    {
        public override ModifierID ModifierID => ModifierID.Pink;
        public override string Key => "Pink";
        public override RarityID Rarity => RarityID.Common;
        public override bool ColorChanger => true;

        public override Color? GetAlpha()
        {
            return Color.DeepPink;
        }
    }
}
