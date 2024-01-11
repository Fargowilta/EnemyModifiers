using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace FargoEnemyModifiers.Modifiers
{
    public class Royal : Modifier
    {
        public override string Name => "Royal";
        public override string Description => "200% HP, 200% size, 60% move speed, immune to knockback, spawns mini versions of itself every so often when hurt";
        public override int Rarity => 4;

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

                //SizeMultiplier = 0.5f + ((float)npc.life / (float)npc.lifeMax * 1.5f);

                //Main.NewText(SizeMultiplier);

                //UpdateModifierStats(npc);
            }
        }
    }
}