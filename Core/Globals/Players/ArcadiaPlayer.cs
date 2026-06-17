using System.Collections.Generic;
using Arcadia.Core.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Arcadia.Core.Globals.Players;

public class ArcadiaPlayer : ModPlayer
{
    // Accessories.
    public bool CosmicLotus;
    public bool DivineAegis;

    // Buffs.
    public static int ChaosStateDuration = 900;
    public static int ChaosStateDuration_BossAlive = 1200;

    // Permanent buffs.
    public bool LunarBlessing;
    public bool EdenApple;

    // Miscellaneous.
    public bool RealityWarper;

    public override void Initialize()
    {
        LunarBlessing = false;
        EdenApple = false;
    }

    public override void SaveData(TagCompound tag)
    {
        var boost = new List<string>();
        boost.AddWithCondition("LunarBlessing", LunarBlessing);
        boost.AddWithCondition("EdenApple", EdenApple);

        tag["boost"] = boost;
    }

    public override void LoadData(TagCompound tag)
    {
        var boost = tag.GetList<string>("boost");
        LunarBlessing = boost.Contains("LunarBlessing");
        EdenApple = boost.Contains("EdenApple");
    }

    public override void ResetEffects()
    {
        CosmicLotus = false;
        DivineAegis = false;

        RealityWarper = false;
    }

    public override void ProcessTriggers(TriggersSet triggersSet)
    {
        // Teleport when the Reality Warper key has been pressed.
        // The player still needs the Reality Warper in their inventory.
        if (KeybindSystem.RealityWarperKey.JustPressed && RealityWarper && Main.myPlayer == Player.whoAmI)
        {
            if (!Player.CCed || !Player.chaosState)
            {
                Vector2 teleportLocation;
                teleportLocation.X = Main.mouseX + Main.screenPosition.X;
                if (Player.gravDir == 1f)
                    teleportLocation.Y = Main.mouseY + Main.screenPosition.Y - Player.height;
                else
                    teleportLocation.Y = Main.screenPosition.Y + Main.screenHeight - Main.mouseY;
                teleportLocation.X -= Player.width / 2;

                if (teleportLocation.X > 50f && teleportLocation.X < Main.maxTilesX * 16 - 50 && teleportLocation.Y > 50f && teleportLocation.Y < Main.maxTilesY * 16 - 50)
                {
                    if (!Collision.SolidCollision(teleportLocation, Player.width, Player.height))
                    {
                        Player.Teleport(teleportLocation, 4);
                        NetMessage.SendData(MessageID.TeleportEntity, -1, -1, null, 0, Player.whoAmI, teleportLocation.X, teleportLocation.Y, 1, 0, 0);
                        SoundEngine.PlaySound(SoundID.Item8, Player.Center);

                        int duration = ArcadiaUtils.AnyBosses().Item1 ? ChaosStateDuration_BossAlive : 360;
                        Player.AddBuff(BuffID.ChaosState, duration);
                    }
                }
            }
        }
    }
}
