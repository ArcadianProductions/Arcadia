using Arcadia.Core.Graphics.Particles;

using Terraria;
using Terraria.ModLoader;

namespace Arcadia.Core.Systems;

public class EntityUpdateSystem : ModSystem
{
    public override void PostUpdateEverything()
    {
        // Update particles.
        if (!Main.dedServ)
            GeneralParticleHandler.Update();
    }
}
