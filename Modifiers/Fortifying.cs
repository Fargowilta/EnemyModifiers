using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace FargoEnemyModifiers.Modifiers
{
    public class Fortifying : Modifier
    {
        public override string Name => "Fortifying";

        public override float DefenseMultiplier => 0.5f;

        public override void AI(NPC npc)
        {
            const int range = 400;

            CreateAura(npc, range);

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC buffedNPC = Main.npc[i];

                if (!buffedNPC.active || buffedNPC.whoAmI == npc.whoAmI ||
                    !(Vector2.Distance(npc.Center, buffedNPC.Center) < range))
                    continue;
                buffedNPC.GetGlobalNPC<EnemyModifiersGlobalNPC>().Fortified = true;
                buffedNPC.GetGlobalNPC<EnemyModifiersGlobalNPC>().FortTimer = 30;

                if (Main.rand.Next(2) != 0)
                    continue;

                Dust dust = Dust.NewDustDirect(buffedNPC.position, buffedNPC.width, buffedNPC.height, DustID.Iron, 0f,
                    -1.5f);
                dust.velocity *= 0.5f;
                dust.noLight = true;
            }
        }
    }
}