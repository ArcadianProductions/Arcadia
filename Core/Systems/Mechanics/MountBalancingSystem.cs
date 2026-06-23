using Terraria;
using Terraria.ModLoader;

namespace Arcadia.Core.Systems;

internal sealed class MountBalancingSystem : ModSystem
{
    public override void OnModLoad()
    {
        // Buff DCU's pickaxe power to equal post-Moon Lord pickaxe capabilities.
        Mount.drillPickPower = 225;
    }

    public override void Unload()
    {
        Mount.drillPickPower = 210;
    }
}
