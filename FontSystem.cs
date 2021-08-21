using ReLogic.Graphics;
using System;
using Terraria.ModLoader;

namespace Creativetools
{
    class FontSystem : ModSystem
    {
        public static DynamicSpriteFont ConsolasFont { get; private set; }

        public override void Load()
        {
            base.Load();

            if (ModContent.RequestIfExists<DynamicSpriteFont>("Creativetools/Fonts/consolas", out var asset, ReLogic.Content.AssetRequestMode.ImmediateLoad))
            {
                ConsolasFont = asset.Value;
            }
        }
        public override void Unload()
        {
            base.Unload();

            ConsolasFont = null;
        }
    }
}
