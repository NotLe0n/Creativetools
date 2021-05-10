using Creativetools.src.UI;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Creativetools.src.NearestToMouse;

namespace Creativetools.src.Tools.MagicCursor
{
    class ModifyItem : GlobalItem
    {
        public override void Update(Item item, ref float gravity, ref float maxFallSpeed)
        {
            if (MagicCursorNPC.MagicCursor && Main.mouseMiddle && Main.item[GetItemMouseClosest()].Hitbox.Distance(Main.MouseWorld) < 500) Main.item[GetItemMouseClosest()].position = Main.MouseWorld;
        }
    }
}