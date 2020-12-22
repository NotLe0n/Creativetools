using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using static Terraria.ModLoader.ModContent;

namespace Creativetools.src.UI.Elements
{
    class MenuButton : UIImage
    {
        public string HoverText;
        private UIImage frame = new UIImage(GetTexture("Creativetools/UI Assets/selected"));
        public MenuButton(string texture, string hoverText, MouseEvent mouseEvent) : base(GetTexture("Creativetools/UI Assets/" + texture))
        {
            OnClick += mouseEvent;
            HoverText = hoverText;
        }
        public void SetState(bool value)
        {
            if (value)
            {
                Append(frame);
            }
            else
            {
                frame.Remove();
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
