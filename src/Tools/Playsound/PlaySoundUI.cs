using Creativetools.UI;
using Creativetools.UI.Elements;
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
using static Creativetools.UI.UIHelper;

namespace Creativetools.Tools.Playsound;

internal class PlaySoundUI : UIState
{
	private readonly UIText soundName, musicName;
	private readonly UIIntRangedDataValue id;
	public static UIIntRangedDataValue musicSound;
	public static bool playMusic;

	private static FieldInfo[] Sounds => typeof(SoundID).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.SetField).Where(x => x.FieldType == typeof(SoundStyle)).OrderBy(x => x.Name).ToArray();

	private static FieldInfo[] Musics => typeof(MusicID).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.SetField);

	public PlaySoundUI()
	{
		var menu = new DragableUIPanel("Play Sound");
		menu.Width.Set(500, 0);
		menu.Height.Set(180, 0);
		menu.VAlign = 0.6f;
		menu.HAlign = 0.2f;
		menu.OnCloseBtnClicked += () => UISystem.userInterface.SetState(new MainUI());
		Append(menu);

		//////////////////Sound/////////////////////
		var soundSlider = MakeSlider(new UIIntRangedDataValue("", 0, 0, Sounds.Length - 1), out id, menu, top: 50, left: -10);
		soundSlider.AppendSliderButton("Play Sound", () =>
		{
			if (Sounds[id.Data].GetValue(null) is SoundStyle sound) {
				SoundEngine.PlaySound(sound);
			}
		}, false);

		soundName = new UIText("") { HAlign = 0.6f, MarginTop = 20 };
		soundSlider.Append(soundName);

		/////////////////Music////////////////////
		var musicSlider = MakeSlider(new UIIntRangedDataValue("", 0, 0, Musics.Length), out musicSound, menu, top: 100, left: -10);

		UITextPanel<string> playMusicBtn = new(text: playMusic ? "Stop Music" : "Play Music");
		playMusicBtn.SetPadding(4);
		playMusicBtn.MarginLeft = 20;
		playMusicBtn.Width.Set(10, 0f);
		playMusicBtn.OnLeftClick += (_, _) =>
		{
			playMusic = !playMusic;
			playMusicBtn.SetText(text: playMusic ? "Stop Music" : "Play Music");
		};
		musicSlider.Append(playMusicBtn);

		musicName = new UIText("") { HAlign = 0.6f, MarginTop = 20 };
		musicSlider.Append(musicName);
	}

	public override void Update(GameTime gameTime)
	{
		base.Update(gameTime);
		soundName.SetText("SoundID." + Sounds[id.Data].Name);

		List<string> musicNames = new(Musics.Length);
		for (int i = 0; i < musicNames.Capacity; i++) {
			musicNames.Add("MusicID." + Musics[i].Name);
		}

		musicNames.Insert(0, "No Music");
		musicNames.Insert(45, "Unused");

		musicName.SetText(musicNames[musicSound.Data]);

	}
	
	// so you can't use items when clicking on the button
	protected override void DrawSelf(SpriteBatch spriteBatch)
	{
		if (ContainsPoint(Main.MouseScreen)) {
			Main.LocalPlayer.mouseInterface = true;
		}
	}
}
