using Terraria.ModLoader;

namespace Arcadia.Core.Systems;

public class KeybindSystem : ModSystem
{
    public static ModKeybind RealityWarperKey { get; private set; }

    public override void Load()
    {
        RealityWarperKey = KeybindLoader.RegisterKeybind(Mod, "RealityWarper", "Z");
    }

    public override void Unload()
    {
        RealityWarperKey = null;
    }
}
