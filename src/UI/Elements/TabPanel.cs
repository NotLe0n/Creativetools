using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;

namespace Creativetools.src.UI.Elements
{
    internal class Tab : UITextPanel<string>
    {
        public UIState _changeStateTo;
        /// <summary>
        /// Creates a new Tab
        /// </summary>
        /// <param name="text">The text on the Tab</param>
        /// <param name="changeStateTo">What UIState should be switched to when clicking the Tab</param>
        public Tab(string text, UIState changeStateTo) : base(text)
        {
            _changeStateTo = changeStateTo;
        }
        public override void OnInitialize()
        {
            SetPadding(7);
            BackgroundColor.A = 255; // solid color
        }
        public override void Click(UIMouseEvent evt)
        {
            // update Last tab
            TabPanel.lastTab = _changeStateTo;

            // change UIState and play click sound
            UISystem.UserInterface.SetState(_changeStateTo);
            SoundEngine.PlaySound(SoundID.MenuTick);
        }
        public override void Update(GameTime gameTime)
        {
            // Highlight
            if (UISystem.UserInterface.CurrentState == _changeStateTo)
            {
                BackgroundColor = new Color(73, 94, 171);
            }
        }
    }

    internal class TabPanel : DragableUIPanel
    {
        /// <summary>
        /// List of all Tabs
        /// </summary>
        public Tab[] Tabs;

        public static UIState lastTab;

        /// <summary>
        /// Creates a new Tab Panel
        /// </summary>
        /// <param name="tabs">All tabs that the panel should hold</param>
        public TabPanel(params Tab[] tabs) : base("")
        {
            Tabs = tabs;
        }
        public override void OnInitialize()
        {
            base.OnInitialize();

            // set correct position for all tabs
            for (int i = 0; i < Tabs.Length; i++)
            {
                if (i > 0 && Tabs[i - 1] != null)
                {
                    Tabs[i].Left.Set(FontAssets.MouseText.Value.MeasureString(Tabs[i - 1].Text).X, 0f);
                }
            }
            // append all tabs
            for (int i = Tabs.Length - 1; i >= 0; i--)
                header.Append(Tabs[i]);
        }
    }
}
