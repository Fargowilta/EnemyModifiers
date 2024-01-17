using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace FargoEnemyModifiers
{
    public class EnemyModifiersClientConfig : ModConfig
    {
        public static EnemyModifiersClientConfig Instance { get; private set; }

        public override ConfigScope Mode => ConfigScope.ClientSide;

        public override void OnLoaded()
        {
            Instance = this;
        }

        [Header("Announcements")]
        [DefaultValue(true)]
        public bool announcementsEnabled;

        [DefaultValue(false)]
        public bool announcementsForever;

        [DefaultValue(3f)]
        [Range(1f, 10f)]
        [Increment(0.1f)]
        public float announcementsDuration;
    }
}