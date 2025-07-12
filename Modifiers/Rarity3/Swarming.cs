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
        public override float SizeMultiplier => 2f;

        public override bool ExtraCondition(NPC npc)
        {
            return NPC.downedBoss1;
        }

        protected bool clonesSpawned;

        public override bool PreAI(NPC npc)
        {
            if (clonesSpawned || Main.netMode == NetmodeID.MultiplayerClient)
                return true;

            SpawnClones(npc);
                return true;
        }

        private void SpawnClones(NPC npc)
        {
            for (int i = 0; i < 8; i++)
            {
                int x = Main.rand.Next((int)npc.position.X - npc.width - 100, (int)npc.position.X + npc.width + 100);
                int y = Main.rand.Next((int)npc.position.Y - npc.height - 100, (int)npc.position.Y);

                int index = NPC.NewNPC(npc.GetSource_FromAI(), x, y, npc.type, npc.whoAmI);


                NPC newNPC = Main.npc[index];
                newNPC.SetDefaults(npc.type);
                newNPC.GetGlobalNPC<EnemyModifiersGlobalNPC>().firstTick = false; // disables modifier picking
                newNPC.netUpdate = true;
            }
            clonesSpawned = true;
        }
    }
}