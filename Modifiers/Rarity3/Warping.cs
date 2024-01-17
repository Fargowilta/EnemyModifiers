//using FargoEnemyModifiers.Utilities;
//using Microsoft.Xna.Framework;
//using Terraria;

//namespace FargoEnemyModifiers.Modifiers
//{
//    public class Warping : Modifier
//    {
//        public override string Key => "Warping";
//        public override RarityID Rarity => RarityID.Rare;

//        protected bool warped;

//        public override void ModifyHitByItem(NPC npc, Player player, Item item, ref NPC.HitModifiers modifiers)
//        {
//            if (warped || npc.life >= npc.lifeMax / 2)
//                return;

//            SwitchPlaces(npc, player);
//            modifiers.Knockback *= 0;
//        }

//        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers)
//        {
//            if (warped || npc.life >= npc.lifeMax / 2)
//                return;

//            SwitchPlaces(npc, Main.player[projectile.owner]);
//            modifiers.Knockback *= 0;
//        }

//        private void SwitchPlaces(NPC npc, Player player)
//        {
//            Vector2 npcPos = npc.position;
//            npcPos.Y -= 10;
//            Vector2 playerPos = player.position;
//            playerPos.Y -= 10;
            
//            if (Vector2.Distance(npcPos, playerPos) > 1000 || !Collision.CanHitLine(npcPos, npc.width, npc.height,
//                playerPos, player.width, player.height))
//                return;

//            if (player.whoAmI != Main.myPlayer || !player.active || player.dead || player.ghost)
//            {
//                return;
//            }

//            npc.Teleport(playerPos);
//            player.Teleport(npcPos);

//            player.immune = true;
//            if (player.immuneTime < 30)
//                player.immuneTime = 30;
//            if (player.hurtCooldowns[0] < 30)
//                player.hurtCooldowns[0] = 30;
//            if (player.hurtCooldowns[1] < 30)
//                player.hurtCooldowns[1] = 30;

//            warped = true;
//        }
//    }
//}