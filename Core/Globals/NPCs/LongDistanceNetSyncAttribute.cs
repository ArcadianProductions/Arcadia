using System;

namespace Arcadia.Core.Globals.NPCs;

// This code is originally from the Calamity Mod.
// Credits are given to the Calamity Team.

/// <summary>
/// This attribute allows a ModNPC to always sync for their position and rotation data at least every 45 frames.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public sealed class LongDistanceNetSyncAttribute : Attribute
{
    /// <summary>
    /// Syncs this NPC to other NPC's sync frame.
    /// <para>If this is not present, we cannot properly sync full NPC body parts in same frame (this is important for worm-type NPCs)!</para>
    /// </summary>
    public Type SyncWith { get; set; }

    public LongDistanceNetSyncAttribute() { }
}
