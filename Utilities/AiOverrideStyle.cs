namespace FargoEnemyModifiers.Utilities
{
    public enum AiOverrideStyle
    {
        /// <summary>
        /// None - No override, the vanilla AI will run as normal
        /// </summary>
        None = 0,
        /// <summary>
        /// Override - Completely override the vanilla AI
        /// </summary>
        Override = 1,
        /// <summary>
        /// PreVanilla - Run the override AI before the vanilla AI
        /// </summary>
        PreVanilla = 2,
        /// <summary>
        /// PostVanilla - Run the override AI after the vanilla AI
        /// </summary>
        PostVanilla = 3
    }
}