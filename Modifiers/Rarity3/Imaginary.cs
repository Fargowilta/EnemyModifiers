using FargoEnemyModifiers.NetCode;
using FargoEnemyModifiers.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace FargoEnemyModifiers.Modifiers
{
    public class Imaginary : Modifier
    {
        public override string Name => "Imaginary";
        public override string Description => "Transparent. Ceases to exist when contact is made in any way";
        public override int Rarity => 3;

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

            if (EnemyModifiersConfig.Instance.AnnouncementsForever)
            {
                int index = npc.GetGlobalNPC<EnemyModifiersGlobalNPC>().combatTextIndex;
                if (index < 0 || index >= Main.combatText.Length)
                {
                    return;
                }
                Main.combatText[index].active = false;
            }
            
            npc.active = false;
        }
    }
}