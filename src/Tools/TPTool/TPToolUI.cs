using Creativetools.src.UI;
using Creativetools.src.UI.Elements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace Creativetools.src.Tools.TPTool
{
    internal class TPToolUI : UIState
    {
        private bool relative = false;
        private Vector2 coordinates = Vector2.Zero;
        private UIList NPCList;

        public override void OnInitialize()
        {
            var panel = new DragableUIPanel("TP Tool") { VAlign = 0.5f, HAlign = 0.1f };
            panel.Width.Set(450, 0);
            panel.Height.Set(300, 0);
            panel.OnCloseBtnClicked += () => UISystem.UserInterface.SetState(new MainUI());
            Append(panel);

            var tp2coords = new UIText("TP to coordinates:");
            tp2coords.Top.Set(40, 0);
            tp2coords.Left.Set(20, 0);
            panel.Append(tp2coords);

            var tp2coordsX = new UIText("X:");
            tp2coordsX.Top.Set(60, 0);
            tp2coordsX.Left.Set(20, 0);
            panel.Append(tp2coordsX);

            var tp2coordsY = new UIText("Y:");
            tp2coordsY.Top.Set(80, 0);
            tp2coordsY.Left.Set(20, 0);
            panel.Append(tp2coordsY);

            var tp2coordsXInput = new NewUITextBox("0");
            tp2coordsXInput.Width.Set(100, 0);
            tp2coordsXInput.Height.Set(20, 0);
            tp2coordsXInput.Top.Set(60, 0);
            tp2coordsXInput.Left.Set(50, 0);
            tp2coordsXInput.OnUnfocus += () => coordinates.X = float.TryParse(tp2coordsXInput.Text, out float temp) ? temp : coordinates.X;
            tp2coordsXInput.unfocusOnEnter = true;
            panel.Append(tp2coordsXInput);

            var tp2coordsYInput = new NewUITextBox("0");
            tp2coordsYInput.Width.Set(100, 0);
            tp2coordsYInput.Height.Set(20, 0);
            tp2coordsYInput.Top.Set(80, 0);
            tp2coordsYInput.Left.Set(50, 0);
            tp2coordsYInput.OnUnfocus += () => coordinates.Y = float.TryParse(tp2coordsYInput.Text, out float temp) ? temp : coordinates.Y;
            tp2coordsYInput.unfocusOnEnter = true;
            panel.Append(tp2coordsYInput);

            var tp2relativeCoordsText = new UIText("relative");
            tp2relativeCoordsText.Top.Set(120, 0);
            tp2relativeCoordsText.Left.Set(20, 0);
            panel.Append(tp2relativeCoordsText);

            var tp2relativeCoords = new UIToggleImage(Main.Assets.Request<Texture2D>("Images\\UI\\Settings_Toggle"), 13, 13, new Point(17, 1), new Point(1, 1));
            tp2relativeCoords.Top.Set(120, 0);
            tp2relativeCoords.Left.Set(80, 0);
            tp2relativeCoords.OnClick += (evt, elm) => relative = tp2relativeCoords.IsOn;
            panel.Append(tp2relativeCoords);

            var tpBtn = new UITextPanel<string>("Teleport");
            tpBtn.Width.Set(20, 0);
            tpBtn.Height.Set(10, 0);
            tpBtn.Top.Set(150, 0);
            tpBtn.Left.Set(20, 0);
            tpBtn.OnClick += Teleport;
            panel.Append(tpBtn);

            var tp2NPCText = new UIText("TP to NPC");
            tp2NPCText.Top.Set(40, 0);
            tp2NPCText.Left.Set(200, 0);
            panel.Append(tp2NPCText);

            var NPCscrollbar = new UIScrollbar();
            NPCscrollbar.Width.Set(20, 0);
            NPCscrollbar.Height.Set(190, 0);
            NPCscrollbar.Left.Set(420, 0);
            NPCscrollbar.Top.Set(70, 0);
            panel.Append(NPCscrollbar);

            NPCList = new UIList();
            NPCList.Width.Set(250, 0);
            NPCList.Height.Set(210, 0);
            NPCList.Left.Set(200, 0);
            NPCList.Top.Set(60, 0);
            NPCList.ListPadding = 8f;
            NPCList.SetScrollbar(NPCscrollbar);
            panel.Append(NPCList);

            foreach (NPC npc in Main.npc.Where(x => x.active))
            {
                var NPCCard = new NPCTPCard(npc);
                NPCCard.Width.Set(200, 0);
                NPCCard.Height.Set(50, 0);
                NPCList.Add(NPCCard);
            }

            base.OnInitialize();
        }

        private void Teleport(UIMouseEvent evt, UIElement listeningElement)
        {
            if (relative)
            {
                Main.LocalPlayer.position += coordinates;
            }
            else
            {
                Main.LocalPlayer.position = coordinates;
            }

            Terraria.Audio.SoundEngine.PlaySound(Terraria.ID.SoundID.Item6);
        }

        public override void Update(GameTime gameTime)
        {
            if (Main.GameUpdateCount % 60 == 0)
            {
                NPCList.Clear();

                foreach (NPC npc in Main.npc.Where(x => x.active))
                {
                    var NPCCard = new NPCTPCard(npc);
                    NPCCard.Width.Set(200, 0);
                    NPCCard.Height.Set(50, 0);
                    NPCList.Add(NPCCard);
                }
            }

            base.Update(gameTime);
        }
    }
}
