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
	private UIHoverImageButton MenuButton;
	public override void OnInitialize()
	{
		MenuButton = new UIHoverImageButton("Creativetools/UI Assets/MenuButton", "Open Menu");
		MenuButton.OnClick += MenuButtonClicked;
		Append(MenuButton);
	}

	private void MenuButtonClicked(UIMouseEvent evt, UIElement listeningElement)
	{
		if (UISystem.UserInterface.CurrentState != null) {
			UISystem.UserInterface.SetState(null);
			SoundEngine.PlaySound(SoundID.MenuClose);
		}
		else {
			UISystem.UserInterface.SetState(new MainUI());
			SoundEngine.PlaySound(SoundID.MenuOpen);
		}
	}

	public override void Update(GameTime gameTime)
	{
		Vector2 offset = ModContent.GetInstance<Config>().MenuBtnOffset * 47.5f;
		if (Main.LocalPlayer.difficulty == 3) {
			offset.X += 47;
		}

		MenuButton.Top.Set(260 + offset.Y, 0);
		MenuButton.Left.Set(20 + System.MathF.Max(offset.X, 0), 0);
		base.Update(gameTime);
	}
}
