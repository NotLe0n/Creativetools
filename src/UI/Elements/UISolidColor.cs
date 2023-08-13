using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.UI;

namespace Creativetools.UI.Elements;

internal class UISolidColor : UIElement
{
	private readonly Color color;
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
