using Terraria;

namespace FargoEnemyModifiers.Modifiers
{
    public class Hyper : Modifier
    {
        public override string Name => "Hyper";
        public override string Description => "Performs all actions twice as fast";
        public override int Rarity => 3;

        public override float SpeedMultiplier => 1.5f;

        public override int LootMultiplier => 2;

        protected bool hyperGo = true;
        protected bool boolToPreventStackOverflow = false;

        public override bool PreAI(NPC npc)
        {
            if (!hyperGo)
                return true;

            hyperGo = false;

            if (!boolToPreventStackOverflow) 
                npc.AI();
            boolToPreventStackOverflow = true;

            return true;
        }

        public override void PostAI(NPC npc)
        {
            boolToPreventStackOverflow = false;
            if (!hyperGo)
                hyperGo = true;
        }
    }
}