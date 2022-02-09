using System.Reflection;
using Terraria;
using Creativetools.src.UI.Elements;
using Mono.Reflection;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using System.Collections.Generic;
using ICSharpCode.Decompiler;
using ICSharpCode.Decompiler.CSharp;
using System.Linq;
using ICSharpCode.Decompiler.TypeSystem;
using Terraria.ModLoader;
using ReLogic.Graphics;
using Creativetools.src.Tools.AssemblyViewer.Elements;
using System.Text.RegularExpressions;

namespace Creativetools.src.Tools.AssemblyViewer;

class ILWindow : UIState
{
	private readonly BetterUIList list;
	private readonly UITextPanel<string> changeViewBtn;
	private bool viewIL;

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
		horizontalScrollbar.Top.Set(0, 0.93f);
		horizontalScrollbar.Left.Set(10, 0f);
		horizontalScrollbar.Width.Set(10, 0.8f);
		panel.Append(horizontalScrollbar);

		list = new BetterUIList();
		list.Top.Set(0, 0.1f);
		list.Left.Set(10, 0);
		list.Width.Set(0, 0.8f);
		list.Height.Set(0, 0.8f);
		list.ListPadding = 5;
		list.SetScrollbar(scrollbar);
		list.SetHorizontalScrollbar(horizontalScrollbar);
		panel.Append(list);

		changeViewBtn = new UITextPanel<string>("IL", 0.8f);
		changeViewBtn.Top.Set(10, 0.9f);
		changeViewBtn.Left.Set(10, 0.85f);
		changeViewBtn.Width.Set(20, 0);
		changeViewBtn.Height.Set(20, 0);
		changeViewBtn.OnClick += (evt, elm) =>
		{
			list.Clear();

			if (viewIL)
			{
				UpdateList(method.DeclaringType.FullName, method.Name);
				changeViewBtn.SetText("IL", 0.8f, false);
			}
			else
			{
				UpdateList(method.GetInstructions());
				changeViewBtn.SetText("c#", 0.8f, false);
			}

			viewIL = !viewIL;
		};
		panel.Append(changeViewBtn);

		UpdateList(method.DeclaringType.FullName, method.Name);
	}

	private void UpdateList(IList<Instruction> instructions)
	{
		for (int i = 0; i < instructions.Count; i++)
		{
			string str = $"[c/ACACAC:IL_{i:X4}]: {instructions[i].OpCode}    {instructions[i].Operand}";
			list.Add(new UIFontText(FontSystem.ConsolasFont, str, 0.8f));
		}
	}

	private void UpdateList(string declaringType, string methodName)
	{
		string loc = typeof(Main).Assembly.Location;
		// generate Decompiler
		var decompiler = new CSharpDecompiler(loc, new DecompilerSettings() { ThrowOnAssemblyResolveErrors = false });

		// get class data
		ITypeDefinition typeInfo = decompiler.TypeSystem.MainModule.Compilation.FindType(new FullTypeName(declaringType)).GetDefinition();

		// get method data
		var token = typeInfo.Methods.First(x => x.Name == methodName).MetadataToken;

		// get c# code
		string code = decompiler.DecompileAsString(token);

		// reformat string
		code = code.Replace("\t", "    ");
		code = code.Highlight();

		// Add string to list
		var str = code.Split('\n');
		foreach (var item in str)
		{
			list.Add(new UIFontText(FontSystem.ConsolasFont, item, 0.8f));
		}
	}
}

static class SyntaxHighlighting
{
	public static string Highlight(this string code)
	{
		return code.
			Highlight(
			(@"(\bMain|ItemID|Item|NPC|Rain|Star|Recipe|Sign|Tile|Player|Utils|Lang|Language|Gore|Framing|Lighting|Dust|Chest|Collision|Liquid|Mount\b)\.", "4ec9b0"),
			(@"(\bVector2|Vector3|Rectangle\b)", "53ac91"),
			(@"(\bforeach|return|if|else|for|switch|continue|break|case| in \b)", "d8a0df"),
			(@"(\bpublic|private|internal|protected|virtual|override|async|readonly|static|this|void|int|string|char|float|short|double|new|object|true|false|bool|using|null|class\b)", "3987d6"),
			(@" (-?[0-9]+(?:\.[0-9]*)?f?)", "9dcea8"),
			(@"(\"".*\"")", "d69d85"),
			(@"(\/\/.+|\/\*[\s\S]*?\*\/)", "307a4a"));
	}

	private static string Highlight(this string str, params (string regex, string hex)[] objs)
	{
		foreach ((string regex, string hex) obj in objs)
		{
			var matches = Regex.Matches(str, obj.regex);
			int offset = 0;
			foreach (Match match in matches)
			{
				str = str.Remove(match.Groups[1].Index + offset, match.Groups[1].Length);
				str = str.Insert(match.Groups[1].Index + offset, $"[c/{obj.hex}:{match.Groups[1].Value}]");

				offset += $"[c/{obj.hex}:]".Length;
			}
		}

		return str;
	}
}
