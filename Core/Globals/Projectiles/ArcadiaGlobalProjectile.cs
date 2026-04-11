using Terraria.ModLoader;

namespace Arcadia.Core.Globals.Projectiles;

public class ArcadiaGlobalProjectile : GlobalProjectile
{
    public override bool InstancePerEntity => true;

    /// <summary>
    /// How many times this projectile has pierced an enemy which applies pierce resist.<br/>
    /// Used for calculating pierce resist damage reduction.
    /// </summary>
    public int TimesPierced;
}
