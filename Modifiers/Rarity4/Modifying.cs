using FargoEnemyModifiers.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace FargoEnemyModifiers.Modifiers.Rarity4
{
    public class Modifying : Modifier
    {
        public override ModifierID ModifierID => ModifierID.Modifying;
        public override string Key => "Modifying";
        public override RarityID Rarity => RarityID.Mythic;

        int counter = 0;
        bool maxModifiers = false;

        public override bool PreAI(NPC npc)
        {
            if (!maxModifiers && ++counter >= 600)
            {
                EnemyModifiersGlobalNPC globalNPC = npc.GetGlobalNPC<EnemyModifiersGlobalNPC>();

                if (globalNPC.modifierTypes.Count >= EnemyModifiersServerConfig.Instance.ModifierAmount)
                {
                    maxModifiers= true;
                    return true;
                }

                ModifierID modifierType = globalNPC.PickModifier(npc);
                globalNPC.ApplyModifier(npc, modifierType);
                //re display name
                globalNPC.FinalizeModifierName(npc);
                globalNPC.ResetAnnouncement();
                globalNPC.ShowModifierName(npc);

                counter = 0;
            }

            return true;
        }
    }
}
