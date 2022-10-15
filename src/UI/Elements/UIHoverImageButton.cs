using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;

namespace Creativetools.UI.Elements;

internal class UIHoverImageButton : UIImageButton
{
	public string hoverText;

	public UIHoverImageButton(string texture, string hoverText) : base(ModContent.Request<Texture2D>(texture, ReLogic.Content.AssetRequestMode.ImmediateLoad))
	{
		this.hoverText = hoverText;
	}

	public override void Update(GameTime gameTime)
	{
		if (IsMouseHovering) {
			Main.LocalPlayer.cursorItemIconText = hoverText;
			Main.LocalPlayer.mouseInterface = true;
		}

		base.Update(gameTime);
	}
}
