using Creativetools.src.UI.Elements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Reflection;
using Creativetools.src.Tools.AssemblyViewer.Elements;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using Creativetools.src.UI;

namespace Creativetools.src.Tools.AssemblyViewer
{
    class InspectValue : UIState
    {
        private readonly MemberInfo _member;

        private UIDataElement dataElement;
        private readonly DragableUIPanel panel;
        private readonly UIText frozenText;
        private readonly UIToggleImage frozenToggle;
        private bool frozen;

        public InspectValue(MemberInfo member)
        {
            _member = member;
            frozen = false;

            panel = new DragableUIPanel(member.Name);
            panel.Top.Set(200, 0);
            panel.Left.Set(500, 0);
            panel.OnCloseBtnClicked += () => UISystem.UserInterface2.SetState(null);
            Append(panel);

            panel.Append(new UIText("Value:")
            {
                Left = new(20, 0),
                Top = new(45, 0),
            });

            // you can only change the value of a field if it is not a const or readonly
            var field = _member as FieldInfo;
            var property = _member as PropertyInfo;

            if ((!field?.IsLiteral).GetValueOrDefault() && (!field?.IsInitOnly).GetValueOrDefault() || (property?.CanWrite).GetValueOrDefault())
            {
                frozenText = new UIText("frozen:")
                {
                    Top = new(45, 0),
                };
                panel.Append(frozenText);

                frozenToggle = new UIToggleImage(Main.Assets.Request<Texture2D>("Images\\UI\\Settings_Toggle"), 13, 13, new Point(17, 1), new Point(1, 1));
                frozenToggle.OnClick += FrozenToggle_OnClick;
                frozenToggle.Top.Set(47, 0);
                panel.Append(frozenToggle);
            }
        }

        private void FrozenToggle_OnClick(UIMouseEvent evt, UIElement listeningElement)
        {
            frozen = !frozen;
            dataElement.IgnoresMouseInteraction = !frozen;
        }

        private bool tempUnfreeze;
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (!frozen || tempUnfreeze)
            {
                if (_member is FieldInfo field)
                    UpdateDataElement(field);
                else if (_member is PropertyInfo property)
                    UpdateDataElement(property);
            }
            else
            {
                if (_member is FieldInfo field)
                    field.SetValue(null, dataElement.data);
                else if (_member is PropertyInfo property)
                    property.SetValue(null, dataElement.data);
            }
        }

        private void UpdateDataElement(FieldInfo field)
        {
            if (dataElement != null)
            {
                dataElement.Remove();
                dataElement = null;
            }

            if (field.FieldType.ContainsGenericParameters)
                return;

            dynamic val = field.GetValue(null);
            dataElement = new UIDataElement(val)
            {
                Top = new(40, 0),
                Left = new(80, 0),
                IgnoresMouseInteraction = !frozen
            };
            dataElement.OnValueChanged += (newData) => ChangeFieldValue(field, newData);
            panel.Append(dataElement);

            tempUnfreeze = false;
        }

        private void ChangeFieldValue(FieldInfo field, object newData)
        {
            field.SetValue(null, newData);
            tempUnfreeze = true;
        }

        private void UpdateDataElement(PropertyInfo property)
        {
            if (dataElement != null)
            {
                dataElement.Remove();
                dataElement = null;
            }

            if (property.PropertyType.ContainsGenericParameters)
                return;

            dynamic val = property.GetValue(null);
            dataElement = new UIDataElement(val)
            {
                Top = new(40, 0),
                Left = new(80, 0),
                IgnoresMouseInteraction = !frozen
            };
            dataElement.OnValueChanged += (newData) => ChangePropertyValue(property, newData);
            panel.Append(dataElement);

            tempUnfreeze = false;
        }

        private void ChangePropertyValue(PropertyInfo property, object newData)
        {
            property.SetValue(null, newData);
            tempUnfreeze = true;
        }

        public override void Recalculate()
        {
            base.Recalculate();

            if (dataElement != null)
            {
                frozenText?.Left.Set(panel.GetDimensions().Width - 100, 0);
                frozenToggle?.Left.Set(panel.GetDimensions().Width - 30, 0);

                panel.Width.Set(Math.Max(dataElement.Width.Pixels + (frozenText?.GetDimensions().Width).GetValueOrDefault() + 150, _member.Name.GetTextSize().X + 40), 0);
                panel.Height.Set(dataElement.Height.Pixels, 0);
                panel.Recalculate();
            }
        }
    }
}
