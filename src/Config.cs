using Microsoft.Xna.Framework;
using System.ComponentModel;
using Terraria.ModLoader.Config;

// ReSharper disable UnassignedField.Global

namespace Creativetools;

public class Config : ModConfig
{
	public override ConfigScope Mode => ConfigScope.ClientSide;

	[DefaultValue(false)] 
	public bool hideMouse;
	
	[DefaultValue(false)] 
	public bool hidePlayer;
	
	[DefaultValue(false)] 
	public bool hideScreen;
	
	[DefaultValue(false)] 
	public bool hideNPC;
	
	[DefaultValue(false)] 
	public bool hideItem;
	
	[DefaultValue(false)] 
	public bool hideProjectile;
	
	[DefaultValue(false)] 
	public bool hideGame;
	
	[DefaultValue(false)] 
	public bool hitboxes;
	
	[DefaultValue(typeof(Vector2), "0, 0")] 
	[Increment(1f)] 
	[Range(-5f, 5f)] 
	[DrawTicks]
	public Vector2 menuBtnOffset;
}
