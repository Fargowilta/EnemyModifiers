using FargoEnemyModifiers.Utilities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace FargoEnemyModifiers.Modifiers.Rarity3
{
    public class Shy : Modifier
    {
        public override ModifierID ModifierID => ModifierID.Shy;
        public override string Key => "Shy";
        public override RarityID Rarity => RarityID.Rare;

        //public override bool ExtraCondition(NPC npc)
        //{
        //    return !npc.boss;
        //}

        private bool hiding = false, firstTick = true;
        private int ogAlpha;

        public override bool PreAI(NPC npc)
        {
            if (firstTick)
            {
                ogAlpha = npc.alpha;
                firstTick = false;
            }

            Player target = Main.player[npc.target];

            if (!target.active)
            {
                npc.TargetClosest();
            }

            if (target.active && npc.Distance(target.Center) < 300 && ((target.direction == 1 && target.Center.X < npc.Center.X) || (target.direction == -1 && target.Center.X > npc.Center.X)))
            {
                npc.velocity /= 2;
                hiding = true;
                npc.alpha = 100;

                npc.direction = target.direction;

                return false;
            }

            npc.alpha = ogAlpha;

            hiding = false;

            return true;
        }

        public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
        {
            if (hiding)
            {
                modifiers.FinalDamage /= 10;
            }
        }




    }
}
