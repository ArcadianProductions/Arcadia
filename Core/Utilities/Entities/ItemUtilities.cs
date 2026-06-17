using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Arcadia;

public static partial class ArcadiaUtils
{
    internal static readonly Color DeveloperItemColor = new(128, 0, 128);

    /// <summary>
    /// Shortcut for automatically placing one keybind within a tooltip. Requires the "[KEY]" string to be replaced.
    /// </summary>
    /// <param name="tooltips">The tooltip list provided to a <b>ModifyTooltips</b> TML hook.</param>
    /// <param name="mhk">The ModKeybind to integrate into the tooltip.</param>
    public static void IntegrateHotkey(this List<TooltipLine> tooltips, ModKeybind mhk)
    {
        if (Main.dedServ || mhk is null)
            return;

        string finalKey = mhk.TooltipHotkeyString();
        tooltips.FindAndReplace("[KEY]", finalKey);
    }

    /// <summary>
    /// Converts the given ModKeybind into a string for insertion into item tooltips.
    /// This allows the user's actual keybind choices to be shown to them in tooltips.
    /// </summary>
    /// <param name="mhk">The ModKeybind to convert to a string.</param>
    /// <returns>The tooltip as a string that can be placed in a tooltip.</returns>
    public static string TooltipHotkeyString(this ModKeybind mhk)
    {
        if (Main.dedServ || mhk is null)
            return "";

        List<string> keys = mhk.GetAssignedKeysOrEmpty();
        if (keys.Count == 0)
            return GetText("Misc.HotkeyNotBound").Value;
        else
        {
            StringBuilder sb = new(16);
            sb.Append(keys[0]);

            // In almost all cases, this code won't run, because there won't be multiple bindings for the hotkey.
            // However, just in case...
            for (int i = 1; i < keys.Count; ++i)
                sb.Append(" / ").Append(keys[i]);
            return sb.ToString();
        }
    }

    /// <summary>
    /// Shortcut for finding a specific string in the tooltip and replacing it with a new string.
    /// Typically used for dynamic tooltip updating. Consider overriding Tooltip or using String. Format for applying constants.
    /// </summary>
    /// <param name="tooltips">The tooltip list provided to a <b>ModifyTooltips</b> hook.</param>
    /// <param name="replacedKey">The key to be replaced.</param>
    /// <param name="replacedKey">The new key.</param>
    public static void FindAndReplace(this List<TooltipLine> tooltips, string replacedKey, string newKey)
    {
        TooltipLine line = tooltips.FirstOrDefault(x => x.Mod == "Terraria" && x.Text.Contains(replacedKey));
        line?.Text = line.Text.Replace(replacedKey, newKey);
    }

    /// <summary>
    /// Shortcut for finding all of a specific string in the tooltip and replacing it with a new string.
    /// Typically used for dynamic tooltip updating. Consider overriding Tooltip or using String.Format for applying constants.
    /// </summary>
    /// <param name="tooltips">The tooltip list provided to a <b>ModifyTooltips</b> hook.</param>
    /// <param name="replacedKey">The key to be replaced.</param>
    /// <param name="replacedKey">The new key.</param>
    public static void FindAndReplaceAll(this List<TooltipLine> tooltips, string replacedKey, string newKey)
    {
        foreach (TooltipLine line in tooltips)
            line.Text = line.Text.Replace(replacedKey, newKey);
    }
}
