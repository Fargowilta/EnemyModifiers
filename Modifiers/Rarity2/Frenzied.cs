using FargoEnemyModifiers.Utilities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace FargoEnemyModifiers.Modifiers.Rarity2
{
    public class Frenzied : Modifier
    {
        public override ModifierID ModifierID => ModifierID.Frenzied;
        public override string Key => "Frenzied";
        public override RarityID Rarity => RarityID.Uncommon;
        public override AiOverrideStyle AiOverride => AiOverrideStyle.PreVanilla;

        private bool firstTick = true;
        private int originalDamage;
        private float percentHP;

        public override void AI(NPC npc)
        {
            if (firstTick)
            {
                firstTick= false;
                originalDamage = npc.damage;
            }

            percentHP = (float)npc.life / (float)npc.lifeMax;

            //Main.NewText(percentHP);

            float multiplier = (1f + (1f - percentHP));
            npc.damage = (int)((float)originalDamage * multiplier);

            if (SpeedMultiplier < multiplier)
            {
                SpeedMultiplier = multiplier;
            }
        }

        public override Color? GetAlpha()
        {
            if (percentHP > 0.5f)
            {
                return base.GetAlpha();
            }

            int r = (int)(255f * (1f - percentHP)); 
            Color color = new Color(r, 0, 0);

            return color;
        }
    }
}
