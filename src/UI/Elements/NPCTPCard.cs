using Creativetools.Tools.TPTool;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;

namespace Creativetools.UI.Elements;

internal class NPCTPCard : UIPanel
{
	private readonly NPC _npc;
	private readonly UIText _NPCPosText;
	public NPCTPCard(NPC npc)
	{
		_npc = npc;

		var NPCIDText = new UIText("ID: " + _npc.type, 0.8f);
		NPCIDText.Left.Set(40, 0);
		Append(NPCIDText);

		_NPCPosText = new UIText(_npc.position.ToPoint().ToString(), 0.8f);
		_NPCPosText.Left.Set(40, 0);
		_NPCPosText.Top.Set(15, 0);
		Append(_NPCPosText);
	}

	internal int frameCounter = 0;
	internal int frameTimer = 0;
	private const int frameDelay = 3;
	public override void Draw(SpriteBatch spriteBatch)
	{
		base.Draw(spriteBatch);

		if (++frameTimer > frameDelay) {
			frameCounter++;
			frameTimer = 0;
			if (frameCounter > Main.npcFrameCount[_npc.type] - 1) {
				frameCounter = 0;
			}
		}

		Texture2D npcTexture = TextureAssets.Npc[_npc.type].Value;
		Vector2 drawPosition = new(GetDimensions().X + 250 / _npc.width, GetDimensions().Y + 150 / _npc.height);
		Rectangle npcDrawRectangle = _npc.frame == default ? new(0, (npcTexture.Height / Main.npcFrameCount[_npc.type]) * frameCounter, npcTexture.Width, npcTexture.Height / Main.npcFrameCount[_npc.type]) : _npc.frame;
		Color drawColor = _npc.color == default ? Color.White : _npc.color;


		spriteBatch.Draw(npcTexture, drawPosition, npcDrawRectangle, drawColor, 0, Vector2.Zero, 0.85f, SpriteEffects.None, 0);
	}

	public override void Update(GameTime gameTime)
	{
		base.Update(gameTime);

		_NPCPosText.SetText(_npc.position.ToPoint().ToString());
	}

	public override void Click(UIMouseEvent evt)
	{
		SoundEngine.PlaySound(SoundID.Item6);

		TPToolUI.SetPosition(_npc.position);
		base.Click(evt);
	}
}
