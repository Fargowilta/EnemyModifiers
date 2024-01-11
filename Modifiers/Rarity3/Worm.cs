using System;
using Terraria;

namespace FargoEnemyModifiers.Modifiers
{
    public class Worm : Modifier
    {
        public override string Name => "Worm";
        public override string Description => "Spawns with several body segments that lag behind it. Gains knockback immunity and 2x HP";
        public override int Rarity => 3;
        public override float HealthMultiplier => 2f;
        public override float KnockBackMultiplier => 0f;

        protected bool bodySpawned;

        public override bool PreAI(NPC npc)
        {
            if (bodySpawned)
                return true;

            int prevIndex = npc.whoAmI;

            for (int i = 0; i < 7; i++)
            {
                int index = NPC.NewNPC(npc.GetSource_FromAI(), (int) (npc.position.X + npc.width / 2f),
                    (int) (npc.position.Y + npc.height), npc.type, prevIndex);

                NPC newNPC = Main.npc[index];
                newNPC.SetDefaults(npc.netID);
                newNPC.GetGlobalNPC<EnemyModifiersGlobalNPC>().firstTick = false;
                newNPC.GetGlobalNPC<EnemyModifiersGlobalNPC>().Modifiers.Add(new WormBody());
                newNPC.realLife = npc.whoAmI;

                //apply same modifiers to all segments
                foreach (int modifierType in npc.GetGlobalNPC<EnemyModifiersGlobalNPC>().modifierTypes)
                {
                    Modifier newMod = (Activator.CreateInstance(EnemyModifiers.Modifiers[modifierType].GetType()) as Modifier);

                    if (newMod.Name == "Worm")
                    {
                        continue;
                    }

                    newNPC.GetGlobalNPC<EnemyModifiersGlobalNPC>().ApplyModifier(newNPC, modifierType);
                }

                if (i != 0)
                {
                    Main.npc[prevIndex].localAI[0] = index;
                }

                newNPC.localAI[1] = prevIndex;
                npc.netUpdate = true;

                prevIndex = index;
            }

            bodySpawned = true;

            return true;
        }
    }
}