using FargoEnemyModifiers.Utilities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace FargoEnemyModifiers.Modifiers
{
    public class Royal : Modifier
    {
        public override ModifierID ModifierID => ModifierID.Royal;
        public override string Key => "Royal";
        public override RarityID Rarity => RarityID.Mythic;

        public override float HealthMultiplier => 2f;
        public override float SizeMultiplier => 2f;
        public override float SpeedMultiplier => 0.6f;
        public override float KnockBackMultiplier => 0;

        public override int LootMultiplier => 3;

        public override bool ExtraCondition(NPC npc)
        {
            return Main.hardMode;
        }

        public override void OnHitByItem(NPC npc, Player player)
        {
            OnHitBoth(npc);
        }

        public override void OnHitByProjectile(NPC npc, Projectile projectile)
        {
            OnHitBoth(npc);
        }

        private void OnHitBoth(NPC npc)
        {
            if (Main.rand.NextBool(3) && NPC.CountNPCS(npc.type) <= 10)
            {
                int npcIndex = NPC.NewNPC(npc.GetSource_FromThis(), (int)npc.Center.X, (int)npc.Center.Y, npc.type);

                NPC baby = Main.npc[npcIndex];
                baby.lifeMax /= 10;
                baby.life = baby.lifeMax;
                baby.defense /= 2;
                baby.damage /= 3;
                baby.scale = .8f;

                baby.velocity = new Vector2(Main.rand.Next(-2, 3), -2);

                baby.GetGlobalNPC<EnemyModifiersGlobalNPC>().firstTick = false;
                baby.GetGlobalNPC<EnemyModifiersGlobalNPC>().DropLoot = false;

                if (Main.netMode == NetmodeID.Server)
                    NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, npcIndex);
            }
        }
    }
}