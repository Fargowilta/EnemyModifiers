using FargoEnemyModifiers.NPCs;
using FargoEnemyModifiers.Utilities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargoEnemyModifiers.Modifiers.Rarity3
{
    public class Creepy : Modifier
    {
        public override ModifierID ModifierID => ModifierID.Creepy;
        public override string Key => "Creepy";
        public override RarityID Rarity => RarityID.Rare;

        public override bool ExtraCondition(NPC npc)
        {
            return NPC.downedBoss2;
        }

        public override AiOverrideStyle AiOverride => AiOverrideStyle.PreVanilla;

        private bool firstTick = true;

        public override void AI(NPC npc)
        {
            if (!firstTick)
            {
                return;
            }


            for (int i = 0; i < 10; i++)
            {
                float x2 = npc.Center.X;
                float y4 = npc.Center.Y;
                x2 += (float)Main.rand.Next(-npc.width, npc.width);
                y4 += (float)Main.rand.Next(-npc.height, npc.height);
                int creeperId = NPC.NewNPC(npc.GetSource_FromAI(), (int)x2, (int)y4, ModContent.NPCType<Creeper>(), 0, 0, 0, npc.whoAmI);

                NPC creeper = Main.npc[creeperId];

                creeper.velocity = new Vector2((float)Main.rand.Next(-30, 31) * 0.1f, (float)Main.rand.Next(-30, 31) * 0.1f);
                creeper.netUpdate = true;

                creeper.GetGlobalNPC<EnemyModifiersGlobalNPC>().firstTick = false;

                creeper.lifeMax = npc.lifeMax / 10;
                creeper.life = creeper.lifeMax;
                creeper.damage = npc.damage / 3;
                creeper.defense = npc.defense / 2;
            }

            firstTick = false;
        }
    }
}
