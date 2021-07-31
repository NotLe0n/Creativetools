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
        public override void OnInitialize()
        {
            DragableUIPanel MenuPanel = new("Event Toggle", 500f, 100f) { VAlign = 0.5f, HAlign = 0.1f };
            MenuPanel.OnCloseBtnClicked += () => { UISystem.UserInterface.SetState(new MainUI()); SoundEngine.PlaySound(SoundID.MenuClose); };
            Append(MenuPanel);

            buttonGrid = new UIGrid(9);
            buttonGrid.Top.Set(40, 0f);
            buttonGrid.Left.Set(10, 0f);
            buttonGrid.Width.Set(0, 1);
            buttonGrid.Height.Set(0, 1);
            buttonGrid.ListPadding = 10f;
            MenuPanel.Append(buttonGrid);
            base.OnInitialize();

            buttonGrid.Add(new MenuButton("bloodmoonToggle", "Toggle bloodmoon", (evt, element) => Main.bloodMoon = !Main.bloodMoon));
            buttonGrid.Add(new MenuButton("frostmoonToggle", "Toggle Frost moon", (evt, element) => { if (Main.snowMoon) Main.stopMoonEvent(); else Main.startSnowMoon(); }));
            buttonGrid.Add(new MenuButton("pumpkinmoonToggle", "Toggle Pumpkin moon", (evt, element) => { if (Main.pumpkinMoon) Main.stopMoonEvent(); else Main.startPumpkinMoon(); }));
            buttonGrid.Add(new MenuButton("solareclipseToggle", "Toggle Solar Eclipse", (evt, element) => Main.eclipse = !Main.eclipse));
            buttonGrid.Add(new MenuButton("halloweenToggle", "Toggle halloween", (evt, element) => Main.halloween = !Main.halloween));
            buttonGrid.Add(new MenuButton("xmasToggle", "Toggle Christmas", (evt, element) => Main.xMas = !Main.xMas));
            buttonGrid.Add(new MenuButton("partyToggle", "Toggle party", (evt, element) => BirthdayParty.GenuineParty = !BirthdayParty.PartyIsUp));
            buttonGrid.Add(new MenuButton("slimerainToggle", "Toggle Slime Rain", (evt, element) => { if (Main.slimeRain) Main.StopSlimeRain(announce: true); else Main.StartSlimeRain(announce: true); }));
            buttonGrid.Add(new MenuButton("spawnMeteor", "spawn Meteor", (evt, element) => WorldGen.dropMeteor()));
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime); // Don't remove, or else dragging won't be smooth
            bool[] check = { Main.bloodMoon, Main.snowMoon, Main.pumpkinMoon, Main.eclipse, Main.halloween, Main.xMas, BirthdayParty.PartyIsUp, Main.slimeRain, WorldGen.spawnMeteor };

            for (int i = 0; i < buttonGrid.Count; i++)
            {
                ((MenuButton)buttonGrid.items[i]).SetState(check[i]);
            }
        }
    }
}
