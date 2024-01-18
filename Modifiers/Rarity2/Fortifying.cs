using FargoEnemyModifiers.Utilities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace FargoEnemyModifiers.Modifiers
{
    public class Fortifying : Modifier
    {
        public override ModifierID ModifierID => ModifierID.Fortifying;
        public override string Key => "Fortifying";
        public override RarityID Rarity => RarityID.Uncommon;
        public override AiOverrideStyle AiOverride => AiOverrideStyle.PreVanilla;

        public override float DefenseMultiplier => 0.5f;

        public override void AI(NPC npc)
        {
            const int range = 400;

            CreateAura(npc, range, DustID.YellowStarDust, Color.Blue);

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC buffedNPC = Main.npc[i];

                if (!buffedNPC.active || buffedNPC.whoAmI == npc.whoAmI || npc.townNPC || NPCID.Sets.CountsAsCritter[npc.type] ||
                    !(Vector2.Distance(npc.Center, buffedNPC.Center) < range))
                    continue;
                buffedNPC.GetGlobalNPC<EnemyModifiersGlobalNPC>().Fortified = true;
                buffedNPC.GetGlobalNPC<EnemyModifiersGlobalNPC>().FortTimer = 30;

                if (Main.rand.Next(2) != 0)
                    continue;

                Dust dust = Dust.NewDustDirect(buffedNPC.position, buffedNPC.width, buffedNPC.height, DustID.YellowStarDust, 0f,
                    -1.5f, 0, Color.Blue);
                dust.velocity *= 0.5f;
                dust.noLight = true;
            }
        }
    }
}