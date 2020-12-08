using Creativetools.src.UI.Elements;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;

namespace Creativetools.src.UI
{
    class MainUI : UIState
    {
        public static bool MagicCursor;
        public List<UIImage> selectList = new List<UIImage> { };
        private UIGrid buttonGrid;

        private void AppendButtons(UIHoverImageButton element, MouseEvent clickEvent, bool openmenu = false)
        {
            element.OnClick += clickEvent;
            buttonGrid.Add(element);

            // should button make click sound?
            if (openmenu)
                element.OnClick += (evt, elm) => Main.PlaySound(SoundID.MenuOpen);
        }
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

            #region buttons
            AppendButtons(new UIHoverImageButton("Creativetools/UI Assets/bloodmoonToggle", "Toggle bloodmoon"), (evt, element) => Main.bloodMoon = !Main.bloodMoon);
            AppendButtons(new UIHoverImageButton("Creativetools/UI Assets/frostmoonToggle", "Toggle Frost moon"), (evt, element) => { if (Main.snowMoon) Main.stopMoonEvent(); else Main.startSnowMoon(); });
            AppendButtons(new UIHoverImageButton("Creativetools/UI Assets/pumpkinmoonToggle", "Toggle Pumpkin moon"), (evt, element) => { if (Main.pumpkinMoon) Main.stopMoonEvent(); else Main.startPumpkinMoon(); });
            AppendButtons(new UIHoverImageButton("Creativetools/UI Assets/solareclipseToggle", "Toggle Solar Eclipse"), (evt, element) => Main.eclipse = !Main.eclipse);
            AppendButtons(new UIHoverImageButton("Creativetools/UI Assets/pirateInvasionToggle", "Toggle Pirate invasion"), (evt, element) =>
                {
                    if (Main.invasionType == InvasionID.None)
                    {
                        Main.StartInvasion(type: InvasionID.PirateInvasion);
                        Main.invasionType = InvasionID.PirateInvasion;
                    }
                    else
                    {
                        Main.invasionType = InvasionID.None;
                        Main.NewText(Language.GetTextValue("LegacyMisc.24"), 143, 61, 209);
                    }
                });
            AppendButtons(new UIHoverImageButton("Creativetools/UI Assets/goblinInvasionToggle", "Toggle Goblin invasion"), (evt, element) =>
                {
                    if (Main.invasionType == InvasionID.None)
                    {
                        Main.StartInvasion(type: InvasionID.GoblinArmy);
                        Main.invasionType = InvasionID.GoblinArmy;
                    }
                    else
                    {
                        Main.invasionType = InvasionID.None;
                        Main.NewText(Language.GetTextValue("LegacyMisc.0"), 143, 61, 209);
                    }
                });
            AppendButtons(new UIHoverImageButton("Creativetools/UI Assets/alienInvasionToggle", "Toggle Martian Madness"), (evt, element) =>
                {
                    if (Main.invasionType == InvasionID.None)
                    {
                        Main.StartInvasion(type: InvasionID.MartianMadness);
                        Main.invasionType = InvasionID.MartianMadness;
                    }
                    else
                    {
                        Main.invasionType = InvasionID.None;
                        Main.NewText(Language.GetTextValue("LegacyMisc.42"), 143, 61, 209);
                    }
                });
            AppendButtons(new UIHoverImageButton("Creativetools/UI Assets/frostLegionToggle", "Toggle Frost Legion"), (evt, element) =>
                 {
                     if (Main.invasionType == InvasionID.None)
                     {
                         Main.StartInvasion(type: InvasionID.SnowLegion);
                         Main.invasionType = InvasionID.SnowLegion;
                     }
                     else
                     {
                         Main.invasionType = InvasionID.None;
                         Main.NewText(Language.GetTextValue("LegacyMisc.4"), 143, 61, 209);
                     }
                 });
            AppendButtons(new UIHoverImageButton("Creativetools/UI Assets/hardmodeToggle", "Toggle hardmode"), (evt, element) => Main.hardMode = !Main.hardMode);
            AppendButtons(new UIHoverImageButton("Creativetools/UI Assets/expertModeToggle", "Toggle Expert Mode"), (evt, element) => Main.expertMode = !Main.expertMode);
            AppendButtons(new UIHoverImageButton("Creativetools/UI Assets/halloweenToggle", "Toggle halloween"), (evt, element) => Main.halloween = !Main.halloween);
            AppendButtons(new UIHoverImageButton("Creativetools/UI Assets/xmasToggle", "Toggle Christmas"), (evt, element) => Main.xMas = !Main.xMas);
            AppendButtons(new UIHoverImageButton("Creativetools/UI Assets/partyToggle", "Toggle party"), (evt, element) => BirthdayParty.GenuineParty = !BirthdayParty.PartyIsUp);
            AppendButtons(new UIHoverImageButton("Creativetools/UI Assets/slimerainToggle", "Toggle Slime Rain"), (evt, element) => { if (Main.slimeRain) Main.StopSlimeRain(announce: true); else Main.StartSlimeRain(announce: true); });
            AppendButtons(new UIHoverImageButton("Creativetools/UI Assets/spawnMeteor", "spawn Meteor"), (evt, element) => WorldGen.dropMeteor());
            AppendButtons(new UIHoverImageButton("Creativetools/UI Assets/clearInventory", "Clear inventory"), (evt, element) => Confirm_Panel.Visible = true, true);
            AppendButtons(new UIHoverImageButton("Creativetools/UI Assets/modifyItem", "Modify Item/Player"), (evt, element) => GetInstance<Creativetools>().UserInterface.SetState(new ItemModUI()), true);
            AppendButtons(new UIHoverImageButton("Creativetools/UI Assets/custom", "Custom Item/NPC"), (evt, element) => GetInstance<Creativetools>().UserInterface.SetState(new CustomNPCUI()), true);
            AppendButtons(new UIHoverImageButton("Creativetools/UI Assets/creativeFly", "Creative Fly"), (evt, element) => ModifyPlayer.CreativeFly = !ModifyPlayer.CreativeFly);
            AppendButtons(new UIHoverImageButton("Creativetools/UI Assets/magicCursor", "Magic Cursor"), (evt, element) => MagicCursor = !MagicCursor);
            AppendButtons(new UIHoverImageButton("Creativetools/UI Assets/Info", "Display Info"), (evt, element) => Info.Visible = !Info.Visible);
            AppendButtons(new UIHoverImageButton("Creativetools/UI Assets/weatherControl", "Weather Control"), (evt, element) => GetInstance<Creativetools>().UserInterface.SetState(new WeatherControl()), true);
            AppendButtons(new UIHoverImageButton("Creativetools/UI Assets/playSound", "Play Sound"), (evt, element) => GetInstance<Creativetools>().UserInterface.SetState(new PlaySoundUI()), true);
            #endregion

            for (int i = 0; i < buttonGrid.items.Count; i++)
            {
                UIImage Selected = new UIImage(GetTexture("Creativetools/UI Assets/selected"));
                Selected.MarginTop = -10000;
                buttonGrid.items[i].Append(Selected);
                selectList.Add(Selected);
            }
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            bool[] check = { Main.bloodMoon, Main.snowMoon, Main.pumpkinMoon, Main.eclipse,
                Main.invasionType == InvasionID.PirateInvasion,
                Main.invasionType == InvasionID.GoblinArmy,
                Main.invasionType == InvasionID.MartianMadness,
                Main.invasionType == InvasionID.SnowLegion,
                Main.hardMode, Main.expertMode, Main.halloween, Main.xMas, BirthdayParty.PartyIsUp, Main.slimeRain, WorldGen.spawnMeteor,
                false, false, false, ModifyPlayer.CreativeFly, MagicCursor, false, false, false };

            for (int i = 0; i < selectList.Count; i++)
            {
                selectList[i].MarginTop = check[i] ? 0 : -10000f;
            }
        }
    }
}