using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Creativetools.src.Tools.Modify
{
    internal class ModifyPlayer : ModPlayer
    {
        public static float playerSize = 1f;
        public static float? luck;

        public override void Load()
        {
            On.Terraria.Player.RecalculateLuck += Player_RecalculateLuck;
        }

        private void Player_RecalculateLuck(On.Terraria.Player.orig_RecalculateLuck orig, Player self)
        {
            orig(self);

            if (luck.HasValue)
                self.luck = luck.Value;
        }

        public override void PreUpdate()
        {
            if (Player.active && playerSize != 1f)
            {
                // Change player hitbox size
                Player.position = Player.Bottom;
                Player.Size = new Vector2(Player.defaultWidth * playerSize, Player.defaultHeight * playerSize);
                Player.Bottom = Player.position;
            }
        }
    }
}