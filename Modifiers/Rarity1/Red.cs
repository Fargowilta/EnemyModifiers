using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FargoEnemyModifiers.Modifiers
{
    public class Red : Modifier
    {
        public override string Name => "Red";
        public override string Description => "Red";
        public override int Rarity => 1;
        public override bool ColorChanger => true;

        public override Color? GetAlpha()
        {
            return Color.Red;
        }
    }
}
