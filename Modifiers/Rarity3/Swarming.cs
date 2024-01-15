using System;
using FargoEnemyModifiers.Utilities;
using Terraria;
using Terraria.ID;

namespace FargoEnemyModifiers.Modifiers
{
    public class Swarming : Modifier
    {
        public override ModifierID ModifierID => ModifierID.Swarming;
        public override string Key => "Swarming";
        public override RarityID Rarity => RarityID.Rare;
        public override float HealthMultiplier => 4f;

        protected bool clonesSpawned;

        public override bool PreAI(NPC npc)
        {
            npc.checkDead();
            if (clonesSpawned || Main.netMode == NetmodeID.MultiplayerClient)
                return true;

            int owner = npc.whoAmI;

            for (int i = 0; i < 8; i++)
            {
                int x = Main.rand.Next((int)npc.position.X - npc.width - 100, (int)npc.position.X + npc.width + 100);
                int y = Main.rand.Next((int)npc.position.Y - npc.height - 100, (int)npc.position.Y + npc.height + 100);

                int index = NPC.NewNPC(npc.GetSource_FromAI(), x, y, npc.type);

                NPC newNPC = Main.npc[index];
                newNPC.SetDefaults(npc.type);
                newNPC.realLife = owner;
                newNPC.GetGlobalNPC<EnemyModifiersGlobalNPC>().firstTick = false;
                newNPC.GetGlobalNPC<EnemyModifiersGlobalNPC>().Modifiers.Add(new Swarmer());
                newNPC.realLife = npc.whoAmI;

                //apply same modifiers to all clones
                foreach (int modifierType in npc.GetGlobalNPC<EnemyModifiersGlobalNPC>().modifierTypes)
                {
                    Modifier newMod = (Activator.CreateInstance(EnemyModifiers.Modifiers[modifierType].GetType()) as Modifier);

                    if (newMod.Key == "Swarming")
                    {
                        continue;
                    }

                    newNPC.GetGlobalNPC<EnemyModifiersGlobalNPC>().ApplyModifier(newNPC, modifierType);
                    newMod.UpdateModifierStats(newNPC);
                }
            }

            clonesSpawned = true;

            return true;
        }
    }
}