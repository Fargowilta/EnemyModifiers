using FargoEnemyModifiers.NPCs;
using FargoEnemyModifiers.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace FargoEnemyModifiers.Modifiers.Rarity3
{
    public class Skeletal : Modifier
    {
        public override ModifierID ModifierID => ModifierID.Skeletal;
        public override string Key => "Skeletal";
        public override RarityID Rarity => RarityID.Rare;
        public override AiOverrideStyle AiOverride => AiOverrideStyle.PreVanilla;

        private bool firstTick = true;

        public override void AI(NPC npc)
        {
            if (firstTick)
            {
                firstTick = false;

                npc.TargetClosest();

                //spawn hands
                NPC hand = Main.npc[NPC.NewNPC(npc.GetSource_FromAI(), (int)(npc.position.X + (float)(npc.width / 2)), (int)npc.position.Y + npc.height / 2, ModContent.NPCType<SkeleArm>(), npc.whoAmI)];

                hand.ai[0] = -1f;
                hand.ai[1] = npc.whoAmI;
                hand.target = npc.target;
                hand.lifeMax = npc.lifeMax / 5;
                hand.life = hand.lifeMax;
                hand.damage = npc.damage / 2;
                hand.GetGlobalNPC<EnemyModifiersGlobalNPC>().firstTick = false;
                hand.netUpdate = true;


                hand = Main.npc[NPC.NewNPC(npc.GetSource_FromAI(), (int)(npc.position.X + (float)(npc.width / 2)), (int)npc.position.Y + npc.height / 2, ModContent.NPCType<SkeleArm>(), npc.whoAmI)];
                hand.ai[0] = 1f;
                hand.ai[1] = npc.whoAmI;
                hand.ai[3] = 150f;
                hand.target = npc.target;
                hand.lifeMax = npc.lifeMax / 5;
                hand.life = hand.lifeMax;
                hand.damage = npc.damage / 2;
                hand.GetGlobalNPC<EnemyModifiersGlobalNPC>().firstTick = false;
                hand.netUpdate = true;
            }
        }
    }
}
