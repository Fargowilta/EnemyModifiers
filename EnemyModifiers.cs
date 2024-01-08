using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FargoEnemyModifiers.Modifiers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace FargoEnemyModifiers
{
    public class EnemyModifiers : Mod
    {
        

        //alphabetical list for forcing specific one
        public static List<Modifier> Modifiers;

        public static TModifier GetModifier<TModifier>() where TModifier : Modifier =>
            (TModifier) Modifiers.FirstOrDefault(x => x.GetType() == typeof(TModifier));

        public static Modifier GetModifier(Modifier modifier) =>
            Modifiers.FirstOrDefault(x => x.GetType() == modifier.GetType());

        public override void PostSetupContent()
        {
            Modifiers = new List<Modifier>();

            //these are added alphabetically
            foreach (Type type in this.Code.GetTypes().Where(x =>
                !x.IsAbstract && x.IsSubclassOf(typeof(Modifier)) && x.GetConstructor(new Type[0]) != null))
            {
                if (Activator.CreateInstance(type) is Modifier modifier && modifier.AutoLoad())
                {
                    Modifiers.Add(modifier);
                }
            }
        }

        public override void Unload()
        {
            Modifiers = null;
        }

        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            switch (reader.ReadByte())
            {
                case 0: //clients sync modifier data
                {
                    NPC npc = Main.npc[reader.ReadByte()]; // npc whoAmI
                    int arrayLength = reader.ReadInt32();
                    int[] modifiers = new int[arrayLength];

                    foreach (int i in modifiers)
                    {
                        int modifier = reader.ReadInt32();
                        modifiers[i] = modifier;
                    }

                    EnemyModifiersGlobalNPC globalNPC = npc.GetGlobalNPC<EnemyModifiersGlobalNPC>();

                    foreach (int modifier in modifiers)
                        if (Main.netMode == NetmodeID.MultiplayerClient && npc.active && modifier > -1 &&
                            modifier < Modifiers.Count)
                        {
                            // modifier index in array, array should be same across clients because all mods should match
                                globalNPC.modifierTypes.Add(modifier);
                                globalNPC.Modifiers.Add(Modifiers[modifier]);
                                globalNPC.ApplyModifier(npc, modifier);

                                //ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("modifier applied: " + modifier), new Color(175, 75, 255));
                                //globalNPC.firstTick = false;
                            }
                }
                    break;

                case 1: //server receives request from ONE client to sync npc
                {
                    //if (Main.netMode == NetmodeID.Server) NetMessage.BroadcastChatMessage(Terraria.Localization.NetworkText.FromLiteral("server got modifier request"), Color.White);

                    NPC npc = Main.npc[reader.ReadByte()]; // npc whoAmI
                    //EnemyModifiersGlobalNPC globalNPC = npc.GetGlobalNPC<EnemyModifiersGlobalNPC>();

                    int playerToSendTo = reader.ReadByte();

                    int modifiertoSend = reader.ReadByte();

                        if (Main.netMode == NetmodeID.Server)
                    {
                        ModPacket packet = GetPacket();
                        packet.Write((byte) 0);
                        packet.Write((byte) npc.whoAmI);
                            //packet.Write(globalNPC.modifierTypes.Count);
                            //foreach (int modifier in globalNPC.modifierTypes)
                            //    packet.Write(globalNPC.modifierTypes[modifier]);

                        packet.Write(1);
                        packet.Write(modifiertoSend);

                            //ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("modifier to apply: " + modifiertoSend), new Color(175, 75, 255));

                            packet.Send(playerToSendTo); //send this info ONLY to player that requested it
                    }
                }
                    break;
            }
        }
    }
}