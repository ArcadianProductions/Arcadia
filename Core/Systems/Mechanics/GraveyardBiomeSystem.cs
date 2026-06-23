using Terraria;
using Terraria.ModLoader;

namespace Arcadia.Core.Systems;

internal sealed class GraveyardBiomeSystem : ModSystem
{
    public override void OnModLoad()
    {
        // Make graveyard biomes require more gravestones.
        SceneMetrics.GraveyardTileMax = 60;
        SceneMetrics.GraveyardTileMin = 40;
        SceneMetrics.GraveyardTileThreshold = 52;
    }

    public override void Unload()
    {
        SceneMetrics.GraveyardTileMax = 36;
        SceneMetrics.GraveyardTileMin = 16;
        SceneMetrics.GraveyardTileThreshold = 28;
    }
}
