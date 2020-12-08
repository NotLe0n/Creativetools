using Creativetools.src.UI.Elements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using static Creativetools.src.UI.UIHelper;

namespace Creativetools.src.UI
{
    class PlaySoundUI : UIState
    {
        private UIText SoundName;
        private UIText LegacyName;
        private UIText MusicName;
        UIIntRangedDataValue ID;
        UIIntRangedDataValue LegacySound;
        public static UIIntRangedDataValue MusicSound;
        public static bool playmusic;
        public override void OnInitialize()
        {
            DragableUIPanel Menu = new DragableUIPanel("Play Sound", 500, 220);
            Menu.VAlign = 0.6f;
            Menu.HAlign = 0.2f;
            Menu.OnCloseBtnClicked += () => ModContent.GetInstance<Creativetools>().UserInterface.SetState(new MainUI());
            Append(Menu);

            //////////////////Sound/////////////////////
            var SoundSlider = MakeSlider(new UIIntRangedDataValue("", 0, 0, 42), out ID, Menu, top: 50, left: -10);
            SliderButtons("Play Sound", SoundSlider, button => button.OnClick += (evt, elm) => Main.PlaySound(ID.Data), false);

            SoundName = new UIText("") { HAlign = 0.6f, MarginTop = 20 };
            SoundSlider.Append(SoundName);

            /////////////////LegacySound/////////////
            var LegacySlider = MakeSlider(new UIIntRangedDataValue("", 0, 0, 334), out LegacySound, Menu, top: 100, left: -10);
            SliderButtons("Play Legacy", LegacySlider, button => button.OnClick += (evt, elm) =>
            {
                var soundlist = new List<LegacySoundStyle> { SoundID.NPCHit1, SoundID.NPCHit2, SoundID.NPCHit3, SoundID.NPCHit4, SoundID.NPCHit5, SoundID.NPCHit6, SoundID.NPCHit7, SoundID.NPCHit8, SoundID.NPCHit9, SoundID.NPCHit10, SoundID.NPCHit11, SoundID.NPCHit12, SoundID.NPCHit13, SoundID.NPCHit14, SoundID.NPCHit15, SoundID.NPCHit16, SoundID.NPCHit17, SoundID.NPCHit18, SoundID.NPCHit19, SoundID.NPCHit20, SoundID.NPCHit21, SoundID.NPCHit22, SoundID.NPCHit23, SoundID.NPCHit24, SoundID.NPCHit25, SoundID.NPCHit26, SoundID.NPCHit27, SoundID.NPCHit28, SoundID.NPCHit29, SoundID.NPCHit30, SoundID.NPCHit31, SoundID.NPCHit32, SoundID.NPCHit33, SoundID.NPCHit34, SoundID.NPCHit35, SoundID.NPCHit36, SoundID.NPCHit37, SoundID.NPCHit38, SoundID.NPCHit39, SoundID.NPCHit40, SoundID.NPCHit41, SoundID.NPCHit42, SoundID.NPCHit43, SoundID.NPCHit44, SoundID.NPCHit45, SoundID.NPCHit46, SoundID.NPCHit47, SoundID.NPCHit48, SoundID.NPCHit49, SoundID.NPCHit50, SoundID.NPCHit51, SoundID.NPCHit52, SoundID.NPCHit53, SoundID.NPCHit54, SoundID.NPCHit55, SoundID.NPCHit56, SoundID.NPCHit57, SoundID.NPCDeath1, SoundID.NPCDeath2, SoundID.NPCDeath3, SoundID.NPCDeath4, SoundID.NPCDeath5, SoundID.NPCDeath6, SoundID.NPCDeath7, SoundID.NPCDeath8, SoundID.NPCDeath9, SoundID.NPCDeath10, SoundID.NPCDeath11, SoundID.NPCDeath12, SoundID.NPCDeath13, SoundID.NPCDeath14, SoundID.NPCDeath15, SoundID.NPCDeath16, SoundID.NPCDeath17, SoundID.NPCDeath18, SoundID.NPCDeath19, SoundID.NPCDeath20, SoundID.NPCDeath21, SoundID.NPCDeath22, SoundID.NPCDeath23, SoundID.NPCDeath24, SoundID.NPCDeath25, SoundID.NPCDeath26, SoundID.NPCDeath27, SoundID.NPCDeath28, SoundID.NPCDeath29, SoundID.NPCDeath30, SoundID.NPCDeath31, SoundID.NPCDeath32, SoundID.NPCDeath33, SoundID.NPCDeath34, SoundID.NPCDeath35, SoundID.NPCDeath36, SoundID.NPCDeath37, SoundID.NPCDeath38, SoundID.NPCDeath39, SoundID.NPCDeath40, SoundID.NPCDeath41, SoundID.NPCDeath42, SoundID.NPCDeath43, SoundID.NPCDeath44, SoundID.NPCDeath45, SoundID.NPCDeath46, SoundID.NPCDeath47, SoundID.NPCDeath48, SoundID.NPCDeath49, SoundID.NPCDeath50, SoundID.NPCDeath51, SoundID.NPCDeath52, SoundID.NPCDeath53, SoundID.NPCDeath54, SoundID.NPCDeath55, SoundID.NPCDeath56, SoundID.NPCDeath57, SoundID.NPCDeath58, SoundID.NPCDeath59, SoundID.NPCDeath60, SoundID.NPCDeath61, SoundID.NPCDeath62, SoundID.Item1, SoundID.Item2, SoundID.Item3, SoundID.Item4, SoundID.Item5, SoundID.Item6, SoundID.Item7, SoundID.Item8, SoundID.Item9, SoundID.Item10, SoundID.Item11, SoundID.Item12, SoundID.Item13, SoundID.Item14, SoundID.Item15, SoundID.Item16, SoundID.Item17, SoundID.Item18, SoundID.Item19, SoundID.Item20, SoundID.Item21, SoundID.Item22, SoundID.Item23, SoundID.Item24, SoundID.Item25, SoundID.Item26, SoundID.Item27, SoundID.Item28, SoundID.Item29, SoundID.Item30, SoundID.Item31, SoundID.Item32, SoundID.Item33, SoundID.Item34, SoundID.Item35, SoundID.Item36, SoundID.Item37, SoundID.Item38, SoundID.Item39, SoundID.Item40, SoundID.Item41, SoundID.Item42, SoundID.Item43, SoundID.Item44, SoundID.Item45, SoundID.Item46, SoundID.Item47, SoundID.Item48, SoundID.Item49, SoundID.Item50, SoundID.Item51, SoundID.Item52, SoundID.Item53, SoundID.Item54, SoundID.Item55, SoundID.Item56, SoundID.Item57, SoundID.Item58, SoundID.Item59, SoundID.Item60, SoundID.Item61, SoundID.Item62, SoundID.Item63, SoundID.Item64, SoundID.Item65, SoundID.Item66, SoundID.Item67, SoundID.Item68, SoundID.Item69, SoundID.Item70, SoundID.Item71, SoundID.Item72, SoundID.Item73, SoundID.Item74, SoundID.Item75, SoundID.Item76, SoundID.Item77, SoundID.Item78, SoundID.Item79, SoundID.Item80, SoundID.Item81, SoundID.Item82, SoundID.Item83, SoundID.Item84, SoundID.Item85, SoundID.Item86, SoundID.Item87, SoundID.Item88, SoundID.Item89, SoundID.Item90, SoundID.Item91, SoundID.Item92, SoundID.Item93, SoundID.Item94, SoundID.Item95, SoundID.Item96, SoundID.Item97, SoundID.Item98, SoundID.Item99, SoundID.Item100, SoundID.Item101, SoundID.Item102, SoundID.Item103, SoundID.Item104, SoundID.Item105, SoundID.Item106, SoundID.Item107, SoundID.Item108, SoundID.Item109, SoundID.Item110, SoundID.Item111, SoundID.Item112, SoundID.Item113, SoundID.Item114, SoundID.Item115, SoundID.Item116, SoundID.Item117, SoundID.Item118, SoundID.Item119, SoundID.Item120, SoundID.Item121, SoundID.Item122, SoundID.Item123, SoundID.Item124, SoundID.Item125, SoundID.DD2_GoblinBomb, SoundID.BlizzardInsideBuildingLoop, SoundID.BlizzardStrongLoop, SoundID.LiquidsHoneyWater, SoundID.LiquidsHoneyLava, SoundID.LiquidsWaterLava, SoundID.DD2_BallistaTowerShot, SoundID.DD2_ExplosiveTrapExplode, SoundID.DD2_FlameburstTowerShot, SoundID.DD2_LightningAuraZap, SoundID.DD2_DefenseTowerSpawn, SoundID.DD2_BetsyDeath, SoundID.DD2_BetsyFireballShot, SoundID.DD2_BetsyFireballImpact, SoundID.DD2_BetsyFlameBreath, SoundID.DD2_BetsyFlyingCircleAttack, SoundID.DD2_BetsyHurt, SoundID.DD2_BetsyScream, SoundID.DD2_BetsySummon, SoundID.DD2_BetsyWindAttack, SoundID.DD2_DarkMageAttack, SoundID.DD2_DarkMageCastHeal, SoundID.DD2_DarkMageDeath, SoundID.DD2_DarkMageHealImpact, SoundID.DD2_DarkMageHurt, SoundID.DD2_DarkMageSummonSkeleton, SoundID.DD2_DrakinBreathIn, SoundID.DD2_DrakinDeath, SoundID.DD2_DrakinHurt, SoundID.DD2_DrakinShot, SoundID.DD2_GoblinDeath, SoundID.DD2_GoblinHurt, SoundID.DD2_GoblinScream, SoundID.DD2_GoblinBomberDeath, SoundID.DD2_GoblinBomberHurt, SoundID.DD2_GoblinBomberScream, SoundID.DD2_GoblinBomberThrow, SoundID.DD2_JavelinThrowersAttack, SoundID.DD2_JavelinThrowersDeath, SoundID.DD2_JavelinThrowersHurt, SoundID.DD2_JavelinThrowersTaunt, SoundID.DD2_KoboldDeath, SoundID.DD2_KoboldExplosion, SoundID.DD2_KoboldHurt, SoundID.DD2_KoboldIgnite, SoundID.DD2_KoboldIgniteLoop, SoundID.DD2_KoboldScreamChargeLoop, SoundID.DD2_KoboldFlyerChargeScream, SoundID.DD2_KoboldFlyerDeath, SoundID.DD2_KoboldFlyerHurt, SoundID.DD2_LightningBugDeath, SoundID.DD2_LightningBugHurt, SoundID.DD2_LightningBugZap, SoundID.DD2_OgreAttack, SoundID.DD2_OgreDeath, SoundID.DD2_OgreGroundPound, SoundID.DD2_OgreHurt, SoundID.DD2_OgreRoar, SoundID.DD2_OgreSpit, SoundID.DD2_SkeletonDeath, SoundID.DD2_SkeletonHurt, SoundID.DD2_SkeletonSummoned, SoundID.DD2_WitherBeastAuraPulse, SoundID.DD2_WitherBeastCrystalImpact, SoundID.DD2_WitherBeastDeath, SoundID.DD2_WitherBeastHurt, SoundID.DD2_WyvernDeath, SoundID.DD2_WyvernHurt, SoundID.DD2_WyvernScream, SoundID.DD2_WyvernDiveDown, SoundID.DD2_EtherianPortalDryadTouch, SoundID.DD2_EtherianPortalIdleLoop, SoundID.DD2_EtherianPortalOpen, SoundID.DD2_EtherianPortalSpawnEnemy, SoundID.DD2_CrystalCartImpact, SoundID.DD2_DefeatScene, SoundID.DD2_WinScene, SoundID.DD2_BetsysWrathShot, SoundID.DD2_BetsysWrathImpact, SoundID.DD2_BookStaffCast, SoundID.DD2_BookStaffTwisterLoop, SoundID.DD2_GhastlyGlaiveImpactGhost, SoundID.DD2_GhastlyGlaivePierce, SoundID.DD2_MonkStaffGroundImpact, SoundID.DD2_MonkStaffGroundMiss, SoundID.DD2_MonkStaffSwing, SoundID.DD2_PhantomPhoenixShot, SoundID.DD2_SonicBoomBladeSlash, SoundID.DD2_SkyDragonsFuryCircle, SoundID.DD2_SkyDragonsFuryShot, SoundID.DD2_SkyDragonsFurySwing };
                Main.PlaySound(soundlist[LegacySound.Data]);
            }, false);

            LegacyName = new UIText("") { HAlign = 0.6f, MarginTop = 20 };
            LegacySlider.Append(LegacyName);

            /////////////////Music////////////////////
            var MusicSlider = MakeSlider(new UIIntRangedDataValue("", 0, 0, 41), out MusicSound, Menu, top: 150, left: -10);

            UITextPanel<string> PlayMusic = new UITextPanel<string>(text: playmusic ? "Stop Music" : "Play Music");
            PlayMusic.SetPadding(4);
            PlayMusic.MarginLeft = 20;
            PlayMusic.Width.Set(10, 0f);
            PlayMusic.OnClick += (evt, element) =>
            {
                playmusic = !playmusic;
                PlayMusic.SetText(text: playmusic ? "Stop Music" : "Play Music");
            };
            MusicSlider.Append(PlayMusic);

            MusicName = new UIText("") { HAlign = 0.6f, MarginTop = 20 };
            MusicSlider.Append(MusicName);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            FieldInfo[] sounds = typeof(SoundID).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.SetField);
            for (int i = 0; i < sounds.Length; i++)
            {
                LegacyName.SetText("SoundID." + sounds[LegacySound.Data].Name);
                SoundName.SetText("SoundID." + sounds[ID.Data + 335].Name);
            }
            FieldInfo[] musics = typeof(MusicID).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.SetField);
            for (int i = 0; i < musics.Length; i++)
            {
                if (MusicSound.Data == 0)
                {
                    MusicName.SetText("No Music");
                }
                else
                {
                    MusicName.SetText("MusicID." + musics[MusicSound.Data - 1].Name);
                }
            }
        }
        // so you can't use items when clicking on the button
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            if (ContainsPoint(Main.MouseScreen))
            {
                Main.LocalPlayer.mouseInterface = true;
            }
        }
    }
}
