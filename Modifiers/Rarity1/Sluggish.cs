using FargoEnemyModifiers.Utilities;
using Terraria;

namespace FargoEnemyModifiers.Modifiers
{
    public class Sluggish : Modifier
    {
        public override ModifierID ModifierID => ModifierID.Sluggish;
        public override string Key => "Sluggish";
        public override RarityID Rarity => RarityID.Common;

        private bool sluggishGo;

        public override bool PreAI(NPC npc)
        {
            if (sluggishGo)
            {
                sluggishGo = false;
                return true;
            }

            sluggishGo = true;
            return false;
        }
    }
}