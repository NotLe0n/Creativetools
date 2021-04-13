using Creativetools.src.UI;
using Creativetools.src.UI.Elements;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace Creativetools.src.Tools.GameModeToggle
{
    public enum GameModeID
    {
        Classic,
        Expert,
        Master,
        Journey
    }

    public class GameModeToggleUI : UIState
    {
        private UIIntRangedDataValue gameMode;
        private UIText gameModeText;
        public override void OnInitialize()
        {
            var panel = new DragableUIPanel("Game Mode Toggle", 400, 100);
            panel.VAlign = 0.4f;
            panel.HAlign = 0.3f;
            panel.OnCloseBtnClicked += () => UISystem.UserInterface.SetState(new MainUI());
            Append(panel);

            UIHelper.MakeSlider(new UIIntRangedDataValue("Game mode", Main.GameMode, 0, 3), out gameMode, panel, top: 50);

            gameModeText = new UIText("");
            gameModeText.Top.Set(75, 0);
            gameModeText.Left.Set(0, 0.5f);
            panel.Append(gameModeText);

        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Main.GameMode = gameMode.Data;

            if (gameModeText != null)
                gameModeText.SetText(((GameModeID)Main.GameMode).ToString());
        }
    }
}
