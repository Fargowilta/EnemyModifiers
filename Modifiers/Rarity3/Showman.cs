using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargoEnemyModifiers.Modifiers
{
    public class Showman : Modifier
    {
        public override string Name => "Showman";
        public override string Description => "On death explodes into smoke and birds";
        public override int Rarity => 3;

        public override void OnKill(NPC npc)
        {
            List<int> possibleBirds = new List<int>();
            possibleBirds.Add(NPCID.Bird);
            possibleBirds.Add(NPCID.BirdBlue);
            possibleBirds.Add(NPCID.BirdRed);
            possibleBirds.Add(NPCID.GoldBird);
            possibleBirds.Add(NPCID.Seagull);
            possibleBirds.Add(NPCID.Toucan);
            possibleBirds.Add(NPCID.Owl);
            possibleBirds.Add(NPCID.Grebe);

            for (int i = 0; i < Main.rand.Next(3, 8); i++)
            {
                NPC.NewNPC(npc.GetSource_Death(), (int)npc.Center.X, (int)npc.Center.Y, Main.rand.NextFromCollection(possibleBirds));
            }

            Imaginary.puffOfSmoke(npc);
        }
    }
}