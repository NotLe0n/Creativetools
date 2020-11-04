using Creativetools.UI.Elements;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;

namespace Creativetools.UI
{
    class MainUI : UIState
    {
        public static bool MagicCursor;
        public DragableUIPanel MenuPanel;
        public int row = 10;
        public int collumn = 50;
        public List<UIHoverImageButton> ButtonList = new List<UIHoverImageButton> { };
        public List<UIImage> selectList = new List<UIImage> { };

        private void AppendButtons(UIHoverImageButton element, Action<UIElement> action, bool openmenu = false)
        {
            action(element);
            MenuPanel.Append(element);
            element.MarginTop = collumn;
            element.MarginLeft = row;

            ButtonList.Add(element);

            // should button make click sound?
            if (openmenu)
                element.OnClick += (evt, elm) => Main.PlaySound(SoundID.MenuOpen);
        }
        public override void OnInitialize()
        {
            MenuPanel = new DragableUIPanel(442f, 212f) { VAlign = 0.5f, HAlign = 0.1f };
            MenuPanel.SetPadding(0);
            Append(MenuPanel);
            MenuPanel.Append(new UIText("Creativetools Menu") { HAlign = 0.5f, MarginTop = 15 });

            UITextPanel<string> backButton = new UITextPanel<string>(Language.GetTextValue(" X "));
            backButton.SetPadding(4);
            backButton.Width.Set(10, 0f);
            backButton.MarginLeft = 400;
            backButton.MarginTop = 10;
            backButton.OnClick += (evt, element) =>
            {
                GetInstance<Creativetools>().UserInterface.SetState(null);
                Main.PlaySound(SoundID.MenuClose);
            };
            MenuPanel.Append(backButton);

            #region buttons
            AppendButtons(new UIHoverImageButton("Creativetools/UI/UI Assets/bloodmoonToggle", "Toggle bloodmoon"), button => button.OnClick += (evt, element) => Main.bloodMoon = !Main.bloodMoon);
            row += 54;
            AppendButtons(new UIHoverImageButton("Creativetools/UI/UI Assets/frostmoonToggle", "Toggle Frost moon"), button => button.OnClick += (evt, element) => { if (Main.snowMoon) Main.stopMoonEvent(); else Main.startSnowMoon(); });
            row += 54;
            AppendButtons(new UIHoverImageButton("Creativetools/UI/UI Assets/pumpkinmoonToggle", "Toggle Pumpkin moon"), button => button.OnClick += (evt, element) => { if (Main.pumpkinMoon) Main.stopMoonEvent(); else Main.startPumpkinMoon(); });
            row += 54;
            AppendButtons(new UIHoverImageButton("Creativetools/UI/UI Assets/solareclipseToggle", "Toggle Solar Eclipse"), button => button.OnClick += (evt, element) => Main.eclipse = !Main.eclipse);
            row += 54;
            AppendButtons(new UIHoverImageButton("Creativetools/UI/UI Assets/pirateInvasionToggle", "Toggle Pirate invasion"), button => button.OnClick += (evt, element) =>
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
            row += 54;
            AppendButtons(new UIHoverImageButton("Creativetools/UI/UI Assets/goblinInvasionToggle", "Toggle Goblin invasion"), button => button.OnClick += (evt, element) =>
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
            row += 54;
            AppendButtons(new UIHoverImageButton("Creativetools/UI/UI Assets/alienInvasionToggle", "Toggle Martian Madness"), button => button.OnClick += (evt, element) =>
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
            row += 54;
            AppendButtons(new UIHoverImageButton("Creativetools/UI/UI Assets/frostLegionToggle", "Toggle Frost Legion"), button => button.OnClick += (evt, element) =>
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
            row = 10;
            collumn += 54;
            AppendButtons(new UIHoverImageButton("Creativetools/UI/UI Assets/hardmodeToggle", "Toggle hardmode"), button => button.OnClick += (evt, element) => Main.hardMode = !Main.hardMode);
            row += 54;
            AppendButtons(new UIHoverImageButton("Creativetools/UI/UI Assets/expertModeToggle", "Toggle Expert Mode"), button => button.OnClick += (evt, element) => Main.expertMode = !Main.expertMode);
            row += 54;
            AppendButtons(new UIHoverImageButton("Creativetools/UI/UI Assets/halloweenToggle", "Toggle halloween"), button => button.OnClick += (evt, element) => Main.halloween = !Main.halloween);
            row += 54;
            AppendButtons(new UIHoverImageButton("Creativetools/UI/UI Assets/xmasToggle", "Toggle Christmas"), button => button.OnClick += (evt, element) => Main.xMas = !Main.xMas);
            row += 54;
            AppendButtons(new UIHoverImageButton("Creativetools/UI/UI Assets/partyToggle", "Toggle party"), button => button.OnClick += (evt, element) => BirthdayParty.GenuineParty = !BirthdayParty.PartyIsUp);
            row += 54;
            AppendButtons(new UIHoverImageButton("Creativetools/UI/UI Assets/slimerainToggle", "Toggle Slime Rain"), button => { button.OnClick += (evt, element) => { if (Main.slimeRain) Main.StopSlimeRain(announce: true); else Main.StartSlimeRain(announce: true); }; });
            row += 54;
            AppendButtons(new UIHoverImageButton("Creativetools/UI/UI Assets/spawnMeteor", "spawn Meteor"), button => button.OnClick += (evt, element) => WorldGen.dropMeteor());
            row += 54;
            AppendButtons(new UIHoverImageButton("Creativetools/UI/UI Assets/clearInventory", "Clear inventory"), button => button.OnClick += (evt, element) => Confirm_Panel.Visible = true, true);
            collumn += 54;
            row = 10;
            AppendButtons(new UIHoverImageButton("Creativetools/UI/UI Assets/modifyItem", "Modify Item/Player"), button => button.OnClick += (evt, element) => GetInstance<Creativetools>().UserInterface.SetState(new ItemModUI()), true);
            row += 54;
            AppendButtons(new UIHoverImageButton("Creativetools/UI/UI Assets/custom", "Custom Item/NPC"), button => button.OnClick += (evt, element) => GetInstance<Creativetools>().UserInterface.SetState(new CustomNPCUI()), true);
            row += 54;
            AppendButtons(new UIHoverImageButton("Creativetools/UI/UI Assets/creativeFly", "Creative Fly"), button => button.OnClick += (evt, element) => ModifyPlayer.CreativeFly = !ModifyPlayer.CreativeFly);
            row += 54;
            AppendButtons(new UIHoverImageButton("Creativetools/UI/UI Assets/magicCursor", "Magic Cursor"), button => button.OnClick += (evt, element) => MagicCursor = !MagicCursor);
            row += 54;
            AppendButtons(new UIHoverImageButton("Creativetools/UI/UI Assets/Info", "Display Info"), button => button.OnClick += (evt, element) => Info.Visible = !Info.Visible);
            row += 54;
            AppendButtons(new UIHoverImageButton("Creativetools/UI/UI Assets/weatherControl", "Weather Control"), button => button.OnClick += (evt, element) => GetInstance<Creativetools>().UserInterface.SetState(new WeatherControl()), true);
            row += 54;
            AppendButtons(new UIHoverImageButton("Creativetools/UI/UI Assets/playSound", "Play Sound"), button => button.OnClick += (evt, element) => GetInstance<Creativetools>().UserInterface.SetState(new PlaySoundUI()), true);
            /*row += 54;
            AppendButtons(new UIHoverImageButton("Creativetools/UI/UI Assets/playSound", "Camera Control"), button => button.OnClick += (evt, element) =>
            {
                Main.playerInventory = false;
                Creativetools.ZoomValue = 1f;
                GetInstance<Creativetools>().UserInterface.SetState(null);
                CameraControlUI.Visible = !CameraControlUI.Visible;
            }, true);*/
            #endregion

            for (int i = 0; i < ButtonList.Count; i++)
            {
                UIImage Selected = new UIImage(GetTexture("Creativetools/UI/UI Assets/selected"));
                Selected.MarginTop = -10000;
                ButtonList[i].Append(Selected);
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