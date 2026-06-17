using System;

namespace Arcadia.Core.Globals.NPCs;

/// <summary>
/// This attribute gives an NPC pierce resistance.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public sealed class HasPierceResistAttribute : Attribute
{
    /// <summary>
    /// <para>Set this to true to indicate this NPC has a single large hitbox which typical piercing projectiles can hit multiple times.
    /// Certain weapons are exempt from pierce resistance on single-hitbox targets.</para>
    /// </summary>
    public bool SingleHitbox { get; }

    public HasPierceResistAttribute(bool singleHitbox = false) =>
        SingleHitbox = singleHitbox;
}
