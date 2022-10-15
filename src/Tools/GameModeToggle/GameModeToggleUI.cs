using Creativetools.UI;
using Creativetools.UI.Elements;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace Creativetools.Tools.GameModeToggle;

public class GameModeToggleUI : UIState
{
	private enum WorldGameModeID
	{
		Classic,
		Expert,
		Master,
		Journey
	}

	private enum PlayerGameModeID
	{
		Classic,
		Mediumcore,
		Hardcore,
		Journey
	}
	
	private readonly UIIntRangedDataValue worldGameMode;
	private readonly UIText worldGameModeText;

	private readonly UIIntRangedDataValue playerGameMode;
	private readonly UIText playerGameModeText;

	public GameModeToggleUI()
	{
		var panel = new DragableUIPanel("Game Mode Toggle");
		panel.Width.Set(400, 0);
		panel.Height.Set(150, 0);
		panel.VAlign = 0.4f;
		panel.HAlign = 0.3f;
		panel.OnCloseBtnClicked += UISystem.BackToMainMenu;
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

		if (Main.GameMode != worldGameMode.Data) {
			if (Main.netMode == Terraria.ID.NetmodeID.SinglePlayer) {
				Main.GameMode = worldGameMode.Data;
			}
			else {
				MultiplayerSystem.SendGamemodePacket((byte)worldGameMode.Data);
			}

		}
		Main.LocalPlayer.difficulty = (byte)playerGameMode.Data;

		worldGameModeText?.SetText(((WorldGameModeID)Main.GameMode).ToString());
		playerGameModeText?.SetText(((PlayerGameModeID)Main.LocalPlayer.difficulty).ToString());
	}


}
