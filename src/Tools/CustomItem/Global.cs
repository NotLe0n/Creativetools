using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Creativetools.src.Tools.CustomItem
{
    class Global : GlobalItem
    {
        public static bool createitem;
        public static string cName = "Customitem";
        public static int cDamage = 0;
        public static int cDefense = 0;
        public static int cShoot = 0;
        public static float cShootSpeed = 10f;
        public static int cCrit = 0;
        public static float cKnockback = 10f;
        public static float cScale = 1f;
        public static int cUseTime = 10;
        public static bool cAutoSwing = false;
        public static bool cTurnAround = false;
        public static Texture2D ctexture = GetTexture("Creativetools/src/Tools/CustomItem/CustomItem");

        public override void SetDefaults(Item item)
        {
            if (item.type == ItemType<CustomItem>() && createitem)
            {
                item.SetNameOverride(cName);
                item.knockBack = cKnockback;
                item.damage = cDamage;
                item.defense = cDefense;
                item.scale = cScale;
                item.shoot = cShoot;
                item.shootSpeed = cShootSpeed;
                item.crit = cCrit - 4;
                item.useTime = cUseTime;
                item.useAnimation = cUseTime;
                item.autoReuse = cAutoSwing;
                item.useTurn = cTurnAround;
                Main.itemTexture[item.type] = ctexture;
                createitem = false;
            }
        }
    }
}
