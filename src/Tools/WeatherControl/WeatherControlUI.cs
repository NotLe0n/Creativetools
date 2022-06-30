using Creativetools.src.UI;
using Creativetools.src.UI.Elements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;
using static Creativetools.src.UI.UIHelper;

namespace Creativetools.src.Tools.WeatherControl;

internal class WeatherControlUI : UIState
{
	private UIIntRangedDataValue time;

	public override void OnInitialize()
	{
		var menu = new DragableUIPanel("Weather Control") {
			VAlign = 0.5f,
			HAlign = 0.1f,
			Width = new(500f, 0),
			Height = new(200f, 0)
		};
		menu.OnCloseBtnClicked += UISystem.BackToMainMenu;
		Append(menu);

		string[] moons = new[] { "FullMoon", "WaningGibbous", "ThirdQuarter", "WaningCrescent", "NewMoon", "WaxingCrescent", "FirstQuarter", "WaxingGibbous" };

		var timeSlider = MakeSlider(new UIIntRangedDataValue("Time Control:", 0, 0, 86399), out time, menu, top: 50);
		menu.Append(new UIText(Language.GetTextValue("LegacyInterface.102") + ":", 0.85f) { MarginTop = 105, MarginLeft = 20 });

		float pos = 0.3f;
		for (int i = 0; i < moons.Length; i++) {
			ImageButtons(new("Creativetools/UI Assets/" + moons[i], Language.GetTextValue("GameUI." + moons[i])),
				menu, () => ChangeMoonPhase(i), MarginTop: 100, HAllign: pos);

			pos += 0.1f;
		}
	}

	public override void Update(GameTime gameTime)
	{
		base.Update(gameTime);
		if (Main.time != time.Data) {
			double transformedTime = !(Main.dayTime = time.Data <= 54000) ? Math.Abs(time.Data - 54000) : time.Data;

			if (Main.netMode == NetmodeID.SinglePlayer) {
				Main.time = transformedTime;
			}
			else {
				MultiplayerSystem.SyncTime(transformedTime);
			}
		}
	}

	private static void ChangeMoonPhase(int phase)
	{
		if (Main.netMode == NetmodeID.SinglePlayer) {
			Main.moonPhase = phase;
		}
		else {
			MultiplayerSystem.SyncMoonPhase(phase);
		}
	}
}
