using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using ReLogic.Graphics;
using System;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.UI.Chat;

namespace Creativetools.src.Tools.AssemblyViewer.Elements;

class UIFontText : UIElement
{
	private object _text = "";
	private float _textScale = 1f;
	private Vector2 _textSize = Vector2.Zero;
	private bool _isLarge;
	private Color _color = Color.White;
	private bool _isWrapped;
	private string _visibleText;
	private string _lastTextReference;

	public string Text => _text.ToString();
	public bool DynamicallyScaleDownToWidth;
	public DynamicSpriteFont Font { get; private set; }
	public float TextOriginX { get; set; }
	public float WrappedTextBottomPadding { get; set; }

	public bool IsWrapped {
		get => _isWrapped;
		set {
			_isWrapped = value;
			InternalSetText(_text, _textScale, _isLarge);
		}
	}

	public Color TextColor {
		get => _color;
		set => _color = value;
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
		InternalSetText(_text, _textScale, _isLarge);
		base.Recalculate();
	}

	public void SetText(string text)
	{
		InternalSetText(text, _textScale, _isLarge);
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

		if (_isLarge) {
			pos.Y -= 10f * _textScale;
		}
		else {
			pos.Y -= 2f * _textScale;
		}

		pos.X += (innerDimensions.Width - _textSize.X) * TextOriginX;

		float num = _textScale;
		if (DynamicallyScaleDownToWidth && _textSize.X > innerDimensions.Width) {
			num *= innerDimensions.Width / _textSize.X;
		}
		if (_isLarge) {
			DrawStringBig(spriteBatch, Font, _visibleText, pos, _color, num);
		}
		else {
			DrawString(spriteBatch, Font, _visibleText, pos, _color, num);
		}
	}

	private void VerifyTextState()
	{
		if ((object)_lastTextReference != Text) {
			InternalSetText(_text, _textScale, _isLarge);
		}
	}

	private void InternalSetText(object text, float textScale, bool large)
	{
		_text = text;
		_isLarge = large;
		_textScale = textScale;
		_lastTextReference = _text.ToString();

		if (IsWrapped) {
			_visibleText = Font.CreateWrappedText(_lastTextReference, GetInnerDimensions().Width / _textScale);
		}
		else {
			_visibleText = _lastTextReference;
		}
		Vector2 vector = Font.MeasureString(_visibleText);
		Vector2 vector2 = _textSize = ((!IsWrapped) ? (new Vector2(vector.X, large ? 32f : 16f) * textScale) : (new Vector2(vector.X, vector.Y + WrappedTextBottomPadding) * textScale));

		MinWidth.Set(vector2.X + PaddingLeft + PaddingRight, 0f);
		MinHeight.Set(vector2.Y + PaddingTop + PaddingBottom, 0f);
		if (OnInternalTextChange != null) {
			OnInternalTextChange();
		}
	}

	private static Vector2 DrawString(SpriteBatch sb, DynamicSpriteFont font, string text, Vector2 pos, Color color, float scale = 1f, float anchorx = 0f, float anchory = 0f, int maxCharactersDisplayed = -1)
	{
		if (maxCharactersDisplayed != -1 && text.Length > maxCharactersDisplayed) {
			text.Substring(0, maxCharactersDisplayed);
		}
		Vector2 vector = font.MeasureString(text);
		ChatManager.DrawColorCodedStringWithShadow(sb, font, text, pos, color, 0f, new Vector2(anchorx, anchory) * vector, new Vector2(scale), -1f, 1.5f);
		return vector * scale;
	}

	private Vector2 DrawStringBig(SpriteBatch spriteBatch, DynamicSpriteFont font, string text, Vector2 pos, Color color, float scale = 1f, float anchorx = 0f, float anchory = 0f, int maxCharactersDisplayed = -1)
	{
		if (maxCharactersDisplayed != -1 && text.Length > maxCharactersDisplayed) {
			text.Substring(0, maxCharactersDisplayed);
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
