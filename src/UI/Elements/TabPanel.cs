using Microsoft.Xna.Framework;
using System.Linq;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;

namespace Creativetools.src.UI.Elements
{
    class Tab : UITextPanel<string>
    {
        public UIState _changeStateTo;
        public static UIState lastTab;
        /// <summary>
        /// List of all Tabs
        /// </summary>
        public static Tab[] tabs = { };

        /// <summary>
        /// Creates a new Tab
        /// </summary>
        /// <param name="text">The text on the Tab</param>
        /// <param name="changeStateTo">What UIState should be switched to when clicking the Tab</param>
        public Tab(string text, UIState changeStateTo) : base(text)
        {
            _changeStateTo = changeStateTo;
            tabs.ToList().Add(this);
        }
        public override void OnInitialize()
        {
            SetPadding(7);
            BackgroundColor.A = 255;

            for (int i = 0; i < tabs.Length; i++)
            {
                if (i > 0 && tabs[i - 1] != null)
                {
                    tabs[i].Left.Set(tabs[i - 1].GetDimensions().Width - 16f, 0f);
                }
                Recalculate();
            }
        }
        public override void Click(UIMouseEvent evt)
        {
            lastTab = _changeStateTo;
            GetInstance<Creativetools>().UserInterface.SetState(_changeStateTo);
            Main.PlaySound(SoundID.MenuTick);
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (GetInstance<Creativetools>().UserInterface.CurrentState == _changeStateTo)
            {
                BackgroundColor = new Color(73, 94, 171);
            }
        }
    }

    class TabPanel : DragableUIPanel
    {
        /// <summary>
        /// Creates a new Tab Panel
        /// </summary>
        /// <param name="width">The width of the panel</param>
        /// <param name="height">The height of the panel</param>
        /// <param name="tabs">All tabs that the panel should hold</param>
        public TabPanel(float width, float height, params Tab[] tabs) : base("", width, height)
        {
            Width.Pixels = width;
            Height.Pixels = height;
            Tab.tabs = tabs;
        }
        public override void OnInitialize()
        {
            SetPadding(0);
            for (int i = Tab.tabs.Length - 1; i >= 0; i--)
                Append(Tab.tabs[i]);
        }
    }
}
