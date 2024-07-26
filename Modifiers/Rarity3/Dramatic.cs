using FargoEnemyModifiers.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using static System.Net.Mime.MediaTypeNames;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Microsoft.Xna.Framework;

namespace FargoEnemyModifiers.Modifiers.Rarity3
{
    public class Dramatic : Modifier
    {
        public override ModifierID ModifierID => ModifierID.Dramatic;
        public override string Key => "Dramatic";
        public override RarityID Rarity => RarityID.Rare;

        public override bool ExtraCondition(NPC npc)
        {
            return !npc.boss;
        }

        private bool firstTick = true;

        public override bool PreAI(NPC npc)
        {
            if (firstTick)
            {
                firstTick = false;

                createChat(npc, "HasAwoken");
            }

            return base.PreAI(npc);
        }

        public override void OnKill(NPC npc)
        {
            createChat(npc, "HasBeenDefeated_Single");
        }

        private void createChat(NPC npc, string announcement)
        {
            LocalizedText text = Language.GetText("Announcement." + announcement);
            string npcName = npc.FullName;

            if (Main.netMode == NetmodeID.Server)
            {
                ChatHelper.BroadcastChatMessage(text.ToNetworkText(npcName), new Color(175, 75, 255));
            }
            else
            {
                Main.NewText(text.Format(npcName), new Color(175, 75, 255));
            }

            SoundEngine.PlaySound(SoundID.Roar, npc.position);
        }
    }
}
