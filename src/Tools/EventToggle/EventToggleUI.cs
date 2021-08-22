using Creativetools.src.UI;
using Creativetools.src.UI.Elements;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.UI;

namespace Creativetools.src.Tools.EventToggle
{
    internal class EventToggleUI : UIState
    {
        private UIGrid buttonGrid;
        private bool starfall;

        public override void OnInitialize()
        {
            var menuPanel = new DragableUIPanel("Event Toggle") { VAlign = 0.5f, HAlign = 0.1f };
            menuPanel.Width.Set(442, 0);
            menuPanel.Height.Set(160, 0);
            menuPanel.OnCloseBtnClicked += () => { UISystem.UserInterface.SetState(new MainUI()); SoundEngine.PlaySound(SoundID.MenuClose); };
            Append(menuPanel);

            buttonGrid = new UIGrid(8);
            buttonGrid.Top.Set(40, 0f);
            buttonGrid.Left.Set(10, 0f);
            buttonGrid.Width.Set(0, 1);
            buttonGrid.Height.Set(0, 1);
            buttonGrid.ListPadding = 10f;
            menuPanel.Append(buttonGrid);
            base.OnInitialize();

            AddButton("bloodmoonToggle",    "Toggle bloodmoon",     () => Main.bloodMoon = !Main.bloodMoon);
            AddButton("frostmoonToggle",    "Toggle Frost moon",    () => { if (Main.snowMoon) Main.stopMoonEvent(); else Main.startSnowMoon(); });
            AddButton("pumpkinmoonToggle",  "Toggle Pumpkin moon",  () => { if (Main.pumpkinMoon) Main.stopMoonEvent(); else Main.startPumpkinMoon(); });
            AddButton("solareclipseToggle", "Toggle Solar Eclipse", () => Main.eclipse = !Main.eclipse);
            AddButton("halloweenToggle",    "Toggle halloween",     () => Main.halloween = !Main.halloween);
            AddButton("xmasToggle",         "Toggle Christmas",     () => Main.xMas = !Main.xMas);
            AddButton("partyToggle",        "Toggle party",         () => BirthdayParty.GenuineParty = !BirthdayParty.PartyIsUp);
            AddButton("slimerainToggle",    "Toggle Slime Rain",    () => { if (Main.slimeRain) Main.StopSlimeRain(announce: true); else Main.StartSlimeRain(announce: true); });

            AddButton("spawnMeteor",        "spawn Meteor",         () => WorldGen.dropMeteor());
            AddButton("lanternnight",       "Lantern Night",        () => LanternNight.GenuineLanterns = !LanternNight.GenuineLanterns);
            AddButton("meteorshower",       "Meteor shower",        () => starfall = !starfall);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime); // Don't remove, or else dragging won't be smooth
            bool[] check = { Main.bloodMoon, Main.snowMoon, Main.pumpkinMoon, Main.eclipse, Main.halloween, Main.xMas, BirthdayParty.PartyIsUp, Main.slimeRain, WorldGen.spawnMeteor, LanternNight.LanternsUp, starfall };

            for (int i = 0; i < buttonGrid.Count; i++)
            {
                ((MenuButton)buttonGrid.items[i]).SetState(check[i]);
            }

            if (starfall) Star.StarFall(Main.LocalPlayer.position.X);
        }

        private void AddButton(string iconName, string name, System.Action action)
        {
            buttonGrid.Add(new MenuButton(iconName, name, (evt, elm) => action()));
        }
    }
}
