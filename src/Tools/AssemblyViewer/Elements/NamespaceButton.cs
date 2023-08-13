using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace Creativetools.Tools.AssemblyViewer.Elements;

internal class NamespaceButton : UIFontText
{
	public readonly Namespace _namespace;
	public NamespaceButton(Namespace ns) : base(FontSystem.ConsolasFont, "  " + ns.Name)
	{
		_namespace = ns;

		var texture = ModContent.Request<Texture2D>("Creativetools/UI Assets/AssemblyViewer/Folder", ReLogic.Content.AssetRequestMode.ImmediateLoad);
		Append(new UIImage(texture));
	}

	public override void LeftClick(UIMouseEvent evt)
	{
		base.LeftClick(evt);

		UISystem.UserInterface.SetState(new AssemblyViewer(_namespace.FullName));
	}
}
