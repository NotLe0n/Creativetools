using Creativetools.src.Tools.EventToggle;
using Creativetools.src.Tools.InvasionToggleUI;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using static Creativetools.src.MultiplayerSystem;

namespace Creativetools.src;

public class Creativetools : Mod
{
	public override void HandlePacket(BinaryReader reader, int whoAmI)
	{
		byte msgType = reader.ReadByte();
		switch (msgType) {
			case EventSync:
				byte eventID = reader.ReadByte();

				if (Main.netMode == NetmodeID.Server) {
					SyncEvent(eventID);
				}
				else {
					switch (eventID) {
						case BloodMoonEvent: EventToggleUI.ToggleBloodMoon(); break;
						case FrostMoonEvent: EventToggleUI.ToggleFrostMoon(); break;
						case PumpkinMoonEvent: EventToggleUI.TogglePumpkinMoon(); break;
						case SolarEclipseEvent: EventToggleUI.ToggleSolarEclipse(); break;
						case HalloweenEvent: EventToggleUI.ToggleHalloween(); break;
						case ChristmasEvent: EventToggleUI.ToggleChristmas(); break;
						case PartyEvent: EventToggleUI.ToggleParty(); break;
						case SlimeRainEvent: EventToggleUI.ToggleSlimeRain(); break;
						case MeteorEvent: EventToggleUI.SpawnMeteor(); break;
						case LanternNightEvent: EventToggleUI.ToggleLanternNight(); break;
						case MeteorShowerEvent: EventToggleUI.ToggleStarFall(); break;
					}
				}
				break;
			case InvasionSync:
				short invasionID = reader.ReadInt16();

				if (Main.netMode == NetmodeID.Server) {
					SyncInvasion(invasionID);
				}
				else {
					InvasionToggleUI.ToggleInvasion(invasionID);
				}
				break;
			case HardmodeSync:
				bool state = reader.ReadBoolean();

				if (Main.netMode == NetmodeID.Server) {
					SyncHardMode(state);
				}
				else {
					Main.hardMode = state;
				}
				break;
			case GamemodeSync:
				byte gameModeID = reader.ReadByte();

				if (Main.netMode == NetmodeID.Server) {
					SyncGameMode(gameModeID);
				}
				else {
					Main.GameMode = gameModeID;
				}
				break;
			case MoonPhaseSync:
				int phase = reader.ReadInt32();

				if (Main.netMode == NetmodeID.Server) {
					SyncMoonPhase(phase);
				}
				else {
					Main.moonPhase = phase;
				}
				break;
			case TimeSync:
				double time = reader.ReadDouble();

				if (Main.netMode == NetmodeID.Server) {
					SyncTime(time);
				}
				else {
					Main.time = time;
				}
				break;
			default:
				Logger.WarnFormat("MyMod: Unknown Message type: {0}", msgType);
				break;
		}
	}
}