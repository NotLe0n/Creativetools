using Creativetools.UI.Elements;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;

namespace Creativetools.Tools.InvasionToggleUI;

internal class InvasionToggleUI : UIState
{
	private UIGrid buttonGrid;

	public override void OnInitialize()
	{
		var menuPanel = new DragableUIPanel("Invasion Toggle") { VAlign = 0.5f, HAlign = 0.1f };
		menuPanel.Width.Set(250f, 0);
		menuPanel.Height.Set(100f, 0);
		menuPanel.OnCloseBtnClicked += UISystem.BackToMainMenu;
		Append(menuPanel);

		buttonGrid = new UIGrid(4);
		buttonGrid.Top.Set(40, 0f);
		buttonGrid.Left.Set(10, 0f);
		buttonGrid.Width.Set(0, 1);
		buttonGrid.Height.Set(0, 1);
		buttonGrid.ListPadding = 10f;
		menuPanel.Append(buttonGrid);

		buttonGrid.Add(new MenuButton("pirateInvasionToggle", "Toggle Pirate invasion", (_, _) => ToggleInvasion2(InvasionID.PirateInvasion)));
		buttonGrid.Add(new MenuButton("goblinInvasionToggle", "Toggle Goblin invasion", (_, _) => ToggleInvasion2(InvasionID.GoblinArmy)));
		buttonGrid.Add(new MenuButton("alienInvasionToggle", "Toggle Martian Madness", (_, _) => ToggleInvasion2(InvasionID.MartianMadness)));
		buttonGrid.Add(new MenuButton("frostLegionToggle", "Toggle Frost Legion", (_, _) => ToggleInvasion2(InvasionID.SnowLegion)));
		base.OnInitialize();
	}

	private static void ToggleInvasion2(short type)
	{
		if (Main.netMode == NetmodeID.SinglePlayer) {
			ToggleInvasion(type);
		}
		else {
			MultiplayerSystem.SendInvasionPacket(type);
		}
	}

	public static void ToggleInvasion(short type)
	{
		string[] text = { "", "LegacyMisc.0", "LegacyMisc.4", "LegacyMisc.24", "LegacyMisc.42" };

		if (Main.invasionType == InvasionID.None) {
			Main.StartInvasion(type: type);
			Main.invasionType = type;
		}
		else {
			Main.invasionType = InvasionID.None;
			var nt = NetworkText.FromLiteral(Language.GetTextValue(text[type]));
			ChatHelper.BroadcastChatMessage(nt, new(143, 61, 209));
		}
	}

	public override void Update(GameTime gameTime)
	{
		base.Update(gameTime); // Don't remove, or else dragging won't be smooth
		bool[] check = {
				Main.invasionType == InvasionID.PirateInvasion,
				Main.invasionType == InvasionID.GoblinArmy,
				Main.invasionType == InvasionID.MartianMadness,
				Main.invasionType == InvasionID.SnowLegion };

		for (int i = 0; i < buttonGrid.Count; i++) {
			((MenuButton)buttonGrid.items[i]).SetState(check[i]);
		}
	}
}
