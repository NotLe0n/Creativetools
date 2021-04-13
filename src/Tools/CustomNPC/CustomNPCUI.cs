using Creativetools.src.Tools.CustomItem;
using Creativetools.src.UI;
using Creativetools.src.UI.Elements;
using Microsoft.Xna.Framework.Graphics;
using System.Windows.Forms;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;
using static Creativetools.src.UI.UIHelper;
using static Terraria.ModLoader.ModContent;

namespace Creativetools.src.Tools.CustomNPC
{
    class CustomNPCUI : UIState
    {
        private NewUITextBox nametext;
        UIIntRangedDataValue LifeDataProperty;
        UIIntRangedDataValue DamageDataProperty;
        UIIntRangedDataValue DefenseDataProperty;
        UIIntRangedDataValue AiSyleDataProperty;
        UIFloatRangedDataValue KnockbackDataProperty;
        UIFloatRangedDataValue ScaleDataProperty;
        UIIntRangedDataValue FrameDataProperty;
        public override void OnInitialize()
        {
            TabPanel Menu = new TabPanel(450, 500, new Tab("Custom NPC", this), new Tab(" Custom Item", new CustomItemUI()));
            Menu.VAlign = 0.6f;
            Menu.HAlign = 0.2f;
            Menu.OnCloseBtnClicked += () => UISystem.UserInterface.SetState(new MainUI());
            Append(Menu);

            UITextPanel<string> CreateButton = new UITextPanel<string>(Language.GetTextValue("Create NPC"));
            CreateButton.SetPadding(4);
            CreateButton.HAlign = 0.05f;
            CreateButton.MarginTop = 460;
            CreateButton.OnClick += CreateButtonButtonClicked;
            Menu.Append(CreateButton);

            UITextPanel<string> CodeButton = new UITextPanel<string>(Language.GetTextValue("Copy Code"));
            CodeButton.SetPadding(4);
            CodeButton.HAlign = 0.5f;
            CodeButton.MarginTop = 460;
            CodeButton.OnClick += CodeButtonClicked;
            Menu.Append(CodeButton);

            UITextPanel<string> FileButton = new UITextPanel<string>(Language.GetTextValue("Select Texture"));
            FileButton.SetPadding(4);
            FileButton.HAlign = 0.9f;
            FileButton.MarginTop = 460;
            FileButton.OnClick += FileButtonClicked;
            Menu.Append(FileButton);

            nametext = new NewUITextBox("Enter name here");
            nametext.HAlign = 0.5f;
            nametext.MarginTop = 50;
            nametext.Width.Set(-40f, 1f);
            Menu.Append(nametext);

            MakeSlider(new UIIntRangedDataValue("Life: ", 0, 0, 999), out LifeDataProperty, Menu, top: 100);
            MakeSlider(new UIIntRangedDataValue("Damage: ", 0, 0, 999), out DamageDataProperty, Menu, top: 150);
            MakeSlider(new UIIntRangedDataValue("Defense: ", 0, 0, 999), out DefenseDataProperty, Menu, top: 200);
            MakeSlider(new UIIntRangedDataValue("AiStyle: ", 1, 0, 111), out AiSyleDataProperty, Menu, top: 250);
            MakeSlider(new UIFloatRangedDataValue("Knockback resist: ", 0, 0, 1), out KnockbackDataProperty, Menu, 300);
            MakeSlider(new UIFloatRangedDataValue("Scale: ", 1, 0, 10), out ScaleDataProperty, Menu, top: 350);

            FrameDataProperty = new UIIntRangedDataValue("", 1, 1, 20);
            UIElement FrameSlider = new UIRange<int>(FrameDataProperty) { MarginTop = 410, HAlign = 0.35f };
            FrameSlider.Width.Set(0, 0.4f);
            FrameSlider.Append(new UIText("Frame count:") { HAlign = 0.9f, MarginTop = -15 });
            Menu.Append(FrameSlider);

            UITextPanel<string> NoCollideButton = new UITextPanel<string>("Collision: true") { HAlign = 0.05f, MarginTop = 400 };
            NoCollideButton.OnClick += (evt, elm) =>
            {
                CustomNPC.cNoCollide = !CustomNPC.cNoCollide;
                NoCollideButton.SetText("Collision: " + !CustomNPC.cNoCollide);
            };
            Menu.Append(NoCollideButton);

            UITextPanel<string> ImmortalButton = new UITextPanel<string>("Immortal: false") { HAlign = 0.95f, MarginTop = 400 };
            ImmortalButton.OnClick += (evt, elm) =>
            {
                CustomNPC.cImmortal = !CustomNPC.cImmortal;
                ImmortalButton.SetText("Immortal: " + CustomNPC.cImmortal);
            };
            Menu.Append(ImmortalButton);
        }

        private void CreateButtonButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            CustomNPC.cName = nametext.Text;
            CustomNPC.cLifeMax = LifeDataProperty.Data;
            CustomNPC.cDamage = DamageDataProperty.Data;
            CustomNPC.cDefense = DefenseDataProperty.Data;
            CustomNPC.cAistyle = AiSyleDataProperty.Data;
            CustomNPC.cKnockback = KnockbackDataProperty.Data;
            CustomNPC.cScale = ScaleDataProperty.Data;
            CustomNPC.cFramecount = FrameDataProperty.Data;
            Player player = Main.LocalPlayer;
            NPC.NewNPC((int)player.position.X, (int)player.position.Y, NPCType<CustomNPC>());
            SoundEngine.PlaySound(SoundID.MenuTick);
        }
        private void CodeButtonClicked(UIMouseEvent evt, UIElement listeningElement) => Clipboard.SetText(
$@"using Terraria.ID;
using Terraria.ModLoader;

namespace YourMod
{{
    public class CustomNPC : ModNPC
    {{
        public override void SetStaticDefaults()
        {{
            DisplayName.SetDefault(""{CustomNPC.cName}"");
            Main.npcFrameCount[npc.type] = 6;
        }}

        public override void SetDefaults()
        {{
            npc.GivenName = {CustomNPC.cName};
            npc.width = 32;
            npc.height = 32;
            npc.aiStyle = {CustomNPC.cAistyle};
            npc.knockBackResist = {CustomNPC.cKnockback};
            npc.damage = {CustomNPC.cDamage};
            npc.defense = {CustomNPC.cDefense};
            npc.lifeMax = {CustomNPC.cLifeMax};
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = 25f;
            npc.scale = {CustomNPC.cScale};
            npc.noTileCollide = {CustomNPC.cNoCollide};
            npc.immortal = {CustomNPC.cImmortal};
            npc.dontTakeDamage = {CustomNPC.cImmortal};
        }}
    }}
}}");
        private void FileButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "PNG (*.png)|*.png|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    CustomNPC.ctexture = Texture2D.FromStream(Main.graphics.GraphicsDevice, openFileDialog.OpenFile());
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