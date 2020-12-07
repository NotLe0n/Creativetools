using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ModLoader;

namespace Creativetools.src
{
    class ModifyPlayer : ModPlayer
    {
        public static bool CreativeFly;
        public bool creativeFly;
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            creativeFly = CreativeFly;
            CreativeFly = creativeFly;
            if (CreativeFly)
            {
                player.gravity = 0f; //player doesn't fall
                player.controlJump = false; //player can't jump
                player.noFallDmg = true; //player doesn't take fall damage
                player.moveSpeed = 0f; //player can't move
                player.noKnockback = true; //player doesn't take knockback
                player.velocity.Y = -0.00000000001f; //fix confusing bug

                float modifier = 1f;
                if (Main.keyState.IsKeyDown(Keys.LeftShift) | Main.keyState.IsKeyDown(Keys.RightShift))
                {
                    modifier = 2f; //go faster
                }
                if (Main.keyState.IsKeyDown(Keys.LeftControl) | Main.keyState.IsKeyDown(Keys.RightControl))
                {
                    modifier = 0.5f; //go slower
                }
                if (Main.keyState.IsKeyDown(Keys.W))
                {
                    player.position.Y -= 8 * modifier; //move up
                }
                if (Main.keyState.IsKeyDown(Keys.S))
                {
                    player.position.Y += 8 * modifier; //move down 
                }
                if (Main.keyState.IsKeyDown(Keys.A))
                {
                    player.position.X -= 8 * modifier; //move left
                }
                if (Main.keyState.IsKeyDown(Keys.D))
                {
                    player.position.X += 8 * modifier; //move right
                }
            }
        }
        public static float playerSize = 1f;
        private static Texture2D[] WingsExtraTexOccupied = { Main.itemFlameTexture[1866], Main.extraTexture[38], Main.FlameTexture[8], Main.glowMaskTexture[92], Main.glowMaskTexture[181], Main.glowMaskTexture[213], Main.glowMaskTexture[183] };
        public override void PreUpdate()
        {
            if (player.active && playerSize != 1f)
            {
                // Change player hitbox size
                player.position = player.Bottom;
                player.Size = new Vector2(Player.defaultWidth * playerSize, Player.defaultHeight * playerSize);
                player.Bottom = player.position;
            }
        }
        // Change player render size
        public static readonly PlayerLayer SizePlayerRender = new PlayerLayer("CreativeTools", "ChangeSize", delegate (PlayerDrawInfo drawInfo)
        {
            if (!Main.gameMenu && playerSize != 1f)
            {
                for (int i = 0; i < Main.playerDrawData.Count; i++)
                {
                    DrawData drawData = Main.playerDrawData[i];
                    drawData.scale *= playerSize;

                    if (drawData.sourceRect.HasValue)
                    {
                        Vector2 texOffset = new Vector2(drawData.sourceRect.Value.Width, drawData.sourceRect.Value.Height) - drawData.origin;
                        texOffset.X -= drawData.sourceRect.Value.Width / 2;
                    }

                    Vector2 totalOffset = Main.LocalPlayer.Bottom - Main.screenPosition - drawData.position;
                    if (drawData.texture == Main.wingsTexture[Main.LocalPlayer.wings])
                    {
                        totalOffset.Y = 0;
                    }

                    foreach (Texture2D wingTexture in WingsExtraTexOccupied)
                    {
                        if (drawData.texture == wingTexture)
                        {
                            totalOffset.Y = 0;
                        }
                    }
                    drawData.position -= totalOffset * (playerSize - 1f);
                    drawData.position.Y -= 2f * (playerSize - 1f);
                    Main.playerDrawData[i] = drawData;
                }
            }
        });
        public override void ModifyDrawLayers(List<PlayerLayer> layers)
        {
            SizePlayerRender.visible = true;
            layers.Add(SizePlayerRender);
        }
    }
}