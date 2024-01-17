using FargoEnemyModifiers.NetCode;
using FargoEnemyModifiers.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace FargoEnemyModifiers.Modifiers
{
    public class Imaginary : Modifier
    {
        public override ModifierID ModifierID => ModifierID.Imaginary;
        public override string Key => "Imaginary";
        public override RarityID Rarity => RarityID.Rare;

        public override bool PreAI(NPC npc)
        {
            npc.alpha = 150;
            npc.immortal = true;
            return true;
        }

        public override void OnHitPlayer(NPC npc, Player player)
        {
            ceaseToExist(npc, player);
        }

        public override void OnHitByItem(NPC npc, Player player)
        {
            ceaseToExist(npc, player);
        }

        public override void OnHitByProjectile(NPC npc, Projectile projectile)
        {
            ceaseToExist(npc, Main.player[projectile.owner]);
        }

        public static void ceaseToExist(NPC npc, Player player)
        {
            if (!npc.active)
            {
                return;
            }
            
            Effects.PuffOfSmoke(npc);
            
            if (NetUtils.IsLocalClient(player))
            {
                ModPacket packet = EnemyModifiers.Instance.GetPacket();
                packet.Write((byte) PacketID.ClientCausedDespawn);
                packet.Write((byte) npc.whoAmI);
                packet.Send();
            }
            
            npc.active = false;
        }
    }
}