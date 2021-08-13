using System.Reflection;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using System.Collections.Generic;

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
        }

        public override bool Remove(UIElement item)
        {
            method_uiElementRemoveChild.Invoke(field_innerList.GetValue(this), new object[] { item });
            return Elements.Remove(item);
        }
    }
}
