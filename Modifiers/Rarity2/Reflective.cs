using FargoEnemyModifiers.Utilities;
using Microsoft.Xna.Framework;
using Terraria;

namespace FargoEnemyModifiers.Modifiers.Rarity2
{
    public class Reflective : Modifier
    {
        public override ModifierID ModifierID => ModifierID.Reflective;
        public override string Key => "Reflective";
        public override RarityID Rarity => RarityID.Uncommon;

        public override AiOverrideStyle AiOverride => AiOverrideStyle.PostVanilla;

        int counter = 0;
        bool reflecting = false;

        public override void AI(NPC npc)
        {
            if (reflecting)
            {
                if (counter > 30)
                {
                    npc.reflectsProjectiles = true;
                }

                if (++counter >= 120)
                {
                    reflecting = false;
                    counter = 0;
                }
            }
            else
            {
                npc.reflectsProjectiles = false;

                //every 15 seconds, turn grey and reflect all
                if (++counter >= 900)
                {
                    reflecting = true;
                    counter = 0;
                }
            }
        }

        public override Color? GetAlpha()
        {
            if (reflecting)
            {
                return Color.DarkGray;
            }

            return base.GetAlpha();
        }
    }
}
