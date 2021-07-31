using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;

namespace Creativetools.src.UI.Elements
{
    internal class UIHoverImageButton : UIImageButton
    {
        internal string HoverText;
        internal string Texture;

        public UIHoverImageButton(string texture, string hoverText) : base(ModContent.Request<Texture2D>(texture))
        {
            HoverText = hoverText;
            Texture = texture;
        }

        public override void Update(GameTime gameTime)
        {
            if (IsMouseHovering)
            {
                Main.hoverItemName = HoverText;
                Main.LocalPlayer.mouseInterface = true;
            }
            base.Update(gameTime);
        }
    }
}