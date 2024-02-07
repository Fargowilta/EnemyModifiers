using System.Collections.Generic;
using FargoEnemyModifiers.Utilities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace FargoEnemyModifiers.Modifiers
{
    public class Showman : Modifier
    {
        public override ModifierID ModifierID => ModifierID.Showman;
        public override string Key => "Showman";
        public override RarityID Rarity => RarityID.Rare;

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

        public override void OnHitByItem(NPC npc, Player player)
        {
            releaseDoves(npc);
        }

        public override void OnHitByProjectile(NPC npc, Projectile projectile)
        {
            releaseDoves(npc);
        }

        private void releaseDoves(NPC npc)
        {
            if (!Main.rand.NextBool(3))
            {
                return;
            }

            Player player = Main.player[npc.target];
            Vector2 velocity = Vector2.Normalize(player.Center - npc.Center) * 5;

            for (int i = 0; i < 3; i++)
            {
                velocity.X += (float)Main.rand.Next(-20, 21) * 0.1f;
                velocity.Y += (float)Main.rand.Next(-20, 21) * 0.1f;
                Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center.X, npc.Center.Y, velocity.X, velocity.Y, ProjectileID.ReleaseDoves, 0, 0, player.whoAmI);
            }

            //Effects.PuffOfSmoke(npc);
        }

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