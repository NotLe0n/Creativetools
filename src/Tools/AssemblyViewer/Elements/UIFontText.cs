using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using Terraria.UI;
using Terraria.UI.Chat;

namespace Creativetools.Tools.AssemblyViewer.Elements;

internal class UIFontText : UIElement
{
	private object text = "";
	private float textScale = 1f;
	private Vector2 textSize = Vector2.Zero;
	private bool isLarge;
	private Color color = Color.White;
	private bool isWrapped;
	private string visibleText;
	private string lastTextReference;

	public string Text => text.ToString();
	public bool DynamicallyScaleDownToWidth;
	public DynamicSpriteFont Font { get; private set; }
	public float TextOriginX { get; set; }
	public float WrappedTextBottomPadding { get; set; }

	public bool IsWrapped {
		get => isWrapped;
		set {
			isWrapped = value;
			InternalSetText(text, textScale, isLarge);
		}
	}

	public Color TextColor {
		get => color;
		set => color = value;
	}

	public event Action OnInternalTextChange;

	public UIFontText(DynamicSpriteFont font, string text, float textScale = 1f, bool large = false)
	{
		Font = font;
		TextOriginX = 0.5f;
		IsWrapped = false;
		WrappedTextBottomPadding = 20f;
		InternalSetText(text, textScale, large);
	}

	public override void Recalculate()
	{
		InternalSetText(text, textScale, isLarge);
		base.Recalculate();
	}

	public void SetText(string text)
	{
		InternalSetText(text, textScale, isLarge);
	}

	public void SetText(string text, float textScale, bool large)
	{
		InternalSetText(text, textScale, large);
	}

	protected override void DrawSelf(SpriteBatch spriteBatch)
	{
		base.DrawSelf(spriteBatch);
		VerifyTextState();
		CalculatedStyle innerDimensions = GetInnerDimensions();
		Vector2 pos = innerDimensions.Position();

		if (isLarge) {
			pos.Y -= 10f * textScale;
		}
		else {
			pos.Y -= 2f * textScale;
		}

		pos.X += (innerDimensions.Width - textSize.X) * TextOriginX;

		float num = textScale;
		if (DynamicallyScaleDownToWidth && textSize.X > innerDimensions.Width) {
			num *= innerDimensions.Width / textSize.X;
		}
		if (isLarge) {
			DrawStringBig(spriteBatch, Font, visibleText, pos, color, num);
		}
		else {
			DrawString(spriteBatch, Font, visibleText, pos, color, num);
		}
	}

	private void VerifyTextState()
	{
		if ((object)lastTextReference != Text) {
			InternalSetText(text, textScale, isLarge);
		}
	}

	private void InternalSetText(object text, float textScale, bool large)
	{
		this.text = text;
		isLarge = large;
		this.textScale = textScale;
		lastTextReference = this.text.ToString();

		if (IsWrapped) {
			visibleText = Font.CreateWrappedText(lastTextReference, GetInnerDimensions().Width / this.textScale);
		}
		else {
			visibleText = lastTextReference;
		}
		Vector2 vector = Font.MeasureString(visibleText);
		Vector2 vector2 = textSize = ((!IsWrapped) ? (new Vector2(vector.X, large ? 32f : 16f) * textScale) : (new Vector2(vector.X, vector.Y + WrappedTextBottomPadding) * textScale));

		MinWidth.Set(vector2.X + PaddingLeft + PaddingRight, 0f);
		MinHeight.Set(vector2.Y + PaddingTop + PaddingBottom, 0f);
        OnInternalTextChange?.Invoke();
    }

	private static Vector2 DrawString(SpriteBatch sb, DynamicSpriteFont font, string text, Vector2 pos, Color color, float scale = 1f, float anchorx = 0f, float anchory = 0f, int maxCharactersDisplayed = -1)
	{
		if (maxCharactersDisplayed != -1 && text.Length > maxCharactersDisplayed) {
			text = text[..maxCharactersDisplayed];
		}
		Vector2 vector = font.MeasureString(text);
		ChatManager.DrawColorCodedStringWithShadow(sb, font, text, pos, color, 0f, new Vector2(anchorx, anchory) * vector, new Vector2(scale), -1f, 1.5f);
		return vector * scale;
	}

	private Vector2 DrawStringBig(SpriteBatch spriteBatch, DynamicSpriteFont font, string text, Vector2 pos, Color color, float scale = 1f, float anchorx = 0f, float anchory = 0f, int maxCharactersDisplayed = -1)
	{
		if (maxCharactersDisplayed != -1 && text.Length > maxCharactersDisplayed) {
			text = text[..maxCharactersDisplayed];
		}
		for (int i = -1; i < 2; i++) {
			for (int j = -1; j < 2; j++) {
				spriteBatch.DrawString(font, text, pos + new Vector2(i, j), Color.Black, 0f, new Vector2(anchorx, anchory) * font.MeasureString(text), scale, SpriteEffects.None, 0f);
			}
		}
		spriteBatch.DrawString(font, text, pos, color, 0f, new Vector2(anchorx, anchory) * font.MeasureString(text), scale, SpriteEffects.None, 0f);
		return font.MeasureString(text) * scale;
	}
}
