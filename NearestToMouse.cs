using Microsoft.Xna.Framework;
using Terraria;

namespace Creativetools
{
    class NearestToMouse
    {
        //get the Projectile closest to your Mouse
        public static int GetProjectileMouseClosest()
        {

            int ProjID = 0;
            for (int i = 0; i < 400; i++)
            {
                if (Main.projectile[i].active && (ProjID == 0 || Main.projectile[i].Hitbox.Distance(Main.MouseWorld) < Main.projectile[ProjID].Hitbox.Distance(Main.MouseWorld)))
                {
                    ProjID = i;
                }
            }
            return ProjID;
        }
        //get the Item closest to your Mouse
        public static int GetItemMouseClosest()
        {
            int ItemID = 0;
            for (int i = 0; i < 400; i++)
            {
                if (Main.item[i].active && (ItemID == 0 || Main.item[i].Hitbox.Distance(Main.MouseWorld) < Main.item[ItemID].Hitbox.Distance(Main.MouseWorld)))
                    ItemID = i;
            }
            return ItemID;
        }
        //get the npc closest to your mouse
        public static int GetNPCMouseClosest()
        {
            int NPCID = 0;
            for (int i = 0; i < 200; i++)
            {
                if (Main.npc[i].active && (NPCID == 0 || Main.npc[i].Hitbox.Distance(Main.MouseWorld) < Main.npc[NPCID].Hitbox.Distance(Main.MouseWorld)))
                {
                    NPCID = i;
                }
            }
            return NPCID;
        }

        //get the npc closest to your mouse on the map
        public static int GetNPCMapMouseClosest()
        {
            Vector2 MouseMapPos = (Main.mapFullscreenPos + (Main.MouseScreen - new Vector2(Main.screenWidth, Main.screenHeight) / 2) / Main.mapFullscreenScale) * 16;
            int NPCID = 0;
            for (int i = 0; i < 200; i++)
            {
                if (Main.npc[i].active && (NPCID == 0 || Main.npc[i].Hitbox.Distance(MouseMapPos) < Main.npc[NPCID].Hitbox.Distance(MouseMapPos)))
                {
                    NPCID = i;
                }
            }
            return NPCID;
        }
    }
}
