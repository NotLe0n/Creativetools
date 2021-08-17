using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Creativetools.src.UI.Elements;
using Mono.Reflection;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace Creativetools.src.Tools.GameInfo2
{
    class ILWindow : UIState
    {
        public ILWindow(MethodBase method)
        {
            var instructions = Disassembler.GetInstructions(method);

            var panel = new DragableUIPanel(method.Name, 400, 500);
            panel.Top.Set(200, 0);
            panel.Left.Set(500, 0);
            panel.OnCloseBtnClicked += () => UISystem.UserInterface2.SetState(null);
            Append(panel);

            var scrollbar = new UIScrollbar();
            scrollbar.Top.Set(0, 0.1f);
            scrollbar.Left.Set(0, 0.95f);
            scrollbar.Width.Set(20, 0);
            scrollbar.Height.Set(0, 0.8f);
            panel.Append(scrollbar);

            var list = new BetterUIList();
            list.Top.Set(0, 0.1f);
            list.Left.Set(10, 0);
            list.Width.Set(0, 0.95f);
            list.Height.Set(0, 0.85f);
            list.ListPadding = 5;
            list.SetScrollbar(scrollbar);
            panel.Append(list);

            for (int i = 0; i < instructions.Count; i++)
            {
                string str = $"[c/ACACAC:IL_{i:X4}]: {instructions[i].OpCode}    {instructions[i].Operand}";
                list.Add(new UIText(str, 0.8f));
            }
        }
    }
}
