using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FargoEnemyModifiers.Modifiers
{
    public class Huge : Modifier
    {
        public Huge()
        {
            name = "Huge";
            healthMultiplier = 1.25f;
            sizeMultiplier = 1.5f;
            speedMultiplier = 0.85f;
        }
    }
}
