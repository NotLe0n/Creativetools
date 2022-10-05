using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Creativetools.src.NearestToMouse;

namespace Creativetools.src.Tools.MagicCursor;

internal class MagicCursorNPC : GlobalNPC
{
	public static bool MagicCursor;

	public override void AI(NPC npc)
	{
		if (MagicCursor) {
			if (Main.mapFullscreen && Main.mouseMiddle) {
				int closestMapNPCID = GetNPCMapMouseClosest();
				Main.npc[closestMapNPCID].position = (Main.mapFullscreenPos + (Main.MouseScreen - new Vector2(Main.screenWidth, Main.screenHeight) / 2) / Main.mapFullscreenScale) * 16;
				Main.npc[closestMapNPCID].velocity = (Main.MouseScreen - new Vector2(Main.lastMouseX, Main.lastMouseY)) / 4;
				
				MultiplayerSystem.SendNPCPacket(closestMapNPCID, Main.npc[closestMapNPCID].position, Main.npc[closestMapNPCID].velocity);
			}
			else if (Main.mouseMiddle && Main.npc[GetNPCMouseClosest()].Hitbox.Distance(Main.MouseWorld) < 500) {
				int closestNPCID = GetNPCMouseClosest();
				Main.npc[closestNPCID].noTileCollide = true; // npc doesn't have collision
				Main.npc[closestNPCID].position = Main.MouseWorld; // npc gets moved to mouse
				Main.npc[closestNPCID].velocity = (Main.MouseScreen - new Vector2(Main.lastMouseX, Main.lastMouseY)) / 8;
				
				MultiplayerSystem.SendNPCPacket(closestNPCID, Main.npc[closestNPCID].position, Main.npc[closestNPCID].velocity);
			}
			Main.npc[GetNPCMouseClosest()].noTileCollide = false;
		}
		//if (Main.npc[NPC.FindFirstNPC(target)].Hitbox.Contains((int)Main.MouseWorld.X, (int)Main.MouseWorld.Y))
	}
}
