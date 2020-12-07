using Creativetools.src.UI;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Creativetools.src.NearestToMouse;

namespace Creativetools.src
{
    class ModifyNPC : GlobalNPC
    {
        public override void AI(NPC npc)
        {
            if (MainUI.MagicCursor)
            {
                if (Main.mapFullscreen && Main.mouseMiddle)
                {
                    Main.npc[GetNPCMapMouseClosest()].position = (Main.mapFullscreenPos + (Main.MouseScreen - new Vector2(Main.screenWidth, Main.screenHeight) / 2) / Main.mapFullscreenScale) * 16;
                    Main.npc[GetNPCMapMouseClosest()].velocity = (Main.MouseScreen - new Vector2(Main.lastMouseX, Main.lastMouseY)) / 4;
                    npc.netUpdate = true;
                }
                else if (Main.mouseMiddle && Main.npc[GetNPCMouseClosest()].Hitbox.Distance(Main.MouseWorld) < 500)
                {
                    Main.npc[GetNPCMouseClosest()].noTileCollide = true; //npc doesn't have collision
                    Main.npc[GetNPCMouseClosest()].position = Main.MouseWorld;   //npc gets moved to mouse
                    Main.npc[GetNPCMouseClosest()].velocity = (Main.MouseScreen - new Vector2(Main.lastMouseX, Main.lastMouseY)) / 8;
                    npc.netUpdate = true;
                }
                Main.npc[GetNPCMouseClosest()].noTileCollide = false;
                if (Main.netMode == NetmodeID.MultiplayerClient)
                {
                    NetMessage.SendData(MessageID.SyncNPC);
                }
            }
            //if (Main.npc[NPC.FindFirstNPC(target)].Hitbox.Contains((int)Main.MouseWorld.X, (int)Main.MouseWorld.Y))
        }
    }
}