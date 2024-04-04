using Creativetools.UI.Elements;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace Creativetools.UI;

internal class ButtonUI : UIState
{
	private readonly UIHoverImageButton menuButton;
	public ButtonUI()
	{
		menuButton = new UIHoverImageButton("Creativetools/UI Assets/MenuButton", "Open Menu");
		menuButton.OnLeftClick += MenuButtonClicked;
		Append(menuButton);
	}

	private static void MenuButtonClicked(UIMouseEvent evt, UIElement listeningElement)
	{
		if (UISystem.userInterface.CurrentState != null) {
			UISystem.userInterface.SetState(null);
			SoundEngine.PlaySound(SoundID.MenuClose);
		}
		else {
			UISystem.userInterface.SetState(new MainUI());
			SoundEngine.PlaySound(SoundID.MenuOpen);
		}
	}

	public override void Update(GameTime gameTime)
	{
		Vector2 offset = ModContent.GetInstance<Config>().menuBtnOffset * 47.5f;
		if (Main.LocalPlayer.difficulty == 3) {
			offset.X += 47;
		}

		menuButton.Top.Set(260 + offset.Y, 0);
		menuButton.Left.Set(20 + System.MathF.Max(offset.X, 0), 0);
		base.Update(gameTime);
	}
}
