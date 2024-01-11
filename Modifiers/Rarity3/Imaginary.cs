using FargoEnemyModifiers.NetCode;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Chat;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
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
            return true;
        }

        public override void OnHitPlayer(NPC npc, Player target)
        {
            ceaseToExist(npc);
        }

        public override void OnHitByItem(NPC npc, Player player)
        {
            ceaseToExist(npc);
        }

        public override void OnHitByProjectile(NPC npc, Projectile projectile)
        {
            ceaseToExist(npc);
        }

        public static void ceaseToExist(NPC npc)
        {
            if (!npc.active)
            {
                return;
            }
            
            puffOfSmoke(npc);
            
            if (Main.netMode == NetmodeID.MultiplayerClient)
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

        public static void puffOfSmoke(NPC npc)
        {
            //puff of smoke
            for (int i = 0; i < 100; i++)
            {
                int num = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 31, 0f, 0f, 100, default(Color), 2f);
                Main.dust[num].position.X += Main.rand.Next(-20, 21);
                Main.dust[num].position.Y += Main.rand.Next(-20, 21);
                Main.dust[num].velocity *= 0.4f;
                Main.dust[num].scale *= 1f + (float)Main.rand.Next(40) * 0.01f;
                //Main.dust[num].shader = GameShaders.Armor.GetSecondaryShader(npc.cWaist, npc);
                if (Main.rand.NextBool(2))
                {
                    Main.dust[num].scale *= 1f + (float)Main.rand.Next(40) * 0.01f;
                    Main.dust[num].noGravity = true;
                }
            }
            int num2 = Gore.NewGore(npc.GetSource_Death(), new Vector2(npc.position.X + (float)(npc.width / 2) - 24f, npc.position.Y + (float)(npc.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64));
            Main.gore[num2].scale = 1.5f;
            Main.gore[num2].velocity.X = (float)Main.rand.Next(-50, 51) * 0.01f;
            Main.gore[num2].velocity.Y = (float)Main.rand.Next(-50, 51) * 0.01f;
            Main.gore[num2].velocity *= 0.4f;
            num2 = Gore.NewGore(npc.GetSource_Death(), new Vector2(npc.position.X + (float)(npc.width / 2) - 24f, npc.position.Y + (float)(npc.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64));
            Main.gore[num2].scale = 1.5f;
            Main.gore[num2].velocity.X = 1.5f + (float)Main.rand.Next(-50, 51) * 0.01f;
            Main.gore[num2].velocity.Y = 1.5f + (float)Main.rand.Next(-50, 51) * 0.01f;
            Main.gore[num2].velocity *= 0.4f;
            num2 = Gore.NewGore(npc.GetSource_Death(), new Vector2(npc.position.X + (float)(npc.width / 2) - 24f, npc.position.Y + (float)(npc.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64));
            Main.gore[num2].scale = 1.5f;
            Main.gore[num2].velocity.X = -1.5f - (float)Main.rand.Next(-50, 51) * 0.01f;
            Main.gore[num2].velocity.Y = 1.5f + (float)Main.rand.Next(-50, 51) * 0.01f;
            Main.gore[num2].velocity *= 0.4f;
            num2 = Gore.NewGore(npc.GetSource_Death(), new Vector2(npc.position.X + (float)(npc.width / 2) - 24f, npc.position.Y + (float)(npc.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64));
            Main.gore[num2].scale = 1.5f;
            Main.gore[num2].velocity.X = 1.5f + (float)Main.rand.Next(-50, 51) * 0.01f;
            Main.gore[num2].velocity.Y = -1.5f - (float)Main.rand.Next(-50, 51) * 0.01f;
            Main.gore[num2].velocity *= 0.4f;
            num2 = Gore.NewGore(npc.GetSource_Death(), new Vector2(npc.position.X + (float)(npc.width / 2) - 24f, npc.position.Y + (float)(npc.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64));
            Main.gore[num2].scale = 1.5f;
            Main.gore[num2].velocity.X = -1.5f - (float)Main.rand.Next(-50, 51) * 0.01f;
            Main.gore[num2].velocity.Y = -1.5f - (float)Main.rand.Next(-50, 51) * 0.01f;
            Main.gore[num2].velocity *= 0.4f;
            if (npc.whoAmI == Main.myPlayer)
            {
                NetMessage.SendData(MessageID.Dodge, -1, -1, null, npc.whoAmI, 1f);
            }
        }
    }
}