using Creativetools.src.Tools.CustomNPC;
using Creativetools.src.UI;
using Creativetools.src.UI.Elements;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;
using static Creativetools.src.UI.UIHelper;
using static Terraria.ModLoader.ModContent;

namespace Creativetools.src.Tools.CustomItem
{
    internal class CustomItemUI : UIState
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
            TabPanel Menu = new(new Tab("Custom NPC", new CustomNPCUI()), new Tab(" Custom Item", this));
            Menu.VAlign = 0.7f;
            Menu.HAlign = 0.2f;
            Menu.Width.Set(450, 0);
            Menu.Height.Set(600, 0);
            Menu.OnCloseBtnClicked += () => UISystem.UserInterface.SetState(new MainUI());
            Append(Menu);

            UITextPanel<string> CreateButton = new(Language.GetTextValue("Create Item"));
            CreateButton.SetPadding(4);
            CreateButton.HAlign = 0.05f;
            CreateButton.MarginTop = 555;
            CreateButton.OnClick += CreateButtonClicked;
            Menu.Append(CreateButton);

            UITextPanel<string> CodeButton = new(Language.GetTextValue("Copy Code"));
            CodeButton.SetPadding(4);
            CodeButton.HAlign = 0.5f;
            CodeButton.MarginTop = 555;
            CodeButton.OnClick += CodeButtonClicked;
            Menu.Append(CodeButton);

            UITextPanel<string> FileButton = new(Language.GetTextValue("Select Texture"));
            FileButton.SetPadding(4);
            FileButton.HAlign = 0.9f;
            FileButton.MarginTop = 555;
            FileButton.OnClick += FileButtonClicked;
            Menu.Append(FileButton);

            nametext = new NewUITextBox("Enter name here");
            nametext.HAlign = 0.5f;
            nametext.MarginTop = 50;
            nametext.Width.Set(-40f, 1f);
            nametext.Height.Set(30, 0);
            Menu.Append(nametext);

            MakeSlider(new UIIntRangedDataValue("Damage: ", 0, 0, 999), out DamageDataProperty, Menu, top: 100, widthPixels: 1);
            MakeSlider(new UIIntRangedDataValue("Crit: ", 0, 0, 100), out CritDataProperty, Menu, top: 150, widthPixels: 1);
            MakeSlider(new UIIntRangedDataValue("Defense: ", 0, 0, 999), out DefenseDataProperty, Menu, top: 200, widthPixels: 1);
            MakeSlider(new UIIntRangedDataValue("Shoot: ", 0, 0, 100), out ShootDataProperty, Menu, top: 250, widthPixels: 1);
            MakeSlider(new UIFloatRangedDataValue("Bullet Speed: ", 10, 0, 50), out ShootSpeedDataProperty, Menu, top: 300, widthPixels: 1);
            MakeSlider(new UIFloatRangedDataValue("Knockback: ", 0, 0, 100), out KnockbackDataProperty, Menu, top: 350, widthPixels: 1);
            MakeSlider(new UIFloatRangedDataValue("Size: ", 1, 0, 10), out ScaleDataProperty, Menu, top: 400, widthPixels: 1);
            MakeSlider(new UIIntRangedDataValue("UseTime: ", 10, 0, 600), out UseTimeDataProperty, Menu, top: 450, widthPixels: 1);

            UITextPanel<string> AutoSwingButton = new("Autoswing: false");
            AutoSwingButton.HAlign = 0.05f;
            AutoSwingButton.MarginTop = 500;
            AutoSwingButton.OnClick += (evt, listeningelement) =>
            {
                Global.cAutoSwing = !Global.cAutoSwing;
                AutoSwingButton.SetText("Autoswing: " + Global.cAutoSwing);
            };
            Menu.Append(AutoSwingButton);

            UITextPanel<string> TurnAroundButton = new("Turnaround: false");
            TurnAroundButton.HAlign = 0.95f;
            TurnAroundButton.MarginTop = 500;
            TurnAroundButton.OnClick += (evt, listeningelement) =>
            {
                Global.cTurnAround = !Global.cTurnAround;
                TurnAroundButton.SetText("Turnaround: " + Global.cTurnAround);
            };
            Menu.Append(TurnAroundButton);
        }
        private void CreateButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            Global.cName = nametext.Text;
            Global.cDamage = DamageDataProperty.Data;
            Global.cCrit = CritDataProperty.Data;
            Global.cDefense = DefenseDataProperty.Data;
            Global.cShoot = ShootDataProperty.Data;
            Global.cShootSpeed = ShootSpeedDataProperty.Data;
            Global.cKnockback = KnockbackDataProperty.Data;
            Global.cScale = ScaleDataProperty.Data;
            Global.cUseTime = UseTimeDataProperty.Data;
            Global.createitem = true;
            Main.LocalPlayer.QuickSpawnItem(ItemType<CustomItem>());
            SoundEngine.PlaySound(SoundID.MenuTick);
        }
        private void CodeButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            /*Clipboard.SetText(
            #region code
$@"using Terraria.ID;
using Terraria.ModLoader;

namespace YourMod 
{{
    public class CustomItem : ModItem
    {{
        public override void SetStaticDefaults()
        {{
            DisplayName.SetDefault(""{Global.cName}"");
        }}

        public override void SetDefaults()
        {{ 
            Item.SetNameOverride(""{Global.cName}"");
            Item.width = 32;
            Item.height = 32;
            Item.melee = true;
            Item.knockBack = {Global.cKnockback};
            Item.damage = {Global.cDamage};
            Item.defense = {Global.cDefense};
            Item.scale = {Global.cScale};
            Item.shoot = {Global.cShoot};
            Item.shootSpeed = {Global.cShootSpeed};
            Item.crit = {Global.cCrit} - 4;
            Item.useTime = {Global.cUseTime};
            Item.useAnimation = {Global.cUseTime};
            Item.autoReuse = {Global.cAutoSwing};
            Item.useTurn = {Global.cTurnAround};
            Item.useStyle = ItemUseStyleID.SwingThrow;
            Item.UseSound = SoundID.Item1;
        }}
    }}
}}"
            #endregion
            );*/
        }
        private void FileButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            /*using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";   //start at c:\\ directory
                openFileDialog.Filter = "PNG (*.png)|*.png|All files (*.*)|*.*";    //supported file types
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Global.ctexture = Texture2D.FromStream(Main.graphics.GraphicsDevice, openFileDialog.OpenFile());
                }
            }*/
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