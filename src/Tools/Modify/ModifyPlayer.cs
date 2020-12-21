using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Creativetools.src.Tools.Modify
{
    class ModifyPlayer : ModPlayer
    {
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