using Creativetools.src.UI;
using Creativetools.src.UI.Elements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;
using static Creativetools.src.UI.UIHelper;

namespace Creativetools.src.Tools.PlaySound
{
    internal class PlaySoundUI : UIState
    {
        private UIText SoundName, MusicName;
        private UIIntRangedDataValue ID;
        public static UIIntRangedDataValue MusicSound;
        public static bool playmusic;

        private List<FieldInfo> Sounds
        {
            get
            {
                List<FieldInfo> sounds = typeof(SoundID).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.SetField).ToList();
                sounds.RemoveAll(x => x.FieldType != typeof(int) && x.FieldType != typeof(LegacySoundStyle));
                return sounds;
            }
        }
        private FieldInfo[] Music => typeof(MusicID).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.SetField);

        public override void OnInitialize()
        {
            DragableUIPanel Menu = new("Play Sound");
            Menu.Width.Set(500, 0);
            Menu.Height.Set(180, 0);
            Menu.VAlign = 0.6f;
            Menu.HAlign = 0.2f;
            Menu.OnCloseBtnClicked += () => UISystem.UserInterface.SetState(new MainUI());
            Append(Menu);

            //////////////////Sound/////////////////////
            var SoundSlider = MakeSlider(new UIIntRangedDataValue("", 0, 0, Sounds.Count - 1), out ID, Menu, top: 50, left: -10);
            SliderButtons("Play Sound", SoundSlider, button => button.OnClick += (evt, elm) =>
            {
                var type = Sounds[ID.Data].FieldType;
                if (type == typeof(int))
                {
                    SoundEngine.PlaySound((int)Sounds[ID.Data].GetValue(null));
                }
                else if (type == typeof(LegacySoundStyle))
                {
                    SoundEngine.PlaySound((LegacySoundStyle)Sounds[ID.Data].GetValue(null));
                }
            }, false);

            SoundName = new UIText("") { HAlign = 0.6f, MarginTop = 20 };
            SoundSlider.Append(SoundName);

            /////////////////Music////////////////////
            var MusicSlider = MakeSlider(new UIIntRangedDataValue("", 0, 0, Music.Length), out MusicSound, Menu, top: 100, left: -10);

            UITextPanel<string> PlayMusic = new(text: playmusic ? "Stop Music" : "Play Music");
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
            SoundName.SetText("SoundID." + Sounds[ID.Data].Name);

            List<string> MusicNames = new(Music.Length);
            for (int i = 0; i < MusicNames.Capacity; i++)
            {
                MusicNames.Add("MusicID." + Music[i].Name);
            }
            MusicNames.Insert(0, "No Music");
            MusicNames.Insert(45, "Unused");

            MusicName.SetText(MusicNames[MusicSound.Data]);

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
