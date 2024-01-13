using Microsoft.Xna.Framework;
using System;
using FargoEnemyModifiers.NetCode;
using FargoEnemyModifiers.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace FargoEnemyModifiers.Modifiers
{
    public class Merchant : Modifier
    {
        public override string Key => "Merchant";
        public override RarityID Rarity => RarityID.Rare;

        public override bool ExtraCondition(NPC npc)
        {
            return !npc.boss && npc.type != NPCID.EaterofWorldsHead;
        }

        private bool firstTick = true;
        private bool hasInteracted;
        private int counter;
        private Player _interactingPlayer = null;

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
                    if (NetUtils.IsLocalClient(_interactingPlayer))
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
                chat = Language.GetTextValue("Mods.FargoEnemyModifiers.Modifiers.Merchant.AllIGot");
                counter = 120;
            }
            else
            {
                int playerIndex = npc.FindClosestPlayer();
                _interactingPlayer = Main.player[playerIndex];
                if (!_interactingPlayer.active) return;
                
                // Player.QuickSpawnItem(Direct) has a built-in multiplayer sync
                _interactingPlayer.QuickSpawnItemDirect(npc.GetSource_Loot(), item);
                chat = Language.GetTextValue("Mods.FargoEnemyModifiers.Modifiers.Merchant.Interaction");

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