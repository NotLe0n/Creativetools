using Creativetools.src.UI;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;
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
        //private UserInterface CameraControlUserInterface;
        /////////////////////////////
        internal Info InfoUI;
        internal ButtonUI ButtonUI;
        internal Confirm_Panel ConfirmPanelUI;
        //internal CameraControlUI CameraControlUI;
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
                /*CameraControlUI = new CameraControlUI();
                CameraControlUI.Activate();*/
                ///////////////////////////////////////////////////
                UserInterface = new UserInterface();
                UserInterface.SetState(null);
                InfoUserInterface = new UserInterface();
                InfoUserInterface.SetState(InfoUI);
                ButtonUserInterface = new UserInterface();
                ButtonUserInterface.SetState(ButtonUI);
                ConfirmPanelUserInterface = new UserInterface();
                ConfirmPanelUserInterface.SetState(ConfirmPanelUI);
                /*CameraControlUserInterface = new UserInterface();
                CameraControlUserInterface.SetState(CameraControlUI);*/
                //////////////////////////////////////////////////
            }
        }
        public override void Unload()
        {
            InfoUI = null;
            ButtonUI = null;
            ConfirmPanelUI = null;
            //CameraControlUI = null;
            InfoUserInterface = null;
            ButtonUserInterface = null;
            ConfirmPanelUserInterface = null;
            //CameraControlUserInterface = null;
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
            /*if (CameraControlUI.Visible)
            {
                CameraControlUserInterface.Update(gameTime);
            }*/
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
                        /*if (CameraControlUI.Visible)
                        {
                            CameraControlUserInterface.Draw(Main.spriteBatch, _lastUpdateUiGameTime);
                        }*/
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
        #region cameracontrol_UNUSED
        /*public static float ZoomValue = 1f;
        public override void ModifyTransformMatrix(ref SpriteViewMatrix Transform)
        {
            if(!Main.gameMenu && CameraControlUI.Visible)
            {
                Transform.Zoom = new Vector2(0.5f);
            }
            if (ZoomValue != 1f && !Main.gameMenu)
            {
                Transform.Zoom = new Vector2(ZoomValue);
            }
        }*/
        #endregion
        public override void UpdateMusic(ref int music, ref MusicPriority priority)
        {
            if (PlaySoundUI.playmusic)
            {
                music = PlaySoundUI.MusicSound.Data;
                priority = MusicPriority.BiomeHigh;
            }
        }
        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            SyncMessageType msgType = (SyncMessageType)reader.ReadByte();
            switch (msgType)
            {
                case SyncMessageType.SyncPlayer:
                    byte playernumber = reader.ReadByte();
                    ModifyPlayer modPlayer = Main.player[playernumber].GetModPlayer<ModifyPlayer>();
                    modPlayer.creativeFly = reader.ReadBoolean();
                    break;
                case SyncMessageType.SyncCreativeFly:
                    break;
            }
        }
        internal enum SyncMessageType : byte
        {
            SyncPlayer,
            SyncCreativeFly
        }
    }
}