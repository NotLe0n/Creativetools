using Creativetools.Tools.ClearInventory;
using Creativetools.Tools.GameInfo;
using Creativetools.UI;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace Creativetools;

internal class UISystem : ModSystem
{
	public static UserInterface userInterface;
	public static UserInterface userInterface2;
	private UserInterface infoUserInterface;
	private UserInterface buttonUserInterface;
	private UserInterface confirmPanelUserInterface;
	/////////////////////////////
	private GameInfo infoUI;
	private ButtonUI buttonUI;
	private ConfirmPanel confirmPanelUI;

	public override void Load()
	{
		if (Main.dedServ) {
			return;
		}

		infoUI = new GameInfo();
		infoUI.Activate();
		buttonUI = new ButtonUI();
		buttonUI.Activate();
		confirmPanelUI = new ConfirmPanel();
		confirmPanelUI.Activate();
		///////////////////////////////////////////////////
		userInterface = new UserInterface();
		userInterface.SetState(null);
		infoUserInterface = new UserInterface();
		infoUserInterface.SetState(infoUI);
		buttonUserInterface = new UserInterface();
		buttonUserInterface.SetState(buttonUI);
		confirmPanelUserInterface = new UserInterface();
		confirmPanelUserInterface.SetState(confirmPanelUI);
		userInterface2 = new UserInterface();
		userInterface2.SetState(null);
		//////////////////////////////////////////////////
	}

	public override void Unload()
	{
		infoUI = null;
		buttonUI = null;
		confirmPanelUI = null;
		infoUserInterface = null;
		buttonUserInterface = null;
		confirmPanelUserInterface = null;

		userInterface = null;
		userInterface2 = null;
	}

	private GameTime lastUpdateUiGameTime;
	public override void UpdateUI(GameTime gameTime)
	{
		lastUpdateUiGameTime = gameTime;

		if (Main.playerInventory) {
			if (Main.LocalPlayer.chest == -1 && Main.LocalPlayer.talkNPC == -1) {
				buttonUserInterface.Update(gameTime);
			}

			if (userInterface.CurrentState != null) {
				userInterface.Update(gameTime);
				userInterface2.Update(gameTime);

				if (ConfirmPanel.visible) {
					confirmPanelUserInterface.Update(gameTime);
				}
			}
		}

		if (GameInfo.visible) {
			infoUserInterface.Update(gameTime);
		}
	}

	private bool DrawUI()
	{
		if (Main.playerInventory) {
			if (Main.LocalPlayer.chest == -1 && Main.LocalPlayer.talkNPC == -1) {
				buttonUserInterface.Draw(Main.spriteBatch, lastUpdateUiGameTime);
			}

			if (userInterface.CurrentState != null) {
				userInterface.Draw(Main.spriteBatch, lastUpdateUiGameTime);
				userInterface2.Draw(Main.spriteBatch, lastUpdateUiGameTime);

				if (ConfirmPanel.visible) {
					confirmPanelUserInterface.Draw(Main.spriteBatch, lastUpdateUiGameTime);
				}
			}
		}

		if (GameInfo.visible) {
			infoUserInterface.Draw(Main.spriteBatch, lastUpdateUiGameTime);
		}
		return true;
	}
	
	public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
	{
		int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
		if (mouseTextIndex != -1) {
			layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
				"CreativeTools: UI", DrawUI, InterfaceScaleType.UI));
		}
		int rulerLayerIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Ruler"));
		if (rulerLayerIndex != -1) {
			layers.Insert(rulerLayerIndex, new LegacyGameInterfaceLayer(
				"Creative tools: Scale fix",
				delegate {
					if (GameInfo.visible) {
						GameInfo.WorldDraw();
					}
					return true;
				},
				InterfaceScaleType.Game)
			);
		}
	}

	public static void BackToMainMenu()
	{
		userInterface.SetState(new MainUI());
		SoundEngine.PlaySound(SoundID.MenuClose);
	}
}
