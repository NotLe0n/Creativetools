using Creativetools.cItem;
using Creativetools.src.UI;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using static Creativetools.src.NearestToMouse;
using static Terraria.ModLoader.ModContent;

namespace Creativetools.src
{
    class ModifyItem : GlobalItem
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
        public static Texture2D ctexture = GetTexture("Creativetools/cItem/CustomItem");

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
        public override void Update(Item item, ref float gravity, ref float maxFallSpeed)
        {
            if (MainUI.MagicCursor && Main.mouseMiddle && Main.item[GetItemMouseClosest()].Hitbox.Distance(Main.MouseWorld) < 500) Main.item[GetItemMouseClosest()].position = Main.MouseWorld;
        }

        public static void ChangeDamage(int change) => Main.LocalPlayer.HeldItem.damage = change;
        public static void ChangeCrit(int change) => Main.LocalPlayer.HeldItem.crit = change - 4;
        public static void ChangeKnock(float change) => Main.LocalPlayer.HeldItem.knockBack = change;
        public static void ChangeUseTime(int change)
        {
            Main.LocalPlayer.HeldItem.useTime = change;
            Main.LocalPlayer.HeldItem.useAnimation = change;
        }
        public static void ChangeDefense(int change) => Main.LocalPlayer.HeldItem.defense = change;
        public static void ChangeShoot(float change) => Main.LocalPlayer.HeldItem.shootSpeed = change;
        public static void ChangeSize(float change) => Main.LocalPlayer.HeldItem.scale = change;
        public static void ToggleAutoSwing() => Main.LocalPlayer.HeldItem.autoReuse = !Main.LocalPlayer.HeldItem.autoReuse;
        public static void ToggleTurnAround() => Main.LocalPlayer.HeldItem.useTurn = !Main.LocalPlayer.HeldItem.useTurn;
    }
}