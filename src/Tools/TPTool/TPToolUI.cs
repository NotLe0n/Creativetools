using Creativetools.UI;
using Creativetools.UI.Elements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;

namespace Creativetools.Tools.TPTool;

internal class TPToolUI : UIState
{
	private bool relative;
	private Vector2 coordinates = Vector2.Zero;
	private readonly UIList _npcList;

	public TPToolUI()
	{
		var panel = new DragableUIPanel("TP Tool") { VAlign = 0.5f, HAlign = 0.1f };
		panel.Width.Set(450, 0);
		panel.Height.Set(300, 0);
		panel.OnCloseBtnClicked += () => UISystem.UserInterface.SetState(new MainUI());
		Append(panel);

		var tp2Coords = new UIText("TP to coordinates:");
		tp2Coords.Top.Set(40, 0);
		tp2Coords.Left.Set(20, 0);
		panel.Append(tp2Coords);

		var tp2CoordsX = new UIText("X:");
		tp2CoordsX.Top.Set(60, 0);
		tp2CoordsX.Left.Set(20, 0);
		panel.Append(tp2CoordsX);

		var tp2CoordsY = new UIText("Y:");
		tp2CoordsY.Top.Set(80, 0);
		tp2CoordsY.Left.Set(20, 0);
		panel.Append(tp2CoordsY);

		var tp2CoordsXInput = new NewUITextBox("0");
		tp2CoordsXInput.Width.Set(100, 0);
		tp2CoordsXInput.Height.Set(20, 0);
		tp2CoordsXInput.Top.Set(60, 0);
		tp2CoordsXInput.Left.Set(50, 0);
		tp2CoordsXInput.OnUnfocus += () => coordinates.X = float.TryParse(tp2CoordsXInput.Text, out float temp) ? temp : coordinates.X;
		tp2CoordsXInput.unfocusOnEnter = true;
		panel.Append(tp2CoordsXInput);

		var tp2CoordsYInput = new NewUITextBox("0");
		tp2CoordsYInput.Width.Set(100, 0);
		tp2CoordsYInput.Height.Set(20, 0);
		tp2CoordsYInput.Top.Set(80, 0);
		tp2CoordsYInput.Left.Set(50, 0);
		tp2CoordsYInput.OnUnfocus += () => coordinates.Y = float.TryParse(tp2CoordsYInput.Text, out float temp) ? temp : coordinates.Y;
		tp2CoordsYInput.unfocusOnEnter = true;
		panel.Append(tp2CoordsYInput);

		var tp2RelativeCoordsText = new UIText("relative");
		tp2RelativeCoordsText.Top.Set(120, 0);
		tp2RelativeCoordsText.Left.Set(20, 0);
		panel.Append(tp2RelativeCoordsText);

		var tp2RelativeCoords = new UIToggleImage(Main.Assets.Request<Texture2D>("Images\\UI\\Settings_Toggle"), 13, 13, new Point(17, 1), new Point(1, 1));
		tp2RelativeCoords.Top.Set(120, 0);
		tp2RelativeCoords.Left.Set(80, 0);
		tp2RelativeCoords.OnClick += (_, _) => relative = tp2RelativeCoords.IsOn;
		panel.Append(tp2RelativeCoords);

		var tpBtn = new UITextPanel<string>("Teleport");
		tpBtn.Width.Set(20, 0);
		tpBtn.Height.Set(10, 0);
		tpBtn.Top.Set(150, 0);
		tpBtn.Left.Set(20, 0);
		tpBtn.OnClick += Teleport;
		panel.Append(tpBtn);

		var tp2NPCText = new UIText("TP to NPC");
		tp2NPCText.Top.Set(40, 0);
		tp2NPCText.Left.Set(200, 0);
		panel.Append(tp2NPCText);

		var npcScrollbar = new UIScrollbar();
		npcScrollbar.Width.Set(20, 0);
		npcScrollbar.Height.Set(190, 0);
		npcScrollbar.Left.Set(420, 0);
		npcScrollbar.Top.Set(70, 0);
		panel.Append(npcScrollbar);

		_npcList = new UIList();
		_npcList.Width.Set(250, 0);
		_npcList.Height.Set(210, 0);
		_npcList.Left.Set(200, 0);
		_npcList.Top.Set(60, 0);
		_npcList.ListPadding = 8f;
		_npcList.SetScrollbar(npcScrollbar);
		panel.Append(_npcList);

		foreach (NPC npc in Main.npc.Where(x => x.active)) {
			var npcCard = new NPCTPCard(npc);
			npcCard.Width.Set(200, 0);
			npcCard.Height.Set(50, 0);
			_npcList.Add(npcCard);
		}
	}

	private void Teleport(UIMouseEvent evt, UIElement listeningElement)
	{
		if (relative) {
			SetPosition(Main.player[Main.myPlayer].position + coordinates);
		}
		else {
			SetPosition(coordinates);
		}

		Terraria.Audio.SoundEngine.PlaySound(SoundID.Item6);
	}

	public override void Update(GameTime gameTime)
	{
		if (Main.GameUpdateCount % 60 == 0) {
			_npcList.Clear();

			foreach (NPC npc in Main.npc.Where(x => x.active)) {
				var npcCard = new NPCTPCard(npc);
				npcCard.Width.Set(200, 0);
				npcCard.Height.Set(50, 0);
				_npcList.Add(npcCard);
			}
		}

		base.Update(gameTime);
	}

	public static void SetPosition(Vector2 v)
	{
		Main.player[Main.myPlayer].position = v;
		if (Main.netMode != NetmodeID.SinglePlayer) {
			MultiplayerSystem.SendPlayerPositionPacket(Main.myPlayer, v);
		}
	}
}
