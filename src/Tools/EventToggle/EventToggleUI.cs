using Creativetools.UI.Elements;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.UI;

namespace Creativetools.Tools.EventToggle;

internal class EventToggleUI : UIState
{
	private readonly UIGrid buttonGrid;
	private static bool starfall;

	public EventToggleUI()
	{
		var menuPanel = new DragableUIPanel("Event Toggle") {
			VAlign = 0.5f,
			HAlign = 0.1f,
			Width = new(442, 0),
			Height = new(160, 0)
		};
		menuPanel.OnCloseBtnClicked += UISystem.BackToMainMenu;
		Append(menuPanel);

		buttonGrid = new UIGrid(8) {
			Top = new(40, 0f),
			Left = new(10, 0f),
			Width = new(0, 1),
			Height = new(0, 1),
			ListPadding = 10f
		};
		menuPanel.Append(buttonGrid);

		AddButton("bloodmoonToggle", "Toggle bloodmoon", MultiplayerSystem.BloodMoonEvent);
		AddButton("frostmoonToggle", "Toggle Frost moon", MultiplayerSystem.FrostMoonEvent);
		AddButton("pumpkinmoonToggle", "Toggle Pumpkin moon", MultiplayerSystem.PumpkinMoonEvent);
		AddButton("solareclipseToggle", "Toggle Solar Eclipse", MultiplayerSystem.SolarEclipseEvent);
		AddButton("halloweenToggle", "Toggle halloween", MultiplayerSystem.HalloweenEvent);
		AddButton("xmasToggle", "Toggle Christmas", MultiplayerSystem.ChristmasEvent);
		AddButton("partyToggle", "Toggle party", MultiplayerSystem.PartyEvent);
		AddButton("slimerainToggle", "Toggle Slime Rain", MultiplayerSystem.SlimeRainEvent);

		AddButton("spawnMeteor", "spawn Meteor", MultiplayerSystem.MeteorEvent);
		AddButton("lanternnight", "Lantern Night", MultiplayerSystem.LanternNightEvent);
		AddButton("meteorshower", "Meteor shower", MultiplayerSystem.MeteorShowerEvent);
	}

	public override void Update(GameTime gameTime)
	{
		base.Update(gameTime); // Don't remove, or else dragging won't be smooth
		bool[] check = { Main.bloodMoon, Main.snowMoon, Main.pumpkinMoon, Main.eclipse, Main.halloween, Main.xMas, BirthdayParty.PartyIsUp, Main.slimeRain, WorldGen.spawnMeteor, LanternNight.LanternsUp, starfall };

		for (int i = 0; i < buttonGrid.Count; i++) {
			((MenuButton)buttonGrid.items[i]).SetState(check[i]);
		}

		if (starfall) {
			Star.StarFall(Main.LocalPlayer.position.X);
		}
	}

	private void AddButton(string iconName, string name, byte eventID)
	{
		buttonGrid.Add(new MenuButton(iconName, name, (_, _) =>
		{
			if (Main.netMode == NetmodeID.SinglePlayer) {
				switch (eventID) {
					case MultiplayerSystem.BloodMoonEvent: ToggleBloodMoon(); break;
					case MultiplayerSystem.FrostMoonEvent: ToggleFrostMoon(); break;
					case MultiplayerSystem.PumpkinMoonEvent: TogglePumpkinMoon(); break;
					case MultiplayerSystem.SolarEclipseEvent: ToggleSolarEclipse(); break;
					case MultiplayerSystem.HalloweenEvent: ToggleHalloween(); break;
					case MultiplayerSystem.ChristmasEvent: ToggleChristmas(); break;
					case MultiplayerSystem.PartyEvent: ToggleParty(); break;
					case MultiplayerSystem.SlimeRainEvent: ToggleSlimeRain(); break;
					case MultiplayerSystem.MeteorEvent: SpawnMeteor(); break;
					case MultiplayerSystem.LanternNightEvent: ToggleLanternNight(); break;
					case MultiplayerSystem.MeteorShowerEvent: ToggleStarFall(); break;
				}
			}
			else {
				MultiplayerSystem.SendEventPacket(eventID);
			}
		}));
	}

	/*
		Toggle Methods
	*/

	public static void ToggleBloodMoon()
	{
		Main.bloodMoon = !Main.bloodMoon;
	}

	public static void ToggleFrostMoon()
	{
		if (Main.snowMoon) {
			Main.stopMoonEvent();
		}
		else {
			Main.startSnowMoon();
		}
	}

	public static void TogglePumpkinMoon()
	{
		if (Main.pumpkinMoon) {
			Main.stopMoonEvent();
		}
		else {
			Main.startPumpkinMoon();
		}
	}

	public static void ToggleSolarEclipse()
	{
		Main.eclipse = !Main.eclipse;
	}

	public static void ToggleHalloween()
	{
		Main.halloween = !Main.halloween;
	}

	public static void ToggleChristmas()
	{
		Main.xMas = !Main.xMas;
	}

	public static void ToggleParty()
	{
		BirthdayParty.GenuineParty = !BirthdayParty.PartyIsUp;
	}

	public static void ToggleSlimeRain()
	{
		if (Main.slimeRain) {
			Main.StopSlimeRain(announce: true);
		}
		else {
			Main.StartSlimeRain(announce: true);
		}
	}

	public static void ToggleLanternNight()
	{
		LanternNight.GenuineLanterns = !LanternNight.GenuineLanterns;
	}

	public static void SpawnMeteor()
	{
		WorldGen.dropMeteor();
	}

	public static void ToggleStarFall()
	{
		starfall = !starfall;
	}
}
