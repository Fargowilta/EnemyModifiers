using FargoEnemyModifiers.Utilities;
using Terraria;

namespace FargoEnemyModifiers.Modifiers
{
    public class Hyper : Modifier
    {
        public override string Key => "Hyper";
        public override RarityID Rarity => RarityID.Rare;

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