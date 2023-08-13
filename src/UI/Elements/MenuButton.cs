using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace Creativetools.UI.Elements;

internal class MenuButton : UIImage
{
	public string HoverText;
	private readonly UIImage frame = new(ModContent.Request<Texture2D>("Creativetools/UI Assets/selected", ReLogic.Content.AssetRequestMode.ImmediateLoad));

	public MenuButton(string texture, string hoverText, MouseEvent mouseEvent) : base(ModContent.Request<Texture2D>("Creativetools/UI Assets/" + texture, ReLogic.Content.AssetRequestMode.ImmediateLoad))
	{
		OnLeftClick += mouseEvent;
		HoverText = hoverText;
	}

	public void SetState(bool value)
	{
		if (value) {
			Append(frame);
		}
		else {
			frame.Remove();
		}
	}

	public override void LeftClick(UIMouseEvent evt)
	{
		SoundEngine.PlaySound(SoundID.MenuTick);
		base.LeftClick(evt);
	}

	protected override void DrawSelf(SpriteBatch spriteBatch)
	{
		base.DrawSelf(spriteBatch);

		if (IsMouseHovering) {
			Main.hoverItemName = HoverText;
		}
		if (ContainsPoint(Main.MouseScreen)) //so you can't use items while clicking the button
		{
			Main.LocalPlayer.mouseInterface = true;
		}
	}
}
