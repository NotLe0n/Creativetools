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
using Terraria.DataStructures;

namespace Creativetools.src.UI
{
    internal class MainUI : UIState
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
            AddButton("bloodmoonToggle",        "Event Toggle",         () => UISystem.UserInterface.SetState(new EventToggleUI()));
            AddButton("pirateInvasionToggle",   "Invasion Toggle",      () => UISystem.UserInterface.SetState(new InvasionToggleUI()));
            AddButton("hardmodeToggle",         "Toggle hardmode",      () => Main.hardMode = !Main.hardMode);
            AddButton("expertModeToggle",        "Toggle Game Mode",    () => UISystem.UserInterface.SetState(new GameModeToggleUI()));
            AddButton("weatherControl",         "Weather Control",      () => UISystem.UserInterface.SetState(new WeatherControlUI()));
            AddButton("creativeFly",            "Creative Fly",         () => MovePlayer.CreativeFly = !MovePlayer.CreativeFly);
            AddButton("magicCursor",            "Magic Cursor",         () => MagicCursorNPC.MagicCursor = !MagicCursorNPC.MagicCursor);
            AddButton("tptool",                 "TP Tool",              () => UISystem.UserInterface.SetState(new TPToolUI()));

            // 2. Zeile
            AddButton("Info",                   "Game Info",            () => GameInfo.Visible = !GameInfo.Visible);
            AddButton("Info",                   "Game Info 2",          () => UISystem.UserInterface.SetState(new Tools.GameInfo2.GameInfo2("Terraria")));
            AddButton("playSound",              "Play Sound",           () => UISystem.UserInterface.SetState(new PlaySoundUI()));
            AddButton("modifyItem",             "Modify Item/Player",   () => UISystem.UserInterface.SetState(new ItemModUI()));
            AddButton("custom",                 "Custom Item/NPC",      () => UISystem.UserInterface.SetState(new CustomNPCUI()));
            AddButton("DownedBossToggle",       "DownedBoss Toggle",    () => UISystem.UserInterface.SetState(new DownedBossToggleUI()));
            AddButton("clearInventory",         "Clear inventory",      () => Confirm_Panel.Visible = true);
            AddButton("killplayer",             "Kill Player",          () => Main.LocalPlayer.KillMe(PlayerDeathReason.LegacyEmpty(), 100, 0));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime); // Don't remove, or else dragging won't be smooth
            bool[] check = { false, false, Main.hardMode, false, false, MovePlayer.CreativeFly, MagicCursorNPC.MagicCursor, false, GameInfo.Visible, false, false, false, false, false, false, false };

            for (int i = 0; i < buttonGrid.Count; i++)
            {
                ((MenuButton)buttonGrid.items[i]).SetState(check[i]);
            }
        }

        private void AddButton(string iconName, string name, System.Action action)
        {
            buttonGrid.Add(new MenuButton(iconName, name, (evt, elm) => action()));
        }
    }
}