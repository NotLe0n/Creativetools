using Terraria.ID;
using Terraria.ModLoader;

namespace Creativetools.cItem
{
    public class CustomItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Customitem");
        }
        public override void SetDefaults()
        {
            item.SetNameOverride("Customitem");
            item.width = 32;
            item.height = 32;
            item.melee = true;
            item.knockBack = 69f;
            item.damage = 69;
            item.defense = 69;
            item.scale = 1f;
            item.shoot = 0;
            item.shootSpeed = 1f;
            item.crit = 69 - 4;
            item.useTime = 10;
            item.useAnimation = 10;
            item.autoReuse = false;
            item.useTurn = false;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.UseSound = SoundID.Item1;
        }
    }
}