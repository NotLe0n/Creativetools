using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Reflection;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace Creativetools.Tools.AssemblyViewer.Elements;

internal class FieldButton : UIFontText
{
	public readonly FieldInfo _field;
	public FieldButton(FieldInfo member) : base(FontSystem.ConsolasFont, "  " + member.Name)
	{
		_field = member;
		TextColor = Color.LightGray;

		string img = "Field";
		if (member.IsLiteral) img = "Constant";
		if (member.IsPrivate) img += "Private";

		var texture = ModContent.Request<Texture2D>("Creativetools/UI Assets/AssemblyViewer/" + img, ReLogic.Content.AssetRequestMode.ImmediateLoad);
		Append(new UIImage(texture));
	}

	public override void LeftClick(UIMouseEvent evt)
	{
		base.LeftClick(evt);

		var userInterface2 = UISystem.UserInterface2;

		userInterface2.SetState(
			userInterface2.CurrentState == null ?
			new InspectValue(_field) : null
		);
	}
}
