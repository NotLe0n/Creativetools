using Terraria;
using Terraria.ModLoader;

namespace Creativetools.Tools.Modify;

internal class ModifyPlayer : ModPlayer
{
	public static float playerSize = 1f;
	public static float? luck;

	public override void Load()
	{
		On_Player.RecalculateLuck += Player_RecalculateLuck;
		Terraria.Graphics.Renderers.On_LegacyPlayerRenderer.DrawPlayerInternal += LegacyPlayerRenderer_DrawPlayerInternal;
	}

	private void LegacyPlayerRenderer_DrawPlayerInternal(Terraria.Graphics.Renderers.On_LegacyPlayerRenderer.orig_DrawPlayerInternal orig, Terraria.Graphics.Renderers.LegacyPlayerRenderer self, Terraria.Graphics.Camera camera, Terraria.Player drawPlayer, Microsoft.Xna.Framework.Vector2 position, float rotation, Microsoft.Xna.Framework.Vector2 rotationOrigin, float shadow, float alpha, float scale, bool headOnly)
	{
		orig(self, camera, drawPlayer, position, rotation, rotationOrigin, shadow, alpha, scale * playerSize, headOnly);
	}

	private void Player_RecalculateLuck(Terraria.On_Player.orig_RecalculateLuck orig, Player self)
	{
		orig(self);

		if (luck.HasValue)
			self.luck = luck.Value;
	}

	public override void PreUpdateMovement()
	{
		int inflatedPlayerWidth = (int)(Player.defaultWidth * playerSize);
		int inflatedPlayerHeight = (int)(Player.defaultHeight * playerSize);

		// Change player hitbox size
		Player.position = Player.Bottom; // to keep the player from flying away
		Player.width = inflatedPlayerWidth;
		Player.height = inflatedPlayerHeight;
		Player.Bottom = Player.position; // to keep the player from flying away
	}
}
