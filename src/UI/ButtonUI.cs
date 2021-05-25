using Creativetools.src.UI.Elements;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.UI;

namespace Creativetools.src.UI
{
    internal class ButtonUI : UIState
    {
        private UIHoverImageButton MenuButton;
        public override void OnInitialize()
        {
            MenuButton = new UIHoverImageButton("Creativetools/UI Assets/MenuButton", "Open Menu");
            MenuButton.Top.Set(260, 0);
            MenuButton.OnClick += new MouseEvent(MenuButtonClicked);
            Append(MenuButton);
        }

        private void MenuButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            if (UISystem.UserInterface.CurrentState != null)
            {
                UISystem.UserInterface.SetState(null);
                SoundEngine.PlaySound(SoundID.MenuClose);
            }
            else
            {
                UISystem.UserInterface.SetState(new MainUI());
                SoundEngine.PlaySound(SoundID.MenuOpen);
            }
        }

        public override void Update(GameTime gameTime)
        {
            MenuButton.Left.Set(Main.GameModeInfo.IsJourneyMode ? 67 : 20, 0);
            base.Update(gameTime);
        }
    }
}