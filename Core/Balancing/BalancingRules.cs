using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace Arcadia.Core.Balancing;

public interface IBalancingRule
{
    bool AppliesTo(NPC npc, NPC.HitModifiers modifiers, Projectile projectile);

    void ApplyBalancingChange(NPC npc, ref NPC.HitModifiers modifiers);
}

public class ClassResistBalancingRule : IBalancingRule
{
    public float DamageMultiplier;
    public DamageClass ApplicableClass;

    public ClassResistBalancingRule(float damageMultiplier, DamageClass dc)
    {
        DamageMultiplier = damageMultiplier;
        ApplicableClass = dc;
    }

    public bool AppliesTo(NPC npc, NPC.HitModifiers modifiers, Projectile projectile) =>
        modifiers.DamageType.CountsAsClass(ApplicableClass);

    public void ApplyBalancingChange(NPC npc, ref NPC.HitModifiers modifiers) =>
        modifiers.SourceDamage *= DamageMultiplier;
}

public class NPCSpecificRequirementBalancingRule : IBalancingRule
{
    public NPCApplicationRequirement Requirement;
    public delegate bool NPCApplicationRequirement(NPC npc);

    public NPCSpecificRequirementBalancingRule(NPCApplicationRequirement npcApplicationRequirement) =>
        Requirement = npcApplicationRequirement;

    public bool AppliesTo(NPC npc, NPC.HitModifiers modifiers, Projectile projectile) => Requirement(npc);

    // This "balancing" rule doesn't actually perform any changes. It simply serves as a means of enforcing NPC-specific requirements, and should be used only as a filter.
    // As such, this method is empty.
    public void ApplyBalancingChange(NPC npc, ref NPC.HitModifiers modifiers) { }
}

public class PierceResistBalancingRule : IBalancingRule
{
    public float DamageMultiplier;

    public PierceResistBalancingRule(float damageMultiplier) =>
        DamageMultiplier = damageMultiplier;

    public bool AppliesTo(NPC npc, NPC.HitModifiers modifiers, Projectile projectile) =>
        projectile is not null && (projectile.maxPenetrate > 1 || projectile.maxPenetrate == -1);

    public void ApplyBalancingChange(NPC npc, ref NPC.HitModifiers modifiers) =>
        modifiers.SourceDamage *= DamageMultiplier;
}

public class ProjectileResistBalancingRule : IBalancingRule
{
    public float DamageMultiplier;
    public int[] ApplicableProjectileTypes;

    public ProjectileResistBalancingRule(float damageMultiplier, params int[] projTypes)
    {
        DamageMultiplier = damageMultiplier;
        ApplicableProjectileTypes = projTypes;
    }

    public bool AppliesTo(NPC npc, NPC.HitModifiers modifiers, Projectile projectile) =>
        projectile is not null && ApplicableProjectileTypes.Contains(projectile.type);

    public void ApplyBalancingChange(NPC npc, ref NPC.HitModifiers modifiers) =>
        modifiers.SourceDamage *= DamageMultiplier;
}

public class ProjectileSpecificRequirementBalancingRule : IBalancingRule
{
    public float DamageMultiplier;
    public ProjectileApplicationRequirement Requirement;
    public delegate bool ProjectileApplicationRequirement(Projectile proj);

    public ProjectileSpecificRequirementBalancingRule(float damageMultiplier, ProjectileApplicationRequirement projApplicationRequirement)
    {
        DamageMultiplier = damageMultiplier;
        Requirement = projApplicationRequirement;
    }

    public bool AppliesTo(NPC npc, NPC.HitModifiers modifiers, Projectile projectile) =>
        projectile is not null && Requirement(projectile);

    public void ApplyBalancingChange(NPC npc, ref NPC.HitModifiers modifiers) =>
        modifiers.SourceDamage *= DamageMultiplier;
}
