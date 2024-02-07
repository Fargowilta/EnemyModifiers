using FargoEnemyModifiers.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace FargoEnemyModifiers.Modifiers.Rarity3
{
    public class Fluctuating : Modifier
    {
        public override ModifierID ModifierID => ModifierID.Fluctuating;
        public override string Key => "Fluctuating";
        public override RarityID Rarity => RarityID.Rare;

        public override float HealthMultiplier => 2f;

        public override AiOverrideStyle AiOverride => AiOverrideStyle.PreVanilla;


        bool growing = true, firstTick = true;
        float counter = 0;

        int ogDamage, ogDefense;

        public override void AI(NPC npc)
        {
            if (firstTick)
            {
                firstTick= false;

                ogDamage = npc.damage;
                ogDefense= npc.defense;
            }

            //grow for 10, shrink for 10
            if (growing)
            {
                counter++;

                if (counter >= 300)
                {
                    growing= false;
                }
            }
            else
            {
                counter--;

                if (counter <= 0)
                {
                    growing= true;
                }
            }

            npc.damage = (int)((float)ogDamage * ( 0.25f + (counter * 0.0075f))); // max is 2.5x with 5 second intervals
            npc.defense = (int)((float)ogDefense * (0.25f + (counter * 0.0075f)));

            //SizeMultiplier = (0.7f + (counter * 0.01f));
            //RedoScale(npc);

            npc.scale = originalScale * (0.7f + (counter * 0.01f));
            

            //UpdateModifierStats(npc, false);
        }
    }
}
