using Creativetools.UI.Elements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.UI;
using static Creativetools.UI.UIHelper;

namespace Creativetools.UI
{
    class WeatherControl : UIState
    {
        private UIPanel WeatherMenu;
        UIIntRangedDataValue Time;
        public override void OnInitialize()
        {
            WeatherMenu = new UIPanel();
            WeatherMenu.VAlign = 0.6f;
            WeatherMenu.HAlign = 0.2f;
            WeatherMenu.Width.Set(500f, 0f);
            WeatherMenu.Height.Set(200f, 0f);
            WeatherMenu.BackgroundColor = new Color(73, 94, 171);
            Append(WeatherMenu);

            WeatherMenu.Append(new UIText("Weather Control") { HAlign = 0.5f, MarginTop = 15 });
            BackButton(WeatherMenu, 415, 155);

            var TimeSlider = MakeSlider(new UIIntRangedDataValue("Time Control:", 0, 0, 86399), out Time, WeatherMenu, top: 50);
            WeatherMenu.Append(new UIText(Language.GetTextValue("LegacyInterface.102") + ":", 0.85f) { MarginTop = 105, MarginLeft = 20 });

            float pos = 0.3f;
            ImageButtons(new UIHoverImageButton("Creativetools/UI/UI Assets/FullMoon", Language.GetTextValue("GameUI.FullMoon")),
                WeatherMenu, button => button.OnClick += (evt, elm) => Main.moonPhase = 0, MarginTop: 100, HAllign: pos);
            pos += 0.1f;
            ImageButtons(new UIHoverImageButton("Creativetools/UI/UI Assets/WaningGibbous", Language.GetTextValue("GameUI.WaningGibbous")),
                WeatherMenu, button => button.OnClick += (evt, elm) => Main.moonPhase = 1, MarginTop: 100, HAllign: pos);
            pos += 0.1f;
            ImageButtons(new UIHoverImageButton("Creativetools/UI/UI Assets/ThirdQuarter", Language.GetTextValue("GameUI.ThirdQuarter")),
                WeatherMenu, button => button.OnClick += (evt, elm) => Main.moonPhase = 2, MarginTop: 100, HAllign: pos);
            pos += 0.1f;
            ImageButtons(new UIHoverImageButton("Creativetools/UI/UI Assets/WaningCrescent", Language.GetTextValue("GameUI.WaningCrescent")),
                WeatherMenu, button => button.OnClick += (evt, elm) => Main.moonPhase = 3, MarginTop: 100, HAllign: pos);
            pos += 0.1f;
            ImageButtons(new UIHoverImageButton("Creativetools/UI/UI Assets/NewMoon", Language.GetTextValue("GameUI.NewMoon")),
                WeatherMenu, button => button.OnClick += (evt, elm) => Main.moonPhase = 4, MarginTop: 100, HAllign: pos);
            pos += 0.1f;
            ImageButtons(new UIHoverImageButton("Creativetools/UI/UI Assets/waxingCrescent", Language.GetTextValue("GameUI.waxingCrescent")),
                WeatherMenu, button => button.OnClick += (evt, elm) => Main.moonPhase = 5, MarginTop: 100, HAllign: pos);
            pos += 0.1f;
            ImageButtons(new UIHoverImageButton("Creativetools/UI/UI Assets/FirstQuarter", Language.GetTextValue("GameUI.FirstQuarter")),
                WeatherMenu, button => button.OnClick += (evt, elm) => Main.moonPhase = 6, MarginTop: 100, HAllign: pos);
            pos += 0.1f;
            ImageButtons(new UIHoverImageButton("Creativetools/UI/UI Assets/WaxingGibbous", Language.GetTextValue("GameUI.WaxingGibbous")),
                WeatherMenu, button => button.OnClick += (evt, elm) => Main.moonPhase = 7, MarginTop: 100, HAllign: pos);

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Main.time = !(Main.dayTime = Time.Data <= 54000) ? Math.Abs(Time.Data - 54000) : Time.Data;
        }

        // so you can't use items when clicking on the button
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            if (WeatherMenu.ContainsPoint(Main.MouseScreen))
            {
                Main.LocalPlayer.mouseInterface = true;
            }
        }
    }
}