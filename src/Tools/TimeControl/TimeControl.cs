using Microsoft.Xna.Framework.Input;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Creativetools.Tools.TimeControl;

class TimeControl : ModSystem
{
	private ModKeybind freezeKeybind, gameSpeedUpKeybind, gameSpeedDownKeybind, frameStepKeybind;
	private KeyboardState lastKeyboard;

	public override void Load()
	{
		base.Load();

		frameStepKeybind = KeybindLoader.RegisterKeybind(Mod, "FrameStep", Keys.None);
		freezeKeybind = KeybindLoader.RegisterKeybind(Mod, "Freeze", Keys.None);
		gameSpeedUpKeybind = KeybindLoader.RegisterKeybind(Mod, "GameSpeedUp", Keys.None);
		gameSpeedDownKeybind = KeybindLoader.RegisterKeybind(Mod, "GameSpeedDown", Keys.None);
		On_Main.DoUpdate += Main_DoUpdate;
	}

	public override void Unload()
	{
		base.Unload();

		frameStepKeybind = freezeKeybind = gameSpeedUpKeybind = gameSpeedDownKeybind = null;
	}

	private ulong timer = 0;
	private uint gameSpeed = 1;
	private bool freeze;
	private bool takeStep;

	private void Main_DoUpdate(On_Main.orig_DoUpdate orig, Main self, ref Microsoft.Xna.Framework.GameTime gameTime)
	{
		if (Main.gameMenu || Main.ingameOptionsWindow || Main.inFancyUI || Main.blockInput || Main.drawingPlayerChat || Terraria.GameInput.PlayerInput.WritingText || Main.netMode == NetmodeID.Server) {
			orig(self, ref gameTime);
			return;
		}
		timer++;

		if (takeStep) freeze = true;

		Keys frameStepKey = Keys.None;
		Keys freezeKey = Keys.None;
		Keys gameSpeedUpKey = Keys.None;
		Keys gameSpeedDownKey = Keys.None;

		bool frameStepValid = frameStepKeybind.GetAssignedKeys().Count > 0 && Enum.TryParse(frameStepKeybind.GetAssignedKeys()[0], out frameStepKey);
		bool freezeValid = freezeKeybind.GetAssignedKeys().Count > 0 && Enum.TryParse(freezeKeybind.GetAssignedKeys()[0], out freezeKey);
		bool gameSpeedUpValid = gameSpeedUpKeybind.GetAssignedKeys().Count > 0 && Enum.TryParse(gameSpeedUpKeybind.GetAssignedKeys()[0], out gameSpeedUpKey);
		bool gameSpeedDownValid = gameSpeedDownKeybind.GetAssignedKeys().Count > 0 && Enum.TryParse(gameSpeedDownKeybind.GetAssignedKeys()[0], out gameSpeedDownKey);

		if (frameStepValid && !lastKeyboard.IsKeyDown(frameStepKey) && Keyboard.GetState().IsKeyDown(frameStepKey)) {
			freeze = false;
			takeStep = true;
			Main.NewText("[c/666666:Time Control:] Stepped");
		}
		if (freezeValid && !lastKeyboard.IsKeyDown(freezeKey) && Keyboard.GetState().IsKeyDown(freezeKey)) {
			freeze = !freeze;
			takeStep = false;
			Main.NewText("[c/666666:Time Control:] Frozen");
		}
		if (gameSpeedUpValid && !lastKeyboard.IsKeyDown(gameSpeedUpKey) && Keyboard.GetState().IsKeyDown(gameSpeedUpKey)) {
			gameSpeed = (gameSpeed % 10) + 1;
			Main.NewText("[c/666666:Time Control:] " + (100.0 / gameSpeed) + "%");
		}
		if (gameSpeedDownValid && !lastKeyboard.IsKeyDown(gameSpeedDownKey) && Keyboard.GetState().IsKeyDown(gameSpeedDownKey)) {
			gameSpeed--;
			if (gameSpeed < 1)
				gameSpeed = 10;
			Main.NewText("[c/666666:Time Control:] " + (100.0 / gameSpeed) + "%");
		}

		lastKeyboard = Keyboard.GetState();

		if (freeze) return;

		if (timer % gameSpeed == 0) {
			orig(self, ref gameTime);
		}
	}
}
