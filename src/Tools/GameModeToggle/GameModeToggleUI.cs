using Creativetools.src.UI;
using Creativetools.src.UI.Elements;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace Creativetools.src.Tools.GameModeToggle;

public enum GameModeID
{
	Classic,
	Expert,
	Master,
	Journey
}

public class GameModeToggleUI : UIState
{
	private UIIntRangedDataValue worldGameMode;
	private UIText worldGameModeText;

	private UIIntRangedDataValue playerGameMode;
	private UIText playerGameModeText;

	public override void OnInitialize()
	{
		var panel = new DragableUIPanel("Game Mode Toggle");
		panel.Width.Set(400, 0);
		panel.Height.Set(150, 0);
		panel.VAlign = 0.4f;
		panel.HAlign = 0.3f;
		panel.OnCloseBtnClicked += () => UISystem.UserInterface.SetState(new MainUI());
		Append(panel);

		UIHelper.MakeSlider(new UIIntRangedDataValue("World Game mode", Main.GameMode, 0, 3), out worldGameMode, panel, top: 50);

		worldGameModeText = new UIText("");
		worldGameModeText.Top.Set(75, 0);
		worldGameModeText.Left.Set(0, 0.5f);
		panel.Append(worldGameModeText);


		UIHelper.MakeSlider(new UIIntRangedDataValue("Player Game mode", Main.LocalPlayer.difficulty, 0, 3), out playerGameMode, panel, top: 100);

		playerGameModeText = new UIText("");
		playerGameModeText.Top.Set(125, 0);
		playerGameModeText.Left.Set(0, 0.5f);
		panel.Append(playerGameModeText);

	}

	public override void Update(GameTime gameTime)
	{
		base.Update(gameTime);

		Main.GameMode = worldGameMode.Data;
		Main.LocalPlayer.difficulty = (byte)playerGameMode.Data;

		worldGameModeText?.SetText(((GameModeID)Main.GameMode).ToString());
		playerGameModeText?.SetText(((GameModeID)Main.LocalPlayer.difficulty).ToString());
	}
}
