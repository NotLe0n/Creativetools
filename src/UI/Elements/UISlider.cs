﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.UI;

namespace Creativetools.UI.Elements;

internal class UISlider : UIElement
{
	private Color _color;
	private Func<string> _TextDisplayFunction;
	private Func<float> _GetStatusFunction;
	private Action<float> _SlideKeyboardAction;
	private Action _SlideGamepadAction;
	private int _sliderIDInPage;
	private Texture2D _toggleTexture;
	private bool colorRange;
	public static bool slidin;

	public UISlider(Func<string> getText, Func<float> getStatus, Action<float> setStatusKeyboard, Action setStatusGamepad, int sliderIDInPage, Color color)
	{
		_color = color;
		_toggleTexture = Main.Assets.Request<Texture2D>("Images\\UI\\Settings_Toggle").Value;
		_TextDisplayFunction = getText ?? (() => "???");
		_GetStatusFunction = getStatus ?? (() => 0f);
		_SlideKeyboardAction = setStatusKeyboard ?? ((_) => { });
		_SlideGamepadAction = setStatusGamepad ?? (() => { });
		_sliderIDInPage = sliderIDInPage;
	}

	public override void Recalculate()
	{
		base.Recalculate();
	}

	public void SetHueMode(bool isHue)
	{
		colorRange = isHue;
	}
	public override void MouseOver(UIMouseEvent evt)
	{
		base.MouseOver(evt);
		SoundEngine.PlaySound(SoundID.MenuTick);
	}
	public override void LeftClick(UIMouseEvent evt)
	{
		base.LeftClick(evt);
		SoundEngine.PlaySound(SoundID.MenuTick);
	}

	private int paddingY = 4;
	private int paddingX = 5;
	protected override void DrawSelf(SpriteBatch spriteBatch)
	{
		CalculatedStyle dimensions = GetInnerDimensions();
		Rectangle rectangle = dimensions.ToRectangle();
		Texture2D ColorSlidertexture = TextureAssets.ColorSlider.Value;

		spriteBatch.Draw(TextureAssets.ColorBar.Value, rectangle, Color.White);
		if (IsMouseHovering) {
			spriteBatch.Draw(TextureAssets.ColorHighlight.Value, rectangle, Main.OurFavoriteColor);
		}

		rectangle.Inflate(-paddingX, -paddingY);
		//int num = 167;
		float scale = 1f;
		float x = rectangle.X + (5f * scale);
		float y = rectangle.Y + (4f * scale);

		x = rectangle.X;
		y = rectangle.Y;
		for (float i = 0f; i < rectangle.Width; i += 1f) {
			float amount = i / rectangle.Width;
			Color color = colorRange ? Main.hslToRgb(amount, 1f, 0.5f) : Color.Lerp(Color.Black, Color.White, amount);
			spriteBatch.Draw(TextureAssets.ColorBlip.Value, new Vector2(x + i * scale, y),
				null, color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
		}

		float percent = _GetStatusFunction();
		percent = Utils.Clamp(percent, 0f, 1f);

		spriteBatch.Draw(ColorSlidertexture,
			new Vector2(x + rectangle.Width * scale * percent, y + 4f * scale),
			null, Color.White, 0f,
			new Vector2(0.5f * ColorSlidertexture.Width, 0.5f * ColorSlidertexture.Height),
			scale, SpriteEffects.None, 0f);

		// LOGIC
		if (IsMouseHovering && Main.mouseLeft) {
			float newPerc = (Main.mouseX - rectangle.X) / (float)rectangle.Width;
			newPerc = Utils.Clamp(newPerc, 0f, 1f);
			_SlideKeyboardAction(newPerc);
			Main.LocalPlayer.mouseInterface = true;
			slidin = true;
		}
		if (ContainsPoint(Main.MouseScreen)) // so you can't use items while on the slider
		{
			Main.LocalPlayer.mouseInterface = true;
		}
		return;
	}
}
