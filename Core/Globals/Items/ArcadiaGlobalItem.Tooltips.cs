using System.Collections.Generic;

using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Arcadia.Core.Globals.Items;

public partial class ArcadiaGlobalItem : GlobalItem
{
    private static string[] MainTooltipBackupInsertionPositions =
    [
        "Material",
        "Consumable",
        "Ammo",
        "Placeable",
        "UseMana",
        "HealMana",
        "HealLife",
        "TileBoost",
        "HammerPower",
        "AxePower",
        "PickPower",
        "Defense",
        "Vanity",
        "Quest",
        "WandConsumes",
        "Equipable",
        "BaitPower",
        "NeedsBait",
        "FishingPower",
        "Knockback",
        "NoTransfer",
        "FavoriteDesc",
        "ItemName"
    ];

    private static string[] TooltipInsertionPositions =
    [
        "Expert",
        "SetBonus",
        "PrefixAccMeleeSpeed",
        "PrefixAccMoveSpeed",
        "PrefixAccDamage",
        "PrefixAccCritChance",
        "PrefixAccMaxMana",
        "PrefixAccDefense",
        "PrefixKnockback",
        "PrefixShootSpeed",
        "PrefixSize",
        "PrefixUseMana",
        "PrefixCritChance",
        "PrefixSpeed",
        "PrefixDamage",
        "OneDropLogo",
        "BuffTime",
        "WellFedExpert",
        "EtherianManaWarning"
    ];

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        // Get the first index, last index, and total count of standard vanilla tooltip lines.
        int firstTooltipIndex = -1;
        int lastTooltipIndex = -1;
        int standardTooltipCount = 0;
        for (int i = 0; i < tooltips.Count; i++)
        {
            if (tooltips[i].Name.StartsWith("Tooltip"))
            {
                if (firstTooltipIndex == -1)
                    firstTooltipIndex = i;

                lastTooltipIndex = i;
                standardTooltipCount++;
            }
        }
        if (firstTooltipIndex == -1)
        {
            foreach (string lineName in MainTooltipBackupInsertionPositions)
            {
                int idx = tooltips.FindIndex((line) => line.Name == lineName);
                if (idx != -1)
                {
                    firstTooltipIndex = lastTooltipIndex = idx;
                    break;
                }
            }
        }

        // The best possible position is identified using a separate backwards search.
        int tooltipIndex = -1;
        foreach (string lineName in TooltipInsertionPositions)
        {
            int idx = tooltips.FindIndex((line) => line.Name == lineName);
            if (idx != -1)
            {
                tooltipIndex = idx;
                break;
            }
        }

        if (DeveloperItem)
        {
            LocalizedText developerText = ArcadiaUtils.GetText("UI.DeveloperItemTooltip");
            string coloredText = ArcadiaUtils.ColorMessage(developerText.Value, ArcadiaUtils.DeveloperItemColor);
            TooltipLine devLine = new(Mod, "Arcadia:DeveloperItem", coloredText);
            tooltips.Insert(++tooltipIndex, devLine);
        }
    }
}
