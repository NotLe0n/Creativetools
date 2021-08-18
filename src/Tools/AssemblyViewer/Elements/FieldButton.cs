using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Reflection;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace Creativetools.src.Tools.AssemblyViewer.Elements
{
    class FieldButton : UIText
    {
        public readonly FieldInfo _field;
        public FieldButton(FieldInfo member) : base("   " + member.Name)
        {
            _field = member;
            TextColor = Color.LightGray;

            string img = "Field";
            if (member.IsLiteral) img = "Constant";
            if (member.IsPrivate) img += "Private";

            var texture = ModContent.Request<Texture2D>("Creativetools/UI Assets/AssemblyViewer/" + img, ReLogic.Content.AssetRequestMode.ImmediateLoad);
            Append(new UIImage(texture));      
        }

        public override void Click(UIMouseEvent evt)
        {
            base.Click(evt);

            if (UISystem.UserInterface2.CurrentState == null)
            {
                UISystem.UserInterface2.SetState(new InspectValue(_field));
            }
            else
            {
                UISystem.UserInterface2.SetState(null);
            }
        }
    }
}
