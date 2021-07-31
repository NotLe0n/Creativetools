﻿using System.Reflection;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace Creativetools.src.UI.Elements
{
    internal class BetterUIList : UIList
    {
        public static FieldInfo field_innerList;
        public static MethodInfo method_uiElementAppend;
        public static MethodInfo method_uiElementRecalcuate;
        public static MethodInfo method_uiElementRemoveChild;

        static BetterUIList()
        {
            field_innerList = typeof(UIList).GetField("_innerList", BindingFlags.Instance | BindingFlags.NonPublic);
            method_uiElementAppend = typeof(UIElement).GetMethod("Append", BindingFlags.Instance | BindingFlags.Public);
            method_uiElementRecalcuate = typeof(UIElement).GetMethod("Recalculate", BindingFlags.Instance | BindingFlags.Public);
            method_uiElementRemoveChild = typeof(UIElement).GetMethod("RemoveChild", BindingFlags.Instance | BindingFlags.Public);
        }

        public override void Add(UIElement item)
        {
            Elements.Add(item);
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
