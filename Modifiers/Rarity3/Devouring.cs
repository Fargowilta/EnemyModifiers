using FargoEnemyModifiers.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;

namespace FargoEnemyModifiers.Modifiers
{
    public class Devouring : Modifier
    {
        protected int baseHeight;
        protected int baseWidth;

        protected int startup = 6;

        public override string Key => "Devouring";
        public override RarityID Rarity => RarityID.Rare;
        public override AiOverrideStyle AiOverride => AiOverrideStyle.PreVanilla;

        public override void Setup(NPC npc)
        {
            baseWidth = npc.width;
            baseHeight = npc.height;
        }

        public override void AI(NPC npc)
        {
            if (npc.realLife != -1)
            {
                return;
            }


            if (startup > 0)
            {
                startup--;
            }
            else
            {
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC otherNpc = Main.npc[i];
                
                    if (otherNpc.active && npc.whoAmI != otherNpc.whoAmI && otherNpc.realLife != npc.whoAmI && npc.realLife != otherNpc.whoAmI
                        && otherNpc.Hitbox.Intersects(npc.Hitbox) && otherNpc.lifeMax <= npc.lifeMax
                        && !otherNpc.dontTakeDamage && !otherNpc.immortal)
                    {
                        int lifeGained = otherNpc.lifeMax / 4;
                        if (lifeGained > 0)
                        {
                            npc.lifeMax += lifeGained;
                            npc.life += lifeGained;
                            npc.HealEffect(lifeGained, false);
                        }

                        npc.defDamage = (int)(npc.defDamage * 1.05f);
                        npc.damage = (int)(npc.damage * 1.05f);

                        otherNpc.GetGlobalNPC<EnemyModifiersGlobalNPC>().DropLoot = false;
                        otherNpc.life = 0;
                        otherNpc.HitEffect();
                        otherNpc.checkDead();
                        //otherNpc.StrikeNPC(otherNpc.lifeMax < 9999999 ? 9999999 : otherNpc.lifeMax, 0, 1, true);

                        SoundEngine.PlaySound(SoundID.NPCDeath13, npc.Center);
                    }
                }
            }
        }
    }
}