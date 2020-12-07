using Creativetools.src.UI.Elements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.UI;
using static Creativetools.src.UI.UIHelper;

namespace Creativetools.src.UI
{
    class WeatherControl : UIState
    {
        private DragableUIPanel WeatherMenu;
        UIIntRangedDataValue Time;
        public override void OnInitialize()
        {
            WeatherMenu = new DragableUIPanel("Weather Control", 500f, 200f);
            WeatherMenu.VAlign = 0.6f;
            WeatherMenu.HAlign = 0.2f;
            Append(WeatherMenu);

            BackButton(WeatherMenu, 415, 155);

            var TimeSlider = MakeSlider(new UIIntRangedDataValue("Time Control:", 0, 0, 86399), out Time, WeatherMenu, top: 50);
            WeatherMenu.Append(new UIText(Language.GetTextValue("LegacyInterface.102") + ":", 0.85f) { MarginTop = 105, MarginLeft = 20 });

            float pos = 0.3f;
            ImageButtons(new UIHoverImageButton("Creativetools/UI Assets/FullMoon", Language.GetTextValue("GameUI.FullMoon")),
                WeatherMenu, button => button.OnClick += (evt, elm) => Main.moonPhase = 0, MarginTop: 100, HAllign: pos);
            pos += 0.1f;
            ImageButtons(new UIHoverImageButton("Creativetools/UI Assets/WaningGibbous", Language.GetTextValue("GameUI.WaningGibbous")),
                WeatherMenu, button => button.OnClick += (evt, elm) => Main.moonPhase = 1, MarginTop: 100, HAllign: pos);
            pos += 0.1f;
            ImageButtons(new UIHoverImageButton("Creativetools/UI Assets/ThirdQuarter", Language.GetTextValue("GameUI.ThirdQuarter")),
                WeatherMenu, button => button.OnClick += (evt, elm) => Main.moonPhase = 2, MarginTop: 100, HAllign: pos);
            pos += 0.1f;
            ImageButtons(new UIHoverImageButton("Creativetools/UI Assets/WaningCrescent", Language.GetTextValue("GameUI.WaningCrescent")),
                WeatherMenu, button => button.OnClick += (evt, elm) => Main.moonPhase = 3, MarginTop: 100, HAllign: pos);
            pos += 0.1f;
            ImageButtons(new UIHoverImageButton("Creativetools/UI Assets/NewMoon", Language.GetTextValue("GameUI.NewMoon")),
                WeatherMenu, button => button.OnClick += (evt, elm) => Main.moonPhase = 4, MarginTop: 100, HAllign: pos);
            pos += 0.1f;
            ImageButtons(new UIHoverImageButton("Creativetools/UI Assets/waxingCrescent", Language.GetTextValue("GameUI.waxingCrescent")),
                WeatherMenu, button => button.OnClick += (evt, elm) => Main.moonPhase = 5, MarginTop: 100, HAllign: pos);
            pos += 0.1f;
            ImageButtons(new UIHoverImageButton("Creativetools/UI Assets/FirstQuarter", Language.GetTextValue("GameUI.FirstQuarter")),
                WeatherMenu, button => button.OnClick += (evt, elm) => Main.moonPhase = 6, MarginTop: 100, HAllign: pos);
            pos += 0.1f;
            ImageButtons(new UIHoverImageButton("Creativetools/UI Assets/WaxingGibbous", Language.GetTextValue("GameUI.WaxingGibbous")),
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