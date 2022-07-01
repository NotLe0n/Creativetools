﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ReLogic.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace Creativetools.src.UI.Elements;

//ty jopojelly and darthmorf
internal class NewUITextBox : UIPanel
{
	internal string currentString = string.Empty;

	internal bool focused = false;

	private readonly int _maxLength = int.MaxValue;

	private readonly string hintText;
	private int textBlinkerCount;
	private int textBlinkerState;

	public event Action OnFocus;
	public event Action OnUnfocus;
	public event Action OnTextChanged;
	public event Action OnTabPressed;
	public event Action OnEnterPressed;

	internal bool unfocusOnEnter = true;
	internal bool unfocusOnTab = true;

	public Color TextColor { get; set; }
	public string Text { get => currentString; set => currentString = value; }
	public float TextScale { get; set; }

	internal NewUITextBox(string hintText, float textScale = 1f, string text = "")
	{
		this.hintText = hintText;
		currentString = text;
		SetPadding(0);
		BackgroundColor = new Color(63, 82, 151) * 0.7f;
		BorderColor = Color.Black;
		TextColor = Color.White;
		TextScale = textScale;
	}

	public override void Click(UIMouseEvent evt)
	{
		Focus();
		base.Click(evt);
	}

	internal void Unfocus()
	{
		if (focused) {
			focused = false;
			Main.blockInput = false;

			OnUnfocus?.Invoke();
		}
	}

	internal void Focus()
	{
		if (!focused) {
			Main.clrInput();
			focused = true;
			Main.blockInput = true;

			OnFocus?.Invoke();
		}
	}

	public override void Update(GameTime gameTime)
	{
		Vector2 MousePosition = new(Main.mouseX, Main.mouseY);
		if (!ContainsPoint(MousePosition) && (Main.mouseLeft || Main.mouseRight)) //This solution is fine, but we need a way to cleanly "unload" a UIElement
		{
			//TODO, figure out how to refocus without triggering unfocus while clicking enable button.
			Unfocus();
		}
		base.Update(gameTime);
	}

	internal void SetText(string text)
	{
		if (text.Length > _maxLength) {
			text = text.Substring(0, _maxLength);
		}
		if (currentString != text) {
			currentString = text;
			OnTextChanged?.Invoke();
		}
	}

	private static bool JustPressed(Keys key)
	{
		return Main.inputText.IsKeyDown(key) && !Main.oldInputText.IsKeyDown(key);
	}

	protected override void DrawSelf(SpriteBatch spriteBatch)
	{
		Rectangle hitbox = GetInnerDimensions().ToRectangle();

		//Draw panel
		base.DrawSelf(spriteBatch);

		if (focused) {
			Terraria.GameInput.PlayerInput.WritingText = true;
			Main.instance.HandleIME();
			string newString = Main.GetInputText(currentString);
			if (!newString.Equals(currentString)) {
				currentString = newString;
				OnTextChanged?.Invoke();
			}
			else {
				currentString = newString;
			}

			if (JustPressed(Keys.Tab)) {
				if (unfocusOnTab) Unfocus();
				OnTabPressed?.Invoke();
			}
			if (JustPressed(Keys.Enter)) {
				Main.drawingPlayerChat = false;
				if (unfocusOnEnter) Unfocus();
				OnEnterPressed?.Invoke();
			}
			if (++textBlinkerCount >= 20) {
				textBlinkerState = (textBlinkerState + 1) % 2;
				textBlinkerCount = 0;
			}
			Main.instance.DrawWindowsIMEPanel(new Vector2(98f, Main.screenHeight - 36), 0f);
		}
		string displayString = currentString;
		if (textBlinkerState == 1 && focused) {
			displayString += "|";
		}
		CalculatedStyle space = GetDimensions();
		Color color = TextColor;
		Vector2 drawPos = space.Position() + new Vector2(4, 2);
		if (currentString.Length == 0 && !focused) {
			color *= 0.5f;
			spriteBatch.DrawString(FontAssets.MouseText.Value, hintText, drawPos, color, 0f, Vector2.Zero, TextScale, SpriteEffects.None, 0);
		}
		else {
			spriteBatch.DrawString(FontAssets.MouseText.Value, displayString, drawPos, TextColor, 0f, Vector2.Zero, TextScale, SpriteEffects.None, 0);
		}
	}
}
