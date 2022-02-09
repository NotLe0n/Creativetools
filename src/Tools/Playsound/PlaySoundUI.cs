﻿using Creativetools.src.UI;
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

namespace Creativetools.src.Tools.PlaySound;

internal class PlaySoundUI : UIState
{
	private UIText soundName, musicName;
	private UIIntRangedDataValue ID;
	private UIIntRangedDataValue MusicSound;
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
		var menu = new DragableUIPanel("Play Sound");
		menu.Width.Set(500, 0);
		menu.Height.Set(180, 0);
		menu.VAlign = 0.6f;
		menu.HAlign = 0.2f;
		menu.OnCloseBtnClicked += () => UISystem.UserInterface.SetState(new MainUI());
		Append(menu);

		//////////////////Sound/////////////////////
		var soundSlider = MakeSlider(new UIIntRangedDataValue("", 0, 0, Sounds.Count - 1), out ID, menu, top: 50, left: -10);
		SliderButtons("Play Sound", soundSlider, button => button.OnClick += (evt, elm) =>
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

		soundName = new UIText("") { HAlign = 0.6f, MarginTop = 20 };
		soundSlider.Append(soundName);

		/////////////////Music////////////////////
		var MusicSlider = MakeSlider(new UIIntRangedDataValue("", 0, 0, Music.Length), out MusicSound, menu, top: 100, left: -10);

		UITextPanel<string> playMusic = new(text: playmusic ? "Stop Music" : "Play Music");
		playMusic.SetPadding(4);
		playMusic.MarginLeft = 20;
		playMusic.Width.Set(10, 0f);
		playMusic.OnClick += (evt, element) =>
		{
			playmusic = !playmusic;
			playMusic.SetText(text: playmusic ? "Stop Music" : "Play Music");
		};
		MusicSlider.Append(playMusic);

		musicName = new UIText("") { HAlign = 0.6f, MarginTop = 20 };
		MusicSlider.Append(musicName);
	}

	public override void Update(GameTime gameTime)
	{
		base.Update(gameTime);
		soundName.SetText("SoundID." + Sounds[ID.Data].Name);

		List<string> musicNames = new(Music.Length);
		for (int i = 0; i < musicNames.Capacity; i++)
		{
			musicNames.Add("MusicID." + Music[i].Name);
		}
		musicNames.Insert(0, "No Music");
		musicNames.Insert(45, "Unused");

		musicName.SetText(musicNames[MusicSound.Data]);

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
