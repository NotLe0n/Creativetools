using Microsoft.Xna.Framework;
using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace Creativetools;

public class Config : ModConfig
{
	public override ConfigScope Mode => ConfigScope.ClientSide;

	[DefaultValue(false)]
	public bool HideMouse { get; set; }

	[DefaultValue(false)]
	public bool HidePlayer { get; set; }

	[DefaultValue(false)]
	public bool HideScreen { get; set; }

	[DefaultValue(false)]
	public bool HideNPC { get; set; }

	[DefaultValue(false)]
	public bool HideItem { get; set; }

	[DefaultValue(false)]
	public bool HideProjectile { get; set; }

	[DefaultValue(false)]
	public bool HideGame { get; set; }

	[DefaultValue(false)]
	public bool Hitboxes { get; set; }

	[DefaultValue(typeof(Vector2), "0, 0")]
	[Increment(1f)]
	[Range(-5f, 5f)]
	[DrawTicks]
	public Vector2 MenuBtnOffset { get; set; }
}
