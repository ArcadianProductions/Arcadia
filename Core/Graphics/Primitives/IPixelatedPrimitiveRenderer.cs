using Arcadia.Core.Graphics.DrawLayers;
using Microsoft.Xna.Framework.Graphics;

namespace Arcadia.Core.Graphics.Primitives;

/// <summary>
/// Use to sucessfully render primitives with pixelation with an NPC or projectile.
/// </summary>
public interface IPixelatedPrimitiveRenderer
{
    /// <summary>
    /// The layer to render the primitive(s) to.
    /// </summary>
    GeneralDrawLayer LayerToRenderTo => GeneralDrawLayer.BeforeProjectiles;

    /// <summary>
    /// Render primitives that use pixelation here.
    /// </summary>
    void RenderPixelatedPrimitives(SpriteBatch spriteBatch, GeneralDrawLayer layer);
}
