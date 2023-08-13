using Microsoft.Xna.Framework;
using Terraria;

namespace Creativetools;

internal class NearestToMouse
{
	//get the Projectile closest to your Mouse
	public static int GetProjectileMouseClosest()
	{

		int projID = 0;
		for (int i = 0; i < Main.maxProjectiles; i++) {
			if (Main.projectile[i].active && (projID == 0 || Main.projectile[i].Hitbox.Distance(Main.MouseWorld) < Main.projectile[projID].Hitbox.Distance(Main.MouseWorld))) {
				projID = i;
			}
		}
		return projID;
	}
	//get the Item closest to your Mouse
	public static int GetItemMouseClosest()
	{
		int itemID = 0;
		for (int i = 0; i < Main.maxItems; i++) {
			if (Main.item[i].active && (itemID == 0 || Main.item[i].Hitbox.Distance(Main.MouseWorld) < Main.item[itemID].Hitbox.Distance(Main.MouseWorld)))
				itemID = i;
		}
		return itemID;
	}
	//get the npc closest to your mouse
	public static int GetNPCMouseClosest()
	{
		int npcID = 0;
		for (int i = 0; i < Main.maxNPCs; i++) {
			if (Main.npc[i].active && (npcID == 0 || Main.npc[i].Hitbox.Distance(Main.MouseWorld) < Main.npc[npcID].Hitbox.Distance(Main.MouseWorld))) {
				npcID = i;
			}
		}
		return npcID;
	}

	//get the npc closest to your mouse on the map
	public static int GetNPCMapMouseClosest()
	{
		Vector2 mouseMapPos = (Main.mapFullscreenPos + (Main.MouseScreen - new Vector2(Main.screenWidth, Main.screenHeight) / 2) / Main.mapFullscreenScale) * 16;
		int npcID = 0;
		for (int i = 0; i < Main.maxNPCs; i++) {
			if (Main.npc[i].active && (npcID == 0 || Main.npc[i].Hitbox.Distance(mouseMapPos) < Main.npc[npcID].Hitbox.Distance(mouseMapPos))) {
				npcID = i;
			}
		}
		return npcID;
	}
}
