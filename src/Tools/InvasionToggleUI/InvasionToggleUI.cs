using Creativetools.src.UI;
using Creativetools.src.UI.Elements;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.UI;

namespace Creativetools.src.Tools.InvasionToggleUI;

internal class InvasionToggleUI : UIState
{
	private UIGrid buttonGrid;

	public override void OnInitialize()
	{
		var menuPanel = new DragableUIPanel("Invasion Toggle") { VAlign = 0.5f, HAlign = 0.1f };
		menuPanel.Width.Set(250f, 0);
		menuPanel.Height.Set(100f, 0);
		menuPanel.OnCloseBtnClicked += () => { UISystem.UserInterface.SetState(new MainUI()); SoundEngine.PlaySound(SoundID.MenuClose); };
		Append(menuPanel);

		buttonGrid = new UIGrid(4);
		buttonGrid.Top.Set(40, 0f);
		buttonGrid.Left.Set(10, 0f);
		buttonGrid.Width.Set(0, 1);
		buttonGrid.Height.Set(0, 1);
		buttonGrid.ListPadding = 10f;
		menuPanel.Append(buttonGrid);

		buttonGrid.Add(new MenuButton("pirateInvasionToggle", "Toggle Pirate invasion", (evt, element) => ToggleInvasion(InvasionID.PirateInvasion)));
		buttonGrid.Add(new MenuButton("goblinInvasionToggle", "Toggle Goblin invasion", (evt, element) => ToggleInvasion(InvasionID.GoblinArmy)));
		buttonGrid.Add(new MenuButton("alienInvasionToggle", "Toggle Martian Madness", (evt, element) => ToggleInvasion(InvasionID.MartianMadness)));
		buttonGrid.Add(new MenuButton("frostLegionToggle", "Toggle Frost Legion", (evt, element) => ToggleInvasion(InvasionID.SnowLegion)));
		base.OnInitialize();
	}

	private void ToggleInvasion(short type)
	{
		string[] text = { "", "LegacyMisc.0", "LegacyMisc.4", "LegacyMisc.24", "LegacyMisc.42" };

		if (Main.invasionType == InvasionID.None)
		{
			Main.StartInvasion(type: type);
			Main.invasionType = type;
		}
		else
		{
			Main.invasionType = InvasionID.None;
			Main.NewText(Terraria.Localization.Language.GetTextValue(text[type]), 143, 61, 209);
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

		for (int i = 0; i < buttonGrid.Count; i++)
		{
			((MenuButton)buttonGrid.items[i]).SetState(check[i]);
		}
	}
}
