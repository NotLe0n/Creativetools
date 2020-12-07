using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Creativetools.src.cNPC
{
    public class CustomNPC : ModNPC
    {
        public static string cName = "CustomNPC";
        public static int cAistyle = -1;
        public static int cDamage = 0;
        public static int cDefense = 0;
        public static int cLifeMax = 1;
        public static float cKnockback = 0f;
        public static float cScale = 1f;
        public static bool cNoCollide = false;
        public static bool cImmortal = false;
        public static Texture2D ctexture = ModContent.GetTexture("Creativetools/src/cNPC/CustomNPC");
        public static int cFramecount = 1;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault(cName);
            Main.npcFrameCount[npc.type] = 1;
        }
        public override void SetDefaults()
        {
            npc.GivenName = cName;
            npc.width = 32;
            npc.height = 32;
            npc.aiStyle = cAistyle;
            npc.knockBackResist = cKnockback;
            npc.damage = cDamage;
            npc.defense = cDefense;
            npc.lifeMax = cLifeMax;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = 25f;
            npc.scale = cScale;
            npc.noTileCollide = cNoCollide;
            npc.immortal = cImmortal;
            npc.dontTakeDamage = cImmortal;
            Main.npcTexture[npc.type] = ctexture;
            Main.npcFrameCount[npc.type] = cFramecount;
        }
    }
}