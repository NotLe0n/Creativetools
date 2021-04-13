using Terraria.ID;
using Terraria.ModLoader;

namespace Creativetools.src.Tools.CustomItem
{
    public class CustomItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Customitem");
        }
        public override void SetDefaults()
        {
            Item.SetNameOverride("Customitem");
            Item.width = 32;
            Item.height = 32;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 69f;
            Item.damage = 69;
            Item.defense = 69;
            Item.scale = 1f;
            Item.shoot = ProjectileID.None;
            Item.shootSpeed = 1f;
            Item.crit = 69 - 4;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.autoReuse = false;
            Item.useTurn = false;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
        }
    }
}