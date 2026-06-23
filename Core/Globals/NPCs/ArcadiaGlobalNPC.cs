using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Arcadia.Core.Globals.NPCs;

public class ArcadiaGlobalNPC : GlobalNPC
{
    #region Variables
    /// <summary>
    /// The damage reduction of an NPC.
    /// </summary>
    public float DR { get; set; }

    /// <summary>
    /// Data structure used for storing the damage reduction values of NPCs.
    /// </summary>
    public static SortedDictionary<int, float> DRValues { get; set; }

    // AI variables.
    public const int ExtraAISlots = 10;
    public float[] ExtraAI = new float[ExtraAISlots];
    internal bool[] HasExtraAIBeenUsed = new bool[ExtraAISlots];

    public override bool InstancePerEntity => true;
    #endregion

    #region Cloning
    public override GlobalNPC Clone(NPC from, NPC to)
    {
        ArcadiaGlobalNPC clone = (ArcadiaGlobalNPC)base.Clone(from, to);

        clone.DR = DR;

        return clone;
    }
    #endregion

    #region Set Defaults
    public override void SetStaticDefaults()
    {
        NPCID.Sets.TrailingMode[NPCID.Plantera] = 1;
        NPCID.Sets.MPAllowedEnemies[NPCID.MoonLordCore] = true;
    }
    
    public override void SetDefaults(NPC npc)
    {
        for (int i = 0; i < ExtraAI.Length; i++)
            ExtraAI[i] = 0f;

        // Apply DR to vanilla NPCs.
        if (DRValues.ContainsKey(npc.type))
        {
            DRValues.TryGetValue(npc.type, out float newDR);
            DR = newDR;
        }

        if (npc.type == NPCID.WallofFleshEye)
            npc.netAlways = true;
    }
    #endregion

    #region AI
    public override bool PreAI(NPC npc)
    {
        // Disable networking offset effects.
        npc.netOffset = Vector2.Zero;

        return base.PreAI(npc);
    }

    public override void PostAI(NPC npc)
    {
        for (int i = 0; i < ExtraAI.Length; i++)
        {
            if (ExtraAI[i] != 0f)
                HasExtraAIBeenUsed[i] = true;
        }
    }
    #endregion

    #region Load/Unload
    public override void Load()
    {
        DRValues = new SortedDictionary<int, float> {
            { NPCID.CultistBoss, 0.15f },
            { NPCID.DukeFishron, 0.15f },
            { NPCID.Golem, 0.15f },
            { NPCID.GolemFistLeft, 0.15f },
            { NPCID.GolemFistRight, 0.15f },
            { NPCID.GolemHead, 0.15f },
            { NPCID.MoonLordCore, 0.15f },
            { NPCID.MoonLordHand, 0.15f },
            { NPCID.MoonLordHead, 0.15f },
            { NPCID.Plantera, 0.15f },
            { NPCID.HallowBoss, 0.15f },
            { NPCID.PrimeCannon, 0.2f },
            { NPCID.PrimeLaser, 0.2f },
            { NPCID.PrimeSaw, 0.2f },
            { NPCID.PrimeVice, 0.2f },
            { NPCID.Retinazer, 0.2f },
            { NPCID.SkeletronPrime, 0.2f },
            { NPCID.Spazmatism, 0.2f },
            { NPCID.TheDestroyer, 0.1f },
            { NPCID.TheDestroyerBody, 0.2f },
            { NPCID.TheDestroyerTail, 0.35f },
            { NPCID.WallofFlesh, 0.15f },
        };
    }

    public override void Unload()
    {
        DRValues?.Clear();
        DRValues = null;
    }
    #endregion
}
