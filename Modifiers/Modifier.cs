using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;

namespace FargoEnemyModifiers.Modifiers
{
    public abstract class Modifier
    {
        public abstract string Name { get; }

        public abstract string Description
        {
            get;
        }

        public abstract int Rarity
        {
            get;
        }

        public virtual bool ExtraCondition(NPC npc)
        {
            return true;
        }

        public virtual bool AllowAnnounceModifier()
        {
            return true;
        }

        public virtual bool ColorChanger { get; set; } = false;


        public virtual float HealthMultiplier { get; set; } = 1f;

        public virtual float DamageMultiplier { get; set; } = 1f;

        public virtual float DefenseMultiplier { get; set; } = 1f;

        public virtual float SizeMultiplier { get; set; } = 1f;

        public virtual float SpeedMultiplier { get; set; } = 1f;

        public virtual float KnockBackMultiplier { get; set; } = 1f;

        public virtual int LootMultiplier { get; set; } = 0;

        public float originalScale;

        public virtual bool AutoLoad() => true;

        public virtual void Setup(NPC npc)
        {
        }

        public virtual void UpdateModifierStats(NPC npc)
        {
            npc.lifeMax = (int)(npc.lifeMax * HealthMultiplier);
            npc.life = npc.lifeMax;
            npc.defDamage = (int)(npc.defDamage * DamageMultiplier);
            npc.damage = (int)(npc.damage * DamageMultiplier);
            npc.defDefense = (int)(npc.defDefense * DefenseMultiplier);
            npc.defense = (int)(npc.defense * DefenseMultiplier);

            //things that already have no kb only get it for kb adding modifiers
            if (npc.knockBackResist == 0 && KnockBackMultiplier > 1)
                npc.knockBackResist = KnockBackMultiplier;
            else
                npc.knockBackResist *= KnockBackMultiplier;


            //vanilla rescale code
            originalScale = npc.scale;

            int scaledWidth = (int)(npc.width * npc.scale);
            int scaledHeight = (int)(npc.height * npc.scale);

            npc.position.X += scaledWidth / 2f;
            npc.position.Y += scaledHeight;
            npc.scale = originalScale * SizeMultiplier;
            npc.width = (int)(npc.width * npc.scale);
            npc.height = (int)(npc.height * npc.scale);

            if (npc.height == 16 || npc.height == 32) 
                npc.height++;

            npc.position.X -= npc.width / 2f;
            npc.position.Y -= npc.height;
        }

        public static void CreateAura(NPC npc, float distance, int dustId, Color color)
        {
            const int baseDistance = 500;
            const int baseMax = 20;

            int dustMax = Utils.Clamp((int)(distance / baseDistance * baseMax), 10, 40);
            float dustScale = Utils.Clamp(distance / baseDistance, 0.75f, 2f);

            for (int i = 0; i < dustMax; i++)
            {
                Vector2 spawnPos = npc.Center + Main.rand.NextVector2CircularEdge(distance, distance);
                Dust dust = Dust.NewDustDirect(spawnPos, 0, 0, dustId, 0, 0, 100, color, dustScale);
                dust.velocity = npc.velocity;

                if (Main.rand.NextBool(3))
                {
                    dust.velocity += Vector2.Normalize(npc.Center - dust.position) * Main.rand.NextFloat(5f) * (1f);
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

        public virtual void ModifyHitByItem(NPC npc, Player player, Item item, ref NPC.HitModifiers modifiers)
        {
        }

        public virtual void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers)
        {
        }

        public virtual void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
        {
        }

        public virtual bool PreNPCLoot(NPC npc)
        {
            return true;
        }

        public virtual bool CheckDead(NPC npc)
        {
            return true;
        }

        public virtual bool SpecialOnKill(NPC npc)
        {
            return false;
        }

        public virtual void OnKill(NPC npc)
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
