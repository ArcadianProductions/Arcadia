using log4net;
using Terraria.ModLoader;

namespace Arcadia;

public class Arcadia : Mod
{
    private static Arcadia instance;
    public static Arcadia Instance => instance ??= ModContent.GetInstance<Arcadia>();

    // This should not be named "Logger", as it hides the instance property "Logger".
    internal static ILog Log => Instance.Logger;

    public bool DebugMode;

    public override void Load()
    {
        instance = this;

        #if DEBUG
        DebugMode = true;
        #endif
    }

    public override void Unload() => instance = null;
}
