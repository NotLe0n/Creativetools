using Creativetools.src.Tools.CustomItem;
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

namespace Creativetools.src.Tools.CustomNPC
{
    internal class CustomNPCUI : UIState
    {
        private NewUITextBox nametext;
        private UIIntRangedDataValue LifeDataProperty;
        private UIIntRangedDataValue DamageDataProperty;
        private UIIntRangedDataValue DefenseDataProperty;
        private UIIntRangedDataValue AiSyleDataProperty;
        private UIFloatRangedDataValue KnockbackDataProperty;
        private UIFloatRangedDataValue ScaleDataProperty;
        private UIIntRangedDataValue FrameDataProperty;

        public override void OnInitialize()
        {
            var menu = new TabPanel(new Tab("Custom NPC", this), new Tab(" Custom Item", new CustomItemUI()));
            menu.VAlign = 0.6f;
            menu.HAlign = 0.2f;
            menu.Width.Set(450, 0);
            menu.Height.Set(500, 0);
            menu.OnCloseBtnClicked += () => UISystem.UserInterface.SetState(new MainUI());
            Append(menu);

            var createButton = new UITextPanel<string>(Language.GetTextValue("Create NPC"));
            createButton.SetPadding(4);
            createButton.HAlign = 0.05f;
            createButton.MarginTop = 460;
            createButton.OnClick += CreateButtonButtonClicked;
            menu.Append(createButton);

            var codeButton = new UITextPanel<string>(Language.GetTextValue("Copy Code"));
            codeButton.SetPadding(4);
            codeButton.HAlign = 0.5f;
            codeButton.MarginTop = 460;
            codeButton.OnClick += CodeButtonClicked;
            menu.Append(codeButton);

            var fileButton = new UITextPanel<string>(Language.GetTextValue("Select Texture"));
            fileButton.SetPadding(4);
            fileButton.HAlign = 0.9f;
            fileButton.MarginTop = 460;
            fileButton.OnClick += FileButtonClicked;
            menu.Append(fileButton);

            nametext = new NewUITextBox("Enter name here");
            nametext.HAlign = 0.5f;
            nametext.MarginTop = 50;
            nametext.Width.Set(-40f, 1f);
            nametext.Height.Set(30, 0);
            menu.Append(nametext);

            MakeSlider(new UIIntRangedDataValue("Life: ", 0, 0, 999), out LifeDataProperty, menu, top: 100);
            MakeSlider(new UIIntRangedDataValue("Damage: ", 0, 0, 999), out DamageDataProperty, menu, top: 150);
            MakeSlider(new UIIntRangedDataValue("Defense: ", 0, 0, 999), out DefenseDataProperty, menu, top: 200);
            MakeSlider(new UIIntRangedDataValue("AiStyle: ", 1, 0, 111), out AiSyleDataProperty, menu, top: 250);
            MakeSlider(new UIFloatRangedDataValue("Knockback resist: ", 0, 0, 1), out KnockbackDataProperty, menu, 300);
            MakeSlider(new UIFloatRangedDataValue("Scale: ", 1, 0, 10), out ScaleDataProperty, menu, top: 350);

            FrameDataProperty = new UIIntRangedDataValue("", 1, 1, 20);
            var FrameSlider = new UIRange<int>(FrameDataProperty) { MarginTop = 410, HAlign = 0.35f };
            FrameSlider.Width.Set(0, 0.4f);
            FrameSlider.Append(new UIText("Frame count:") { HAlign = 0.9f, MarginTop = -15 });
            menu.Append(FrameSlider);

            var noCollideButton = new UITextPanel<string>("Collision: true") { HAlign = 0.05f, MarginTop = 400 };
            noCollideButton.OnClick += (evt, elm) =>
            {
                CustomNPC.cNoCollide = !CustomNPC.cNoCollide;
                noCollideButton.SetText("Collision: " + !CustomNPC.cNoCollide);
            };
            menu.Append(noCollideButton);

            var immortalButton = new UITextPanel<string>("Immortal: false") { HAlign = 0.95f, MarginTop = 400 };
            immortalButton.OnClick += (evt, elm) =>
            {
                CustomNPC.cImmortal = !CustomNPC.cImmortal;
                immortalButton.SetText("Immortal: " + CustomNPC.cImmortal);
            };
            menu.Append(immortalButton);
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
        private void CodeButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            /*Clipboard.SetText(
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
}}");*/
        }

        private void FileButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            /*using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "PNG (*.png)|*.png|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    CustomNPC.ctexture = Texture2D.FromStream(Main.graphics.GraphicsDevice, openFileDialog.OpenFile());
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