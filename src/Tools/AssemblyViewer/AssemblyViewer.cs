using Creativetools.src.UI;
using Creativetools.src.UI.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria.UI;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Microsoft.Xna.Framework;
using Creativetools.src.Tools.AssemblyViewer.Elements;

namespace Creativetools.src.Tools.AssemblyViewer;

class AssemblyViewer : UIState
{
	private static float oldViewPosition;
	private UIScrollbar _scrollbar;
	private Namespace terrariaAssembly = new();

	public AssemblyViewer(string currentNamespace, TypeInfo selectedClass = null)
	{
		var menu = new DragableUIPanel("AssemblyViewer");
		menu.Width.Set(850, 0);
		menu.Height.Set(650, 0);
		menu.VAlign = 0.6f;
		menu.HAlign = 0.6f;
		menu.OnCloseBtnClicked += () => UISystem.UserInterface.SetState(new MainUI());
		Append(menu);

		CreateNamespaceStructure();
		terrariaAssembly = new Namespace()
		{
			Name = "Terraria",
			Children = { terrariaAssembly }
		};

		ConstructNamespaceInterface(currentNamespace, menu);

		if (selectedClass != null)
		{
			ConstructClassInterface(selectedClass, menu);
		}
	}

	private void ConstructNamespaceInterface(string currentNamespace, DragableUIPanel menu)
	{
		_scrollbar = new UIScrollbar();
		_scrollbar.Top.Set(0, 0.1f);
		_scrollbar.Left.Set(0, 0.45f);
		_scrollbar.Width.Set(20, 0);
		_scrollbar.Height.Set(0, 0.825f);
		menu.Append(_scrollbar);

		var horizontalScrollbar = new UIHorizontalScrollbar();
		horizontalScrollbar.Top.Set(0, 0.95f);
		horizontalScrollbar.Left.Set(20, 0);
		horizontalScrollbar.Width.Set(0, 0.41f);
		horizontalScrollbar.Height.Set(20, 0);
		menu.Append(horizontalScrollbar);

		var list = new BetterUIList();
		list.Top.Set(0, 0.1f);
		list.Left.Set(10, 0);
		list.Width.Set(0, 0.43f);
		list.Height.Set(0, 0.825f);
		list.ListPadding = 10;
		list.SetScrollbar(_scrollbar);
		list.SetHorizontalScrollbar(horizontalScrollbar);
		menu.Append(list);

		menu.Append(new Header(currentNamespace)
		{
			Top = new(0, 0.06f),
			Left = new(10, 0)
		});
		menu.Append(new UIHorizontalSeparator
		{
			Top = new(0, 0.09f),
			Left = new(10, 0),
			Width = new(0, 0.4f),
			Height = new(2, 0),
			Color = new Color(44, 57, 105) * 0.7f
		});

		var namespaceSearchbar = new NewUITextBox("search:", 0.8f)
		{
			Top = new(0, 0.06f),
			Left = new(currentNamespace.GetTextSize(FontSystem.ConsolasFont).X + 30, 0),
			Width = new(320 - currentNamespace.GetTextSize(FontSystem.ConsolasFont).X, 0),
			Height = new(20, 0),
			BackgroundColor = default, // remove background
			BorderColor = default // renove border
		};
		namespaceSearchbar.OnTextChanged += () =>
		{
				// searchbar logic
				string searchbarText = namespaceSearchbar.currentString;
			bool firstSearch(UIElement x) => ((x as NamespaceButton)?._namespace.Name.Equals(searchbarText, StringComparison.InvariantCultureIgnoreCase)).GetValueOrDefault() || ((x as ClassButton)?._class.Name.Equals(searchbarText, StringComparison.InvariantCultureIgnoreCase)).GetValueOrDefault();
			bool secondSearch(UIElement x) => ((x as NamespaceButton)?._namespace.Name.ToLower().Contains(searchbarText, StringComparison.InvariantCultureIgnoreCase)).GetValueOrDefault() || ((x as ClassButton)?._class.Name.ToLower().Contains(searchbarText, StringComparison.InvariantCultureIgnoreCase)).GetValueOrDefault();

			list._items = list._items.OrderByDescending(firstSearch).ThenByDescending(secondSearch).ToList();
		};
		menu.Append(namespaceSearchbar);

		// add namespaces and classes
		foreach (var child in Search(currentNamespace).Children)
		{
			var text = new NamespaceButton(child);
			list.Add(text);
		}

		foreach (var item in Search(currentNamespace).Items)
		{
			var text = new ClassButton(item);
			text.TextColor = Color.LightGray;
			list.Add(text);
		}
	}

	private void ConstructClassInterface(TypeInfo selectedClass, DragableUIPanel menu)
	{
		menu.Append(new UIFontText(FontSystem.ConsolasFont, selectedClass.Name)
		{
			Top = new(0, 0.06f),
			Left = new(0, 0.48f),
		});
		menu.Append(new UIHorizontalSeparator
		{
			Top = new(0, 0.09f),
			Left = new(0, 0.48f),
			Width = new(0, 0.4f),
			Height = new(2, 0),
			Color = new Color(44, 57, 105) * 0.7f
		});

		var memberScrollbar = new UIScrollbar();
		memberScrollbar.Top.Set(0, 0.1f);
		memberScrollbar.Left.Set(0, 0.96f);
		memberScrollbar.Width.Set(20, 0);
		memberScrollbar.Height.Set(0, 0.825f);
		menu.Append(memberScrollbar);

		var horizontalMemberScrollbar = new UIHorizontalScrollbar();
		horizontalMemberScrollbar.Top.Set(0, 0.95f);
		horizontalMemberScrollbar.Left.Set(0, 0.49f);
		horizontalMemberScrollbar.Width.Set(0, 0.45f);
		horizontalMemberScrollbar.Height.Set(20, 0);
		menu.Append(horizontalMemberScrollbar);

		var memberList = new BetterUIList();
		memberList.Top.Set(0, 0.1f);
		memberList.Left.Set(0, 0.48f);
		memberList.Width.Set(0, 0.48f);
		memberList.Height.Set(0, 0.825f);
		memberList.ListPadding = 10;
		memberList.SetScrollbar(memberScrollbar);
		memberList.SetHorizontalScrollbar(horizontalMemberScrollbar);
		menu.Append(memberList);

		var memberSearchbar = new NewUITextBox("search:", 0.8f)
		{
			Top = new(0, 0.06f),
			Left = new(selectedClass.Name.GetTextSize().X + 30, 0.48f),
			Width = new(320 - selectedClass.Name.GetTextSize().X, 0.2f),
			Height = new(20, 0),
			BackgroundColor = default, // remove background
			BorderColor = default // renove border
		};
		memberSearchbar.OnTextChanged += () =>
		{
				// searchbar logic
				string searchbarText = memberSearchbar.currentString;
			bool firstSearch(UIElement x) => ((x as FieldButton)?._field.Name.Equals(searchbarText, StringComparison.InvariantCultureIgnoreCase)).GetValueOrDefault() || ((x as MethodButton)?._method.Name.Equals(searchbarText, StringComparison.InvariantCultureIgnoreCase)).GetValueOrDefault() || ((x as PropertyButton)?._property.Name.Contains(searchbarText, StringComparison.InvariantCultureIgnoreCase)).GetValueOrDefault();
			bool secondSearch(UIElement x) => ((x as FieldButton)?._field.Name.Contains(searchbarText, StringComparison.InvariantCultureIgnoreCase)).GetValueOrDefault() || ((x as MethodButton)?._method.Name.Contains(searchbarText, StringComparison.InvariantCultureIgnoreCase)).GetValueOrDefault() || ((x as PropertyButton)?._property.Name.Contains(searchbarText, StringComparison.InvariantCultureIgnoreCase)).GetValueOrDefault();

			memberList._items = memberList._items.OrderByDescending(firstSearch).ThenByDescending(secondSearch).ToList();
		};
		menu.Append(memberSearchbar);

		// add members
		var fields = selectedClass.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
		foreach (var field in fields)
		{
			memberList.Add(new FieldButton(field));
		}

		var properties = selectedClass.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
		foreach (var property in properties)
		{
			memberList.Add(new PropertyButton(property));
		}

		var methods = selectedClass.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
		foreach (var method in methods)
		{
			memberList.Add(new MethodButton(method));
		}
	}

	public override void Recalculate()
	{
		base.Recalculate();
		if (_scrollbar != null)
		{
			_scrollbar.ViewPosition = oldViewPosition;
		}
	}
	public override void Update(GameTime gameTime)
	{
		base.Update(gameTime);
		if (_scrollbar != null)
		{
			oldViewPosition = _scrollbar.ViewPosition;
		}
	}

	/// <param name="query">a namespace like Terraria.GameContent.UI</param>
	/// <returns>the Namespace at <paramref name="query"/> or <c>null</c> if not found</returns>
	private Namespace Search(string query)
	{
		string[] splitted = query.Split('.');

		var result = terrariaAssembly;
		for (int i = 0; i < splitted.Length; i++)
		{
			result = result.Children.Find(x => x.Name == splitted[i]);
		}

		return result;
	}

	private void CreateNamespaceStructure()
	{
		Assembly assembly = typeof(Main).Assembly;
		TypeInfo[] types = assembly.DefinedTypes.Where(x => x.FullName.StartsWith("Terraria") && !x.FullName.Contains("ModLoader")).ToArray();

		terrariaAssembly = new()
		{
			Name = "Terraria"
		};

		for (int i = 0; i < types.Length; i++)
		{
			string directory = types[i].Namespace;
			CreateNamespaceStructure(directory, types[i]);
		}
	}

	private Namespace CreateNamespaceStructure(string dir, TypeInfo item, Namespace parent = null)
	{
		string[] splitted = dir.Split('.');
		string name = splitted[0];

		if (splitted.Length > 1)
		{
			string childDir = string.Join('.', splitted[1..splitted.Length]); // remove first index in array and join together

			// if a folder with the same name already exists
			if (parent != null && parent.Children.Exists(x => x.Name == name))
			{
				var current = parent.Children.Find(x => x.Name == name);
				var folder = CreateNamespaceStructure(childDir, item, current);

				if (folder != null)
					current.Children.Add(folder);
				return null;
			}

			// This only occurs with "Terraria.x"
			if (parent == null)
			{
				var folder = CreateNamespaceStructure(childDir, item, terrariaAssembly);

				if (folder != null)
					terrariaAssembly.Children.Add(folder);
				return null;
			}

			// Add new Folder
			var f = new Namespace()
			{
				Name = name,
				Parent = parent
			};
			f.Children = new() { CreateNamespaceStructure(childDir, item, f) };
			return f;
		}
		else
		{
			// if a folder with the same name already exists
			if (parent != null && parent.Children.Exists(x => x.Name == name))
			{
				// Add item to Subdirectory
				parent.Children.Find(x => x.Name == name).Items.Add(item);
				return null;
			}

			// This only occurs with "Terraria.x"
			if (parent == null)
			{
				// Add Item to "Terraria"
				terrariaAssembly.Items.Add(item);
				return null;
			}

			// Add new Folder
			return new Namespace()
			{
				Name = name,
				Parent = parent,
				Items = { item }
			};
		}
	}
}

class Namespace
{
	public string Name { get; set; }
	public string FullName => Parent != null ? $"{Parent.FullName}.{Name}" : Name;
	public List<Namespace> Children { get; set; } = new();
	public List<TypeInfo> Items { get; set; } = new();
	public Namespace Parent { get; set; }

	public override string ToString() => $"Name=\"{Name}\" FullName=\"{FullName}\"";
}
