using Creativetools.UI.Elements;
using Terraria;
using Terraria.ID;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;

namespace Creativetools.UI
{
    class ButtonUI : UIState
    {
        public override void OnInitialize()
        {
            UIHoverImageButton Menu_button = new UIHoverImageButton("Creativetools/UI/UI Assets/MenuButton", "Open Menu");
            Menu_button.MarginTop = 260;
            Menu_button.MarginLeft = 20;
            Menu_button.OnClick += new MouseEvent(Menu_buttonClicked);
            Append(Menu_button);
        }
        private void Menu_buttonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            if (GetInstance<Creativetools>().UserInterface.CurrentState != null)
            {
                GetInstance<Creativetools>().UserInterface.SetState(null);
                Main.PlaySound(SoundID.MenuClose);
            }
            else
            {

                GetInstance<Creativetools>().UserInterface.SetState(new MainUI());
                Main.PlaySound(SoundID.MenuOpen);
            }
        }
    }
}