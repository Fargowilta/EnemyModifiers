using Terraria;
using Terraria.Audio;

namespace FargoEnemyModifiers.Modifiers
{
    public class Devouring : Modifier
    {
        protected int baseHeight = 0;
        protected int baseWidth = 0;

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


                otherNpc.GetGlobalNPC<EnemyModifiersGlobalNPC>().DropLoot = false;
                otherNpc.StrikeNPC(otherNpc.lifeMax, 0, 1, true);

                Main.PlaySound(new LegacySoundStyle(4, 13), npc.Center);
            }
        }
    }
}