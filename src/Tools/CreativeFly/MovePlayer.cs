using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;

namespace Creativetools.src.Tools.CreativeFly;

internal class MovePlayer : ModPlayer
{
	public static bool creativeFly;

	public override void ProcessTriggers(TriggersSet triggersSet)
	{
		if (creativeFly) {
			Player.gravity = 0f; //player doesn't fall
			Player.controlJump = false; //player can't jump
			Player.noFallDmg = true; //player doesn't take fall damage
			Player.moveSpeed = 0f; //player can't move
			Player.noKnockback = true; //player doesn't take knockback
			Player.velocity.Y = -0.00000000001f; //fix confusing bug

			float modifier = 1f;
			if (Main.keyState.IsKeyDown(Keys.LeftShift) | Main.keyState.IsKeyDown(Keys.RightShift)) {
				modifier = 2f; //go faster
			}
			if (Main.keyState.IsKeyDown(Keys.LeftControl) | Main.keyState.IsKeyDown(Keys.RightControl)) {
				modifier = 0.5f; //go slower
			}
			if (Main.keyState.IsKeyDown(Keys.W)) {
				Player.position.Y -= 8 * modifier; //move up
			}
			if (Main.keyState.IsKeyDown(Keys.S)) {
				Player.position.Y += 8 * modifier; //move down 
			}
			if (Main.keyState.IsKeyDown(Keys.A)) {
				Player.position.X -= 8 * modifier; //move left
			}
			if (Main.keyState.IsKeyDown(Keys.D)) {
				Player.position.X += 8 * modifier; //move right
			}
		}
	}
}
