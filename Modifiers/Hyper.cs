using Terraria;

namespace FargoEnemyModifiers.Modifiers
{
    public class Hyper : Modifier
    {
        public override string Name => "Hyper";

        public override float SpeedMultiplier => 1.5f;

        protected bool hyperGo = true;

        public override bool PreAI(NPC npc)
        {
            if (!hyperGo)
                return true;

            hyperGo = false;
            npc.AI();
            return true;
        }

        public override void PostAI(NPC npc)
        {
            if (!hyperGo)
                hyperGo = true;
        }
    }
}