using Microsoft.Xna.Framework.Graphics;
using System.Reflection;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace Creativetools.src.UI.Elements
{
    // Original: https://github.com/Pbone3/FargowiltasSouls/blob/master/UI/UIToggleList.cs (1.3)
    // THANK YOU PBONE <33333333

    internal class BetterUIList : UIList
    {
        private readonly FieldInfo field_innerList;
        private readonly MethodInfo method_uiElementAppend;
        private readonly MethodInfo method_uiElementRecalcuate;
        private readonly MethodInfo method_uiElementRemoveChild;

        private float _innerListWidth;
        public UIHorizontalScrollbar horizontalScrollbar;

        public BetterUIList()
        {
            field_innerList = typeof(UIList).GetField("_innerList", BindingFlags.Instance | BindingFlags.NonPublic);
            method_uiElementAppend = typeof(UIElement).GetMethod("Append", BindingFlags.Instance | BindingFlags.Public);
            method_uiElementRecalcuate = typeof(UIElement).GetMethod("Recalculate", BindingFlags.Instance | BindingFlags.Public);
            method_uiElementRemoveChild = typeof(UIElement).GetMethod("RemoveChild", BindingFlags.Instance | BindingFlags.Public);
        }

        public override void Add(UIElement item)
        {
            _items.Add(item);
            method_uiElementAppend.Invoke(field_innerList.GetValue(this), new object[] { item });
            method_uiElementRecalcuate.Invoke(field_innerList.GetValue(this), null);
            UpdateHorizontalScrollbar();
        }

        public override bool Remove(UIElement item)
        {
            method_uiElementRemoveChild.Invoke(field_innerList.GetValue(this), new object[] { item });
            UpdateHorizontalScrollbar();
            return Elements.Remove(item);
        }

        public override void Recalculate()
        {
            base.Recalculate();
            UpdateHorizontalScrollbar();
        }

        public override void RecalculateChildren()
        {
            base.RecalculateChildren();
            float width = 0f;
            for (int i = 0; i < _items.Count; i++)
            {
                CalculatedStyle outerDimensions = _items[i].GetOuterDimensions();
                if (width < outerDimensions.Width)
                    width = outerDimensions.Width;
            }

            _innerListWidth = width;
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            if (horizontalScrollbar != null)
            {
                (field_innerList.GetValue(this) as UIElement).Left.Set(0f - horizontalScrollbar.ViewPosition, 0f);
            }

            base.DrawSelf(spriteBatch);
        }

        public void SetHorizontalScrollbar(UIHorizontalScrollbar scrollbar)
        {
            horizontalScrollbar = scrollbar;
            UpdateHorizontalScrollbar();
        }

        private void UpdateHorizontalScrollbar()
        {
            if (horizontalScrollbar != null)
            {
                float width = GetInnerDimensions().Width;
                horizontalScrollbar.SetView(width, _innerListWidth);
            }
        }
    }
}
