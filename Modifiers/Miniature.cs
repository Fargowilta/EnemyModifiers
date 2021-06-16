using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FargoEnemyModifiers.Modifiers
{
    public class Miniature : Modifier
    {
        public Miniature()
        {
            name = "Miniature";
            healthMultiplier = 0.75f;
            sizeMultiplier = 0.5f;
            speedMultiplier = 1.2f;
        }
    }
}
