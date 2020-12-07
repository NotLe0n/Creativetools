using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace Creativetools.src.UI.Elements
{
    internal class UIHoverImageButton : UIImageButton
    {
        internal string HoverText;
        internal string Texture;
        public UIHoverImageButton(string texture, string hoverText) : base(ModContent.GetTexture(texture))
        {
            HoverText = hoverText;
            Texture = texture;
        }
        public override void Click(UIMouseEvent evt)
        {
            base.Click(evt);
            if (Main.netMode == NetmodeID.Server)
            {
                NetMessage.SendData(MessageID.WorldData); // Immediately inform clients of new world state.
            }
        }
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            if (IsMouseHovering)
            {
                Main.hoverItemName = HoverText;
            }
            if (ContainsPoint(Main.MouseScreen)) //so you can't use items while clicking the button
            {
                Main.LocalPlayer.mouseInterface = true;
            }
        }
    }
}