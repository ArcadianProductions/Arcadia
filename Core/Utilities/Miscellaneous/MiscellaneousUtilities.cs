using System.Collections.Generic;
using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;

namespace Arcadia;

public static partial class ArcadiaUtils
{
    public static void AddWithCondition<T>(this List<T> list, T type, bool condition)
    {
        if (condition)
            list.Add(type);
    }

    public static void ChangeTime(bool changeToDay)
    {
        Main.time = 0D;
        Main.dayTime = changeToDay;
        SyncWorld();
    }

    public static List<string> GetAssignedKeysOrEmpty(this ModKeybind keybind, InputMode mode = InputMode.Keyboard)
    {
        if (keybind == null)
            return [];

        if (Main.dedServ)
            return [];

        try
        {
            return keybind.GetAssignedKeys(mode);
        }
        catch
        {
            return [];
        }
    }
}
