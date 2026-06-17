using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace Arcadia.Core.Balancing;

public sealed class BalancingChangesManager : ModSystem
{
    internal static List<NPCBalancingChange> NPCSpecificBalancingChanges;

    public override void SetStaticDefaults()
    {
        #region Eater of Worlds
        // 50% resistance to Demon Scythe.
        //NPCSpecificBalancingChanges.AddRange(Bundle(ArcadiaNPCTypeSets.EaterOfWorlds, Do(new ProjectileResistBalancingRule(0.5f, ProjectileID.DemonScythe))));
        #endregion
    }

    public override void Unload() => NPCSpecificBalancingChanges = null;

    public static void ApplyFromProjectile(NPC npc, ref NPC.HitModifiers modifiers, Projectile proj)
    {
        // Apply NPC-specific balancing rules.
        foreach (NPCBalancingChange balanceChange in NPCSpecificBalancingChanges)
        {
            if (npc.type != balanceChange.NPCType)
                continue;

            foreach (IBalancingRule balancingRule in balanceChange.BalancingRules)
            {
                if (balancingRule.AppliesTo(npc, modifiers, proj))
                    balancingRule.ApplyBalancingChange(npc, ref modifiers);
            }
        }
    }

    // This function simply concatenates a bunch of balancing rules into an array.
    // It looks a lot nicer than constantly typing "new IBalancingRule[]".
    internal static IBalancingRule[] Do(params IBalancingRule[] rules) => rules;

    // Shorthand for bundling balancing balancing rules in such a way that it applies to multiple NPCs at once.
    // This is useful for preventing having to store the rules and apply it to each NPC individually.
    internal static NPCBalancingChange[] Bundle(IEnumerable<int> npcIDs, params IBalancingRule[] rules)
    {
        NPCBalancingChange[] changes = new NPCBalancingChange[npcIDs.Count()];
        for (int i = 0; i < changes.Length; i++)
            changes[i] = new NPCBalancingChange(npcIDs.ElementAt(i), rules);

        return changes;
    }
}
