using FargoEnemyModifiers.Utilities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace FargoEnemyModifiers.Modifiers
{
    public class Flaming : Modifier
    {
        public override string Key => "Flaming";
        public override RarityID Rarity => RarityID.Uncommon;
        public override AiOverrideStyle AiOverride => AiOverrideStyle.PreVanilla;

        public override float SpeedMultiplier => 1.5f;

        private int counter = 0;

        public override void AI(NPC npc)
        {
            npc.buffImmune[BuffID.OnFire] = false;
            npc.AddBuff(BuffID.OnFire, 2);

            if (Main.netMode == NetmodeID.MultiplayerClient)
                return;
            if (counter++ >= 60)
            {
                Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center, Vector2.Zero, ProjectileID.GreekFire1, npc.damage / 5, 0);
                counter = 0;
            }
        }
    }
}