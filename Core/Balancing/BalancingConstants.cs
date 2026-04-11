namespace Arcadia.Core.Balancing;

public static class BalancingConstants
{
    #region Pierce Resistance
    /// <summary>
    ///     Constant variable used to determine the percentage a projectile's damage is reduced by pierce resist on each hit.
    /// </summary>
    public const float PierceResistHarshness = 0.12f;

    /// <summary>
    ///     Constant variable used to determine the maximum percentage a projectile's damage that can be reduced by pierce resist.
    /// </summary>
    public const float PierceResistCap = 0.8f;
    #endregion

    #region Multiplayer Boss Health Scaling
    // These values are used to replace the vanilla 1.35f and 1.9166...f for two-player and three-player cases, respectively.
    // Editing them via reflection and whatnot will apply to newly spawned bosses during gameplay.
    public const float ExpertHealthScalingOverride_2Players = 1.75f;
    public const float ExpertHealthScalingOverride_3Players = 2.25f;
    #endregion
}
