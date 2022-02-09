using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;
using static Creativetools.src.NearestToMouse;

namespace Creativetools.src.Tools.GameInfo;

internal class GameInfo : UIState
{
	public static bool Visible;
	private readonly UIText Mouseinfo = new("") { TextColor = Color.AntiqueWhite, MarginTop = 20 };
	private readonly UIText Playerinfo = new("") { TextColor = Color.Firebrick, MarginTop = 50 };
	private readonly UIText Screeninfo = new("") { TextColor = Color.Goldenrod };
	private readonly UIText NPCinfo = new("") { TextColor = Color.Salmon };
	private readonly UIText Iteminfo = new("") { TextColor = Color.DodgerBlue };
	private readonly UIText Projectileinfo = new("") { TextColor = Color.HotPink };
	private readonly UIText Gameinfo = new("") { TextColor = Color.DeepSkyBlue, MarginTop = Main.screenHeight - 20 };

	private static Config ConfigInstance => ModContent.GetInstance<Config>();

	public override void OnInitialize()
	{
		if (!ConfigInstance.HideMouse) Append(Mouseinfo);
		if (!ConfigInstance.HidePlayer) Append(Playerinfo);
		if (!ConfigInstance.HideScreen) Append(Screeninfo);
		if (!ConfigInstance.HideNPC) Append(NPCinfo);
		if (!ConfigInstance.HideItem) Append(Iteminfo);
		if (!ConfigInstance.HideProjectile) Append(Projectileinfo);
		if (!ConfigInstance.HideGame) Append(Gameinfo);
	}

	public override void Update(GameTime gameTime)
	{
		NPC selectedNPC = Main.npc[GetNPCMouseClosest()];
		Item selectedItem = Main.item[GetItemMouseClosest()];
		Projectile selectedProjectile = Main.projectile[GetProjectileMouseClosest()];

		//mouse information
		Mouseinfo.Left.Set(Main.mouseX, 0f);
		Mouseinfo.Top.Set(Main.mouseY, 0f);
		Mouseinfo.SetText(!ConfigInstance.HideMouse ?
			$"Mouse position relative to world: {Main.MouseWorld}"
			+ $"\nMouse position relative to screen: {Main.MouseScreen}"
			+ "\nLast Mouse position relative to screen: {" + $"{Main.lastMouseX}, {Main.lastMouseY}" + "}"
			+ $"\nMouse Color: {Main.mouseColor}"
			+ $"\nBorder Color: {Main.MouseBorderColor}" : "");

		//player information
		Player player = Main.LocalPlayer;
		Playerinfo.Left.Set(player.position.X - Main.screenPosition.X, 0f);
		Playerinfo.Top.Set(player.position.Y - Main.screenPosition.Y, 0f);
		Playerinfo.SetText(!ConfigInstance.HidePlayer ?
			$"Player name: {player.name}, whoAmI: {player.whoAmI}"
			+ $"\nPosition: {player.position}, Velocity: {player.velocity}"
			+ $"\nliferegen: {player.lifeRegen}, liferegen time: {player.lifeRegenTime}, manaregen: {player.manaRegen}, minion number: {player.numMinions}"
			+ $"\nrocket time: {player.rocketTime}, respawn time: {player.respawnTimer}, imunity timer: {player.immuneTime}, flight timer: {player.wingTime}"
			+ $"\nluck: {player.luck}" : "");

		//screen information
		Screeninfo.SetText(!ConfigInstance.HideScreen ? $"Screen position: {Main.screenPosition}, Screen Zoom:  {Main.GameZoomTarget}" : "");

		//NPC information
		NPCinfo.Left.Set(selectedNPC.position.X - Main.screenPosition.X, 0f);
		NPCinfo.Top.Set(Main.ReverseGravitySupport(selectedNPC.Bottom - Main.screenPosition).Y, 0f);
		NPCinfo.SetText(!ConfigInstance.HideNPC ?
			$"NPC name/type/aiStyle: {selectedNPC.TypeName}/{selectedNPC.type}/{selectedNPC.aiStyle}"
			+ $"\nPosition: {selectedNPC.position}, Velocity: {selectedNPC.velocity}"
			+ $"\nDistance to Mouse: {selectedNPC.Hitbox.Distance(Main.MouseWorld)}" : "");

		//item information
		Iteminfo.Left.Set(selectedItem.position.X - Main.screenPosition.X, 0f);
		Iteminfo.Top.Set(Main.ReverseGravitySupport(selectedItem.Bottom - Main.screenPosition).Y, 0f);
		Iteminfo.SetText(!ConfigInstance.HideItem ?
			$"Item name/type: {selectedItem.Name} / {selectedItem.type}"
			+ $"\nPosition: {selectedItem.position}"
			+ $"\nDistance to Mouse: {selectedItem.Hitbox.Distance(Main.MouseWorld)}" : "");

		//Projectile information
		Projectileinfo.Left.Set(selectedProjectile.position.X - Main.screenPosition.X, 0f);
		Projectileinfo.Top.Set(Main.ReverseGravitySupport(selectedProjectile.Bottom - Main.screenPosition).Y, 0f);
		Projectileinfo.SetText(!ConfigInstance.HideProjectile ?
			$"Projectile name/type: {selectedProjectile.Name} / {selectedProjectile.type}"
			+ $"\nPosition: {selectedProjectile.position}, Velocity: {selectedProjectile.velocity} ({selectedProjectile.direction})"
			+ $"\nDistance to Mouse: {selectedProjectile.Hitbox.Distance(Main.MouseWorld)}" : "");

		//Game information
		Gameinfo.MarginTop = Main.screenHeight - 20;
		Gameinfo.SetText(!ConfigInstance.HideGame ? $"Time: {Main.time}, Global Timer: {Main.GlobalTimeWrappedHourly}, Rain Time: {Main.rainTime}" : "");
	}

	public static void WorldDraw()
	{
		var playerRect = new Rectangle((int)Main.LocalPlayer.position.X, (int)Main.LocalPlayer.position.Y, Main.LocalPlayer.Hitbox.Width, Main.LocalPlayer.Hitbox.Height);
		playerRect.Offset((int)-Main.screenPosition.X, (int)-Main.screenPosition.Y);
		playerRect = Main.ReverseGravitySupport(playerRect);

		var npcRect = Main.npc[GetNPCMouseClosest()].getRect();
		npcRect.Offset((int)-Main.screenPosition.X, (int)-Main.screenPosition.Y);
		npcRect = Main.ReverseGravitySupport(npcRect);

		var projRect = Main.projectile[GetProjectileMouseClosest()].getRect();
		projRect.Offset((int)-Main.screenPosition.X, (int)-Main.screenPosition.Y);
		projRect = Main.ReverseGravitySupport(projRect);

		var itemRect = Main.item[GetItemMouseClosest()].getRect();
		itemRect.Offset((int)-Main.screenPosition.X, (int)-Main.screenPosition.Y);
		itemRect = Main.ReverseGravitySupport(itemRect);

		if (!ConfigInstance.Hitboxes)
		{
			if (!ConfigInstance.HidePlayer) Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, playerRect, Color.Firebrick * 0.5f);
			if (!ConfigInstance.HideNPC) Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, npcRect, Color.Salmon * 0.5f);
			if (!ConfigInstance.HideProjectile) Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, projRect, Color.HotPink * 0.5f);
			if (!ConfigInstance.HideItem) Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, itemRect, Color.DodgerBlue * 0.5f);
		}
	}

	protected override void DrawSelf(SpriteBatch spriteBatch)
	{
		base.DrawSelf(spriteBatch);
		if (!ConfigInstance.Hitboxes && !ConfigInstance.HideMouse)
		{
			Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value,
				new Rectangle(Main.mouseX,
					Main.mouseY,
					Main.ThickMouse ? (int)(14 * 1.1f) : 14,
					Main.ThickMouse ? (int)(14 * 1.1f) : 14),
				Color.AntiqueWhite * 0.5f);
		}
	}
}
