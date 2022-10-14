using Creativetools.UI.Elements;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;

namespace Creativetools.Tools.ClearInventory;

internal class ConfirmPanel : UIState
{
	public static bool Visible;
	public override void OnInitialize()
	{
		DragableUIPanel confirmPanel = new("Are you sure you want to delete all your items?");
		confirmPanel.SetPadding(0);
		confirmPanel.Width.Set(550, 0);
		confirmPanel.Height.Set(100, 0);
		confirmPanel.VAlign = confirmPanel.HAlign = 0.5f;
		confirmPanel.OnCloseBtnClicked += () => Visible = false;
		Append(confirmPanel);

		UIPanel yepButton = new();
		yepButton.Width.Set(100, 0);
		yepButton.Height.Set(50, 0);
		yepButton.HAlign = 0.03f;
		yepButton.Top.Set(35, 0);
		yepButton.OnClick += yepClicked;
		confirmPanel.Append(yepButton);

		UIPanel delFavbutton = new();
		delFavbutton.Width.Set(300, 0);
		delFavbutton.Height.Set(50, 0);
		delFavbutton.HAlign = 0.5f;
		delFavbutton.Top.Set(35, 0);
		delFavbutton.OnClick += DelFavClicked;
		confirmPanel.Append(delFavbutton);

		UIPanel noButton = new();
		noButton.Width.Set(100, 0);
		noButton.Height.Set(50, 0);
		noButton.HAlign = 0.97f;
		noButton.Top.Set(35, 0);
		noButton.OnClick += NoClicked;
		confirmPanel.Append(noButton);

		yepButton.Append(new UIText("Yes") { HAlign = 0.5f, VAlign = 0.5f });
		delFavbutton.Append(new UIText("Delete all, except favorited items") { HAlign = 0.5f, VAlign = 0.5f });
		noButton.Append(new UIText("NO!") { HAlign = 0.5f, VAlign = 0.5f });
	}
	
	private void yepClicked(UIMouseEvent evt, UIElement listeningElement)
	{
		//delete all items in the inventory
		for (int i = 0; i < Main.InventorySlotsTotal; i++) {
			Main.LocalPlayer.inventory[i].TurnToAir();
			SoundEngine.PlaySound(SoundID.Grab);
		}
		Visible = false; //close
		SoundEngine.PlaySound(SoundID.MenuClose);
	}
	
	private void DelFavClicked(UIMouseEvent evt, UIElement listeningElement)
	{
		//delete all except favorited items
		for (int i = 0; i < Main.InventorySlotsTotal; i++) {
			if (Main.LocalPlayer.inventory[i].favorited) {
				continue;
			}

			Main.LocalPlayer.inventory[i].TurnToAir();
			SoundEngine.PlaySound(SoundID.Grab);
		}
		Visible = false; //close
		SoundEngine.PlaySound(SoundID.MenuClose);
	}
	
	private void NoClicked(UIMouseEvent evt, UIElement listeningElement)
	{
		Visible = false; //close
		SoundEngine.PlaySound(SoundID.MenuClose);
	}
}
