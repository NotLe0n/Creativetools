using Creativetools.src.UI.Elements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;
using static Creativetools.src.UI.UIHelper;
using static Terraria.ModLoader.ModContent;

namespace Creativetools.src.UI
{
    class PlayerModUI : UIState
    {
        private UIPanel PlayerMenu;
        UIIntRangedDataValue LifeDataProperty;
        UIIntRangedDataValue ManaDataProperty;
        UIIntRangedDataValue MaxLifeDataProperty;
        UIIntRangedDataValue MaxManaDataProperty;
        UIFloatRangedDataValue SizeDataProperty;

        public override void OnInitialize()
        {
            PlayerMenu = new UIPanel();
            PlayerMenu.SetPadding(0);
            PlayerMenu.VAlign = 0.6f;
            PlayerMenu.HAlign = 0.2f;
            PlayerMenu.Width.Set(450f, 0f);
            PlayerMenu.Height.Set(350f, 0f);
            PlayerMenu.BackgroundColor = new Color(73, 94, 171);
            Append(PlayerMenu);

            PlayerMenu.Append(new UIText("Change Player Properties") { HAlign = 0.5f, MarginTop = 15 });
            BackButton(PlayerMenu, 370, 310);

            UITextPanel<string> PrevButton = new UITextPanel<string>("<") { HAlign = 0.5f, MarginTop = 290 };
            PrevButton.OnClick += (evt, elm) =>
            {
                GetInstance<Creativetools>().UserInterface.SetState(new ItemModUI());
                Main.PlaySound(SoundID.MenuOpen);
            };
            PlayerMenu.Append(PrevButton);

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