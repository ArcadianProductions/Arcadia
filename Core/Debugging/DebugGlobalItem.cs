using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;

using Terraria;
using Terraria.ModLoader;

namespace Arcadia.Core.Debugging;

/// <summary>
///     Cancels various behaviors of debug items when debug mode is not enabled.
/// </summary>
public class DebugGlobalItem : GlobalItem
{
    public override bool AppliesToEntity(Item entity, bool lateInstantiation)
    {
        if (entity.ModItem is null)
            return false;

        return Attribute.IsDefined(entity.ModItem.GetType(), typeof(DebugItemAttribute));
    }

    public override void UpdateInventory(Item item, Player player)
    {
        if (!Arcadia.Instance.DebugMode)
            item.TurnToAir();
    }

    public override bool PreDrawInInventory(Item item, SpriteBatch spriteBatch,
        Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale) => Arcadia.Instance.DebugMode;

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        if (!Arcadia.Instance.DebugMode)
        {
            tooltips.Clear();

            TooltipLine line = new(Mod, "DebugLine", "[DISABLED]\nDebug only!")
            {
                OverrideColor = Color.Red
            };

            tooltips.Add(line);
        }
    }

    public override void Update(Item item, ref float gravity, ref float maxFallSpeed)
    {
        if (!Arcadia.Instance.DebugMode)
            item.TurnToAir();
    }

    public override bool CanPickup(Item item, Player player) => Arcadia.Instance.DebugMode;

    public override bool PreDrawInWorld(Item item, SpriteBatch spriteBatch, Color lightColor, Color alphaColor,
        ref float rotation, ref float scale, int whoAmI) => Arcadia.Instance.DebugMode;
}
