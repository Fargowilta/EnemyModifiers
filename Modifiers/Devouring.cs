using Terraria;
using Terraria.Audio;

namespace FargoEnemyModifiers.Modifiers
{
    public class Devouring : Modifier
    {
        protected int baseHeight;
        protected int baseWidth;

        public override string Name => "Devouring";

        public override void Setup(NPC npc)
        {
            baseWidth = npc.width;
            baseHeight = npc.height;
        }

        public override void AI(NPC npc)
        {
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC otherNpc = Main.npc[i];

<<<<<<< HEAD
                if (!otherNpc.active || npc.whoAmI == otherNpc.whoAmI || otherNpc.realLife == npc.whoAmI ||
                    npc.realLife == otherNpc.whoAmI || !otherNpc.Hitbox.Intersects(npc.Hitbox) ||
                    otherNpc.lifeMax > npc.lifeMax)
                    continue;

                int lifeGained = otherNpc.lifeMax / 4;
                npc.lifeMax += lifeGained;
                npc.life += lifeGained;
                npc.HealEffect(lifeGained);

                npc.damage = (int) (npc.damage * 1.05f);

                npc.position = npc.Center;
                npc.scale *= 1.1f;
                npc.width = (int) (baseWidth * npc.scale);
                npc.height = (int) (baseHeight * npc.scale);
                npc.Center = npc.position;
=======
                if (otherNpc.active && npc.whoAmI != otherNpc.whoAmI && otherNpc.realLife != npc.whoAmI && npc.realLife != otherNpc.whoAmI
                    && otherNpc.Hitbox.Intersects(npc.Hitbox) && otherNpc.lifeMax <= npc.lifeMax
                    && !otherNpc.dontTakeDamage && !otherNpc.immortal)
                {
                    int lifeGained = otherNpc.lifeMax / 4;
                    npc.lifeMax += lifeGained;
                    npc.life += lifeGained;
                    npc.HealEffect(lifeGained);

                    npc.defDamage = (int)(npc.defDamage * 1.05f);
                    npc.damage = (int)(npc.damage * 1.05f);
>>>>>>> 9e94e3e16eca90b12f41c32c4a5c9fe4b429a9fd


                otherNpc.GetGlobalNPC<EnemyModifiersGlobalNPC>().DropLoot = false;
                otherNpc.StrikeNPC(otherNpc.lifeMax, 0, 1, true);

                Main.PlaySound(new LegacySoundStyle(4, 13), npc.Center);
            }
        }
    }
}