using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Reflection;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace Creativetools.Tools.AssemblyViewer.Elements;

internal class PropertyButton : UIFontText
{
	public readonly PropertyInfo _property;
	public PropertyButton(PropertyInfo property) : base(FontSystem.ConsolasFont, "  " + property.Name)
	{
		_property = property;
		TextColor = Color.LightGray;

		string img = "Property";
		if (!property.CanWrite) img += "Getter";
		if (property.GetMethod.IsPrivate) img += "Private";

		var texture = ModContent.Request<Texture2D>("Creativetools/UI Assets/AssemblyViewer/" + img, ReLogic.Content.AssetRequestMode.ImmediateLoad);
		Append(new UIImage(texture));
	}

	public override void Click(UIMouseEvent evt)
	{
		base.Click(evt);

		if (UISystem.UserInterface2.CurrentState == null) {
			UISystem.UserInterface2.SetState(new InspectValue(_property));
		}
		else {
			UISystem.UserInterface2.SetState(null);
		}
	}
}
