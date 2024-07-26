using FargoEnemyModifiers.Utilities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;

namespace FargoEnemyModifiers.Modifiers.Rarity2
{
    public class Polite : Modifier
    {
        public override ModifierID ModifierID => ModifierID.Polite;
        public override string Key => "Polite";
        public override RarityID Rarity => RarityID.Uncommon;

        public override AiOverrideStyle AiOverride => AiOverrideStyle.PostVanilla;

        private bool saidHello = false;

        public override void AI(NPC npc)
        {
            Player target = Main.player[npc.target];

            if (!target.active)
            {
                npc.TargetClosest();
            }

            if (target.Distance(npc.Center) < 300 && !saidHello)
            {
                saidHello = true;

                List<string> possibleGreetings = new List<string>();
                possibleGreetings.Add("Hello there!");
                possibleGreetings.Add("Top of the morning to you!");
                possibleGreetings.Add("Hi, how are ya?");
                possibleGreetings.Add("Good afternoon!");
                possibleGreetings.Add("Good evening!");
                possibleGreetings.Add("Hi!");
                possibleGreetings.Add("You look good today!");

                CombatText.NewText(npc.Hitbox, Color.White, Main.rand.NextFromCollection(possibleGreetings));
            }
        }

        public override void OnHitPlayer(NPC npc, Player target)
        {
            List<string> possibleOnHit = new List<string>();
            possibleOnHit.Add("Sorry for bumpin’ into ya!");
            possibleOnHit.Add("Oops, my bad!");
            possibleOnHit.Add("Excuse me!");

            CombatText.NewText(npc.Hitbox, Color.White, Main.rand.NextFromCollection(possibleOnHit));
        }

        public override void OnKill(NPC npc)
        {
            List<string> possibleByes = new List<string>();
            possibleByes.Add("It was nice to meet you!");
            possibleByes.Add("Bye!");
            possibleByes.Add("Farewell!");
            possibleByes.Add("Sorry for bothering you!");


            CombatText.NewText(npc.Hitbox, Color.White, Main.rand.NextFromCollection(possibleByes));
        }


    }
}
