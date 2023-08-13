using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.UI;

namespace Creativetools.UI.Elements;

internal class UIHorizontalScrollbar : UIElement
{
	private float _viewPosition;
	private float _viewSize = 1f;
	private float _maxViewSize = 20f;
	private bool _isDragging;
	private float _dragXOffset;
	private bool _isHoveringOverHandle;
	private readonly Asset<Texture2D> _texture;
	private readonly Asset<Texture2D> _innerTexture;

	public UIHorizontalScrollbar()
	{
		Height.Set(20f, 0f);
		MaxHeight.Set(20f, 0f);
		_texture = Main.Assets.Request<Texture2D>("Images/UI/Scrollbar");
		_innerTexture = Main.Assets.Request<Texture2D>("Images/UI/ScrollbarInner");
		PaddingLeft = 5f;
		PaddingRight = 5f;
	}

	public float ViewPosition {
		get => _viewPosition;
		set => _viewPosition = MathHelper.Clamp(value, 0f, _maxViewSize - _viewSize);
	}

	public void SetView(float viewSize, float maxViewSize)
	{
		viewSize = MathHelper.Clamp(viewSize, 0f, maxViewSize);
		_viewPosition = MathHelper.Clamp(_viewPosition, 0f, maxViewSize - viewSize);
		_viewSize = viewSize;
		_maxViewSize = maxViewSize;
	}

	private Rectangle GetHandleRectangle()
	{
		CalculatedStyle innerDimensions = GetInnerDimensions();
		if (_maxViewSize == 0f && _viewSize == 0f) {
			_viewSize = 1f;
			_maxViewSize = 1f;
		}

		return new Rectangle(
			(int)(innerDimensions.X + innerDimensions.Width * (_viewPosition / _maxViewSize)) - 3,
			(int)innerDimensions.Y,
			(int)(innerDimensions.Width * (_viewSize / _maxViewSize)) + 7,
			20);
	}

	private void DrawBar(SpriteBatch spriteBatch, Rectangle dimensions)
	{
		Texture2D texture = _texture.Value;

		// draw top
		spriteBatch.Draw(texture, new Rectangle(dimensions.X - 6, dimensions.Y, 6, dimensions.Height), new Rectangle(0, 0, 6, texture.Height), Color.White);
		// draw middle
		spriteBatch.Draw(texture, new Rectangle(dimensions.X, dimensions.Y, dimensions.Width, dimensions.Height), new Rectangle(8, 0, 4, texture.Height), Color.White);
		// draw bottom
		spriteBatch.Draw(texture, new Rectangle(dimensions.X + dimensions.Width, dimensions.Y, 6, dimensions.Height), new Rectangle(texture.Width - 6, 0, 6, texture.Height), Color.White);
	}

	private void DrawHandle(SpriteBatch spriteBatch, Rectangle dimensions)
	{
		Texture2D texture = _innerTexture.Value;
		Color color = Color.White * ((_isDragging || _isHoveringOverHandle) ? 1f : 0.85f);

		// draw top
		spriteBatch.Draw(texture, new Rectangle(dimensions.X - 8, dimensions.Y + 3, 8, dimensions.Height - 6), new Rectangle(0, 0, 8, texture.Height), color);
		// draw middle
		spriteBatch.Draw(texture, new Rectangle(dimensions.X, dimensions.Y + 3, dimensions.Width, dimensions.Height - 6), new Rectangle(8, 0, 4, texture.Height), color);
		// draw bottom
		spriteBatch.Draw(texture, new Rectangle(dimensions.X + dimensions.Width, dimensions.Y + 3, 8, dimensions.Height - 6), new Rectangle(texture.Width - 8, 0, 8, texture.Height), color);
	}

	protected override void DrawSelf(SpriteBatch spriteBatch)
	{
		CalculatedStyle dimensions = GetDimensions();
		CalculatedStyle innerDimensions = GetInnerDimensions();

		if (_isDragging) {
			float num = UserInterface.ActiveInstance.MousePosition.X - innerDimensions.X - _dragXOffset;
			_viewPosition = MathHelper.Clamp(num / innerDimensions.Width * _maxViewSize, 0f, _maxViewSize - _viewSize);
		}

		// play tick sound on hover
		Rectangle handleRectangle = GetHandleRectangle();
		Vector2 mousePosition = UserInterface.ActiveInstance.MousePosition;
		bool isHoveringOverHandle = _isHoveringOverHandle;
		_isHoveringOverHandle = handleRectangle.Contains(new Point((int)mousePosition.X, (int)mousePosition.Y));

		if (!isHoveringOverHandle && _isHoveringOverHandle && Main.hasFocus)
			SoundEngine.PlaySound(SoundID.MenuTick);

		// Draw Scrollbar
		DrawBar(spriteBatch, dimensions.ToRectangle());
		DrawHandle(spriteBatch, handleRectangle);
	}

	public override void LeftMouseDown(UIMouseEvent evt)
	{
		base.LeftMouseDown(evt);
		if (evt.Target == this) {
			Rectangle handleRectangle = GetHandleRectangle();
			if (handleRectangle.Contains(new Point((int)evt.MousePosition.X, (int)evt.MousePosition.Y))) {
				_isDragging = true;
				_dragXOffset = evt.MousePosition.X - handleRectangle.X;
			}
			else {
				CalculatedStyle innerDimensions = GetInnerDimensions();
				float num = UserInterface.ActiveInstance.MousePosition.X - innerDimensions.X - (handleRectangle.Width >> 1);
				_viewPosition = MathHelper.Clamp(num / innerDimensions.Width * _maxViewSize, 0f, _maxViewSize - _viewSize);
			}
		}
	}

	public override void LeftMouseUp(UIMouseEvent evt)
	{
		base.LeftMouseUp(evt);
		_isDragging = false;
	}
}
