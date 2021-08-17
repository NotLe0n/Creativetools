using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace Creativetools.src.Tools.GameInfo2.Elements
{
    class Header : UIElement
    {
        public string text;
        private readonly string[] spliced;

        public Header(string text)
        {
            this.text = text;
            spliced = text.Split('.');

            float xOffset = 0;
            for (int i = 0; i < spliced.Length; i++)
            {
                var segment = new HeaderSegment(spliced, i);
                segment.Left.Set(xOffset, 0);
                segment.TextColor = i != spliced.Length - 1 ? Color.LightGray : Color.White;
                Append(segment);

                xOffset += FontAssets.MouseText.Value.MeasureString(spliced[i]).X;

                // To not put a dot at the end (e.g: "Terraria.ID.")
                if (i < spliced.Length - 1)
                {
                    Append(new UIText(".")
                    {
                        Left = new(xOffset, 0)
                    });

                    xOffset += FontAssets.MouseText.Value.MeasureString(".").X;
                }
            }
        }

        class HeaderSegment : UIText
        {
            private readonly string[] spliced;
            private readonly int index;

            public HeaderSegment(string[] spliced, int index) : base(spliced[index])
            {
                this.spliced = spliced;
                this.index = index;
            }

            public override void Update(GameTime gameTime)
            {
                base.Update(gameTime);

                // This is in update because the click event doesn't work :))
                if (ContainsPoint(Main.MouseScreen) && Main.mouseLeft)
                {
                    string target = string.Join('.', spliced[0..(index + 1)]);
                    UISystem.UserInterface.SetState(new GameInfo2(target));
                }
            }
        }
    }
}
