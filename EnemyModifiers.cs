using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ModLoader;

namespace FargoEnemyModifiers
{
	public class EnemyModifiers : Mod
	{
        private int numModifiers = 33;
        public static List<int> ModifierTypes;

        public override void PostSetupContent()
        {
            ModifierTypes = new List<int>();

            for (int i = 0; i <= numModifiers; i++)
            {
                ModifierTypes.Add(i);
            }
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