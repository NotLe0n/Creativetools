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
        private NewUITextBox nametext;
        private UIIntRangedDataValue DamageDataProperty;
        private UIIntRangedDataValue CritDataProperty;
        private UIIntRangedDataValue DefenseDataProperty;
        private UIIntRangedDataValue ShootDataProperty;
        private UIFloatRangedDataValue ShootSpeedDataProperty;
        private UIFloatRangedDataValue KnockbackDataProperty;
        private UIFloatRangedDataValue ScaleDataProperty;
        private UIIntRangedDataValue UseTimeDataProperty;

        public override void OnInitialize()
        {
            var menu = new TabPanel(new Tab("Custom NPC", new CustomNPCUI()), new Tab(" Custom Item", this));
            menu.VAlign = 0.7f;
            menu.HAlign = 0.2f;
            menu.Width.Set(450, 0);
            menu.Height.Set(600, 0);
            menu.OnCloseBtnClicked += () => UISystem.UserInterface.SetState(new MainUI());
            Append(menu);

            var createButton = new UITextPanel<string>(Language.GetTextValue("Create Item"));
            createButton.SetPadding(4);
            createButton.HAlign = 0.05f;
            createButton.MarginTop = 555;
            createButton.OnClick += CreateButtonClicked;
            menu.Append(createButton);

            var codeButton = new UITextPanel<string>(Language.GetTextValue("Copy Code"));
            codeButton.SetPadding(4);
            codeButton.HAlign = 0.5f;
            codeButton.MarginTop = 555;
            codeButton.OnClick += CodeButtonClicked;
            menu.Append(codeButton);

            var fileButton = new UITextPanel<string>(Language.GetTextValue("Select Texture"));
            fileButton.SetPadding(4);
            fileButton.HAlign = 0.9f;
            fileButton.MarginTop = 555;
            fileButton.OnClick += FileButtonClicked;
            menu.Append(fileButton);

            nametext = new NewUITextBox("Enter name here");
            nametext.HAlign = 0.5f;
            nametext.MarginTop = 50;
            nametext.Width.Set(-40f, 1f);
            nametext.Height.Set(30, 0);
            menu.Append(nametext);

            MakeSlider(new UIIntRangedDataValue("Damage: ", 0, 0, 999), out DamageDataProperty, menu, top: 100, widthPixels: 1);
            MakeSlider(new UIIntRangedDataValue("Crit: ", 0, 0, 100), out CritDataProperty, menu, top: 150, widthPixels: 1);
            MakeSlider(new UIIntRangedDataValue("Defense: ", 0, 0, 999), out DefenseDataProperty, menu, top: 200, widthPixels: 1);
            MakeSlider(new UIIntRangedDataValue("Shoot: ", 0, 0, 100), out ShootDataProperty, menu, top: 250, widthPixels: 1);
            MakeSlider(new UIFloatRangedDataValue("Bullet Speed: ", 10, 0, 50), out ShootSpeedDataProperty, menu, top: 300, widthPixels: 1);
            MakeSlider(new UIFloatRangedDataValue("Knockback: ", 0, 0, 100), out KnockbackDataProperty, menu, top: 350, widthPixels: 1);
            MakeSlider(new UIFloatRangedDataValue("Size: ", 1, 0, 10), out ScaleDataProperty, menu, top: 400, widthPixels: 1);
            MakeSlider(new UIIntRangedDataValue("UseTime: ", 10, 0, 600), out UseTimeDataProperty, menu, top: 450, widthPixels: 1);

            var autoSwingButton = new UITextPanel<string>("Autoswing: false");
            autoSwingButton.HAlign = 0.05f;
            autoSwingButton.MarginTop = 500;
            autoSwingButton.OnClick += (evt, listeningelement) =>
            {
                Global.cAutoSwing = !Global.cAutoSwing;
                autoSwingButton.SetText("Autoswing: " + Global.cAutoSwing);
            };
            menu.Append(autoSwingButton);

            var turnAroundButton = new UITextPanel<string>("Turnaround: false");
            turnAroundButton.HAlign = 0.95f;
            turnAroundButton.MarginTop = 500;
            turnAroundButton.OnClick += (evt, listeningelement) =>
            {
                Global.cTurnAround = !Global.cTurnAround;
                turnAroundButton.SetText("Turnaround: " + Global.cTurnAround);
            };
            menu.Append(turnAroundButton);
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