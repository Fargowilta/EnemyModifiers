using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FargoEnemyModifiers.Modifiers;
using FargoEnemyModifiers.NetCode;
using FargoEnemyModifiers.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargoEnemyModifiers
{
    public class EnemyModifiers : Mod
    {
        public static Mod Instance;

        //alphabetical list for forcing specific one
        //public static List<Modifier> Modifiers;
        public static Dictionary<ModifierID, Modifier> Modifiers;


//        public static TModifier GetModifier<TModifier>() where TModifier : Modifier =>
//            (TModifier) Modifiers.FirstOrDefault(x => x.GetType() == typeof(TModifier));
//
//        public static Modifier GetModifier(Modifier modifier) =>
//            Modifiers.FirstOrDefault(x => x.GetType() == modifier.GetType());

        public override void PostSetupContent()
        {
            Instance = this;
            
            Modifiers = new Dictionary<ModifierID, Modifier>();

            //these are added alphabetically
            foreach (Type type in this.Code.GetTypes().Where(x =>
                !x.IsAbstract && x.IsSubclassOf(typeof(Modifier)) && x.GetConstructor(new Type[0]) != null))
            {
                if (Activator.CreateInstance(type) is Modifier modifier && modifier.AutoLoad())
                {
//                    if (modifier.Rarity == RarityID.Hidden)
//                        continue;
                    Modifiers.Add(modifier.ModifierID, modifier);
                }
            }

            // Alphabetical sort is not viable anymore
            // Modifiers.Sort((x, y) => x.ModifierID.CompareTo(y.ModifierID));
        }

        public override void Unload()
        {
            Modifiers = null;
        }

        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            PacketID id = (PacketID)reader.ReadByte();

            switch (id)
            {
                // Packet:
                // byte: packet id
                // byte: npc whoAmI
                // byte: modifier amount
                // byte[]: modifiers
                case PacketID.MobSpawn: // Server is sending modifier data to clients
                    if (Main.netMode != NetmodeID.MultiplayerClient) return;


                    int npcIndex = reader.ReadByte();
                    NPC npc = Main.npc[npcIndex]; // npc whoAmI
                    if (!npc.active)
                    {
                        reader.ReadBytes(reader.ReadByte()); // skip modifiers
                        return; // npc is dead
                    }
                    EnemyModifiersGlobalNPC modNpc = npc.GetGlobalNPC<EnemyModifiersGlobalNPC>();
                    
                    int[] modifiers = new int[reader.ReadByte()]; // modifier amount
                    for (int i = 0; i < modifiers.Length; i++)
                    {
                        modifiers[i] = reader.ReadByte();
                    }
                    
                    foreach (ModifierID modifier in modifiers)
                    {
                        modNpc.modifierTypes.Add(modifier);
                        modNpc.ApplyModifier(npc, modifier);
                    }
                
                    modNpc.finalizeModifierName(npc);
                    
                    break;
                case PacketID.ClientCausedDespawn:
                    if (Main.netMode != NetmodeID.Server) return;
                    
                    npcIndex = reader.ReadByte();
                    NPC npcToDespawn = Main.npc[npcIndex];
                    
                    npcToDespawn.active = false;
                    break;
            }
        }
    }
}