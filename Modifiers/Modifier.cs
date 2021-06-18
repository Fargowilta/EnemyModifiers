using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;

namespace FargoEnemyModifiers.Modifiers
{
    public abstract class Modifier
    {
        public abstract string Name { get; }

        public virtual float HealthMultiplier { get; set; } = 1f;

        public virtual float DamageMultiplier { get; set; } = 1f;

        public virtual float DefenseMultiplier { get; set; } = 1f;

        public virtual float SizeMultiplier { get; set; } = 1f;

        public virtual float SpeedMultiplier { get; set; } = 1f;

        public virtual float KnockBackMultiplier { get; set; } = 1f;

        public float originalScale;
        public string originalName = "";

        public virtual bool AutoLoad() => true;

        public virtual void Setup(NPC npc)
        {
        }

        public virtual void UpdateModifierStats(NPC npc)
        {
            npc.lifeMax = (int)(npc.lifeMax * HealthMultiplier);
            npc.life = npc.lifeMax;
<<<<<<< HEAD
            npc.damage = (int)(npc.damage * DamageMultiplier);
            npc.defense = (int)(npc.defense * DefenseMultiplier);
=======
            npc.defDamage = npc.defDamage * damageMultiplier;
            npc.damage = npc.damage * damageMultiplier;
            npc.defDefense = npc.defDefense * defenseMultiplier;
            npc.defense = npc.defense * defenseMultiplier;
>>>>>>> 9e94e3e16eca90b12f41c32c4a5c9fe4b429a9fd

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

            if (originalName == "") 
                originalName = npc.FullName;

            npc.GivenName = Name + " " + originalName;
        }

        public static void CreateAura(NPC npc, float distance, bool reverse = false, int dustId = DustID.GoldFlame)
        {
            const int baseDistance = 500;
            const int baseMax = 20;

            int dustMax = Utils.Clamp((int)(distance / baseDistance * baseMax), 10, 40);
            float dustScale = Utils.Clamp(distance / baseDistance, 0.75f, 2f);

            for (int i = 0; i < dustMax; i++)
            {
                Vector2 spawnPos = npc.Center + Main.rand.NextVector2CircularEdge(distance, distance);
                Dust dust = Dust.NewDustDirect(spawnPos, 0, 0, dustId, 0, 0, 100, Color.White, dustScale);
                dust.velocity = npc.velocity;

                if (Main.rand.NextBool(3))
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
