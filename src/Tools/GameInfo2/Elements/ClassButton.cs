using Microsoft.Xna.Framework.Graphics;
using System.Reflection;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace Creativetools.src.Tools.GameInfo2.Elements
{
    class ClassButton : UIText
    {
        public readonly TypeInfo _class;
        public ClassButton(TypeInfo clas) : base("   " + clas.Name)
        {
            _class = clas;

            var texture = ModContent.Request<Texture2D>("Creativetools/UI Assets/GameInfo2/class", ReLogic.Content.AssetRequestMode.ImmediateLoad);
            Append(new UIImage(texture));
        }

        public override void Click(UIMouseEvent evt)
        {
            base.Click(evt);

            UISystem.UserInterface.SetState(new GameInfo2(_class.Namespace, _class));
        }
    }
}
