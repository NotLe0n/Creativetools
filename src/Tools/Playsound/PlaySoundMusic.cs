using Terraria;
using Terraria.ModLoader;

namespace Creativetools.Tools.Playsound;

internal class PlaySoundMusic : ModSceneEffect
{
	public override int Music => PlaySoundUI.musicSound.Data > 50 ? PlaySoundUI.musicSound.Data + 1 : PlaySoundUI.musicSound.Data; // I have no idea why + 1
	public override string Name => "CreativeTools: PlaySound Music";
	public override SceneEffectPriority Priority => SceneEffectPriority.BossHigh;

	public override bool IsSceneEffectActive(Player player)
	{
		return PlaySoundUI.playMusic;
	}
}
