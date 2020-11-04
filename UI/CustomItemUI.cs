﻿using Creativetools.cItem;
using Creativetools.UI.Elements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Windows.Forms;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;
using static Creativetools.UI.UIHelper;
using static Terraria.ModLoader.ModContent;

namespace Creativetools.UI
{
    class CustomItemUI : UIState
    {
        internal NewUITextBox nametext;
        public UIIntRangedDataValue DamageDataProperty;
        public UIIntRangedDataValue CritDataProperty;
        public UIIntRangedDataValue DefenseDataProperty;
        public UIIntRangedDataValue ShootDataProperty;
        public UIFloatRangedDataValue ShootSpeedDataProperty;
        public UIFloatRangedDataValue KnockbackDataProperty;
        public UIFloatRangedDataValue ScaleDataProperty;
        public UIIntRangedDataValue UseTimeDataProperty;

        public override void OnInitialize()
        {
            UIPanel Menu = new UIPanel();
            Menu.VAlign = 0.7f;
            Menu.HAlign = 0.2f;
            Menu.Width.Set(500f, 0f);
            Menu.Height.Set(600f, 0f);
            Menu.BackgroundColor = new Color(73, 94, 171);
            Append(Menu);

            Menu.Append(new UIText("Create Item") { HAlign = 0.5f, MarginTop = 15 });
            BackButton(Menu, 0, 555, 0.95f);

            UITextPanel<string> CreateButton = new UITextPanel<string>(Language.GetTextValue("Create"));
            CreateButton.SetPadding(4);
            CreateButton.HAlign = 0.05f;
            CreateButton.MarginTop = 555;
            CreateButton.OnClick += CreateButtonClicked;
            Menu.Append(CreateButton);

            UITextPanel<string> CodeButton = new UITextPanel<string>(Language.GetTextValue("Copy Code"));
            CodeButton.SetPadding(4);
            CodeButton.HAlign = 0.25f;
            CodeButton.MarginTop = 555;
            CodeButton.OnClick += CodeButtonClicked;
            Menu.Append(CodeButton);

            UITextPanel<string> PrevButton = new UITextPanel<string>(Language.GetTextValue("<"));
            PrevButton.HAlign = 0.5f;
            PrevButton.MarginTop = 540;
            PrevButton.OnClick += (evt, elm) =>
            {
                GetInstance<Creativetools>().UserInterface.SetState(new CustomNPCUI());
                Main.PlaySound(SoundID.MenuOpen);
            };
            Menu.Append(PrevButton);

            UITextPanel<string> FileButton = new UITextPanel<string>(Language.GetTextValue("Select Texture"));
            FileButton.SetPadding(4);
            FileButton.HAlign = 0.75f;
            FileButton.MarginTop = 555;
            FileButton.OnClick += FileButtonClicked;
            Menu.Append(FileButton);

            nametext = new NewUITextBox("Enter name here");
            nametext.HAlign = 0.5f;
            nametext.MarginTop = 50;
            nametext.Width.Set(-40f, 1f);
            Menu.Append(nametext);

            MakeSlider(new UIIntRangedDataValue("Damage: ", 0, 0, 999), out DamageDataProperty, Menu, top: 100, widthPixels: 1);
            MakeSlider(new UIIntRangedDataValue("Crit: ", 0, 0, 100), out CritDataProperty, Menu, top: 150, widthPixels: 1);
            MakeSlider(new UIIntRangedDataValue("Defense: ", 0, 0, 999), out DefenseDataProperty, Menu, top: 200, widthPixels: 1);
            MakeSlider(new UIIntRangedDataValue("Shoot: ", 0, 0, 100), out ShootDataProperty, Menu, top: 250, widthPixels: 1);
            MakeSlider(new UIFloatRangedDataValue("Bullet Speed: ", 10, 0, 50), out ShootSpeedDataProperty, Menu, top: 300, widthPixels: 1);
            MakeSlider(new UIFloatRangedDataValue("Knockback: ", 0, 0, 100), out KnockbackDataProperty, Menu, top: 350, widthPixels: 1);
            MakeSlider(new UIFloatRangedDataValue("Size: ", 1, 0, 10), out ScaleDataProperty, Menu, top: 400, widthPixels: 1);
            MakeSlider(new UIIntRangedDataValue("UseTime: ", 10, 0, 600), out UseTimeDataProperty, Menu, top: 450, widthPixels: 1);

            UITextPanel<string> AutoSwingButton = new UITextPanel<string>("Autoswing: false");
            AutoSwingButton.HAlign = 0.05f;
            AutoSwingButton.MarginTop = 500;
            AutoSwingButton.OnClick += (evt, listeningelement) =>
            {
                ModifyItem.cAutoSwing = !ModifyItem.cAutoSwing;
                AutoSwingButton.SetText("Autoswing: " + ModifyItem.cAutoSwing);
            };
            Menu.Append(AutoSwingButton);

            UITextPanel<string> TurnAroundButton = new UITextPanel<string>("Turaround: false");
            TurnAroundButton.HAlign = 0.95f;
            TurnAroundButton.MarginTop = 500;
            TurnAroundButton.OnClick += (evt, listeningelement) =>
            {
                ModifyItem.cTurnAround = !ModifyItem.cTurnAround;
                TurnAroundButton.SetText("Turnaround: " + ModifyItem.cTurnAround);
            };
            Menu.Append(TurnAroundButton);
        }
        private void CreateButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            ModifyItem.cName = nametext.Text;
            ModifyItem.cDamage = DamageDataProperty.Data;
            ModifyItem.cCrit = CritDataProperty.Data;
            ModifyItem.cDefense = DefenseDataProperty.Data;
            ModifyItem.cShoot = ShootDataProperty.Data;
            ModifyItem.cShootSpeed = ShootSpeedDataProperty.Data;
            ModifyItem.cKnockback = KnockbackDataProperty.Data;
            ModifyItem.cScale = ScaleDataProperty.Data;
            ModifyItem.cUseTime = UseTimeDataProperty.Data;
            ModifyItem.createitem = true;
            Main.LocalPlayer.QuickSpawnItem(ItemType<CustomItem>());
            Main.PlaySound(SoundID.MenuTick);
        }
        private void CodeButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            Clipboard.SetText(
            #region code
$@"using Terraria.ID;
using Terraria.ModLoader;

namespace YourMod 
{{
    public class CustomItem : ModItem
    {{
        public override void SetStaticDefaults()
        {{
            DisplayName.SetDefault(""{ModifyItem.cName}"");
        }}

        public override void SetDefaults()
        {{ 
            item.SetNameOverride(""{ModifyItem.cName}"");
            item.width = 32;
            item.height = 32;
            item.melee = true;
            item.knockBack = {ModifyItem.cKnockback};
            item.damage = {ModifyItem.cDamage};
            item.defense = {ModifyItem.cDefense};
            item.scale = {ModifyItem.cScale};
            item.shoot = {ModifyItem.cShoot};
            item.shootSpeed = {ModifyItem.cShootSpeed};
            item.crit = {ModifyItem.cCrit} - 4;
            item.useTime = {ModifyItem.cUseTime};
            item.useAnimation = {ModifyItem.cUseTime};
            item.autoReuse = {ModifyItem.cAutoSwing};
            item.useTurn = {ModifyItem.cTurnAround};
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.UseSound = SoundID.Item1;
        }}
    }}
}}"
            #endregion
            );
        }
        private void FileButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";   //start at c:\\ directory
                openFileDialog.Filter = "PNG (*.png)|*.png|All files (*.*)|*.*";    //supported file types
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ModifyItem.ctexture = Texture2D.FromStream(Main.graphics.GraphicsDevice, openFileDialog.OpenFile());
                }
            }
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