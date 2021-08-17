using Creativetools.src.UI;
using Creativetools.src.UI.Elements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace Creativetools.src.Tools.GameInfo2.Elements
{
    class UIDataElement : UIElement
    {
        public object data;

        public delegate void ValueChanged(object newData);
        public event ValueChanged OnValueChanged;

        #region basic
        public UIDataElement(object data)
        {
            this.data = data;
            Width.Set(150, 0);
            Height.Set(100, 0);

            Append(new UIText(data.ToString())
            {
                Top = new(5, 0)
            });
        }
        public UIDataElement(bool data)
        {
            this.data = data;
            Width.Set(150, 0);
            Height.Set(100, 0);

            Append(new UIText(data.ToString())
            {
                Top = new(5, 0)
            });

            var toggle = new UIToggleImage(Main.Assets.Request<Texture2D>("Images\\UI\\Settings_Toggle"), 13, 13, new Point(17, 1), new Point(1, 1));
            toggle.SetState(data);
            toggle.Left.Set(50, Left.Precent);
            toggle.Top.Set(6, 0);
            toggle.OnClick += (evt, elm) => OnValueChanged.Invoke(!(bool)this.data);
            Append(toggle);
        }
        #region numeric
        private void NumericTextbox(object data, Type conversionType)
        {
            var textBox = new NewUITextBox(data.ToString());
            textBox.Width.Set(data.ToString().GetTextSize().X + 50, 0);
            textBox.Height.Set(data.ToString().GetTextSize().Y, 0);
            textBox.unfocusOnEnter = true;
            textBox.OnEnterPressed += () => OnValueChanged?.Invoke(textBox.Text != "" ? Convert.ChangeType(textBox.Text, conversionType) : data);
            textBox.OnTextChanged += () => textBox.SetText(textBox.currentString != "" && !CanParseNumericString(textBox.currentString, conversionType) ? textBox.currentString[0..^1] : textBox.currentString);
            Append(textBox);
        }
        private static bool CanParseNumericString(string str, Type type)
        {
            // allow minus sign for signed types
            if ((type == typeof(sbyte) || type == typeof(short) || type == typeof(int) || type == typeof(double) || type == typeof(long))
                && str == "-")
                return true;

            try
            {
                Convert.ChangeType(str, type);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public UIDataElement(byte data)
        {
            this.data = data;
            Width.Set(150, 0);
            Height.Set(100, 0);

            NumericTextbox(data, typeof(byte));
        }
        public UIDataElement(short data)
        {
            this.data = data;
            Width.Set(150, 0);
            Height.Set(100, 0);

            NumericTextbox(data, typeof(short));
        }
        public UIDataElement(int data)
        {
            this.data = data;
            Width.Set(150, 0);
            Height.Set(100, 0);

            NumericTextbox(data, typeof(int));
        }
        public UIDataElement(long data)
        {
            this.data = data;
            Width.Set(150, 0);
            Height.Set(100, 0);

            NumericTextbox(data, typeof(long));
        }
        public UIDataElement(float data)
        {
            this.data = data;
            Width.Set(150, 0);
            Height.Set(100, 0);

            NumericTextbox(data, typeof(float));
        }
        public UIDataElement(double data)
        {
            this.data = data;
            Width.Set(150, 0);
            Height.Set(100, 0);

            NumericTextbox(data, typeof(double));
        }
        #endregion
        public UIDataElement(string data)
        {
            this.data = data;
            Width.Set(150, 0);
            Height.Set(100, 0);

            if (data != null)
            {
                var textBox = new NewUITextBox(data.ToString());
                textBox.Width.Set(data.ToString().GetTextSize().X + 50, 0);
                textBox.MinHeight.Set(25, 0);
                textBox.Height.Set(data.ToString().GetTextSize().Y, 0);
                textBox.unfocusOnEnter = true;
                textBox.OnEnterPressed += () => OnValueChanged.Invoke(textBox.Text);
                Append(textBox);
            }
            else
            {
                Append(new UIText("null")
                {
                    Top = new(5, 0)
                });
            }
        }
        #endregion
        public UIDataElement(Vector2 data)
        {
            this.data = data;

            Width.Set(160, 0);
            Height.Set(100, 0);

            var elmX = new UIDataElement(data.X);
            elmX.Width.Set(100, 0);
            elmX.Height.Set(30, 0);
            Append(elmX);

            var elmY = new UIDataElement(data.Y);
            elmY.Width.Set(100, 0);
            elmY.Height.Set(30, 0);
            elmY.Left.Set(110, 0);
            Append(elmY);
        }
        public UIDataElement(Vector3 data)
        {
            this.data = data;

            Width.Set(150, 0);
            Height.Set(100, 0);

            var elmX = new UIDataElement(data.X);
            elmX.Width.Set(50, 0);
            elmX.Height.Set(30, 0);
            Append(elmX);

            var elmY = new UIDataElement(data.Y);
            elmY.Width.Set(50, 0);
            elmY.Height.Set(30, 0);
            elmY.Left.Set(60, 0);
            Append(elmY);

            var elmZ = new UIDataElement(data.Z);
            elmZ.Width.Set(50, 0);
            elmZ.Height.Set(30, 0);
            elmZ.Left.Set(110, 0);
            Append(elmZ);
        }
        public UIDataElement(Matrix data)
        {
            this.data = data;

            float[,] matrixArr = {
                { data.M11, data.M12, data.M13, data.M14, },
                { data.M21, data.M22, data.M23, data.M24, },
                { data.M31, data.M32, data.M33, data.M34, },
                { data.M41, data.M42, data.M43, data.M44 }
            };

            for (int i = 0; i < matrixArr.GetLength(0); i++)
            {
                for (int j = 0; j < matrixArr.GetLength(1); j++)
                {
                    var width = 50;
                    var height = 30;

                    var elm = new UIDataElement(matrixArr[i, j]);
                    elm.Width.Set(width, 0);
                    elm.Height.Set(height, 0);
                    elm.Left.Set(width * i + 10, 0);
                    elm.Top.Set(height * j + 10, 0);
                    Append(elm);

                    Width.Pixels += i + 10;
                    Height.Pixels += j + 10;
                }
            }
        }
        public UIDataElement(Color data)
        {
            this.data = data;

            Width.Set(190, 0);
            Height.Set(100, 0);

            var colorImg = new UISolidColor(data);
            colorImg.Width.Set(20, 0);
            colorImg.Height.Set(20, 0);
            Append(colorImg);

            var elmR = new UIDataElement(data.R);
            elmR.Width.Set(50, 0);
            elmR.Height.Set(30, 0);
            elmR.Left.Set(30, 0);
            Append(elmR);

            var elmG = new UIDataElement(data.G);
            elmG.Width.Set(50, 0);
            elmG.Height.Set(30, 0);
            elmG.Left.Set(80, 0);
            Append(elmG);

            var elmB = new UIDataElement(data.B);
            elmB.Width.Set(50, 0);
            elmB.Height.Set(30, 0);
            elmB.Left.Set(130, 0);
            Append(elmB);

            var elmA = new UIDataElement(data.A);
            elmA.Width.Set(50, 0);
            elmA.Height.Set(30, 0);
            elmA.Left.Set(180, 0);
            Append(elmA);
        }
    }
}
