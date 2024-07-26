using FargoEnemyModifiers.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargoEnemyModifiers.Modifiers
{
    public class Devouring : Modifier
    {
        public override ModifierID ModifierID => ModifierID.Devouring;
        public override string Key => "Devouring";
        public override RarityID Rarity => RarityID.Rare;
        public override AiOverrideStyle AiOverride => AiOverrideStyle.PreVanilla;

        protected int startup = 6;

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
                        && !otherNpc.dontTakeDamage && !otherNpc.immortal && otherNpc.type != NPCID.DD2EterniaCrystal)
                    {
                        int lifeGained = otherNpc.lifeMax / 4;
                        if (lifeGained > 0)
                        {
                            npc.lifeMax += lifeGained;
                            npc.life += lifeGained;
                            npc.HealEffect(lifeGained, false);
                        }

                        npc.defDamage = (int)(npc.defDamage * 1.05f) + 1;
                        npc.damage = (int)(npc.damage * 1.1f) + 1;
                        npc.scale *= 1.1f;

                        EnemyModifiersGlobalNPC globalNPCBase = npc.GetGlobalNPC<EnemyModifiersGlobalNPC>();


                        if (globalNPCBase.modifierTypes.Count < EnemyModifiersServerConfig.Instance.ModifierAmount)
                        {
                            //absorb any modifiers
                            bool newMods = false;

                            foreach (ModifierID modifier in otherNpc.GetGlobalNPC<EnemyModifiersGlobalNPC>().modifierTypes)
                            {
                                if (!modifiersContains(globalNPCBase.modifierTypes, modifier))
                                {
                                    globalNPCBase.ApplyModifier(npc, modifier);
                                    newMods = true;
                                }

                                if (globalNPCBase.modifierTypes.Count >= EnemyModifiersServerConfig.Instance.ModifierAmount)
                                {
                                    break;
                                }
                            }

                            if (newMods)
                            {
                                globalNPCBase.FinalizeModifierName(npc);
                                globalNPCBase.ResetAnnouncement();
                                globalNPCBase.ShowModifierName(npc);
                            }
                        }

                        otherNpc.GetGlobalNPC<EnemyModifiersGlobalNPC>().DropLoot = false;
                        otherNpc.life = 0;
                        otherNpc.HitEffect();
                        otherNpc.checkDead();
                        
                        SoundEngine.PlaySound(SoundID.NPCDeath13, npc.Center);
                    }
                }
            }
        }

        private bool modifiersContains(List<ModifierID> modifierTypes, ModifierID modifier)
        {
            foreach (ModifierID modifierType in modifierTypes)
            {
                if (((int)modifierType).Equals(((int)modifier)))
                {
                    return true;
                }
            }

            return false;


        }
    }
}