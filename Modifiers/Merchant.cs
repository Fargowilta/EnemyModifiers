using Terraria;

namespace FargoEnemyModifiers.Modifiers
{
    public class Merchant : Modifier
    {
        private int counter;
        public override bool PreAI(NPC npc)
        {
            npc.aiStyle = 7;
            npc.friendly = true;
            npc.homeless = true;

            if (counter == 0)
            {
                npc.townNPC = true;
            }

            return false;
        }

        public override void GetChat(NPC npc, ref string chat)
        {
            int item;

            do
            {
                item = Main.rand.Next(Main.travelShop);
            } while (item == 0);

            Item.NewItem(npc.Hitbox, item);

            npc.townNPC = false;
            counter = 1;

            chat = "Don't tell anyone, but take this. Pretend you never saw me..";
        }

        public override bool PreNPCLoot(NPC npc)
        {
            return false;
        }
    }
}
