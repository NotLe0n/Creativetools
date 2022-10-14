using Creativetools.UI;
using Creativetools.UI.Elements;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;
using static Creativetools.UI.UIHelper;

namespace Creativetools.Tools.Modify;

internal class ItemModUI : UIState
{
	public override void OnInitialize()
	{
		var itemMenu = new TabPanel(new Tab("Change Item", this), new Tab(" Change Player", new PlayerModUI())) {
			VAlign = 0.6f,
			HAlign = 0.2f,
			Width = new(450, 0),
			Height = new(450, 0)
		};
		itemMenu.OnCloseBtnClicked += () => UISystem.UserInterface.SetState(new MainUI());
		Append(itemMenu);

		var damageSlider = MakeSlider(new UIIntRangedDataValue("", 0, 0, 999), out var damageDataProperty, itemMenu, top: 50, left: -10);
		damageSlider.AppendSliderButton("Set Damage", () => ChangeDamage(damageDataProperty.Data));

		var critSlider = MakeSlider(new UIIntRangedDataValue("", 0, 0, 100), out var critDataProperty, itemMenu, top: 100, left: -10);
		critSlider.AppendSliderButton("Set Crit", () => ChangeCrit(critDataProperty.Data));

		var knockSlider = MakeSlider(new UIFloatRangedDataValue("", 0, 0, 100), out var knockbackDataProperty, itemMenu, top: 150, left: -10);
		knockSlider.AppendSliderButton("Set Knockback", () => ChangeKnock(knockbackDataProperty.Data));

		var usetimeSlider = MakeSlider(new UIIntRangedDataValue("", 0, 0, 50), out var usetimeDataProperty, itemMenu, top: 200, left: -10);
		usetimeSlider.AppendSliderButton("Set Usetime", () => ChangeUseTime(usetimeDataProperty.Data));

		var defenseSlider = MakeSlider(new UIIntRangedDataValue("", 0, 0, 100), out var defenseDataProperty, itemMenu, top: 250, left: -10);
		defenseSlider.AppendSliderButton("Set Defense", () => ChangeDefense(defenseDataProperty.Data));

		var shootSlider = MakeSlider(new UIFloatRangedDataValue("", 0, 0, 999), out var shootspeedDataProperty, itemMenu, top: 300, left: -10);
		shootSlider.AppendSliderButton("Set Bullet speed", () => ChangeShoot(shootspeedDataProperty.Data));

		var sizeSlider = MakeSlider(new UIFloatRangedDataValue("", 0, 0, 50), out var sizeDataProperty, itemMenu, top: 350, left: -10);
		sizeSlider.AppendSliderButton("Set Size", () => ChangeSize(sizeDataProperty.Data));

		var autoswingButton = new UITextPanel<string>("Toggle Autoswing");
		autoswingButton.SetPadding(4);
		autoswingButton.MarginLeft = 10;
		autoswingButton.MarginTop = 400;
		autoswingButton.Width.Set(10, 0f);
		autoswingButton.OnClick += (_, _) =>
		{
			ToggleAutoSwing();
			SoundEngine.PlaySound(SoundID.MenuTick);
		};
		itemMenu.Append(autoswingButton);

		var turnaroundButton = new UITextPanel<string>("Toggle Turnaround");
		turnaroundButton.SetPadding(4);
		turnaroundButton.MarginLeft = 275;
		turnaroundButton.MarginTop = 400;
		turnaroundButton.Width.Set(10, 0f);
		turnaroundButton.OnClick += (_, _) =>
		{
			ToggleTurnAround();
			SoundEngine.PlaySound(SoundID.MenuTick);
		};
		itemMenu.Append(turnaroundButton);
	}

	private static void ChangeDamage(int change) => Main.LocalPlayer.HeldItem.damage = change;
	private static void ChangeCrit(int change) => Main.LocalPlayer.HeldItem.crit = change - 4;
	private static void ChangeKnock(float change) => Main.LocalPlayer.HeldItem.knockBack = change;
	private static void ChangeUseTime(int change)
	{
		Main.LocalPlayer.HeldItem.useTime = change;
		Main.LocalPlayer.HeldItem.useAnimation = change;
	}
	private static void ChangeDefense(int change) => Main.LocalPlayer.HeldItem.defense = change;
	private static void ChangeShoot(float change) => Main.LocalPlayer.HeldItem.shootSpeed = change;
	private static void ChangeSize(float change) => Main.LocalPlayer.HeldItem.scale = change;
	private static void ToggleAutoSwing() => Main.LocalPlayer.HeldItem.autoReuse = !Main.LocalPlayer.HeldItem.autoReuse;
	private static void ToggleTurnAround() => Main.LocalPlayer.HeldItem.useTurn = !Main.LocalPlayer.HeldItem.useTurn;
}
