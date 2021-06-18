using Terraria;

namespace FargoEnemyModifiers.Modifiers
{
    public class Juggernaut : Modifier
    {
        public Juggernaut()
        {
            name = "Juggernaut";

            HealthMultiplier = 2.5f;
            DefenseMultiplier = 1.25f;
            DamageMultiplier = 1.25f;
            SizeMultiplier = 1.25f;
        }

        private bool sluggishGo;
        public override bool PreAI(NPC npc)
        {
            if (sluggishGo)
            {
                sluggishGo = false;
                return true;
            }

            sluggishGo = true;
            return false;
        }
    }
}
