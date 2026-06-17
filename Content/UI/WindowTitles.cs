using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using ReLogic.OS;

using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Arcadia.Content.UI;

public class WindowTitles : ModSystem
{
    private static LocalizedText _arcadiaModifiedText;
    private static bool loaded;

    public override void PostSetupContent()
    {
        if (Main.dedServ)
            return;

        Main.QueueMainThreadAction(() =>
        {
            var vanillaTitles = Language.FindAll(new Regex("^GameTitle\\.")).ToList();
            var customTitles = Language.FindAll(new Regex("^Mods\\.Arcadia\\.UI\\.WindowTitle\\.")).ToList();

            var allTitles = new List<LocalizedText>();
            allTitles.AddRange(vanillaTitles);
            allTitles.AddRange(customTitles);

            _arcadiaModifiedText ??= allTitles[Main.rand.Next(allTitles.Count)];

            Platform.Get<IWindowService>().SetUnicodeTitle(Main.instance.Window, _arcadiaModifiedText.Value);
            Platform.Get<IWindowService>().SetIcon(Main.instance.Window);

            loaded = true;
        });
    }

    public override void Unload()
    {
        if (Main.dedServ)
            return;

        Main.QueueMainThreadAction(() =>
        {
            Platform.Get<IWindowService>().SetUnicodeTitle(Main.instance.Window, Lang.GetRandomGameTitle());
            Platform.Get<IWindowService>().SetIcon(Main.instance.Window);

            _arcadiaModifiedText = null;
            loaded = false;
        });

    }
}
