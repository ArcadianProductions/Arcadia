using Arcadia.Core.Globals.Players;
using Arcadia.Core.Globals.Projectiles;
using Terraria;

namespace Arcadia;

public static partial class ArcadiaUtils
{
    public static ArcadiaPlayer Arcadia(this Player player) => player.GetModPlayer<ArcadiaPlayer>();

    public static ArcadiaGlobalProjectile Arcadia(this Projectile projectile) => projectile.GetGlobalProjectile<ArcadiaGlobalProjectile>();
}
