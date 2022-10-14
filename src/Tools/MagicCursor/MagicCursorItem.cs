using Terraria;
using Terraria.ModLoader;
using static Creativetools.NearestToMouse;

namespace Creativetools.Tools.MagicCursor;

internal class ModifyItem : GlobalItem
{
	public override void Update(Item item, ref float gravity, ref float maxFallSpeed)
	{
		if (MagicCursorNPC.MagicCursor && Main.mouseMiddle && Main.item[GetItemMouseClosest()].Hitbox.Distance(Main.MouseWorld) < 500) {
			Main.item[GetItemMouseClosest()].position = Main.MouseWorld;

			MultiplayerSystem.SendItemPosPacket(GetItemMouseClosest(), Main.item[GetItemMouseClosest()].position);
		}
	}
}
