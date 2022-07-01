using Creativetools.src.Tools.ClearInventory;
using Creativetools.src.Tools.GameInfo;
using Creativetools.src.UI;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace Creativetools.src;

internal class UISystem : ModSystem
{
	public static UserInterface UserInterface;
	public static UserInterface UserInterface2;
	private UserInterface InfoUserInterface;
	private UserInterface ButtonUserInterface;
	private UserInterface ConfirmPanelUserInterface;
	/////////////////////////////
	internal GameInfo InfoUI;
	internal ButtonUI ButtonUI;
	internal ConfirmPanel ConfirmPanelUI;

	public override void Load()
	{
		if (!Main.dedServ) {
			InfoUI = new GameInfo();
			InfoUI.Activate();
			ButtonUI = new ButtonUI();
			ButtonUI.Activate();
			ConfirmPanelUI = new ConfirmPanel();
			ConfirmPanelUI.Activate();
			///////////////////////////////////////////////////
			UserInterface = new UserInterface();
			UserInterface.SetState(null);
			InfoUserInterface = new UserInterface();
			InfoUserInterface.SetState(InfoUI);
			ButtonUserInterface = new UserInterface();
			ButtonUserInterface.SetState(ButtonUI);
			ConfirmPanelUserInterface = new UserInterface();
			ConfirmPanelUserInterface.SetState(ConfirmPanelUI);
			UserInterface2 = new UserInterface();
			UserInterface2.SetState(null);
			//////////////////////////////////////////////////
		}
	}

	public override void Unload()
	{
		InfoUI = null;
		ButtonUI = null;
		ConfirmPanelUI = null;
		InfoUserInterface = null;
		ButtonUserInterface = null;
		ConfirmPanelUserInterface = null;

		UserInterface = null;
		UserInterface2 = null;
	}

	private GameTime _lastUpdateUiGameTime;
	public override void UpdateUI(GameTime gameTime)
	{
		_lastUpdateUiGameTime = gameTime;

		if (Main.playerInventory) {
			if (Main.LocalPlayer.chest == -1 && Main.LocalPlayer.talkNPC == -1) {
				ButtonUserInterface.Update(gameTime);
			}

			if (UserInterface.CurrentState != null) {
				UserInterface.Update(gameTime);
				UserInterface2.Update(gameTime);

				if (ConfirmPanel.Visible) {
					ConfirmPanelUserInterface.Update(gameTime);
				}
			}
		}

		if (GameInfo.Visible) {
			InfoUserInterface.Update(gameTime);
		}
	}

	public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
	{
		int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
		if (mouseTextIndex != -1) {
			layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
				"CreativeTools: UI",
				delegate {
					if (Main.playerInventory) {
						if (Main.LocalPlayer.chest == -1 && Main.LocalPlayer.talkNPC == -1) {
							ButtonUserInterface.Draw(Main.spriteBatch, _lastUpdateUiGameTime);
						}

						if (UserInterface.CurrentState != null) {
							UserInterface.Draw(Main.spriteBatch, _lastUpdateUiGameTime);
							UserInterface2.Draw(Main.spriteBatch, _lastUpdateUiGameTime);

							if (ConfirmPanel.Visible) {
								ConfirmPanelUserInterface.Draw(Main.spriteBatch, _lastUpdateUiGameTime);
							}
						}
					}

					if (GameInfo.Visible) {
						InfoUserInterface.Draw(Main.spriteBatch, _lastUpdateUiGameTime);
					}
					return true;
				},
				   InterfaceScaleType.UI));
		}
		int rulerLayerIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Ruler"));
		if (rulerLayerIndex != -1) {
			layers.Insert(rulerLayerIndex, new LegacyGameInterfaceLayer(
				"Creative tools: Scale fix",
				delegate {
					if (GameInfo.Visible) {
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
		UserInterface.SetState(new MainUI());
		SoundEngine.PlaySound(SoundID.MenuClose);
	}
}
