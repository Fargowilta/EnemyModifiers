using FargoEnemyModifiers.Utilities;
using Terraria;

namespace FargoEnemyModifiers.Modifiers
{
    public class Undying : Modifier
    {
        public override string Key => "Undying";
        public override RarityID Rarity => RarityID.Rare;
        public override AiOverrideStyle AiOverride => AiOverrideStyle.PreVanilla;

        bool undyingActivated = false;
        int counter = 300;

        public override void AI(NPC npc)
        {
            if (npc.life <= 5)
            {
                undyingActivated = true;
            }

            if (undyingActivated && counter != 0)
            {
                npc.life = 5;
                counter--;
            }
        }

        public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
        {
            if (counter != 0)
            {
                modifiers.SetMaxDamage(npc.life - 5);
            }

            base.ModifyIncomingHit(npc, ref modifiers);
        }

        public override bool CheckDead(NPC npc)
        {
            bool retVal = counter <= 0;

            return retVal;
        }
    }
}