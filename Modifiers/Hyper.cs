using Terraria;

namespace FargoEnemyModifiers.Modifiers
{
    public class Hyper : Modifier
    {
        public Hyper()
        {
            name = "Hyper";
            SpeedMultiplier = 1.5f;
        }

        private bool hyperGo = true;
        public override bool PreAI(NPC npc)
        {
            if (hyperGo)
            {
                hyperGo = false;
                npc.AI();
            }
            return true;
        }

        public override void PostAI(NPC npc)
        {
            if (!hyperGo)
            {
                hyperGo = true;
            }
        }
    }
}
