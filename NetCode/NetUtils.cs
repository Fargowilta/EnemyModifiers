using Terraria;
using Terraria.ID;

namespace FargoEnemyModifiers.NetCode
{
    public static class NetUtils
    {
        public static bool IsLocalClient(Player player)
        {
            if (player == null) return false;
            return Main.netMode == NetmodeID.MultiplayerClient && Main.myPlayer == player.whoAmI;
        }
    }
}