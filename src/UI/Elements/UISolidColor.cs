using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.UI;

namespace Creativetools.src.UI.Elements
{
    class UISolidColor : UIElement
    {
        public Color color;
        public UISolidColor(Color color)
        {
            this.color = color;
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            spriteBatch.Draw(TextureAssets.MagicPixel.Value, GetDimensions().ToRectangle(), color);
        }
    }
}
