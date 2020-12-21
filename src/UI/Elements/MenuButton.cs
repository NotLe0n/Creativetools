using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using static Terraria.ModLoader.ModContent;

namespace Creativetools.src.UI.Elements
{
    class MenuButton : UIToggleImage
    {
        public string HoverText;
        public MenuButton(string texture, string hoverText, MouseEvent mouseEvent) : base(GetTexture("Creativetools/UI Assets/" + texture), 44, 44, new Point(45, 0), new Point(0, 0))
        {
            OnClick += mouseEvent;
            HoverText = hoverText;
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
