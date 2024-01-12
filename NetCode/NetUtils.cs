using Terraria;
using Terraria.ID;

namespace FargoEnemyModifiers.NetCode
{
    public class NetUtils
    {
        public static bool IsLocalClient(Player player)
        {
            return Main.netMode == NetmodeID.MultiplayerClient && Main.myPlayer == player.whoAmI;
        }
    }
}