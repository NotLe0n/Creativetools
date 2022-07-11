using Creativetools.src.UI.Elements;
using Microsoft.Xna.Framework;
using ReLogic.Graphics;
using System;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;

namespace Creativetools.src.UI;

/// <summary>
/// Helps make UI code smaller and easier to work with.
/// </summary>
internal static class UIHelper
{
	/// <summary>
	/// Creates a UITextPanel with a click event, used with sliders
	/// </summary>
	/// <param name="appendTo">The UIElement, the button should be appended to</param>
	/// <param name="text">What Text the button has</param>
	/// <param name="onClick">click action</param>
	/// <param name="tick">if the button should make a click sound</param>
	public static void AppendSliderButton(this UIElement appendTo, string text, Action onClick, bool tick = true)
	{
		UITextPanel<string> button = new(text);
		button.SetPadding(4);
		button.MarginLeft = 20;
		button.Width.Set(10, 0f);

		button.OnClick += (_, __) =>
		{
			if (tick) {
				SoundEngine.PlaySound(SoundID.MenuTick);
			}
			onClick();
		};

		appendTo.Append(button);
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

	public static Vector2 GetTextSize(this string str)
	{
		return FontAssets.MouseText.Value.MeasureString(str);
	}

	public static Vector2 GetTextSize(this string str, DynamicSpriteFont font)
	{
		return font.MeasureString(str);
	}
}
