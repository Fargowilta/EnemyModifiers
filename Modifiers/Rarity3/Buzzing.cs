using FargoEnemyModifiers.Utilities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace FargoEnemyModifiers.Modifiers.Rarity3
{
    public class Buzzing : Modifier
    {
        public override ModifierID ModifierID => ModifierID.Buzzing;
        public override string Key => "Buzzing";
        public override RarityID Rarity => RarityID.Rare;

        public override bool ExtraCondition(NPC npc)
        {
            return NPC.downedBoss1;
        }

        public override void OnHitByProjectile(NPC npc, Projectile projectile)
        {
            OnHitBoth(npc);
        }

        public override void OnHitByItem(NPC npc, Player player)
        {
            OnHitBoth(npc);
        }

        private void OnHitBoth(NPC npc)
        {
            int num748 = 6;
            for (int num749 = 0; num749 < num748; num749++)
            {
                if (num749 % 2 != 1 || Main.rand.Next(3) == 0)
                {
                    Vector2 vector60 = npc.position;
                    Vector2 vector61 = npc.oldVelocity;
                    vector61.Normalize();
                    vector61 *= 8f;
                    float num750 = (float)Main.rand.Next(-35, 36) * 0.01f;
                    float num751 = (float)Main.rand.Next(-35, 36) * 0.01f;
                    vector60 -= vector61 * num749;
                    num750 += npc.oldVelocity.X / 6f;
                    num751 += npc.oldVelocity.Y / 6f;

                    int bee = NPC.NewNPC(npc.GetSource_FromThis(), (int)vector60.X, (int)vector60.Y, NPCID.Bee);

                    Main.npc[bee].damage = npc.damage / 3;

                    Main.npc[bee].GetGlobalNPC<EnemyModifiersGlobalNPC>().firstTick = false;
                }
            }
        }
    }
}
