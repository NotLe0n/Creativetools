﻿using Creativetools.src.UI;
using Creativetools.src.UI.Elements;
using Terraria;
using Terraria.UI;
using static Creativetools.src.UI.UIHelper;

namespace Creativetools.src.Tools.Modify;

internal class PlayerModUI : UIState
{
	public override void OnInitialize()
	{
		var playerMenu = new TabPanel(new Tab("Change Item", new ItemModUI()), new Tab(" Change Player", this)) {
			VAlign = 0.6f,
			HAlign = 0.2f,
			Width = new(450, 0),
			Height = new(350, 0)
		};
		playerMenu.OnCloseBtnClicked += () => UISystem.UserInterface.SetState(new MainUI());
		Append(playerMenu);

		var lifeSlider = MakeSlider(new UIIntRangedDataValue("", 0, 0, 999), out var lifeDataProperty, playerMenu, top: 50, left: -10);
		lifeSlider.AppendSliderButton("Set Life", () => Main.LocalPlayer.statLife = lifeDataProperty.Data);

		var manaSlider = MakeSlider(new UIIntRangedDataValue("", 0, 0, 400), out var manaDataProperty, playerMenu, top: 100, left: -10);
		lifeSlider.AppendSliderButton("Set Mana", () => Main.LocalPlayer.statMana = manaDataProperty.Data);

		var maxLifeSlider = MakeSlider(new UIIntRangedDataValue("", 0, 0, 999), out var maxLifeDataProperty, playerMenu, top: 150, left: -10);
		lifeSlider.AppendSliderButton("Set Max Life", () => Main.LocalPlayer.statLifeMax = maxLifeDataProperty.Data);

		var maxManaSlider = MakeSlider(new UIIntRangedDataValue("", 0, 0, 400), out var maxManaDataProperty, playerMenu, top: 200, left: -10);
		lifeSlider.AppendSliderButton("Set Max Mana", () => Main.LocalPlayer.statManaMax = maxManaDataProperty.Data);

		var sizeSlider = MakeSlider(new UIFloatRangedDataValue("", 1, 0.01f, 10), out var sizeDataProperty, playerMenu, top: 250, left: -10);
		lifeSlider.AppendSliderButton("Set Size", () => ModifyPlayer.playerSize = sizeDataProperty.Data);

		var luckSlider = MakeSlider(new UIFloatRangedDataValue("", 0, -0.7f, 1), out var luckDataProperty, playerMenu, top: 300, left: -10);
		lifeSlider.AppendSliderButton("Set Luck", () => ModifyPlayer.luck = luckDataProperty.Data);
	}
}
