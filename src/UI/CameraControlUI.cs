/*using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;

namespace Creativetools.UI
{
    class CameraControlUI : UIState
    {
        public static bool Visible;
        public static Rectangle ScreenRect;
        public static Rectangle ZoomRect;
        public static Vector2 MouseClickPos;
        public bool dragging;
        public static Vector2 changedscreenpos = Main.screenPosition;
        public static bool Applied { get; set; }
        public override void OnInitialize()
        {
            UITextPanel<string> applyButton = new UITextPanel<string>("Apply");
            applyButton.HAlign = 0.1f;
            applyButton.VAlign = 0.9f;
            applyButton.OnClick += Apply;
            Append(applyButton);

            UITextPanel<string> ResetButton = new UITextPanel<string>("Reset");
            ResetButton.HAlign = 0.15f;
            ResetButton.VAlign = 0.9f;
            ResetButton.OnClick += Reset;
            Append(ResetButton);

            UITextPanel<string> ExitButton = new UITextPanel<string>("Exit");
            ExitButton.HAlign = 0.2f;
            ExitButton.VAlign = 0.9f;
            ExitButton.OnClick += (evt, elm) => Visible = false;
            Append(ExitButton);

            ScreenRect = new Rectangle(Main.screenWidth / 4, Main.screenHeight / 4, Main.screenWidth / 2, Main.screenHeight / 2);
        }
        public override void MouseDown(UIMouseEvent evt)
        {
            dragging = true;
            MouseClickPos = Main.MouseScreen;
            base.MouseDown(evt);
        }
        public override void MouseUp(UIMouseEvent evt)
        {
            dragging = false;
            base.MouseDown(evt);
        }
        private void Apply(UIMouseEvent evt, UIElement listeningElement)
        {
            Creativetools.ZoomValue = (Main.screenWidth / ScreenRect.Width) / 2f;
            changedscreenpos = ScreenRect.Location.ToVector2();// - new Vector2((Main.screenWidth / 4), (Main.screenHeight / 4));
            Applied = true;
            Visible = false;
            ScreenRect.Location = new Point(Main.screenWidth / 4, Main.screenHeight / 4);
        }
        private void Reset(UIMouseEvent evt, UIElement listeningElement)
        {
            Applied = false;
            Creativetools.ZoomValue = 1f;
            ScreenRect.Location = new Point(Main.screenWidth / 4, Main.screenHeight / 4);
            ScreenRect.Width = Main.screenWidth / 2;
            ScreenRect.Height = Main.screenHeight / 2;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Main.mouseLeft)
            {
                if ((ZoomRect.Center.ToVector2() - Main.MouseScreen).Between(new Vector2(-100, -100), new Vector2(100, 100)))
                {
                    ScreenRect.Width -= (int)(ZoomRect.Center.ToVector2() - Main.MouseScreen).X;
                    ScreenRect.Height -= (int)(ZoomRect.Center.ToVector2() - Main.MouseScreen).Y;
                }

                else if (ScreenRect.Contains(Main.MouseScreen.ToPoint()))
                {
                    if (dragging)
                    {
                        ScreenRect.Location = new Point(Main.screenWidth / 4, Main.screenHeight / 4);
                        ScreenRect.X -= (int)(MouseClickPos - Main.MouseScreen).X;
                        ScreenRect.Y -= (int)(MouseClickPos - Main.MouseScreen).Y;
                    }
                }
            }

            ZoomRect = new Rectangle((int)ScreenRect.BottomRight().X - 10, (int)(ScreenRect.BottomRight().Y - 10), 20, 20);
            spriteBatch.Draw(Main.magicPixel, ScreenRect, Color.White * 0.5f);
            spriteBatch.Draw(Main.magicPixel, ZoomRect, Color.White);
            base.Draw(spriteBatch);
        }
    }
    public class CameraPlayer : ModPlayer
    {
        public override void ModifyScreenPosition()
        {
            Main.NewText("screenPos: " + Main.screenPosition);
            Main.NewText("changedScreenPos: " + CameraControlUI.changedscreenpos);
            Main.NewText("ScreenRect.Location: " + CameraControlUI.ScreenRect.Location);
            Main.NewText("newVector: " + new Vector2(Main.screenWidth / 4, Main.screenHeight / 4));
            Main.NewText("mouseclickpos: " + CameraControlUI.MouseClickPos);
            Main.NewText("---------------------------");
            //if (CameraControlUI.Applied)
            //{
                Main.screenPosition.X = CameraControlUI.ScreenRect.Center().X + Main.screenPosition.X;
                Main.screenPosition.Y = CameraControlUI.ScreenRect.Center().Y + Main.screenPosition.Y;
            //}
        }
    }
}
*/