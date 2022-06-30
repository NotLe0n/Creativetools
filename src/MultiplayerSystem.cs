using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace Creativetools.src;

internal class MultiplayerSystem
{
	/* SyncIDs */
	public const byte EventSync = 0;
	public const byte InvasionSync = 1;
	public const byte HardmodeSync = 2;
	public const byte GamemodeSync = 3;
	public const byte MoonPhaseSync = 4;
	public const byte TimeSync = 5;

	/* EventIDs */
	public const byte BloodMoonEvent = 0;
	public const byte FrostMoonEvent = 1;
	public const byte PumpkinMoonEvent = 2;
	public const byte SolarEclipseEvent = 3;
	public const byte HalloweenEvent = 4;
	public const byte ChristmasEvent = 5;
	public const byte PartyEvent = 6;
	public const byte SlimeRainEvent = 7;
	public const byte MeteorEvent = 8;
	public const byte LanternNightEvent = 9;
	public const byte MeteorShowerEvent = 10;

	public static void SyncEvent(byte eventID)
	{
		if (Main.netMode == NetmodeID.SinglePlayer) {
			return;
		}

		ModPacket myPacket = ModContent.GetInstance<Creativetools>().GetPacket();
		myPacket.Write(EventSync);
		myPacket.Write(eventID);
		myPacket.Send();
	}

	public static void SyncInvasion(short invasionID)
	{
		if (Main.netMode == NetmodeID.SinglePlayer) {
			return;
		}

		ModPacket myPacket = ModContent.GetInstance<Creativetools>().GetPacket();
		myPacket.Write(InvasionSync);
		myPacket.Write(invasionID);
		myPacket.Send();
	}

	public static void SyncHardMode(bool state)
	{
		if (Main.netMode == NetmodeID.SinglePlayer) {
			return;
		}

		ModPacket myPacket = ModContent.GetInstance<Creativetools>().GetPacket();
		myPacket.Write(HardmodeSync);
		myPacket.Write(state);
		myPacket.Send();
	}

	public static void SyncGameMode(byte gameMode)
	{
		if (Main.netMode == NetmodeID.SinglePlayer) {
			return;
		}

		ModPacket myPacket = ModContent.GetInstance<Creativetools>().GetPacket();
		myPacket.Write(GamemodeSync);
		myPacket.Write(gameMode);
		myPacket.Send();
	}

	public static void SyncMoonPhase(int phase)
	{
		if (Main.netMode == NetmodeID.SinglePlayer) {
			return;
		}

		ModPacket myPacket = ModContent.GetInstance<Creativetools>().GetPacket();
		myPacket.Write(MoonPhaseSync);
		myPacket.Write(phase);
		myPacket.Send();
	}

	public static void SyncTime(double time)
	{
		if (Main.netMode == NetmodeID.SinglePlayer) {
			return;
		}

		ModPacket myPacket = ModContent.GetInstance<Creativetools>().GetPacket();
		myPacket.Write(TimeSync);
		myPacket.Write(time);
		myPacket.Send();
	}
}
