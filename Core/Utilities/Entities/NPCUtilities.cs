using System;
using Terraria;

namespace Arcadia;

public static partial class ArcadiaUtils
{
    /// <summary>
    ///     Returns if there is a boss currently active.
    /// </summary>
    public static Tuple<bool, int> AnyBosses()
    {
        bool bossExists = false;
        int bossID = -1;
        foreach (NPC npc in Main.npc)
        {
            if (npc.active && npc.boss)
                bossExists = true;
            bossID = npc.type;
        }

        return Tuple.Create(bossExists, bossID);
    }
}
