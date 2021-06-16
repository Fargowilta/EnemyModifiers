using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FargoEnemyModifiers.Modifiers
{
    public class Slow : Modifier
    {
        public Slow()
        {
            name = "Slow";
            speedMultiplier = 0.5f;
        }
    }
}
