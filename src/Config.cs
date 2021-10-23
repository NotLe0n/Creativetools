using Microsoft.Xna.Framework;
using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace Creativetools.src
{
    public class Config : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [DefaultValue(false)]
        [Label("Hide Mouse info")]
        public bool HideMouse { get; set; }

        [DefaultValue(false)]
        [Label("Hide Player info")]
        public bool HidePlayer { get; set; }

        [DefaultValue(false)]
        [Label("Hide Screen info")]
        public bool HideScreen { get; set; }

        [DefaultValue(false)]
        [Label("Hide NPC info")]
        public bool HideNPC { get; set; }

        [DefaultValue(false)]
        [Label("Hide Item info")]
        public bool HideItem { get; set; }

        [DefaultValue(false)]
        [Label("Hide Projectile info")]
        public bool HideProjectile { get; set; }

        [DefaultValue(false)]
        [Label("Hide Game info")]
        public bool HideGame { get; set; }

        [DefaultValue(false)]
        [Label("Hide Hitboxes")]
        public bool Hitboxes { get; set; }

        [DefaultValue(typeof(Vector2), "0, 0")]
        [Label("Menu button offset")]
        [Increment(1f)]
        [Range(-5f, 5f)]
        [DrawTicks]
        public Vector2 MenuBtnOffset { get; set; }
    }
}