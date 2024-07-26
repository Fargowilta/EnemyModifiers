using FargoEnemyModifiers.Utilities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace FargoEnemyModifiers.Modifiers.Rarity2
{
    public class Sleepy : Modifier
    {
        public override ModifierID ModifierID => ModifierID.Sleepy;
        public override string Key => "Sleepy";
        public override RarityID Rarity => RarityID.Uncommon;
        
        private bool asleep = false, savedNoGrav, savedNoTileCollide, firstTick = true;
        private int noHitCounter = 0, zCounter = 0;

        public override bool PreAI(NPC npc)
        {
            if (firstTick)
            {
                firstTick= false;
                savedNoGrav = npc.noGravity;
                savedNoTileCollide = npc.noTileCollide;
            }

            if (noHitCounter++ >= 600)
            {
                //start falling asleep
                asleep = true;
            }

            if (asleep)
            {
                if (zCounter++ >= 10)
                {
                    int index = CombatText.NewText(npc.Hitbox, Color.Black, "Z");

                    if (index != 100)
                    {
                        Main.combatText[index].velocity = new Vector2(Main.rand.Next(-2, 2), -5);
                        zCounter = 0;
                    }
                }

                npc.velocity = Vector2.Zero;
                npc.noGravity = false;
                npc.noTileCollide = false;
                npc.GravityMultiplier *= 20;

                return false;
            }
            else
            {
                npc.noGravity = savedNoGrav;
                npc.noTileCollide = savedNoTileCollide;
            }

            return true;
        }

        public override void OnHitByItem(NPC npc, Player player)
        {
            OnHitBoth();
        }

        public override void OnHitByProjectile(NPC npc, Projectile projectile)
        {
            OnHitBoth();
        }

        private void OnHitBoth()
        {
            noHitCounter = 0;
        }

        public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
        {
            if (asleep)
            {
                modifiers.FlatBonusDamage += 50;
                modifiers.SetCrit();
                modifiers.ScalingBonusDamage += 1f;

                asleep= false;
            }
        }
    }
}
