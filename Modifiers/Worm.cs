using Terraria;

namespace FargoEnemyModifiers.Modifiers
{
    public class Worm : Modifier
    {
        public override string Name => "Worm";

        public override float HealthMultiplier => 2f;

        public override float KnockBackMultiplier => 0f;

        protected bool bodySpawned;

        public override bool PreAI(NPC npc)
        {
            if (bodySpawned)
                return true;

            int prevIndex = npc.whoAmI;

            for (int i = 0; i < 7; i++)
            {
                int index = NPC.NewNPC((int) (npc.position.X + npc.width / 2f),
                    (int) (npc.position.Y + npc.height), npc.type, prevIndex);

                NPC newNPC = Main.npc[index];
                newNPC.GetGlobalNPC<EnemyModifiersGlobalNPC>().firstTick = false;
                newNPC.GetGlobalNPC<EnemyModifiersGlobalNPC>().Modifiers.RemoveAll(x => x.GetType() == typeof(Worm));
                newNPC.GetGlobalNPC<EnemyModifiersGlobalNPC>().Modifiers.Add(new WormBody());
                newNPC.realLife = npc.whoAmI;

                if (i != 0)
                {
                    Main.npc[prevIndex].localAI[0] = index;
                }

                newNPC.localAI[1] = prevIndex;
                npc.netUpdate = true;

                prevIndex = index;
            }

            bodySpawned = true;

            return true;
        }
    }
}