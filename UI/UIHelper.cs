using Creativetools.UI.Elements;
using System;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;

namespace Creativetools.UI
{
    /// <summary>
    /// Helps make UI code smaller and easier to work with.
    /// </summary>
    class UIHelper
    {
        /// <summary>
        /// Creates a UIHoverImageButton with a click event
        /// </summary>
        /// <param name="element">The UIHoverImageButton which should be created</param>
        /// <param name="AppendTo">What UIElement it should append to</param>
        /// <param name="action">The Click Event</param>
        /// <param name="MarginLeft">X Position relative to "AppendTo"</param>
        /// <param name="MarginTop">Y Position relative to "AppendTo"</param>
        /// <param name="HAllign">Percentage X Position relative to "AppendTo"</param>
        /// <param name="VAllign">Percentage Y Position relative to "AppendTo"</param>
        /// <returns></returns>
        public static UIElement ImageButtons(UIHoverImageButton element, UIElement AppendTo, Action<UIElement> action, float MarginLeft = 0, float MarginTop = 0, float HAllign = 0, float VAllign = 0)
        {
            action(element);
            AppendTo.Append(element);
            element.MarginLeft = MarginLeft;
            element.MarginTop = MarginTop;
            element.HAlign = HAllign;
            element.VAlign = VAllign;

            return element;
        }
        /// <summary>
        /// Creates a button to close the current UIState and open the Creative Tools Menu
        /// </summary>
        /// <param name="AppendTo">The UIElement it should be appended to</param>
        /// <param name="MarginLeft">X Position relative to appendTo (0 is left most pixel)</param>
        /// <param name="MarginTop">Y Position relative to appendTo (0 is top most pixel)</param>
        /// <param name="HAllign">percentage X Position relative to appendTo (0 is left most pixel, 1 is right most pixel)</param>
        /// <param name="VAllign">percentage Y Position (in percent) relative to appendTo (0 is top most pixel, 1 is bottom most pixel)</param>
        public static UIElement BackButton(UIElement AppendTo, float MarginLeft = 0, float MarginTop = 0, float HAllign = 0, float VAllign = 0)
        {
            UITextPanel<string> backButton = new UITextPanel<string>(Language.GetTextValue("UI.Back"));
            backButton.SetPadding(4);
            backButton.Width.Set(10, 0f);
            backButton.MarginLeft = MarginLeft;
            backButton.MarginTop = MarginTop;
            backButton.HAlign = HAllign;
            backButton.VAlign = VAllign;
            backButton.OnClick += (evt, elm) =>
            {
                GetInstance<Creativetools>().UserInterface.SetState(new MainUI());
                Main.PlaySound(SoundID.MenuOpen);
            };
            AppendTo.Append(backButton);

            return backButton;
        }
        /// <summary>
        /// Creates a UITextPanel with a click event, used with sliders
        /// </summary>
        /// <param name="text">What Text the button has</param>
        /// <param name="appendTo">The UIElement, the button should be appended to</param>
        /// <param name="action">The Click event</param>
        /// <param name="tick">if the button should make a click sound</param>
        public static UIElement SliderButtons(string text, UIElement appendTo, Action<UIElement> action, bool tick = true)
        {
            UITextPanel<string> button = new UITextPanel<string>(text);
            button.SetPadding(4);
            button.MarginLeft = 20;
            button.Width.Set(10, 0f);

            if (tick) button.OnClick += (evt, elm) => Main.PlaySound(SoundID.MenuTick);
            appendTo.Append(button);
            action(button);

            return button;
        }
        /// <summary>
        /// Creates a slider with the type int
        /// </summary>
        /// <param name="inDataValue"></param>
        /// <param name="outDataValue"></param>
        /// <param name="top">Y Position relative to the UIElement it's appended to (0 is Top most pixel)</param>
        /// <param name="topPixels">Percentage Y Position relative to the UIElement it's appended to (0 is Top most pixel, 1 is bottom most pixel)</param>
        /// <param name="width">How wide the slider is</param>
        /// <param name="widthPixels">How wide (in percent) the slider is</param>
        /// <param name="appendTo">which UIElement it should append to</param>
        public static UIElement MakeSlider(UIIntRangedDataValue inDataValue, out UIIntRangedDataValue outDataValue, UIElement appendTo, float top = 0, float left = -20, float width = 0, float widthPixels = 1)
        {
            outDataValue = inDataValue;
            UIElement Slider = new UIRange<int>(inDataValue);
            Slider.MarginTop = top;
            Slider.MarginLeft = left;
            Slider.Width.Set(width, widthPixels);
            appendTo.Append(Slider);

            return Slider;
        }
        /// <summary>
        /// Creates a slider with the type float
        /// </summary>
        /// <param name="inDataValue"></param>
        /// <param name="outDataValue"></param>
        /// <param name="top">Y Position relative to the UIElement it's appended to (0 is Top most pixel)</param>
        /// <param name="topPixels">Percentage Y Position relative to the UIElement it's appended to (0 is Top most pixel, 1 is bottom most pixel)</param>
        /// <param name="width">How wide the slider is</param>
        /// <param name="widthPixels">How wide (in percent) the slider is</param>
        /// <param name="appendTo">which UIElement it should append to</param>
        public static UIElement MakeSlider(UIFloatRangedDataValue inDataValue, out UIFloatRangedDataValue outDataValue, UIElement appendTo, float top = 0, float left = -20, float width = 0, float widthPixels = 1)
        {
            outDataValue = inDataValue;
            UIElement Slider = new UIRange<float>(inDataValue);
            Slider.MarginTop = top;
            Slider.MarginLeft = left;
            Slider.Width.Set(width, widthPixels);
            appendTo.Append(Slider);

            return Slider;
        }
    }
}
