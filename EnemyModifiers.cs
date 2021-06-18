using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FargoEnemyModifiers.Modifiers;
using Terraria;
using Terraria.ModLoader;

namespace FargoEnemyModifiers
{
    public class EnemyModifiers : Mod
    {
        public static List<Modifier> Modifiers;

        public static TModifier GetModifer<TModifier>() where TModifier : Modifier =>
            (TModifier) Modifiers.FirstOrDefault(x => x.GetType() == typeof(TModifier));

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
                    Modifier modifier = Activator.CreateInstance(type) as Modifier;
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
                case 1:
                    NPC npc = Main.npc[reader.ReadByte()];
                    int modifierType = reader.ReadByte();

                    EnemyModifiersGlobalNPC enemyGlobalNPC = npc.GetGlobalNPC<EnemyModifiersGlobalNPC>();
                    enemyGlobalNPC.firstTick = false;
                    enemyGlobalNPC.ApplyModifier(npc, modifierType);
                    break;
            }
        }
    }
}