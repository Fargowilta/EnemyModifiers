using Terraria;

namespace FargoEnemyModifiers.Modifiers
{
    public class Sluggish : Modifier
    {
        public override string Name => "Sluggish";
        public override string Description => "Performs all actions half as fast";
        public override int Rarity => 1;

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