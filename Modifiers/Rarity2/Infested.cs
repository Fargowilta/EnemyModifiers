using FargoEnemyModifiers.Utilities;
using FargoEnemyModifiers.NPCs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargoEnemyModifiers.Modifiers
{
    public class Infested : Modifier
    {
        public override string Key => "Infested";
        public override RarityID Rarity => RarityID.Uncommon;
        public override AiOverrideStyle AiOverride => AiOverrideStyle.PostVanilla;

        private int counter;

        public override void AI(NPC npc)
        {
            if (NPC.CountNPCS(ModContent.NPCType<BabySpider>()) >= 10 || ++counter <= 120 ||
                Collision.SolidCollision(npc.Center, npc.width, npc.height))
                return;

            int index = NPC.NewNPC(npc.GetSource_FromAI(), (int) npc.Center.X, (int) npc.Center.Y, ModContent.NPCType<BabySpider>());
            NPC spider = Main.npc[index];
            spider.lifeMax = npc.lifeMax / 10;
            spider.life = spider.lifeMax;
            spider.damage = npc.damage / 2;

            spider.GetGlobalNPC<EnemyModifiersGlobalNPC>().firstTick = false;
            spider.velocity = new Vector2(Main.rand.Next(-2, 3), -2);

            if (Main.netMode == NetmodeID.Server)
                NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, index);

            counter = 0;
        }
    }
}