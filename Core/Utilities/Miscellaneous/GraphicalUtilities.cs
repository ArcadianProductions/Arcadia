using System.Text;
using Microsoft.Xna.Framework;
using Terraria;

namespace Arcadia;

public static partial class ArcadiaUtils
{
    /// <summary>
    ///     Calculates perspective matrices for usage by vertex shaders.
    /// </summary>
    /// <param name="viewMatrix">The view matrix.</param>
    /// <param name="projectionMatrix">The projection matrix.</param>
    public static void CalculatePerspectiveMatricies(out Matrix viewMatrix, out Matrix projectionMatrix)
    {
        Vector2 zoom = Main.GameViewMatrix.Zoom;
        Matrix zoomScaleMatrix = Matrix.CreateScale(zoom.X, zoom.Y, 1f);

        // Screen bounds.
        int width = Main.instance.GraphicsDevice.Viewport.Width;
        int height = Main.instance.GraphicsDevice.Viewport.Height;

        // Get a matrix that aims towards the Z-axis (these calculations are relative to a 2D world).
        viewMatrix = Matrix.CreateLookAt(Vector3.Zero, Vector3.UnitZ, Vector3.Up);

        // Offset the matrix to the appropriate position.
        viewMatrix *= Matrix.CreateTranslation(0f, -height, 0f);

        // Flip the matrix around 180 degrees.
        viewMatrix *= Matrix.CreateRotationZ(MathHelper.Pi);

        // Account for the inverted gravity effect.
        if (Main.LocalPlayer.gravDir == -1f)
            viewMatrix *= Matrix.CreateScale(1f, -1f, 1f) * Matrix.CreateTranslation(0f, height, 0f);

        // Account for the current zoom.
        viewMatrix *= zoomScaleMatrix;

        projectionMatrix = Matrix.CreateOrthographicOffCenter(0f, width * zoom.X, 0f, height * zoom.Y, 0f, 1f) * zoomScaleMatrix;
    }

    /// <summary>
    ///     Returns a colored message.
    /// </summary>
    public static string ColorMessage(string msg, Color color)
    {
        StringBuilder sb;
        if (!msg.Contains('\n'))
        {
            sb = new StringBuilder(msg.Length + 12);
            sb.Append("[c/").Append(color.Hex3()).Append(':').Append(msg).Append(']');
        }
        else
        {
            sb = new StringBuilder();
            foreach (string newlineSlice in msg.Split('\n'))
                sb.Append("[c/").Append(color.Hex3()).Append(':').Append(newlineSlice).Append(']').Append('\n');
        }

        return sb.ToString();
    }
}
