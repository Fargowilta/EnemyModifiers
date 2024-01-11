using FargoEnemyModifiers.NetCode;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace FargoEnemyModifiers.Modifiers
{
    public class Flaming : Modifier
    {
        public override string Name => "Flaming";
        public override string Description => "Spawns with a permanent On Fire debuff, leaves a trail of damaging flames, 50% increased speed";
        public override int Rarity => 2;
        public override AiOverrideStyle AiOverride => AiOverrideStyle.PreVanilla;

        public override float SpeedMultiplier => 1.5f;

        private int counter = 0;

        public override void AI(NPC npc)
        {
            npc.buffImmune[BuffID.OnFire] = false;
            npc.AddBuff(BuffID.OnFire, 2);

            if (counter++ >= 60)
            {
                Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center, Vector2.Zero, ProjectileID.GreekFire1, npc.damage / 5, 0);
                counter = 0;
            }
        }
    }
}