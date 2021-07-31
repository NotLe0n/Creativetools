using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Creativetools.src.Tools.CustomNPC
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
        public static Texture2D ctexture = ModContent.Request<Texture2D>("Creativetools/src/Tools/CustomNPC/CustomNPC").Value;
        public static int cFramecount = 1;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault(cName);
            Main.npcFrameCount[NPC.type] = 1;
        }
        public override void SetDefaults()
        {
            NPC.GivenName = cName;
            NPC.width = 32;
            NPC.height = 32;
            NPC.aiStyle = cAistyle;
            NPC.knockBackResist = cKnockback;
            NPC.damage = cDamage;
            NPC.defense = cDefense;
            NPC.lifeMax = cLifeMax;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = 25f;
            NPC.scale = cScale;
            NPC.noTileCollide = cNoCollide;
            NPC.immortal = cImmortal;
            NPC.dontTakeDamage = cImmortal;
            //Main.npcTexture[NPC.type] = ctexture;
            Main.npcFrameCount[NPC.type] = cFramecount;
        }
    }
}