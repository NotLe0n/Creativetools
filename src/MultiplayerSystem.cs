using Creativetools.Tools.DownedBossToggle;
using Creativetools.Tools.EventToggle;
using Creativetools.Tools.InvasionToggleUI;
using Creativetools.UI;
using Microsoft.Xna.Framework;
using System;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Creativetools;

internal class MultiplayerSystem
{
	/* SyncIDs */
	private const byte EventSync = 0;
	private const byte InvasionSync = 1;
	private const byte HardmodeSync = 2;
	private const byte GamemodeSync = 3;
	private const byte MoonPhaseSync = 4;
	private const byte TimeSync = 5;
	private const byte DownedBossSync = 6;
	private const byte PlayerPositionSync = 7;
	private const byte KillPlayerSync = 8;
	private const byte NPCSync = 9;
	private const byte ItemPosSync = 10;
	private const byte PlayerHealthSync = 11;
	private const byte PlayerManaSync = 12;
	
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

	private static ModPacket CreateLabeledPacket(byte packetID)
	{
		if (Main.netMode == NetmodeID.SinglePlayer) {
			return null;
		}

		ModPacket myPacket = ModContent.GetInstance<Creativetools>().GetPacket();
		myPacket.Write(packetID);

		return myPacket;
	}

	public static void SendEventPacket(byte eventID)
	{
		ModPacket myPacket = CreateLabeledPacket(EventSync);
		myPacket.Write(eventID);
		myPacket.Send();
	}

	public static void SendInvasionPacket(short invasionID)
	{
		ModPacket myPacket = CreateLabeledPacket(InvasionSync);
		myPacket.Write(invasionID);
		myPacket.Send();
	}

	public static void SendHardmodePacket(bool state)
	{
		ModPacket myPacket = CreateLabeledPacket(HardmodeSync);
		myPacket.Write(state);
		myPacket.Send();
	}

	public static void SendGamemodePacket(byte gameMode)
	{
		ModPacket myPacket = CreateLabeledPacket(GamemodeSync);
		myPacket.Write(gameMode);
		myPacket.Send();
	}

	public static void SendMoonPhasePacket(int phase)
	{
		ModPacket myPacket = CreateLabeledPacket(MoonPhaseSync);
		myPacket.Write(phase);
		myPacket.Send();
	}

	public static void SendTimePacket(bool dayTime, double time)
	{
		ModPacket myPacket = CreateLabeledPacket(TimeSync);
		myPacket.Write(dayTime);
		myPacket.Write(time);
		myPacket.Send();
	}

	public static void SendDownedBossPacket(string fieldName, bool state, bool all = false)
	{
		ModPacket myPacket = CreateLabeledPacket(DownedBossSync);
		myPacket.Write(fieldName);
		myPacket.Write(state);
		myPacket.Write(all);
		myPacket.Send();
	}

	public static void SendPlayerPositionPacket(int player, Vector2 vec)
	{
		ModPacket myPacket = CreateLabeledPacket(PlayerPositionSync);
		myPacket.Write(player);
		myPacket.WriteVector2(vec);
		myPacket.Send();
	}

	public static void SendKillPlayerPacket()
	{
		ModPacket myPacket = CreateLabeledPacket(KillPlayerSync);
		myPacket.Send();
	}

	public static void SendNPCPacket(int id, Vector2 position, Vector2 velocity)
	{
		ModPacket myPacket = CreateLabeledPacket(NPCSync);
		myPacket.Write(id);
		myPacket.WriteVector2(position);
		myPacket.WriteVector2(velocity);
		myPacket.Send();
	}

	public static void SendItemPosPacket(int id, Vector2 position)
	{
		ModPacket myPacket = CreateLabeledPacket(ItemPosSync);
		myPacket.Write(id);
		myPacket.WriteVector2(position);
		myPacket.Send();
	}

	public static void SendPlayerHealthPacket(int id, int curr, int max)
	{
		ModPacket myPacket = CreateLabeledPacket(PlayerHealthSync);
		myPacket.Write(id);
		myPacket.Write(curr);
		myPacket.Write(max);
		myPacket.Send();
	}
	
	public static void SendPlayerManaPacket(int id, int curr, int max)
	{
		ModPacket myPacket = CreateLabeledPacket(PlayerManaSync);
		myPacket.Write(id);
		myPacket.Write(curr);
		myPacket.Write(max);
		myPacket.Send();
	}
	
	public static void HandlePacket(BinaryReader reader, int whoAmI)
	{
		byte msgType = reader.ReadByte();
		Console.WriteLine("Handling: " + msgType);
		switch (msgType) {
			case EventSync:
				byte eventID = reader.ReadByte();

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

				if (Main.netMode == NetmodeID.Server) {
					NetMessage.SendData(MessageID.WorldData);
				}
				break;
			case InvasionSync:
				short invasionID = reader.ReadInt16();

				InvasionToggleUI.ToggleInvasion(invasionID);
				if (Main.netMode == NetmodeID.Server) {
					NetMessage.SendData(MessageID.WorldData);
				}
				break;
			case HardmodeSync:
				bool state = reader.ReadBoolean();

				Main.hardMode = state;
				if (Main.netMode == NetmodeID.Server) {
					NetMessage.SendData(MessageID.WorldData);
				}
				break;
			case GamemodeSync:
				byte gameModeID = reader.ReadByte();

				Main.GameMode = gameModeID;
				if (Main.netMode == NetmodeID.Server) {
					NetMessage.SendData(MessageID.WorldData);
				}
				break;
			case MoonPhaseSync:
				int phase = reader.ReadInt32();

				Main.moonPhase = phase;
				if (Main.netMode == NetmodeID.Server) {
					NetMessage.SendData(MessageID.WorldData);
				}
				break;
			case TimeSync:
				bool dayTime = reader.ReadBoolean();
				double time = reader.ReadDouble();

				Main.dayTime = dayTime;
				Main.time = time;

				if (Main.netMode == NetmodeID.Server) {
					NetMessage.SendData(MessageID.WorldData);
				}
				break;
			case DownedBossSync:
				string fieldName = reader.ReadString();
				bool state2 = reader.ReadBoolean();
				bool all = reader.ReadBoolean();

				if (all) {
					DownedBossToggleUI.SetAll(state2);
				}
				else {
					typeof(NPC).GetField(fieldName)?.SetValue(typeof(NPC), state2);
				}
				
				if (Main.netMode == NetmodeID.Server) {
					NetMessage.SendData(MessageID.WorldData);
				}		
				break;
			case PlayerPositionSync:
				int player = reader.ReadInt32();
				Vector2 pos = reader.ReadVector2();

				Main.player[player].position = pos;
				if (Main.netMode == NetmodeID.Server) {
					NetMessage.SendData(MessageID.PlayerControls);
				}
				break;
			case KillPlayerSync:
				MainUI.KillPlayer(Main.player[whoAmI]);
				if (Main.netMode == NetmodeID.Server) {
					NetMessage.SendPlayerDeath(whoAmI, PlayerDeathReason.LegacyEmpty(), int.MaxValue, 0, false);
				}
				break;
			case NPCSync:
				int NPCID = reader.ReadInt32();
				Main.npc[NPCID].position = reader.ReadVector2();
				Main.npc[NPCID].velocity = reader.ReadVector2();
				Main.npc[NPCID].netUpdate = true;
				
				if (Main.netMode == NetmodeID.Server) {
					NetMessage.SendData(MessageID.SyncNPC);
				}
				break;
			case ItemPosSync:
				int ItemID = reader.ReadInt32();
				Main.item[ItemID].position = reader.ReadVector2();
				
				if (Main.netMode == NetmodeID.Server) {
					NetMessage.SendData(MessageID.SyncItem);
				}
				break;
			case PlayerHealthSync:
				int player2 = reader.ReadInt32();
				Main.player[player2].statLife = reader.ReadInt32();
				Main.player[player2].statLifeMax = reader.ReadInt32();
				
				if (Main.netMode == NetmodeID.Server) {
					NetMessage.SendData(MessageID.PlayerLifeMana);
				}
				break;
			case PlayerManaSync:
				int player3 = reader.ReadInt32();
				Main.player[player3].statMana = reader.ReadInt32();
				Main.player[player3].statManaMax = reader.ReadInt32();
				
				if (Main.netMode == NetmodeID.Server) {
					NetMessage.SendData(MessageID.PlayerMana);
				}
				break;
			default:
				ModContent.GetInstance<Creativetools>().Logger.WarnFormat("MyMod: Unknown Message type: {0}", msgType);
				break;
		}
	}
}
