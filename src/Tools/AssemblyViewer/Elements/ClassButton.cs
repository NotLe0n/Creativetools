using Microsoft.Xna.Framework.Graphics;
using System.Reflection;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace Creativetools.Tools.AssemblyViewer.Elements;

internal class ClassButton : UIFontText
{
	public readonly TypeInfo _class;
	public ClassButton(TypeInfo clas) : base(FontSystem.ConsolasFont, $"  {(clas.DeclaringType != null ? $"{clas.DeclaringType.Name}." : "")}{clas.Name}")
	{
		_class = clas;

		string img = "Class";
		if (clas.IsEnum)
			img = "EnumItem";

		if (clas.IsNotPublic)
			img += "Private";

		var texture = ModContent.Request<Texture2D>("Creativetools/UI Assets/AssemblyViewer/" + img, ReLogic.Content.AssetRequestMode.ImmediateLoad);
		Append(new UIImage(texture));
	}

	public override void LeftClick(UIMouseEvent evt)
	{
		base.LeftClick(evt);

		UISystem.UserInterface.SetState(new AssemblyViewer(_class.Namespace, _class));
	}
}
