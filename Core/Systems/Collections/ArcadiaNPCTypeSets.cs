using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;

namespace Arcadia.Core.Systems.Collections;

[ReinitializeDuringResizeArrays]
public static class ArcadiaNPCTypeSets
{
    private static SetFactory Factory = NPCID.Sets.Factory;

    public static bool[] Zombie = Factory.CreateNamedSet("Zombie")
    .Description("Contains different IDs from vanilla's Zombie set.")
    .RegisterBoolSet(NPCID.Zombie, NPCID.ArmedZombie, NPCID.BaldZombie, NPCID.PincushionZombie, NPCID.ArmedZombiePincussion, NPCID.SlimedZombie,
        NPCID.ArmedZombieSlimed, NPCID.SwampZombie, NPCID.ArmedZombieSwamp, NPCID.TwiggyZombie, NPCID.ArmedZombieTwiggy, NPCID.FemaleZombie, NPCID.ArmedZombieCenx,
        NPCID.ZombieRaincoat, NPCID.ZombieEskimo, NPCID.ArmedZombieEskimo, NPCID.MaggotZombie);

    public static List<int> Destroyer = [NPCID.TheDestroyer, NPCID.TheDestroyerBody, NPCID.TheDestroyerTail];

    public static List<int> EaterOfWorlds = [NPCID.EaterofWorldsHead, NPCID.EaterofWorldsBody, NPCID.EaterofWorldsTail];
}
