using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;

namespace FargoEnemyModifiers.Modifiers
{
    public class Merchant : Modifier
    {
        public override string Name => "Merchant";
        public override string Description => "Replaces its usual AI with Town NPC AI. It will give you one random free item from the Travelling Merchant's current shop then vanish";
        public override int Rarity => 3;

        public override bool ExtraCondition(NPC npc)
        {
            return !npc.boss && npc.type != NPCID.EaterofWorldsHead;
        }

        private bool firstTick = true;
        private bool hasInteracted;
        private int counter;

        public override bool PreAI(NPC npc)
        {
            if (firstTick)
            {
                firstTick = false;
            }

            if (hasInteracted)
            {
                if (--counter <= 0)
                {
                    npc.active = false;
                    GrossVanillaDodgeDust(npc);


                }
            }
            else
            {
                npc.aiStyle = 7;
                npc.friendly = true;
                npc.homeless = true;
                npc.townNPC = true;
            }

            return false;
        }

        public override void GetChat(NPC npc, ref string chat)
        {
            if (chat == null)
                throw new ArgumentNullException(nameof(chat));

            int item;

            do
            {
                item = Main.rand.Next(Main.travelShop);
            } while (item == 0);

            if (hasInteracted)
            {
                chat = "That's all I got for you..";
                counter = 120;
            }
            else
            {
                Item.NewItem(npc.GetSource_Loot(), npc.Hitbox, item);
                chat = "Don't tell anyone, but take this. Pretend you never saw me..";

                hasInteracted = true;
                counter = 300; //countdown to poof
            }
        }

        public override bool PreNPCLoot(NPC npc)
        {
            return false;
        }

        public static void GrossVanillaDodgeDust(Entity entity)
        {
            for (int index1 = 0; index1 < 100; ++index1)
            {
                int index2 = Dust.NewDust(entity.position, entity.width, entity.height, 31, 0.0f, 0.0f, 100, new Color(), 2f);
                Main.dust[index2].position.X += Main.rand.Next(-20, 21);
                Main.dust[index2].position.Y += Main.rand.Next(-20, 21);
                Dust dust = Main.dust[index2];
                dust.velocity *= 0.4f;
                Main.dust[index2].scale *= 1f + Main.rand.Next(40) * 0.01f;
                if (Main.rand.NextBool())
                {
                    Main.dust[index2].scale *= 1f + Main.rand.Next(40) * 0.01f;
                    Main.dust[index2].noGravity = true;
                }
            }

            int index3 = Gore.NewGore(entity.GetSource_FromThis(), new Vector2(entity.Center.X - 24, entity.Center.Y - 24), new Vector2(), Main.rand.Next(61, 64), 1f);
            Main.gore[index3].scale = 1.5f;
            Main.gore[index3].velocity.X = Main.rand.Next(-50, 51) * 0.01f;
            Main.gore[index3].velocity.Y = Main.rand.Next(-50, 51) * 0.01f;
            Main.gore[index3].velocity *= 0.4f;

            int index4 = Gore.NewGore(entity.GetSource_FromThis(), new Vector2(entity.Center.X - 24, entity.Center.Y - 24), new Vector2(), Main.rand.Next(61, 64), 1f);
            Main.gore[index4].scale = 1.5f;
            Main.gore[index4].velocity.X = 1.5f + Main.rand.Next(-50, 51) * 0.01f;
            Main.gore[index4].velocity.Y = 1.5f + Main.rand.Next(-50, 51) * 0.01f;
            Main.gore[index4].velocity *= 0.4f;

            int index5 = Gore.NewGore(entity.GetSource_FromThis(), new Vector2(entity.Center.X - 24, entity.Center.Y - 24), new Vector2(), Main.rand.Next(61, 64), 1f);
            Main.gore[index5].scale = 1.5f;
            Main.gore[index5].velocity.X = -1.5f - Main.rand.Next(-50, 51) * 0.01f;
            Main.gore[index5].velocity.Y = 1.5f + Main.rand.Next(-50, 51) * 0.01f;
            Main.gore[index5].velocity *= 0.4f;

            int index6 = Gore.NewGore(entity.GetSource_FromThis(), new Vector2(entity.Center.X - 24, entity.Center.Y - 24), new Vector2(), Main.rand.Next(61, 64), 1f);
            Main.gore[index6].scale = 1.5f;
            Main.gore[index6].velocity.X = 1.5f - Main.rand.Next(-50, 51) * 0.01f;
            Main.gore[index6].velocity.Y = -1.5f + Main.rand.Next(-50, 51) * 0.01f;
            Main.gore[index6].velocity *= 0.4f;

            int index7 = Gore.NewGore(entity.GetSource_FromThis(), new Vector2(entity.Center.X - 24, entity.Center.Y - 24), new Vector2(), Main.rand.Next(61, 64), 1f);
            Main.gore[index7].scale = 1.5f;
            Main.gore[index7].velocity.X = -1.5f - Main.rand.Next(-50, 51) * 0.01f;
            Main.gore[index7].velocity.Y = -1.5f + Main.rand.Next(-50, 51) * 0.01f;
            Main.gore[index7].velocity *= 0.4f;
        }
    }
}