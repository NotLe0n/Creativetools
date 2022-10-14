using Creativetools.Tools.AssemblyViewer;
using Creativetools.Tools.ClearInventory;
using Creativetools.Tools.CreativeFly;
using Creativetools.Tools.DownedBossToggle;
using Creativetools.Tools.EventToggle;
using Creativetools.Tools.GameInfo;
using Creativetools.Tools.GameModeToggle;
using Creativetools.Tools.InvasionToggleUI;
using Creativetools.Tools.MagicCursor;
using Creativetools.Tools.Modify;
using Creativetools.Tools.Playsound;
using Creativetools.Tools.TPTool;
using Creativetools.Tools.WeatherControl;
using Creativetools.UI.Elements;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.UI;

namespace Creativetools.UI;

internal class MainUI : UIState
{
	private UIGrid buttonGrid;

	public override void OnInitialize()
	{
		var menuPanel = new DragableUIPanel("Creativetools Menu") { VAlign = 0.5f, HAlign = 0.1f };
		menuPanel.Width.Set(442f, 0);
		menuPanel.Height.Set(160f, 0);
		menuPanel.OnCloseBtnClicked += () => { UISystem.UserInterface.SetState(null); SoundEngine.PlaySound(SoundID.MenuClose); };
		Append(menuPanel);

		buttonGrid = new UIGrid(8);
		buttonGrid.Top.Set(40, 0f);
		buttonGrid.Left.Set(10, 0f);
		buttonGrid.Width.Set(0, 1);
		buttonGrid.Height.Set(0, 1);
		buttonGrid.ListPadding = 10f;
		menuPanel.Append(buttonGrid);

		// 1. Zeile
		AddButton("bloodmoonToggle", "Event Toggle", () => UISystem.UserInterface.SetState(new EventToggleUI()));
		AddButton("pirateInvasionToggle", "Invasion Toggle", () => UISystem.UserInterface.SetState(new InvasionToggleUI()));
		AddButton("hardmodeToggle", "Toggle hardmode", ToggleHardmode);
		AddButton("expertModeToggle", "Toggle Game Mode", () => UISystem.UserInterface.SetState(new GameModeToggleUI()));
		AddButton("weatherControl", "Weather Control", () => UISystem.UserInterface.SetState(new WeatherControlUI()));
		AddButton("creativeFly", "Creative Fly", () => MovePlayer.creativeFly = !MovePlayer.creativeFly);
		AddButton("magicCursor", "Magic Cursor", () => MagicCursorNPC.MagicCursor = !MagicCursorNPC.MagicCursor);
		AddButton("tptool", "TP Tool", () => UISystem.UserInterface.SetState(new TPToolUI()));

		// 2. Zeile
		AddButton("Info", "Game Info", () => GameInfo.Visible = !GameInfo.Visible);
		AddButton("Info", "AssemblyViewer", () => UISystem.UserInterface.SetState(new AssemblyViewer("Terraria")));
		AddButton("playSound", "Play Sound", () => UISystem.UserInterface.SetState(new PlaySoundUI()));
		AddButton("modifyItem", "Modify Item/Player", () => UISystem.UserInterface.SetState(new ItemModUI()));
		AddButton("DownedBossToggle", "DownedBoss Toggle", () => UISystem.UserInterface.SetState(new DownedBossToggleUI()));
		AddButton("clearInventory", "Clear inventory", () => ConfirmPanel.Visible = true);
		AddButton("killplayer", "Kill Player", KillMe);
	}

	public override void Update(GameTime gameTime)
	{
		base.Update(gameTime); // Don't remove, or else dragging won't be smooth
		bool[] check = { false, false, Main.hardMode, false, false, MovePlayer.creativeFly, MagicCursorNPC.MagicCursor, false, GameInfo.Visible, false, false, false, false, false, false, false };

		for (int i = 0; i < buttonGrid.Count; i++) {
			((MenuButton)buttonGrid.items[i]).SetState(check[i]);
		}
	}

	private void AddButton(string iconName, string name, System.Action action)
	{
		buttonGrid.Add(new MenuButton(iconName, name, (_, _) => action()));
	}

	private void ToggleHardmode()
	{
		if (Main.netMode == NetmodeID.SinglePlayer) {
			Main.hardMode = !Main.hardMode;
		}
		else {
			MultiplayerSystem.SendHardmodePacket(!Main.hardMode);
		}
	}

	private static void KillMe()
	{
		if (Main.netMode == NetmodeID.SinglePlayer) {
			KillPlayer(Main.LocalPlayer);
		}
		else {
			MultiplayerSystem.SendKillPlayerPacket(Main.myPlayer);
		}
	}

	public static void KillPlayer(Player p)
	{
		bool old = p.creativeGodMode;
		p.creativeGodMode = false;
		p.KillMe(PlayerDeathReason.LegacyEmpty(), 100, 0);
		p.creativeGodMode = old;
	}
}
