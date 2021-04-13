using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace Creativetools.src.Tools.Modify
{
    class ModifyPlayer : ModPlayer
    {
        public static float playerSize = 1f;
        private Texture2D[] WingsExtraTexOccupied = { TextureAssets.ItemFlame[1866].Value, TextureAssets.Extra[38].Value, TextureAssets.Flames[8].Value, TextureAssets.GlowMask[92].Value, TextureAssets.GlowMask[181].Value, TextureAssets.GlowMask[213].Value, TextureAssets.GlowMask[183].Value };
        public override void PreUpdate()
        {
            if (Player.active && playerSize != 1f)
            {
                // Change player hitbox size
                Player.position = Player.Bottom;
                Player.Size = new Vector2(Player.defaultWidth * playerSize, Player.defaultHeight * playerSize);
                Player.Bottom = Player.position;
            }
        }
        // Change player render size
        /*public static readonly PlayerLayer SizePlayerRender = new PlayerLayer("CreativeTools", "ChangeSize", delegate (PlayerDrawInfo drawInfo)
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
        public override void ModifyDrawLayerOrdering(IDictionary<PlayerDrawLayer, PlayerDrawLayer.Position> positions)
        {
            SizePlayerRender.visible = true;
            positions.Add(SizePlayerRender);
        }*/
    }
}