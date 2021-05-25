using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Creativetools.src.Tools.Modify
{
    internal class ModifyPlayer : ModPlayer
    {
        public static float playerSize = 1f;
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