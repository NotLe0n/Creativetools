using Creativetools.src.Tools.ClearInventory;
using Creativetools.src.Tools.CreativeFly;
using Creativetools.src.Tools.CustomNPC;
using Creativetools.src.Tools.DownedBossToggle;
using Creativetools.src.Tools.EventToggle;
using Creativetools.src.Tools.GameInfo;
using Creativetools.src.Tools.GameModeToggle;
using Creativetools.src.Tools.InvasionToggleUI;
using Creativetools.src.Tools.MagicCursor;
using Creativetools.src.Tools.Modify;
using Creativetools.src.Tools.PlaySound;
using Creativetools.src.Tools.TPTool;
using Creativetools.src.Tools.WeatherControl;
using Creativetools.src.UI.Elements;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.UI;

namespace Creativetools.src.UI
{
    class MainUI : UIState
    {
        private UIGrid buttonGrid;

        public override void OnInitialize()
        {
            DragableUIPanel MenuPanel = new("Creativetools Menu", 442f, 160f) { VAlign = 0.5f, HAlign = 0.1f };
            MenuPanel.OnCloseBtnClicked += () => { UISystem.UserInterface.SetState(null); SoundEngine.PlaySound(SoundID.MenuClose); };
            Append(MenuPanel);

            buttonGrid = new UIGrid(8);
            buttonGrid.Top.Set(40, 0f);
            buttonGrid.Left.Set(10, 0f);
            buttonGrid.Width.Set(0, 1);
            buttonGrid.Height.Set(0, 1);
            buttonGrid.ListPadding = 10f;
            MenuPanel.Append(buttonGrid);

            // 1. Zeile
            buttonGrid.Add(new MenuButton("bloodmoonToggle", "Event Toggle", (evt, element) => UISystem.UserInterface.SetState(new EventToggleUI())));
            buttonGrid.Add(new MenuButton("pirateInvasionToggle", "Toggle Pirate invasion", (evt, element) => UISystem.UserInterface.SetState(new InvasionToggleUI())));
            buttonGrid.Add(new MenuButton("hardmodeToggle", "Toggle hardmode", (evt, element) => Main.hardMode = !Main.hardMode));
            buttonGrid.Add(new MenuButton("expertModeToggle", "Toggle Game Mode", (evt, element) => UISystem.UserInterface.SetState(new GameModeToggleUI())));
            buttonGrid.Add(new MenuButton("weatherControl", "Weather Control", (evt, element) => UISystem.UserInterface.SetState(new WeatherControlUI())));
            buttonGrid.Add(new MenuButton("creativeFly", "Creative Fly", (evt, element) => MovePlayer.CreativeFly = !MovePlayer.CreativeFly));
            buttonGrid.Add(new MenuButton("magicCursor", "Magic Cursor", (evt, element) => MagicCursorNPC.MagicCursor = !MagicCursorNPC.MagicCursor));
            buttonGrid.Add(new MenuButton("MenuButton", "TP Tool", (evt, element) => UISystem.UserInterface.SetState(new TPToolUI())));

            // 2. Zeile
            buttonGrid.Add(new MenuButton("Info", "Game Info", (evt, element) => GameInfo.Visible = !GameInfo.Visible));
            buttonGrid.Add(new MenuButton("playSound", "Play Sound", (evt, element) => UISystem.UserInterface.SetState(new PlaySoundUI())));
            buttonGrid.Add(new MenuButton("modifyItem", "Modify Item/Player", (evt, element) => UISystem.UserInterface.SetState(new ItemModUI())));
            buttonGrid.Add(new MenuButton("custom", "Custom Item/NPC", (evt, element) => UISystem.UserInterface.SetState(new CustomNPCUI())));
            buttonGrid.Add(new MenuButton("DownedBossToggle", "DownedBoss Toggle", (evt, element) => UISystem.UserInterface.SetState(new DownedBossToggleUI())));
            buttonGrid.Add(new MenuButton("clearInventory", "Clear inventory", (evt, element) => Confirm_Panel.Visible = true));
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime); // Don't remove, or else dragging won't be smooth
            bool[] check = { false, false, Main.hardMode, false, false, MovePlayer.CreativeFly, MagicCursorNPC.MagicCursor, false, false, false, false, false, false, false };

            for (int i = 0; i < buttonGrid.Count; i++)
            {
                ((MenuButton)buttonGrid.items[i]).SetState(check[i]);
            }
        }
    }
}