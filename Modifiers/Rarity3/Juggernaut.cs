using FargoEnemyModifiers.Utilities;
using Terraria;

namespace FargoEnemyModifiers.Modifiers
{
    public class Juggernaut : Modifier
    {
        public override ModifierID ModifierID => ModifierID.Juggernaut;
        public override string Key => "Juggernaut";
        public override RarityID Rarity => RarityID.Rare;

        public override float HealthMultiplier => 2.5f;
        public override float DefenseMultiplier => 1.5f;
        public override float DamageMultiplier => 1.5f;
        public override float SizeMultiplier => 1.6f;
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