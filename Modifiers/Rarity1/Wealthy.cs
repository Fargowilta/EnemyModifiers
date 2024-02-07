using FargoEnemyModifiers.Utilities;
using Microsoft.Xna.Framework;
using Terraria;

namespace FargoEnemyModifiers.Modifiers
{
    public class Wealthy : Modifier
    {
        public override ModifierID ModifierID => ModifierID.Wealthy;
        public override string Key => "Wealthy";
        public override RarityID Rarity => RarityID.Common;

        public override Color? GetAlpha()
        {
            return Color.Gold;
        }

        private bool looted = false;

        public override bool PreNPCLoot(NPC npc)
        {
            if (!looted)
            {
                npc.value *= 5;
            }

            looted= true;
            
            return true;
        }

        public override void HitEffect(NPC npc, NPC.HitInfo hit)
        {
            int num30 = 7;
            float num31 = 1.1f;
            int num32 = 10;
            Color newColor6 = default(Color);
            if (npc.life <= 0)
            {
                num31 = 1.5f;
                num30 = 40;
                for (int num33 = 0; num33 < 8; num33++)
                {
                    int num34 = Gore.NewGore(npc.GetSource_Death(), new Vector2(npc.position.X, npc.Center.Y - 10f), Vector2.Zero, 1218);
                    Main.gore[num34].velocity = new Vector2((float)Main.rand.Next(1, 10) * 0.3f * 2.5f * (float)hit.HitDirection, 0f - (3f + (float)Main.rand.Next(4) * 0.3f));
                }
            }
            else
            {
                for (int num35 = 0; num35 < 3; num35++)
                {
                    int num36 = Gore.NewGore(npc.GetSource_OnHit(npc), new Vector2(npc.position.X, npc.Center.Y - 10f), Vector2.Zero, 1218);
                    Main.gore[num36].velocity = new Vector2((float)Main.rand.Next(1, 10) * 0.3f * 2f * (float)hit.HitDirection, 0f - (2.5f + (float)Main.rand.Next(4) * 0.3f));
                }
            }
            for (int num37 = 0; num37 < num30; num37++)
            {
                int num38 = Dust.NewDust(npc.position, npc.width, npc.height, num32, 2 * hit.HitDirection, -1f, 80, newColor6, num31);
                if (Main.rand.Next(3) != 0)
                {
                    Main.dust[num38].noGravity = true;
                }
            }
        }
    }
}