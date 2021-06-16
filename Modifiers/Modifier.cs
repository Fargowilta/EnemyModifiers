using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;

namespace FargoEnemyModifiers.Modifiers
{
    public abstract class Modifier
    {
        public int type;
        public string name;

        public float healthMultiplier = 1f;
        public float damageMultiplier = 1f;
        public float defenseMultiplier = 1f;
        public float sizeMultiplier = 1f;
        public float speedMultiplier = 1f;
        public float kbMultiplier = 1f;

        public float originalScale;
        public string originalName = "";

        public void UpdateModifierStats(NPC npc)
        {
            npc.lifeMax = (int)(npc.lifeMax * healthMultiplier);
            npc.life = npc.lifeMax;
            npc.damage = (int)(npc.damage * damageMultiplier);
            npc.defense = (int)(npc.defense * defenseMultiplier);

            //things that already have no kb only get it for kb adding modifiers
            if (npc.knockBackResist == 0 && kbMultiplier > 1)
            {
                npc.knockBackResist = kbMultiplier;
            }
            else
            {
                npc.knockBackResist = npc.knockBackResist * kbMultiplier;
            }


            //vanilla rescale code
            originalScale = npc.scale;
            int num3 = (int)((float)npc.width * npc.scale);
            int num4 = (int)((float)npc.height * npc.scale);
            npc.position.X = npc.position.X + (float)(num3 / 2);
            npc.position.Y = npc.position.Y + (float)num4;
            npc.scale = originalScale * sizeMultiplier;
            npc.width = (int)((float)npc.width * npc.scale);
            npc.height = (int)((float)npc.height * npc.scale);
            if (npc.height == 16 || npc.height == 32)
            {
                npc.height++;
            }
            npc.position.X = npc.position.X - (float)(npc.width / 2);
            npc.position.Y = npc.position.Y - (float)npc.height;

            if (originalName == "")
            {
                originalName = npc.FullName;
            }

            npc.GivenName = name + " " + originalName;
        }

        public static void Aura(NPC npc, float distance, bool reverse = false, int dustid = DustID.GoldFlame)
        {
            const int baseDistance = 500;
            const int baseMax = 20;

            int dustMax = (int)(distance / baseDistance * baseMax);
            if (dustMax < 10)
                dustMax = 10;
            if (dustMax > 40)
                dustMax = 40;

            float dustScale = distance / baseDistance;
            if (dustScale < 0.75f)
                dustScale = 0.75f;
            if (dustScale > 2f)
                dustScale = 2f;

            for (int i = 0; i < dustMax; i++)
            {
                Vector2 spawnPos = npc.Center + Main.rand.NextVector2CircularEdge(distance, distance);
                
                Dust dust = Main.dust[Dust.NewDust(spawnPos, 0, 0, dustid, 0, 0, 100, Color.White, dustScale)];
                dust.velocity = npc.velocity;
                if (Main.rand.Next(3) == 0)
                {
                    dust.velocity += Vector2.Normalize(npc.Center - dust.position) * Main.rand.NextFloat(5f) * (reverse ? -1f : 1f);
                    dust.position += dust.velocity * 5f;
                }
                dust.noGravity = true;
            }
        }

        public virtual bool PreAI(NPC npc)
        {
            return true;
        }

        public virtual void AI(NPC npc)
        {
        }

        public virtual void PostAI(NPC npc)
        {
        }

        public virtual void UpdateLifeRegen(NPC npc, ref int damage)
        {
        
        }

        public virtual void OnHitByItem(NPC npc, Player player)
        {
        }

        public virtual void OnHitByProjectile(NPC npc, Projectile projectile)
        {
        }

        public virtual void OnHitPlayer(NPC npc, Player target)
        {
        }

        public virtual void ModifyHitByItem(NPC npc, Player player, Item item, ref int damage, ref float knockback)
        {
        }

        public virtual void ModifyHitByProjectile(NPC npc, Projectile projectile, ref int damage, ref float knockback)
        {
        }

        public virtual bool PreNPCLoot(NPC npc)
        {
            return true;
        }

        public virtual void NPCLoot(NPC npc)
        {
        }

        public virtual void GetChat(NPC npc, ref string chat)
        {
        }

        public virtual Color? GetAlpha()
        {
            return null;
        }

        public virtual bool PreDraw(NPC npc, SpriteBatch spriteBatch, Color drawColor)
        {
            return true;
        }
    }
}
