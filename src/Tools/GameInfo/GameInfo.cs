using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;
using static Creativetools.NearestToMouse;

namespace Creativetools.Tools.GameInfo;

internal class GameInfo : UIState
{
	public static bool visible;
	private readonly UIText mouseInfo = new("") { TextColor = Color.AntiqueWhite, MarginTop = 20 };
	private readonly UIText playerInfo = new("") { TextColor = Color.Firebrick, MarginTop = 50 };
	private readonly UIText screenInfo = new("") { TextColor = Color.Goldenrod };
	private readonly UIText npcInfo = new("") { TextColor = Color.Salmon };
	private readonly UIText itemInfo = new("") { TextColor = Color.DodgerBlue };
	private readonly UIText projectileInfo = new("") { TextColor = Color.HotPink };
	private readonly UIText gameInfo = new("") { TextColor = Color.DeepSkyBlue, MarginTop = Main.screenHeight - 20 };

	private static Config ConfigInstance => ModContent.GetInstance<Config>();

	public override void OnInitialize()
	{
		if (!ConfigInstance.hideMouse) Append(mouseInfo);
		if (!ConfigInstance.hidePlayer) Append(playerInfo);
		if (!ConfigInstance.hideScreen) Append(screenInfo);
		if (!ConfigInstance.hideNPC) Append(npcInfo);
		if (!ConfigInstance.hideItem) Append(itemInfo);
		if (!ConfigInstance.hideProjectile) Append(projectileInfo);
		if (!ConfigInstance.hideGame) Append(gameInfo);
	}

	public override void Update(GameTime gameTime)
	{
		NPC selectedNPC = Main.npc[GetNPCMouseClosest()];
		Item selectedItem = Main.item[GetItemMouseClosest()];
		Projectile selectedProjectile = Main.projectile[GetProjectileMouseClosest()];

		//mouse information
		mouseInfo.Left.Set(Main.mouseX, 0f);
		mouseInfo.Top.Set(Main.mouseY, 0f);
		mouseInfo.SetText(!ConfigInstance.hideMouse ?
			$"Mouse position relative to world: {Main.MouseWorld}"
			+ $"\nMouse position relative to screen: {Main.MouseScreen}"
			+ $"\nLast Mouse position relative to screen: {{{Main.lastMouseX}, {Main.lastMouseY}}}"
			+ $"\nMouse Tile position: {Main.MouseWorld.ToTileCoordinates()}"
			+ $"\nMouse Color: {Main.mouseColor}"
			+ $"\nBorder Color: {Main.MouseBorderColor}" : "");

		//player information
		Player player = Main.LocalPlayer;
		playerInfo.Left.Set(player.position.X - Main.screenPosition.X, 0f);
		playerInfo.Top.Set(player.position.Y - Main.screenPosition.Y, 0f);
		playerInfo.SetText(!ConfigInstance.hidePlayer ?
			$"Player name: {player.name}, whoAmI: {player.whoAmI}"
			+ $"\nPosition: {player.position}, Velocity: {player.velocity}"
			+ $"\nliferegen: {player.lifeRegen}, liferegen time: {player.lifeRegenTime}, manaregen: {player.manaRegen}, minion number: {player.numMinions}"
			+ $"\nrocket time: {player.rocketTime}, respawn time: {player.respawnTimer}, imunity timer: {player.immuneTime}, flight timer: {player.wingTime}"
			+ $"\nluck: {player.luck}" : "");

		//screen information
		screenInfo.SetText(!ConfigInstance.hideScreen ? $"Screen position: {Main.screenPosition}, Screen Zoom:  {Main.GameZoomTarget}" : "");

		//NPC information
		npcInfo.Left.Set(selectedNPC.position.X - Main.screenPosition.X, 0f);
		npcInfo.Top.Set(Main.ReverseGravitySupport(selectedNPC.Bottom - Main.screenPosition).Y, 0f);
		npcInfo.SetText(!ConfigInstance.hideNPC ?
			$"NPC name/type/aiStyle: {selectedNPC.TypeName}/{selectedNPC.type}/{selectedNPC.aiStyle}"
			+ $"\nPosition: {selectedNPC.position}, Velocity: {selectedNPC.velocity}"
			+ $"\nDistance to Mouse: {selectedNPC.Hitbox.Distance(Main.MouseWorld)}" : "");

		//item information
		itemInfo.Left.Set(selectedItem.position.X - Main.screenPosition.X, 0f);
		itemInfo.Top.Set(Main.ReverseGravitySupport(selectedItem.Bottom - Main.screenPosition).Y, 0f);
		itemInfo.SetText(!ConfigInstance.hideItem ?
			$"Item name/type: {selectedItem.Name} / {selectedItem.type}"
			+ $"\nPosition: {selectedItem.position}"
			+ $"\nDistance to Mouse: {selectedItem.Hitbox.Distance(Main.MouseWorld)}" : "");

		//Projectile information
		projectileInfo.Left.Set(selectedProjectile.position.X - Main.screenPosition.X, 0f);
		projectileInfo.Top.Set(Main.ReverseGravitySupport(selectedProjectile.Bottom - Main.screenPosition).Y, 0f);
		projectileInfo.SetText(!ConfigInstance.hideProjectile ?
			$"Projectile name/type: {selectedProjectile.Name} / {selectedProjectile.type}"
			+ $"\nPosition: {selectedProjectile.position}, Velocity: {selectedProjectile.velocity} ({selectedProjectile.direction})"
			+ $"\nDistance to Mouse: {selectedProjectile.Hitbox.Distance(Main.MouseWorld)}" : "");

		//Game information
		gameInfo.MarginTop = Main.screenHeight - 20;
		gameInfo.SetText(!ConfigInstance.hideGame ? $"Time: {Main.time}, Global Timer: {Main.GlobalTimeWrappedHourly}, Rain Time: {Main.rainTime}" : "");
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

		if (!ConfigInstance.hitboxes) {
			if (!ConfigInstance.hidePlayer) Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, playerRect, Color.Firebrick * 0.5f);
			if (!ConfigInstance.hideNPC) Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, npcRect, Color.Salmon * 0.5f);
			if (!ConfigInstance.hideProjectile) Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, projRect, Color.HotPink * 0.5f);
			if (!ConfigInstance.hideItem) Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, itemRect, Color.DodgerBlue * 0.5f);
		}
	}

	protected override void DrawSelf(SpriteBatch spriteBatch)
	{
		base.DrawSelf(spriteBatch);
		if (!ConfigInstance.hitboxes && !ConfigInstance.hideMouse) {
			Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value,
				new Rectangle(Main.mouseX,
					Main.mouseY,
					Main.ThickMouse ? (int)(14 * 1.1f) : 14,
					Main.ThickMouse ? (int)(14 * 1.1f) : 14),
				Color.AntiqueWhite * 0.5f);
		}
	}
}
