using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FargoEnemyModifiers.Modifiers;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargoEnemyModifiers
{
    public class EnemyModifiers : Mod
    {
        public static List<Modifier> Modifiers;

        public static TModifier GetModifier<TModifier>() where TModifier : Modifier =>
            (TModifier) Modifiers.FirstOrDefault(x => x.GetType() == typeof(TModifier));

        public static Modifier GetModifier(Modifier modifier) =>
            Modifiers.FirstOrDefault(x => x.GetType() == modifier.GetType());

        public override void PostSetupContent()
        {
            Modifiers = new List<Modifier>();

            foreach (Mod mod in ModLoader.Mods)
            {
                if (mod.Code is null)
                {
                    Logger.Warn($"Mod assembly was null: {mod.Name}");
                    continue;
                }

                foreach (Type type in mod.Code.GetTypes().Where(x =>
                    !x.IsAbstract && x.IsSubclassOf(typeof(Modifier)) && x.GetConstructor(new Type[0]) != null))
                {
                    if (Activator.CreateInstance(type) is Modifier modifier && modifier.AutoLoad())
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
                            globalNPC.Modifiers.Add(
                                Modifiers[
                                    modifier]); // modifier index in array, array should be same across clients because all mods should match
                            globalNPC.ApplyModifier(npc, modifier);
                            //globalNPC.firstTick = false;
                        }
                }
                    break;

                case 1: //server receives request from ONE client to sync npc
                {
                    //if (Main.netMode == NetmodeID.Server) NetMessage.BroadcastChatMessage(Terraria.Localization.NetworkText.FromLiteral("server got modifier request"), Color.White);

                    NPC npc = Main.npc[reader.ReadByte()]; // npc whoAmI
                    EnemyModifiersGlobalNPC globalNPC = npc.GetGlobalNPC<EnemyModifiersGlobalNPC>();

                    int playerToSendTo = reader.ReadByte();

                    if (Main.netMode == NetmodeID.Server)
                    {
                        ModPacket packet = GetPacket();
                        packet.Write((byte) 0);
                        packet.Write((byte) npc.whoAmI);
                        packet.Write(globalNPC.modifierTypes.Count);
                        foreach (int modifier in globalNPC.modifierTypes)
                            packet.Write(globalNPC.modifierTypes[modifier]);
                        packet.Send(playerToSendTo); //send this info ONLY to player that requested it
                    }
                }
                    break;
            }
        }
    }
}