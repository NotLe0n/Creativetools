using Creativetools.src.UI;
using Creativetools.src.UI.Elements;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;
using static Creativetools.src.UI.UIHelper;
using static Terraria.ModLoader.ModContent;

namespace Creativetools.src.Tools.Modify
{
    class ItemModUI : UIState
    {
        UIIntRangedDataValue DamageDataProperty;
        UIIntRangedDataValue CritDataProperty;
        UIFloatRangedDataValue KnockbackDataProperty;
        UIIntRangedDataValue UsetimeDataProperty;
        UIIntRangedDataValue DefenseDataProperty;
        UIFloatRangedDataValue ShootspeedDataProperty;
        UIFloatRangedDataValue SizeDataProperty;
        public override void OnInitialize()
        {
            TabPanel ItemMenu = new TabPanel(450, 450, new Tab("Change Item", this), new Tab(" Change Player", new PlayerModUI()));
            ItemMenu.VAlign = 0.6f;
            ItemMenu.HAlign = 0.2f;
            ItemMenu.OnCloseBtnClicked += () => GetInstance<UISystem>().UserInterface.SetState(new MainUI());
            Append(ItemMenu);

            var DamageSlider = MakeSlider(new UIIntRangedDataValue("", 0, 0, 999), out DamageDataProperty, ItemMenu, top: 50, left: -10);
            SliderButtons("Set Damage", DamageSlider, button => button.OnClick += (evt, elm) => ChangeDamage(DamageDataProperty.Data));

            var CritSlider = MakeSlider(new UIIntRangedDataValue("", 0, 0, 100), out CritDataProperty, ItemMenu, top: 100, left: -10);
            SliderButtons("Set Crit", CritSlider, button => button.OnClick += (evt, elm) => ChangeCrit(CritDataProperty.Data));

            var KnockSlider = MakeSlider(new UIFloatRangedDataValue("", 0, 0, 100), out KnockbackDataProperty, ItemMenu, top: 150, left: -10);
            SliderButtons("Set Knockback", KnockSlider, button => button.OnClick += (evt, elm) => ChangeKnock(KnockbackDataProperty.Data));

            var UsetimeSlider = MakeSlider(new UIIntRangedDataValue("", 0, 0, 50), out UsetimeDataProperty, ItemMenu, top: 200, left: -10);
            SliderButtons("Set Usetime", UsetimeSlider, button => button.OnClick += (evt, elm) => ChangeUseTime(UsetimeDataProperty.Data));

            var DefenseSlider = MakeSlider(new UIIntRangedDataValue("", 0, 0, 100), out DefenseDataProperty, ItemMenu, top: 250, left: -10);
            SliderButtons("Set Defense", DefenseSlider, button => button.OnClick += (evt, elm) => ChangeDefense(DefenseDataProperty.Data));

            var ShootSlider = MakeSlider(new UIFloatRangedDataValue("", 0, 0, 999), out ShootspeedDataProperty, ItemMenu, top: 300, left: -10);
            SliderButtons("Set Bullet speed", ShootSlider, button => button.OnClick += (evt, elm) => ChangeShoot(ShootspeedDataProperty.Data));

            var SizeSlider = MakeSlider(new UIFloatRangedDataValue("", 0, 0, 50), out SizeDataProperty, ItemMenu, top: 350, left: -10);
            SliderButtons("Set Size", SizeSlider, button => button.OnClick += (evt, elm) => ChangeSize(SizeDataProperty.Data));

            UITextPanel<string> AutoswingButton = new UITextPanel<string>("Toggle Autoswing");
            AutoswingButton.SetPadding(4);
            AutoswingButton.MarginLeft = 10;
            AutoswingButton.MarginTop = 400;
            AutoswingButton.Width.Set(10, 0f);
            AutoswingButton.OnClick += (evt, elm) =>
            {
                ToggleAutoSwing();
                SoundEngine.PlaySound(SoundID.MenuTick);
            };
            ItemMenu.Append(AutoswingButton);

            UITextPanel<string> TurnaroundButton = new UITextPanel<string>("Toggle Turnaround");
            TurnaroundButton.SetPadding(4);
            TurnaroundButton.MarginLeft = 275;
            TurnaroundButton.MarginTop = 400;
            TurnaroundButton.Width.Set(10, 0f);
            TurnaroundButton.OnClick += (evt, elm) =>
            {
                ToggleTurnAround();
                SoundEngine.PlaySound(SoundID.MenuTick);
            };
            ItemMenu.Append(TurnaroundButton);
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