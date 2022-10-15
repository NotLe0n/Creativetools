using System.IO;
using Terraria.ModLoader;

namespace Creativetools;

public class Creativetools : Mod
{
	public override void HandlePacket(BinaryReader reader, int whoAmI)
	{
		MultiplayerSystem.HandlePacket(reader, whoAmI);
	}
}