using Terraria;
using Terraria.ModLoader;

namespace Creativetools.Tools.Playsound;

internal class PlaySoundMusic : ModSceneEffect
{
	public override int Music => PlaySoundUI.MusicSound.Data > 50 ? PlaySoundUI.MusicSound.Data + 1 : PlaySoundUI.MusicSound.Data; // I have no idea why + 1
	public override string Name => "CreativeTools: PlaySound Music";
	public override SceneEffectPriority Priority => SceneEffectPriority.BossHigh;

	public override bool IsSceneEffectActive(Player player)
	{
		return PlaySoundUI.playmusic;
	}
}
