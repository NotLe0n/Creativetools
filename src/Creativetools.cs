using Creativetools.src.Tools.EventToggle;
using Creativetools.src.Tools.InvasionToggleUI;
using System.IO;
using System.Reflection;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using static Creativetools.src.MultiplayerSystem;

namespace Creativetools.src;

public class Creativetools : Mod
{
	public override void HandlePacket(BinaryReader reader, int whoAmI)
	{
		MultiplayerSystem.HandlePacket(reader, whoAmI);
	}
}