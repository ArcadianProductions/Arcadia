using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.ModLoader;
using Terraria.ModLoader.Core;

namespace Arcadia;

public static partial class ArcadiaUtils
{
    public static IEnumerable<Type> GetEveryModsTypes() =>
        ModLoader.Mods.SelectMany(mod => AssemblyManager.GetLoadableTypes(mod.Code));

    public static bool IsSubclass(Type baseType, Type type, bool includeBaseType) =>
        type.IsSubclassOf(baseType) && !type.IsAbstract && (!includeBaseType && type != baseType);

    public static void IterateEveryModsTypes<T>(Action<Type> action, bool includeBaseType = false)
    {
        if (action is null)
            return;

        Type baseType = typeof(T);
        var types = GetEveryModsTypes().Where(t => IsSubclass(baseType, t, includeBaseType));

        foreach (var type in types)
            action.Invoke(type);
    }
}
