using Creativetools.src.Tools.ClearInventory;
using Creativetools.src.Tools.CreativeFly;
using Creativetools.src.Tools.CustomNPC;
using Creativetools.src.Tools.DownedBossToggle;
using Creativetools.src.Tools.GameInfo;
using Creativetools.src.Tools.Modify;
using Creativetools.src.Tools.PlaySound;
using Creativetools.src.Tools.WeatherControl;
using Creativetools.src.UI.Elements;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;

namespace Creativetools.src.UI
{
    class MainUI : UIState
    {
        public static bool MagicCursor;
        private UIGrid buttonGrid;

        public override void OnInitialize()
        {
            DragableUIPanel MenuPanel = new DragableUIPanel("Creativetools Menu", 442f, 212f) { VAlign = 0.5f, HAlign = 0.1f };
            MenuPanel.OnCloseBtnClicked += () => { GetInstance<Creativetools>().UserInterface.SetState(null); Main.PlaySound(SoundID.MenuClose); };
            Append(MenuPanel);

            buttonGrid = new UIGrid(8);
            buttonGrid.Top.Set(40, 0f);
            buttonGrid.Left.Set(10, 0f);
            buttonGrid.Width.Set(0, 1);
            buttonGrid.Height.Set(0, 1);
            buttonGrid.ListPadding = 10f;
            MenuPanel.Append(buttonGrid);

            // 1. Zeile
            buttonGrid.Add(new MenuButton("bloodmoonToggle", "Toggle bloodmoon", (evt, element) => Main.bloodMoon = !Main.bloodMoon));
            buttonGrid.Add(new MenuButton("frostmoonToggle", "Toggle Frost moon", (evt, element) => { if (Main.snowMoon) Main.stopMoonEvent(); else Main.startSnowMoon(); }));
            buttonGrid.Add(new MenuButton("pumpkinmoonToggle", "Toggle Pumpkin moon", (evt, element) => { if (Main.pumpkinMoon) Main.stopMoonEvent(); else Main.startPumpkinMoon(); }));
            buttonGrid.Add(new MenuButton("solareclipseToggle", "Toggle Solar Eclipse", (evt, element) => Main.eclipse = !Main.eclipse));
            buttonGrid.Add(new MenuButton("pirateInvasionToggle", "Toggle Pirate invasion", (evt, element) => ToggleInvasion(InvasionID.PirateInvasion)));
            buttonGrid.Add(new MenuButton("goblinInvasionToggle", "Toggle Goblin invasion", (evt, element) => ToggleInvasion(InvasionID.GoblinArmy)));
            buttonGrid.Add(new MenuButton("alienInvasionToggle", "Toggle Martian Madness", (evt, element) => ToggleInvasion(InvasionID.MartianMadness)));
            buttonGrid.Add(new MenuButton("frostLegionToggle", "Toggle Frost Legion", (evt, element) => ToggleInvasion(InvasionID.SnowLegion)));

            // 2. Zeile
            buttonGrid.Add(new MenuButton("hardmodeToggle", "Toggle hardmode", (evt, element) => Main.hardMode = !Main.hardMode));
            buttonGrid.Add(new MenuButton("expertModeToggle", "Toggle Expert Mode", (evt, element) => Main.expertMode = !Main.expertMode));
            buttonGrid.Add(new MenuButton("halloweenToggle", "Toggle halloween", (evt, element) => Main.halloween = !Main.halloween));
            buttonGrid.Add(new MenuButton("xmasToggle", "Toggle Christmas", (evt, element) => Main.xMas = !Main.xMas));
            buttonGrid.Add(new MenuButton("partyToggle", "Toggle party", (evt, element) => BirthdayParty.GenuineParty = !BirthdayParty.PartyIsUp));
            buttonGrid.Add(new MenuButton("slimerainToggle", "Toggle Slime Rain", (evt, element) => { if (Main.slimeRain) Main.StopSlimeRain(announce: true); else Main.StartSlimeRain(announce: true); }));
            buttonGrid.Add(new MenuButton("spawnMeteor", "spawn Meteor", (evt, element) => WorldGen.dropMeteor()));
            buttonGrid.Add(new MenuButton("clearInventory", "Clear inventory", (evt, element) => Confirm_Panel.Visible = true));

            // 3. Zeile
            buttonGrid.Add(new MenuButton("modifyItem", "Modify Item/Player", (evt, element) => GetInstance<Creativetools>().UserInterface.SetState(new ItemModUI())));
            buttonGrid.Add(new MenuButton("custom", "Custom Item/NPC", (evt, element) => GetInstance<Creativetools>().UserInterface.SetState(new CustomNPCUI())));
            buttonGrid.Add(new MenuButton("creativeFly", "Creative Fly", (evt, element) => MovePlayer.CreativeFly = !MovePlayer.CreativeFly));
            buttonGrid.Add(new MenuButton("magicCursor", "Magic Cursor", (evt, element) => MagicCursor = !MagicCursor));
            buttonGrid.Add(new MenuButton("Info", "Game Info", (evt, element) => GameInfo.Visible = !GameInfo.Visible));
            buttonGrid.Add(new MenuButton("weatherControl", "Weather Control", (evt, element) => GetInstance<Creativetools>().UserInterface.SetState(new WeatherControlUI())));
            buttonGrid.Add(new MenuButton("playSound", "Play Sound", (evt, element) => GetInstance<Creativetools>().UserInterface.SetState(new PlaySoundUI())));
            buttonGrid.Add(new MenuButton("DownedBossToggle", "DownedBoss Toggle", (evt, element) => GetInstance<Creativetools>().UserInterface.SetState(new DownedBossToggleUI())));
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime); // Don't remove, or else dragging won't be smooth
            bool[] check = { Main.bloodMoon, Main.snowMoon, Main.pumpkinMoon, Main.eclipse,
                Main.invasionType == InvasionID.PirateInvasion,
                Main.invasionType == InvasionID.GoblinArmy,
                Main.invasionType == InvasionID.MartianMadness,
                Main.invasionType == InvasionID.SnowLegion,
                Main.hardMode, Main.expertMode, Main.halloween, Main.xMas, BirthdayParty.PartyIsUp, Main.slimeRain, WorldGen.spawnMeteor,
                false, false, false, MovePlayer.CreativeFly, MagicCursor, false, false, false, false };

            for (int i = 0; i < buttonGrid.Count; i++)
            {
                ((MenuButton)buttonGrid.items[i]).SetState(check[i]);
            }
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
                Main.NewText(Language.GetTextValue(text[type]), 143, 61, 209);
            }
        }
    }
}