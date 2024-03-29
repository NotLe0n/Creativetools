﻿using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace Creativetools.UI.Elements;

public class DragableUIPanel : UIPanel
{
	public bool active = false;
	public event Action OnCloseBtnClicked;
	internal readonly UIPanel header;

	public DragableUIPanel(string headingtext)
	{
		SetPadding(0);

		header = new UIPanel();
		header.SetPadding(0);
		header.Height.Set(30, 0f);
		header.BackgroundColor.A = 255;
		header.OnLeftMouseDown += Header_OnMouseDown;
		header.OnLeftMouseUp += Header_OnMouseUp;
		Append(header);

		var heading = new UIText(headingtext, 0.9f);
		heading.VAlign = 0.5f;
		heading.MarginLeft = 16f;
		header.Append(heading);

		var closeBtn = new UITextPanel<char>('X');
		closeBtn.SetPadding(7);
		closeBtn.Width.Set(40, 0);
		closeBtn.Left.Set(-40, 1f);
		closeBtn.BackgroundColor.A = 255;
		closeBtn.OnLeftClick += (_, _) => OnCloseBtnClicked?.Invoke();
		header.Append(closeBtn);
	}
	public override void OnInitialize()
	{
		base.OnInitialize();
		header.Width = Width;
	}
	public override void Recalculate()
	{
		base.Recalculate();
		header.Width = Width;
	}

	#region Drag code yoiked from ExampleMod 

	private Vector2 offset;
	public bool dragging;
	public static Vector2 lastPos = new(600, 200);
	private void Header_OnMouseDown(UIMouseEvent evt, UIElement elm)
	{
		base.LeftMouseDown(evt);
		offset = new Vector2(evt.MousePosition.X - Left.Pixels, evt.MousePosition.Y - Top.Pixels);
		dragging = true;
	}

	private void Header_OnMouseUp(UIMouseEvent evt, UIElement elm)
	{
		base.LeftMouseUp(evt);
		dragging = false;

		Left.Set(evt.MousePosition.X - offset.X, 0f);
		Top.Set(evt.MousePosition.Y - offset.Y, 0f);
		Recalculate();
	}

	public override void Update(GameTime gameTime)
	{
		base.Update(gameTime); // don't remove.

		// Checking ContainsPoint and then setting mouseInterface to true is very common. This causes clicks on this UIElement to not cause the player to use current items. 
		if (ContainsPoint(Main.MouseScreen))
			Main.LocalPlayer.mouseInterface = true;


		if (dragging) {
			Left.Set(Main.mouseX - offset.X, 0f);
			Top.Set(Main.mouseY - offset.Y, 0f);
			Recalculate();

			lastPos = new Vector2(Left.Pixels, Top.Pixels);
		}

		// Here we check if the DragableUIPanel is outside the Parent UIElement rectangle. 
		// (In our example, the parent would be ExampleUI, a UIState. This means that we are checking that the DragableUIPanel is outside the whole screen)
		// By doing this and some simple math, we can snap the panel back on screen if the user resizes his window or otherwise changes resolution.
		var parentSpace = Parent.GetDimensions().ToRectangle();
		if (!GetDimensions().ToRectangle().Intersects(parentSpace)) {
			Left.Pixels = Utils.Clamp(Left.Pixels, 0, parentSpace.Right - Width.Pixels);
			Top.Pixels = Utils.Clamp(Top.Pixels, 0, parentSpace.Bottom - Height.Pixels);
			// Recalculate forces the UI system to do the positioning math again.
			Recalculate();
		}
	}
	#endregion
}
