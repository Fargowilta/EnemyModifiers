using FargoEnemyModifiers.Utilities;
using Terraria;

namespace FargoEnemyModifiers.Modifiers
{
    public class Swarmer : Modifier
    {
        public override string Key => "Swarmer";
        public override RarityID Rarity => RarityID.Hidden;

        public override bool PreAI(NPC npc)
        {
            npc.value = 0;

            if (!Main.npc[npc.realLife].active)
            {
                Effects.PuffOfSmoke(npc);
                npc.active = false;
                return false;
            }

            return true;
        }

        public override bool SpecialOnKill(NPC npc)
        {
            return true;
        }
    }
}