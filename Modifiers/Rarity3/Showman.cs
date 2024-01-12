using System.Collections.Generic;
using FargoEnemyModifiers.Utilities;
using Terraria;
using Terraria.ID;

namespace FargoEnemyModifiers.Modifiers
{
    public class Showman : Modifier
    {
        public override string Name => "Showman";
        public override string Description => "On death explodes into smoke and birds";
        public override int Rarity => 3;

        private readonly List<int> _possibleBirds = new List<int> {
            NPCID.Bird,
            NPCID.BirdBlue,
            NPCID.BirdRed,
            NPCID.GoldBird,
            NPCID.Seagull,
            NPCID.Toucan,
            NPCID.Owl,
            NPCID.Grebe
        };

        public override void HitEffect(NPC npc, NPC.HitInfo hit)
        {
            if (npc.life > 0)
            {
                return;
            }

            Effects.PuffOfSmoke(npc);
        }

        public override void OnKill(NPC npc)
        {
            for (int i = 0; i < Main.rand.Next(3, 8); i++)
            {
                NPC.NewNPC(npc.GetSource_Death(), (int)npc.Center.X, (int)npc.Center.Y, Main.rand.NextFromCollection(_possibleBirds));
            }
        }
    }
}