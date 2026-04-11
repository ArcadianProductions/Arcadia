using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Arcadia.Core.Globals.Items;

public partial class ArcadiaGlobalItem : GlobalItem
{
    public override bool InstancePerEntity => true;

    private BitsByte flag = 0;

    /// <summary>
    /// Set to true if this item is dedicated to an Arcadia developer.<br/>
    /// Adds "- Developer Item -" to the bottom of the item's tooltip.
    /// </summary>
    public bool DeveloperItem
    {
        get => flag[0];
        set => flag[0] = value;
    }

    public override GlobalItem Clone(Item? from, Item to)
    {
        ArcadiaGlobalItem clone = (ArcadiaGlobalItem)base.Clone(from, to);

        clone.flag = flag;

        return clone;
    }

    public override void SetDefaults(Item item)
    {
        if (item.type == ItemID.FlameWakerBoots)
            item.vanity = false;

        if (item.type == ItemID.VolatileGelatin)
            item.rare = ItemRarityID.Pink;
    }
}
