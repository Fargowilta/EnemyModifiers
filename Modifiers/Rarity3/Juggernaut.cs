using Terraria;

namespace FargoEnemyModifiers.Modifiers
{
    public class Juggernaut : Modifier
    {
        public override string Name => "Juggernaut";
        public override string Description => " HP increased by 150%. Damage, defense, and size increased by 25%. Immune to knockback, but has Sluggish built in (performs actions half as fast)";
        public override int Rarity => 3;

        public override float HealthMultiplier => 2.5f;

        public override float DefenseMultiplier => 1.25f;

        public override float DamageMultiplier => 1.25f;

        public override float SizeMultiplier => 1.25f;

        public override float KnockBackMultiplier => 0f;

        public override int LootMultiplier => 2;

        protected bool sluggishGo;

        public override bool PreAI(NPC npc)
        {
            if (sluggishGo)
            {
                sluggishGo = false;
                return true;
            }

            sluggishGo = true;
            return false;
        }
    }
}