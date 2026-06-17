using System.Collections.Generic;
using Arcadia.Core.Globals.Items;
using Arcadia.Core.Systems;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Arcadia.Content.Items.Tools;

public class RealityWarper : ModItem
{
    public override void SetDefaults()
    {
        Item.width = Item.height = 32;
        Item.value = Item.sellPrice(1);
        Item.rare = ItemRarityID.Purple;
    }

    public override void ModifyTooltips(List<TooltipLine> list) =>
        list.IntegrateHotkey(KeybindSystem.RealityWarperKey);

    public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup) =>
        itemGroup = (ContentSamples.CreativeHelper.ItemGroup)ArcadiaResearchSorting.OtherTools;

    public override void UpdateInventory(Player player) =>
        player.Arcadia().RealityWarper = true;

    public override void AddRecipes()
    {
        CreateRecipe().
            AddIngredient(ItemID.RodOfHarmony).
            AddIngredient(ItemID.FragmentNebula, 30).
            AddIngredient(ItemID.FragmentStardust, 30).
            AddTile(TileID.LunarCraftingStation).
            Register();
    }
}
