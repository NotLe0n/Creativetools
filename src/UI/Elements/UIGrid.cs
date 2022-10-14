using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace Creativetools.UI.Elements;

public class UIGrid : UIElement
{
	public delegate bool ElementSearchMethod(UIElement element);

	private class UIInnerList : UIElement
	{
		public override bool ContainsPoint(Vector2 point)
		{
			return true;
		}

		protected override void DrawChildren(SpriteBatch spriteBatch)
		{
			Vector2 position = Parent.GetDimensions().Position();
			Vector2 dimensions = new(Parent.GetDimensions().Width, Parent.GetDimensions().Height);

			foreach (UIElement current in Elements) {
				Vector2 position2 = current.GetDimensions().Position();
				Vector2 dimensions2 = new(current.GetDimensions().Width, current.GetDimensions().Height);

				if (Collision.CheckAABBvAABBCollision(position, dimensions, position2, dimensions2)) {
					current.Draw(spriteBatch);
				}
			}
		}
	}

	public List<UIElement> items = new();
	protected UIScrollbar scrollbar;
	internal UIElement innerList = new UIInnerList();
	private float innerListHeight;
	public float ListPadding = 5f;

	public int Count {
		get {
			return items.Count;
		}
	}

	private int cols = 1;

	public UIGrid(int columns = 1)
	{
		cols = columns;
		innerList.OverflowHidden = false;
		innerList.Width.Set(0f, 1f);
		innerList.Height.Set(0f, 1f);
		OverflowHidden = true;
		Append(innerList);
	}

	public float GetTotalHeight()
	{
		return innerListHeight;
	}
	public float GetRowWidth()
	{
		float width = 0;
		for (int i = 0; i < items.Count; i++) {
			width = MathHelper.Clamp(items.Count * (items[i].Width.Pixels + ListPadding * 2), items[i].Width.Pixels, 300);
		}
		return width;
	}

	public void Goto(ElementSearchMethod searchMethod, bool center = false)
	{
		for (int i = 0; i < items.Count; i++) {
			if (searchMethod(items[i])) {
				scrollbar.ViewPosition = items[i].Top.Pixels;
				if (center) {
					scrollbar.ViewPosition = items[i].Top.Pixels - GetInnerDimensions().Height / 2 + items[i].GetOuterDimensions().Height / 2;
				}
				return;
			}
		}
	}

	public virtual void Add(UIElement item)
	{
		items.Add(item);
		innerList.Append(item);
		UpdateOrder();
		innerList.Recalculate();
	}

	public virtual bool Remove(UIElement item)
	{
		innerList.RemoveChild(item);
		UpdateOrder();
		return items.Remove(item);
	}

	public virtual void Clear()
	{
		innerList.RemoveAllChildren();
		items.Clear();
	}

	public override void Recalculate()
	{
		base.Recalculate();
		UpdateScrollbar();
	}

	public override void ScrollWheel(UIScrollWheelEvent evt)
	{
		base.ScrollWheel(evt);
		if (scrollbar != null) {
			scrollbar.ViewPosition -= evt.ScrollWheelValue;
		}
	}

	public override void RecalculateChildren()
	{
		base.RecalculateChildren();
		float top = 0f;
		float left = 0f;
		for (int i = 0; i < items.Count; i++) {
			items[i].Top.Set(top, 0f);
			items[i].Left.Set(left, 0f);
			items[i].Recalculate();
			if (i % cols == cols - 1) {
				top += items[i].GetOuterDimensions().Height + ListPadding;
				left = 0;
			}
			else {
				left += items[i].GetOuterDimensions().Width + ListPadding;
			}
			//num += this._items[i].GetOuterDimensions().Height + this.ListPadding;
		}
		if (items.Count > 0) {
			top += ListPadding + items[0].GetOuterDimensions().Height;
		}
		innerListHeight = top;
	}

	private void UpdateScrollbar()
	{
		if (scrollbar == null) {
			return;
		}
		scrollbar.SetView(GetInnerDimensions().Height, innerListHeight);
	}

	public void SetScrollbar(UIScrollbar scrollbar)
	{
		this.scrollbar = scrollbar;
		UpdateScrollbar();
	}

	public void UpdateOrder()
	{
		//items.Sort(new Comparison<UIElement>(SortMethod));
		UpdateScrollbar();
	}

	public int SortMethod(UIElement item1, UIElement item2)
	{
		return item1.CompareTo(item2);
	}

	public override List<SnapPoint> GetSnapPoints()
	{
		List<SnapPoint> list = new();
		if (GetSnapPoint(out SnapPoint item)) {
			list.Add(item);
		}
		foreach (UIElement current in items) {
			list.AddRange(current.GetSnapPoints());
		}
		return list;
	}

	protected override void DrawSelf(SpriteBatch spriteBatch)
	{
		if (scrollbar != null) {
			innerList.Top.Set(-scrollbar.GetValue(), 0f);
		}
		Recalculate();
	}
}
