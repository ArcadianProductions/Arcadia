using System.ComponentModel;
using Terraria.Localization;
using Terraria.ModLoader.Config;

namespace Arcadia.Core.Configuration;

[BackgroundColor(127, 0, 255, 215)]
public class ArcadiaServerConfig : ModConfig
{
    public static ArcadiaServerConfig Instance;

    public override ConfigScope Mode => ConfigScope.ServerSide;
    
    public override bool AcceptClientChanges(ModConfig pendingConfig, int whoAmI, ref NetworkText message)
    {
        if (whoAmI == 0)
            return true;
        if (whoAmI != 0)
        {
            message = ArcadiaUtils.GetText("Configs.ArcadiaServerConfig.Denied").ToNetworkText();
            return false;
        }
        return false;
    }

    [Header("Difficulty")]

    [BackgroundColor(153, 51, 255, 190)]
    [DefaultValue(true)]
    public bool NerfExpertDebuffs { get; set; }
}
