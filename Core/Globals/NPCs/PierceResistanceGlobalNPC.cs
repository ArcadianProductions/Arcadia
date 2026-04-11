using System;
using System.Collections.Generic;
using System.Reflection;

using Arcadia.Core.Balancing;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Arcadia.Core.Globals.NPCs;

public sealed class PierceResistanceGlobalNPC : GlobalNPC
{
    internal static HashSet<int> ExemptProjectiles;
    internal static HashSet<int> PierceResistNPC;
    internal static HashSet<int> SingleHitboxNPC;
    internal static Dictionary<int, bool> SingleHitboxExemptProjectiles;

    public override void Load()
    {
        ExemptProjectiles = [];
        PierceResistNPC = [];
        SingleHitboxNPC = [];
        SingleHitboxExemptProjectiles = [];
    }

    public override void Unload()
    {
        ExemptProjectiles?.Clear();
        ExemptProjectiles = null;
        PierceResistNPC?.Clear();
        PierceResistNPC = null;
        SingleHitboxNPC?.Clear();
        SingleHitboxNPC = null;
        SingleHitboxExemptProjectiles?.Clear();
        SingleHitboxExemptProjectiles = null;
    }

    public override void SetStaticDefaults()
    {
        // Specific vanilla NPCs with pierce resistance.
        PierceResistNPC.Add(NPCID.EaterofWorldsHead);
        PierceResistNPC.Add(NPCID.EaterofWorldsBody);
        PierceResistNPC.Add(NPCID.EaterofWorldsTail);
        PierceResistNPC.Add(NPCID.Creeper);
        PierceResistNPC.Add(NPCID.TheDestroyer);
        PierceResistNPC.Add(NPCID.TheDestroyerBody);
        PierceResistNPC.Add(NPCID.TheDestroyerTail);

        // Specific vanilla projectile exemptions.
        ExemptProjectiles.Add(ProjectileID.Arkhalis);
        ExemptProjectiles.Add(ProjectileID.ChargedBlasterLaser);
        ExemptProjectiles.Add(ProjectileID.ClingerStaff);
        ExemptProjectiles.Add(ProjectileID.FinalFractal);
        ExemptProjectiles.Add(ProjectileID.FlyingKnife);
        ExemptProjectiles.Add(ProjectileID.HallowJoustingLance);
        ExemptProjectiles.Add(ProjectileID.JoustingLance);
        ExemptProjectiles.Add(ProjectileID.LastPrismLaser);
        ExemptProjectiles.Add(ProjectileID.MonkStaffT3);
        ExemptProjectiles.Add(ProjectileID.PiercingStarlight);
        ExemptProjectiles.Add(ProjectileID.ShadowJoustingLance);
        ExemptProjectiles.Add(ProjectileID.Terragrim);

        // Specific vanilla projectile single hitbox exemptions.
        SingleHitboxExemptProjectiles[ProjectileID.NettleBurstEnd] = true;
        SingleHitboxExemptProjectiles[ProjectileID.NettleBurstLeft] = true;
        SingleHitboxExemptProjectiles[ProjectileID.NettleBurstRight] = true;
        SingleHitboxExemptProjectiles[ProjectileID.PrincessWeapon] = true;
        SingleHitboxExemptProjectiles[ProjectileID.ToxicCloud] = true;
        SingleHitboxExemptProjectiles[ProjectileID.ToxicCloud2] = true;
        SingleHitboxExemptProjectiles[ProjectileID.ToxicCloud3] = true;

        var projectiles = GetContent<ModProjectile>();
        foreach (var projectile in projectiles)
        {
            try
            {
                var type = projectile.GetType();
                var pierceResistException = type.GetCustomAttribute<PierceResistExceptionAttribute>();
                if (pierceResistException == null)
                    continue;

                int projectileType = projectile.Type;
                if (pierceResistException.OnlyForSingleHitbox)
                {
                    SingleHitboxExemptProjectiles[projectileType] = true;
                    continue;
                }
                else
                {
                    ExemptProjectiles.Add(projectileType);
                }
            }
            catch (Exception e)
            {
                Arcadia.Log.Error($"Exception thrown while evaluating type \"{projectile.FullName}\": {e}");
            }
        }
    }

    public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers)
    {
        // Skip if NPC does not have pierce resistance or the projectile is exempt.
        if (!PierceResistNPC.Contains(npc.type) || ExemptProjectiles.Contains(projectile.type))
            return;

        // Skip if the projectile is exempt on single hitboxes and the NPC is a single hitbox.
        if (SingleHitboxExemptProjectiles.TryGetValue(projectile.type, out bool isSingleHitboxExempt) && isSingleHitboxExempt && SingleHitboxNPC.Contains(npc.type))
            return;

        PierceResistGlobal(projectile, npc, ref modifiers);
    }

    // Generalized pierce resistance that stacks with all other resistances for some specific bosses defined in a list.
    private static void PierceResistGlobal(Projectile projectile, NPC npc, ref NPC.HitModifiers modifiers)
    {
        float damageReduction = projectile.Arcadia().TimesPierced * BalancingConstants.PierceResistHarshness;
        if (damageReduction > BalancingConstants.PierceResistCap)
            damageReduction = BalancingConstants.PierceResistCap;

        modifiers.FinalDamage *= 1f - damageReduction;

        bool aiStyleExempt = projectile.aiStyle == ProjAIStyleID.Flail || projectile.aiStyle == ProjAIStyleID.MechanicalPiranha || projectile.aiStyle == ProjAIStyleID.Yoyo;
        if ((projectile.penetrate > 1 || projectile.penetrate == -1) && !projectile.CountsAsClass<SummonDamageClass>() && !aiStyleExempt)
            projectile.Arcadia().TimesPierced++;
    }
}
