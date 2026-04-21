using Microsoft.Xna.Framework.Graphics;

using ReLogic.Content;

using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace Arcadia.Assets.Effects;

[Autoload(Side = ModSide.Client)]
public sealed class ArcadiaShaders : ModSystem
{
    private const string ShaderPath = "Effects/";
    internal const string ShaderPrefix = "Arcadia:";

    internal static Asset<Effect> StandardPrimitiveShader;

    public override void PostSetupContent()
    {
        AssetRepository assets = Arcadia.Instance.Assets;

        // Shorthand to load shaders immediately.
        // Strings provided to LoadShader are the .xnb file paths.
        Asset<Effect> LoadShader(string path) => assets.Request<Effect>($"{ShaderPath}{path}", AssetRequestMode.ImmediateLoad);

        StandardPrimitiveShader = LoadShader("StandardPrimitiveShader");
        RegisterShader(StandardPrimitiveShader, "PrimitivePass", "StandardPrimitiveShader");
    }

    // Shorthand to register a loaded shader in Terraria's graphics engine.
    // All shaders registered this way are accessible under GameShaders.Misc.
    // They will use the prefix described above.
    private static void RegisterShader(Asset<Effect> shader, string passName, string registrationName)
    {
        MiscShaderData passParamRegistration = new(shader, passName);
        GameShaders.Misc[$"{ShaderPrefix}{registrationName}"] = passParamRegistration;
    }
}
