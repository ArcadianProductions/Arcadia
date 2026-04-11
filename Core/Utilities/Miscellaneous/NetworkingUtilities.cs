using Terraria;
using Terraria.ID;

namespace Arcadia;

public static partial class ArcadiaUtils
{
    /// <summary>
    ///     Syncs world updates on a server.
    /// </summary>
    public static void SyncWorld()
    {
        if (Main.dedServ)
            NetMessage.SendData(MessageID.WorldData);
    }
}
