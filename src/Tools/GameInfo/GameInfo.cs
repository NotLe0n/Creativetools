using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using static Creativetools.src.NearestToMouse;

namespace Creativetools.src.Tools.GameInfo
{
    class GameInfo : UIState
    {
        #region vars
        public static bool Visible;
        public UIText Mouseinfo = new("") { TextColor = Color.AntiqueWhite, MarginTop = 20 };
        public UIText Playerinfo = new("") { TextColor = Color.Firebrick, MarginTop = 50 };
        public UIText Screeninfo = new("") { TextColor = Color.Goldenrod };
        public UIText NPCinfo = new("") { TextColor = Color.Salmon };
        public UIText Iteminfo = new("") { TextColor = Color.DodgerBlue };
        public UIText Projectileinfo = new("") { TextColor = Color.HotPink };
        public UIText Gameinfo = new("") { TextColor = Color.DeepSkyBlue, MarginTop = Main.screenHeight - 20 };
        #endregion

        public override void OnInitialize()
        {
            if (!Config.Instance.HideMouse) Append(Mouseinfo);
            if (!Config.Instance.HidePlayer) Append(Playerinfo);
            if (!Config.Instance.HideScreen) Append(Screeninfo);
            if (!Config.Instance.HideNPC) Append(NPCinfo);
            if (!Config.Instance.HideItem) Append(Iteminfo);
            if (!Config.Instance.HideProjectile) Append(Projectileinfo);
            if (!Config.Instance.HideGame) Append(Gameinfo);
        }
        public override void Update(GameTime gameTime)
        {
            NPC selectedNPC = Main.npc[GetNPCMouseClosest()];
            Item selectedItem = Main.item[GetItemMouseClosest()];
            Projectile selectedProjectile = Main.projectile[GetProjectileMouseClosest()];

            //mouse information
            Mouseinfo.Left.Set(Main.mouseX, 0f);
            Mouseinfo.Top.Set(Main.mouseY, 0f);
            Mouseinfo.SetText(!Config.Instance.HideMouse ?
                $"Mouse position relative to world: {Main.MouseWorld}"
                + $"\nMouse position relative to screen: {Main.MouseScreen}"
                + "\nLast Mouse position relative to screen: {" + $"{Main.lastMouseX}, {Main.lastMouseY}" + "}"
                + $"\nMouse Color: {Main.mouseColor}"
                + $"\nBorder Color: {Main.MouseBorderColor}" : "");

            //player information
            Player player = Main.LocalPlayer;
            Playerinfo.Left.Set(player.position.X - Main.screenPosition.X, 0f);
            Playerinfo.Top.Set(player.position.Y - Main.screenPosition.Y, 0f);
            Playerinfo.SetText(!Config.Instance.HidePlayer ?
                $"Player name: {player.name}, whoAmI: {player.whoAmI}"
                + $"\nPosition: {player.position}, Velocity: {player.velocity}"
                + $"\nliferegen: {player.lifeRegen}, liferegen time: {player.lifeRegenTime}, manaregen: {player.manaRegen}, minion number: {player.numMinions}"
                + $"\nrocket time: {player.rocketTime}, respawn time: {player.respawnTimer}, imunity timer: {player.immuneTime}, flight timer: {player.wingTime}" : "");
            //screen information
            Screeninfo.SetText(!Config.Instance.HideScreen ? $"Screen position: {Main.screenPosition}, Screen Zoom:  {Main.GameZoomTarget}" : "");

            //NPC information
            NPCinfo.Left.Set(selectedNPC.position.X - Main.screenPosition.X, 0f);
            NPCinfo.Top.Set(Main.ReverseGravitySupport(selectedNPC.Bottom - Main.screenPosition).Y, 0f);
            NPCinfo.SetText(!Config.Instance.HideNPC ?
                $"NPC name/type/aiStyle: {selectedNPC.TypeName}/{selectedNPC.type}/{selectedNPC.aiStyle}"
                + $"\nPosition: {selectedNPC.position}, Velocity: {selectedNPC.velocity}"
                + $"\nDistance to Mouse: {selectedNPC.Hitbox.Distance(Main.MouseWorld)}" : "");

            //item information
            Iteminfo.Left.Set(selectedItem.position.X - Main.screenPosition.X, 0f);
            Iteminfo.Top.Set(Main.ReverseGravitySupport(selectedItem.Bottom - Main.screenPosition).Y, 0f);
            Iteminfo.SetText(!Config.Instance.HideItem ?
                $"Item name/type: {selectedItem.Name} / {selectedItem.type}"
                + $"\nPosition: {selectedItem.position}"
                + $"\nDistance to Mouse: {selectedItem.Hitbox.Distance(Main.MouseWorld)}" : "");

            //Projectile information
            Projectileinfo.Left.Set(selectedProjectile.position.X - Main.screenPosition.X, 0f);
            Projectileinfo.Top.Set(Main.ReverseGravitySupport(selectedProjectile.Bottom - Main.screenPosition).Y, 0f);
            Projectileinfo.SetText(!Config.Instance.HideProjectile ?
                $"Projectile name/type: {selectedProjectile.Name} / {selectedProjectile.type}"
                + $"\nPosition: {selectedProjectile.position}, Velocity: {selectedProjectile.velocity} ({selectedProjectile.direction})"
                + $"\nDistance to Mouse: {selectedProjectile.Hitbox.Distance(Main.MouseWorld)}" : "");

            //Game information
            Gameinfo.MarginTop = Main.screenHeight - 20;
            Gameinfo.SetText(!Config.Instance.HideGame ? $"Time: {Main.time}, Global Timer: {Main.GlobalTimeWrappedHourly}, Rain Time: {Main.rainTime}" : "");
        }

        public static void WorldDraw()
        {
            var playerRect = new Rectangle((int)Main.LocalPlayer.position.X, (int)Main.LocalPlayer.position.Y, Main.LocalPlayer.Hitbox.Width, Main.LocalPlayer.Hitbox.Height);
            playerRect.Offset((int)-Main.screenPosition.X, (int)-Main.screenPosition.Y);
            playerRect = Main.ReverseGravitySupport(playerRect);

            var npcRect = Main.npc[GetNPCMouseClosest()].getRect();
            npcRect.Offset((int)-Main.screenPosition.X, (int)-Main.screenPosition.Y);
            npcRect = Main.ReverseGravitySupport(npcRect);

            var projRect = Main.projectile[GetProjectileMouseClosest()].getRect();
            projRect.Offset((int)-Main.screenPosition.X, (int)-Main.screenPosition.Y);
            projRect = Main.ReverseGravitySupport(projRect);

            var itemRect = Main.item[GetItemMouseClosest()].getRect();
            itemRect.Offset((int)-Main.screenPosition.X, (int)-Main.screenPosition.Y);
            itemRect = Main.ReverseGravitySupport(itemRect);

            if (!Config.Instance.Hitboxes)
            {
                if (!Config.Instance.HidePlayer) Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, playerRect, Color.Firebrick * 0.5f);
                if (!Config.Instance.HideNPC) Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, npcRect, Color.Salmon * 0.5f);
                if (!Config.Instance.HideProjectile) Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, projRect, Color.HotPink * 0.5f);
                if (!Config.Instance.HideItem) Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, itemRect, Color.DodgerBlue * 0.5f);
            }
        }
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
            if (!Config.Instance.Hitboxes && !Config.Instance.HideMouse) Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(Main.mouseX, Main.mouseY, Main.ThickMouse ? (int)(14 * 1.1f) : 14, Main.ThickMouse ? (int)(14 * 1.1f) : 14), Color.AntiqueWhite * 0.5f);
        }
    }
}