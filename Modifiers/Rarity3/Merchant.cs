using Microsoft.Xna.Framework;
using System;
using FargoEnemyModifiers.NetCode;
using FargoEnemyModifiers.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

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

            npc.aiStyle = NPCAIStyleID.Passive;
            npc.friendly = true;
            npc.homeless = true;

            NPCID.Sets.NoTownNPCHappiness[npc.type] = true;
            NPCID.Sets.ActsLikeTownNPC[npc.type] = true;

            if (hasInteracted)
            {
                if (--counter <= 0)
                {
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        ModPacket packet = EnemyModifiers.Instance.GetPacket();
                        packet.Write((byte) PacketID.ClientCausedDespawn);
                        packet.Write((byte) npc.whoAmI);
                        packet.Send();
                    }

                    npc.active = false;
                    Effects.PuffOfSmoke(npc);
                }
            }

            return true;
        }

        public override bool? CanChat(NPC npc)
        {
            return true;
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
                int playerIndex = npc.FindClosestPlayer();
                Player player = Main.player[playerIndex];
                if (!player.active) return;
                
                // Player.QuickSpawnItem(Direct) has a built-in multiplayer sync
                player.QuickSpawnItemDirect(npc.GetSource_Loot(), item);
                chat = "Don't tell anyone, but take this. Pretend you never saw me..";

                hasInteracted = true;
                counter = 300; //countdown to poof
            }
        }

        public override bool PreNPCLoot(NPC npc)
        {
            return false;
        }
    }
}