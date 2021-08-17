using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria;
using Creativetools.src.UI.Elements;
using Mono.Reflection;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace Creativetools.src.Tools.AssemblyViewer
{
    class ILWindow : UIState
    {
        public ILWindow(MethodBase method)
        {
            var instructions = Disassembler.GetInstructions(method);

            var panel = new DragableUIPanel(method.Name);
            panel.Top.Set(200, 0);
            panel.Left.Set(500, 0);
            panel.Width.Set(400, 0);
            panel.Height.Set(550, 0);
            panel.OnCloseBtnClicked += () => UISystem.UserInterface2.SetState(null);
            Append(panel);

            if (method.GetParameters().Length == 0)
            {
                var invokeButton = new UITextPanel<string>("invoke", 0.8f);
                invokeButton.SetPadding(5);
                invokeButton.Top.Set(31, 0);
                invokeButton.Left.Set(10, 0);
                invokeButton.Width.Set(100, 0);
                invokeButton.Height.Set(10, 0);
                invokeButton.OnClick += (evt, elm) =>
                {
                    object ret = method.Invoke(null, null);
                    Main.NewText($"<AssemblyViewer> Invoked method [c/FF0000:{method.Name}()] successfully!");

                    if (ret is not null)
                        Main.NewText($"<AssemblyViewer> [c/FF0000:{method.Name}()] returned {ret}");
                };
                panel.Append(invokeButton);
            }

            var scrollbar = new UIScrollbar();
            scrollbar.Top.Set(0, 0.1f);
            scrollbar.Left.Set(0, 0.90f);
            scrollbar.Width.Set(20, 0);
            scrollbar.Height.Set(0, 0.8f);
            panel.Append(scrollbar);

            var horizontalScrollbar = new UIHorizontalScrollbar();
            horizontalScrollbar.Top.Set(0, 0.925f);
            horizontalScrollbar.Left.Set(10, 0f);
            horizontalScrollbar.Width.Set(0, 0.8f);
            panel.Append(horizontalScrollbar);

            var list = new BetterUIList();
            list.Top.Set(0, 0.1f);
            list.Left.Set(10, 0);
            list.Width.Set(0, 0.8f);
            list.Height.Set(0, 0.8f);
            list.ListPadding = 5;
            list.SetScrollbar(scrollbar);
            list.SetHorizontalScrollbar(horizontalScrollbar);
            panel.Append(list);

            for (int i = 0; i < instructions.Count; i++)
            {
                string str = $"[c/ACACAC:IL_{i:X4}]: {instructions[i].OpCode}    {instructions[i].Operand}";
                list.Add(new UIText(str, 0.8f));
            }
        }
    }
}
