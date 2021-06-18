using Terraria;

namespace FargoEnemyModifiers.Modifiers
{
    public class Juggernaut : Modifier
    {
        public override string Name => "Juggernaut";

        public override float HealthMultiplier => 2.5f;

        public override float DefenseMultiplier => 1.25f;

        public override float DamageMultiplier => 1.25f;

        public override float SizeMultiplier => 1.25f;

        protected bool sluggishGo;

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