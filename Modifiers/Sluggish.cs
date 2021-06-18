using Terraria;

namespace FargoEnemyModifiers.Modifiers
{
    public class Sluggish : Modifier
    {
        public Sluggish()
        {
            name = "Sluggish";
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
