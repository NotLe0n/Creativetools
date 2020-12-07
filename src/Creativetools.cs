using Creativetools.src.UI;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace Creativetools.src
{
    public class Creativetools : Mod
    {
        internal UserInterface UserInterface;
        private UserInterface InfoUserInterface;
        private UserInterface ButtonUserInterface;
        private UserInterface ConfirmPanelUserInterface;
        /////////////////////////////
        internal Info InfoUI;
        internal ButtonUI ButtonUI;
        internal Confirm_Panel ConfirmPanelUI;
        public override void Load()
        {
            if (!Main.dedServ)
            {
                InfoUI = new Info();
                InfoUI.Activate();
                ButtonUI = new ButtonUI();
                ButtonUI.Activate();
                ConfirmPanelUI = new Confirm_Panel();
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
        }
        private GameTime _lastUpdateUiGameTime;

        public override void UpdateUI(GameTime gameTime)
        {
            _lastUpdateUiGameTime = gameTime;
            if (Main.playerInventory)
            {
                ButtonUserInterface.Update(gameTime);
                UserInterface.Update(gameTime);
                if (UserInterface.CurrentState != null)
                {
                    if (Confirm_Panel.Visible)
                    {
                        ConfirmPanelUserInterface.Update(gameTime);
                    }
                }
            }
            if (Info.Visible)
            {
                InfoUserInterface.Update(gameTime);
            }
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "CreativeTools: UI",
                    delegate
                    {
                        if (Main.playerInventory)
                        {
                            ButtonUserInterface.Draw(Main.spriteBatch, _lastUpdateUiGameTime);
                            if (UserInterface.CurrentState != null)
                            {
                                UserInterface.Draw(Main.spriteBatch, _lastUpdateUiGameTime);
                                if (Confirm_Panel.Visible)
                                {
                                    ConfirmPanelUserInterface.Draw(Main.spriteBatch, _lastUpdateUiGameTime);
                                }
                            }
                        }
                        if (Info.Visible)
                        {
                            InfoUserInterface.Draw(Main.spriteBatch, _lastUpdateUiGameTime);
                        }
                        return true;
                    },
                       InterfaceScaleType.UI));
            }
            int rulerLayerIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Ruler"));
            if (rulerLayerIndex != -1)
            {
                layers.Insert(rulerLayerIndex, new LegacyGameInterfaceLayer(
                    "ModdersToolkit: Tools Game Scale",
                    delegate
                    {
                        if (Info.Visible)
                        {
                            Info.WorldDraw();
                        }
                        return true;
                    },
                    InterfaceScaleType.Game)
                );
            }
        }
        public override void UpdateMusic(ref int music, ref MusicPriority priority)
        {
            if (PlaySoundUI.playmusic)
            {
                music = PlaySoundUI.MusicSound.Data;
                priority = MusicPriority.BiomeHigh;
            }
        }
    }
}