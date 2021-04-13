using Terraria.ModLoader;

namespace Creativetools.src.Tools.PlaySound
{
    class MusicSystem : ModSystem
    {
        public override void UpdateMusic(ref int music, ref MusicPriority priority)
        {
            if (PlaySoundUI.playmusic)
            {
                music = PlaySoundUI.MusicSound.Data;
                priority = MusicPriority.BiomeHigh;
            }
        }
    }
}
