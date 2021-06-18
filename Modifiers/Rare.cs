using Terraria;

namespace FargoEnemyModifiers.Modifiers
{
    public class Rare : Modifier
    {
        public override string Name => "Rare";

        private bool firstLoot = true;

        public override void NPCLoot(NPC npc)
        {
            if (firstLoot)
            {
                firstLoot = false;

                for (int i = 1; i <= 5; i++)
                {
                    npc.NPCLoot();
                    npc.value = 0;

                    if (NPC.killCount[Item.NPCtoBanner(npc.BannerID())] % 50 != 0)
                        NPC.killCount[Item.NPCtoBanner(npc.BannerID())]--;
                }
            }

            firstLoot = false;
        }
    }
}