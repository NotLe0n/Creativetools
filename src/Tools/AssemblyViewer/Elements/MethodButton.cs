using Microsoft.Xna.Framework.Graphics;
using System.Reflection;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace Creativetools.Tools.AssemblyViewer.Elements;

internal class MethodButton : UIFontText
{
	public readonly MethodInfo _method;
	public MethodButton(MethodInfo method) : base(FontSystem.ConsolasFont, "  " + method.Name + $"({GetParametersAsString(method)})")
	{
		_method = method;
		TextColor = Microsoft.Xna.Framework.Color.LightGray;

		string img = "Method";
		if (method.Name.StartsWith("add_") || method.Name.StartsWith("remove_"))
			img = "Event";

		if (method.Name.StartsWith("op_"))
			img = "Operator";

		if (method.IsPrivate)
			img += "Private";

		var texture = ModContent.Request<Texture2D>("Creativetools/UI Assets/AssemblyViewer/" + img, ReLogic.Content.AssetRequestMode.ImmediateLoad);
		Append(new UIImage(texture));
	}

	public override void LeftClick(UIMouseEvent evt)
	{
		base.LeftClick(evt);
		var based = _method as MethodBase;

		if (UISystem.UserInterface2.CurrentState == null) {
			UISystem.UserInterface2.SetState(new ILWindow(based));
		}
		else {
			UISystem.UserInterface2.SetState(null);
		}
	}

	private static string GetParametersAsString(MethodInfo method)
	{
		string str = string.Empty;
		foreach (var parameter in method.GetParameters()) {
			str += $"{PrimitiveTypeNameToStructName(parameter.ParameterType.Name)} {parameter.Name}";
			if (parameter.Position != method.GetParameters().Length - 1) {
				str += ", ";
			}
		}
		return str;
	}

	// :)
	private static string PrimitiveTypeNameToStructName(string name)
	{
		if (name.Contains("Byte"))
			return name.Replace("Byte", "byte");
		if (name.Contains("SByte"))
			return name.Replace("SByte", "sbyte");
		if (name.Contains("UInt16"))
			return name.Replace("UInt16", "ushort");
		if (name.Contains("Int16"))
			return name.Replace("Int16", "short");
		if (name.Contains("UInt32"))
			return name.Replace("UInt32", "uint");
		if (name.Contains("Int32"))
			return name.Replace("Int32", "int");
		if (name.Contains("UInt64"))
			return name.Replace("UInt64", "ulong");
		if (name.Contains("Int64"))
			return name.Replace("Int64", "long");
		if (name.Contains("Boolean"))
			return name.Replace("Boolean", "bool");
		if (name.Contains("Double"))
			return name.Replace("Double", "double");
		if (name.Contains("Single"))
			return name.Replace("Single", "float");
		if (name.Contains("String"))
			return name.Replace("String", "string");
		if (name.Contains("Object"))
			return name.Replace("Object", "object");
		return name;
	}
}
