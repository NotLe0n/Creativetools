using Creativetools.src.UI.Elements;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;
using static Creativetools.src.UI.UIHelper;
using static Terraria.ModLoader.ModContent;

namespace Creativetools.src.UI
{
    class PlayerModUI : UIState
    {
        private TabPanel PlayerMenu;
        UIIntRangedDataValue LifeDataProperty;
        UIIntRangedDataValue ManaDataProperty;
        UIIntRangedDataValue MaxLifeDataProperty;
        UIIntRangedDataValue MaxManaDataProperty;
        UIFloatRangedDataValue SizeDataProperty;

        public override void OnInitialize()
        {
            PlayerMenu = new TabPanel(450, 300, new Tab("Change Item", new ItemModUI()), new Tab(" Change Player", this));
            PlayerMenu.VAlign = 0.6f;
            PlayerMenu.HAlign = 0.2f;
            PlayerMenu.OnCloseBtnClicked += () => GetInstance<Creativetools>().UserInterface.SetState(new MainUI());
            Append(PlayerMenu);

            var LifeSlider = MakeSlider(new UIIntRangedDataValue("", 0, 0, 999), out LifeDataProperty, PlayerMenu, top: 50, left: -10);
            SliderButtons("Set Life", LifeSlider, button => button.OnClick += (evt, element) => Main.LocalPlayer.statLife = LifeDataProperty.Data);

            var ManaSlider = MakeSlider(new UIIntRangedDataValue("", 0, 0, 400), out ManaDataProperty, PlayerMenu, top: 100, left: -10);
            SliderButtons("Set Mana", ManaSlider, button => button.OnClick += (evt, element) => Main.LocalPlayer.statMana = ManaDataProperty.Data);

            var MaxLifeSlider = MakeSlider(new UIIntRangedDataValue("", 0, 0, 999), out MaxLifeDataProperty, PlayerMenu, top: 150, left: -10);
            SliderButtons("Set Max Life", MaxLifeSlider, button => button.OnClick += (evt, element) => Main.LocalPlayer.statLifeMax = MaxLifeDataProperty.Data);

            var MaxManaSlider = MakeSlider(new UIIntRangedDataValue("", 0, 0, 400), out MaxManaDataProperty, PlayerMenu, top: 200, left: -10);
            SliderButtons("Set Max Mana", MaxManaSlider, button => button.OnClick += (evt, element) => Main.LocalPlayer.statManaMax = MaxManaDataProperty.Data);

            var SizeSlider = MakeSlider(new UIFloatRangedDataValue("", 0.1f, 0.1f, 4), out SizeDataProperty, PlayerMenu, top: 250, left: -10);
            SliderButtons("Set Size", SizeSlider, button => button.OnClick += (evt, element) => ModifyPlayer.playerSize = SizeDataProperty.Data);
        }

        // so you can't use items when clicking on the button
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            if (ContainsPoint(Main.MouseScreen))
            {
                Main.LocalPlayer.mouseInterface = true;
            }
        }
    }
}