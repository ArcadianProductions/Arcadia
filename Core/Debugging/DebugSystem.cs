using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Arcadia.Core.Debugging;

public class DebugSystem : ModSystem
{
    public override void Load()
    {
        On_Main.Update += DoUpdate;
        On_Main.DrawInterface += DrawDebugMenu;
    }

    private void DrawDebugMenu(On_Main.orig_DrawInterface orig, Main self, GameTime gameTime)
    {
        orig(self, gameTime);

        if (!Arcadia.Instance.DebugMode || Main.playerInventory)
            return;

        string menu = "Debug mode enabled!";

        Main.spriteBatch.Begin();
        Utils.DrawBorderString(Main.spriteBatch, menu, new Vector2(32, 120), new Color(230, 230, 255));
        Main.spriteBatch.End();
    }

    private void DoUpdate(On_Main.orig_Update orig, Main self, GameTime gameTime)
    {
        if (Main.LocalPlayer.position == Vector2.Zero || float.IsNaN(Main.LocalPlayer.position.X) || float.IsNaN(Main.LocalPlayer.position.Y))
            Main.LocalPlayer.position = new Vector2(Main.spawnTileX * 16, Main.spawnTileY * 16);

        orig(self, gameTime);
    }
}
