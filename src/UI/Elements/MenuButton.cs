using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;

namespace Creativetools.src.UI.Elements
{
    internal class MenuButton : UIImage
    {
        public string HoverText;
        private UIImage frame = new(GetTexture("Creativetools/UI Assets/selected"));
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
        public override void Click(UIMouseEvent evt)
        {
            SoundEngine.PlaySound(SoundID.MenuTick);
            base.Click(evt);
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
