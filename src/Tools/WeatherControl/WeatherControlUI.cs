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
	private DragableUIPanel menu;
	private UIIntRangedDataValue time;

	public override void OnInitialize()
	{
		menu = new DragableUIPanel("Weather Control");
		menu.Width.Set(500f, 0);
		menu.Height.Set(200f, 0);
		menu.VAlign = 0.6f;
		menu.HAlign = 0.2f;
		menu.OnCloseBtnClicked += () => UISystem.UserInterface.SetState(new MainUI());
		Append(menu);

		var TimeSlider = MakeSlider(new UIIntRangedDataValue("Time Control:", 0, 0, 86399), out time, menu, top: 50);
		menu.Append(new UIText(Language.GetTextValue("LegacyInterface.102") + ":", 0.85f) { MarginTop = 105, MarginLeft = 20 });

		float pos = 0.3f;
		ImageButtons(new UIHoverImageButton("Creativetools/UI Assets/FullMoon", Language.GetTextValue("GameUI.FullMoon")),
			menu, button => button.OnClick += (evt, elm) => Main.moonPhase = 0, MarginTop: 100, HAllign: pos);
		pos += 0.1f;
		ImageButtons(new UIHoverImageButton("Creativetools/UI Assets/WaningGibbous", Language.GetTextValue("GameUI.WaningGibbous")),
			menu, button => button.OnClick += (evt, elm) => Main.moonPhase = 1, MarginTop: 100, HAllign: pos);
		pos += 0.1f;
		ImageButtons(new UIHoverImageButton("Creativetools/UI Assets/ThirdQuarter", Language.GetTextValue("GameUI.ThirdQuarter")),
			menu, button => button.OnClick += (evt, elm) => Main.moonPhase = 2, MarginTop: 100, HAllign: pos);
		pos += 0.1f;
		ImageButtons(new UIHoverImageButton("Creativetools/UI Assets/WaningCrescent", Language.GetTextValue("GameUI.WaningCrescent")),
			menu, button => button.OnClick += (evt, elm) => Main.moonPhase = 3, MarginTop: 100, HAllign: pos);
		pos += 0.1f;
		ImageButtons(new UIHoverImageButton("Creativetools/UI Assets/NewMoon", Language.GetTextValue("GameUI.NewMoon")),
			menu, button => button.OnClick += (evt, elm) => Main.moonPhase = 4, MarginTop: 100, HAllign: pos);
		pos += 0.1f;
		ImageButtons(new UIHoverImageButton("Creativetools/UI Assets/waxingCrescent", Language.GetTextValue("GameUI.WaxingCrescent")),
			menu, button => button.OnClick += (evt, elm) => Main.moonPhase = 5, MarginTop: 100, HAllign: pos);
		pos += 0.1f;
		ImageButtons(new UIHoverImageButton("Creativetools/UI Assets/FirstQuarter", Language.GetTextValue("GameUI.FirstQuarter")),
			menu, button => button.OnClick += (evt, elm) => Main.moonPhase = 6, MarginTop: 100, HAllign: pos);
		pos += 0.1f;
		ImageButtons(new UIHoverImageButton("Creativetools/UI Assets/WaxingGibbous", Language.GetTextValue("GameUI.WaxingGibbous")),
			menu, button => button.OnClick += (evt, elm) => Main.moonPhase = 7, MarginTop: 100, HAllign: pos);
	}

	public override void Update(GameTime gameTime)
	{
		base.Update(gameTime);
		Main.time = !(Main.dayTime = time.Data <= 54000) ? Math.Abs(time.Data - 54000) : time.Data;

		if (Main.netMode == NetmodeID.MultiplayerClient)
		{
			NetMessage.SendData(MessageID.WorldData);
		}
	}

	// so you can't use items when clicking on the button
	protected override void DrawSelf(SpriteBatch spriteBatch)
	{
		if (menu.ContainsPoint(Main.MouseScreen))
		{
			Main.LocalPlayer.mouseInterface = true;
		}
	}
}
