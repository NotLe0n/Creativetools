﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria.GameContent.UI;
using Terraria.UI;
using Creativetools.src.UI.Elements;
using Terraria.GameContent.UI.Elements;
using Creativetools.src.UI;
using Microsoft.Xna.Framework;
using Terraria.Graphics;
using Terraria.ModLoader;
using Terraria;
using System.Reflection;

namespace Creativetools.src.Tools.DownedBossToggle
{
    public class DownedBossToggleUI : UIState
    {
        UIList toggleList, textList;
        public override void OnInitialize()
        {
            var panel = new DragableUIPanel("DownedBoss Toggle", 600, 430);
            panel.VAlign = 0.6f;
            panel.HAlign = 0.2f;
            panel.OnCloseBtnClicked += () => ModContent.GetInstance<Creativetools>().UserInterface.SetState(new MainUI());
            Append(panel);

            var scrollbar = new UIScrollbar();
            scrollbar.Width.Set(20, 0);
            scrollbar.Height.Set(340, 0);
            scrollbar.Left.Set(0, 0.9f);
            scrollbar.Top.Set(0, 0.1f);
            panel.Append(scrollbar);

            toggleList = new BetterUIList();
            toggleList.Width.Set(0, 0.1f);
            toggleList.Height.Set(340, 0);
            toggleList.Left.Set(0, 0.1f);
            toggleList.Top.Set(0, 0.1f);
            toggleList.ListPadding = 8f;
            toggleList.SetScrollbar(scrollbar);
            panel.Append(toggleList);

            textList = new BetterUIList();
            textList.Width.Set(0, 0.9f);
            textList.Height.Set(340, 0);
            textList.Left.Set(0, 0.2f);
            textList.Top.Set(0, 0.1f);
            textList.ListPadding = 5f;
            textList.SetScrollbar(scrollbar);
            panel.Append(textList);

            AddToggle("downedBoss1", "(EoC)");
            AddToggle("downedBoss2", "(BoC/EoW)");
            AddToggle("downedBoss3", "(Skeletron)");
            AddToggle("downedQueenBee");
            AddToggle("downedSlimeKing");
            AddToggle("downedGoblins");
            AddToggle("downedFrost");
            AddToggle("downedPirates");
            AddToggle("downedClown");
            AddToggle("downedPlantBoss");
            AddToggle("downedGolemBoss");
            AddToggle("downedMartians");
            AddToggle("downedFishron");
            AddToggle("downedHalloweenTree");
            AddToggle("downedHalloweenKing");
            AddToggle("downedChristmasIceQueen");
            AddToggle("downedChristmasTree");
            AddToggle("downedChristmasSantank");
            AddToggle("downedAncientCultist");
            AddToggle("downedMoonlord");
            AddToggle("downedTowerSolar");
            AddToggle("downedTowerVortex");
            AddToggle("downedTowerNebula");
            AddToggle("downedTowerStardust");
            AddToggle("downedMechBossAny");
            AddToggle("downedMechBoss1", "(The Destroyer)");
            AddToggle("downedMechBoss2", "(The Twins)");
            AddToggle("downedMechBoss3", "(Skeletron Prime)");

            var resetBtn = new UITextPanel<string>("Uncheck all");
            resetBtn.SetPadding(4);
            resetBtn.Left.Set(0, 0.5f);
            resetBtn.Top.Set(0, 0.92f);
            resetBtn.Width.Set(0, 0.3f);
            resetBtn.OnClick += (evt, elm) => SetAll(false);
            panel.Append(resetBtn);

            var allTrueBtn = new UITextPanel<string>("Check all");
            allTrueBtn.SetPadding(4);
            allTrueBtn.Left.Set(0, 0.1f);
            allTrueBtn.Top.Set(0, 0.92f);
            allTrueBtn.Width.Set(0, 0.3f);
            allTrueBtn.OnClick += (evt, elm) => SetAll(true);
            panel.Append(allTrueBtn);

            base.OnInitialize();
        }

        private void AddToggle(string fieldName, string additionalInfo = "")
        {
            // get field
            Type type = typeof(NPC);
            var field = type.GetField(fieldName, BindingFlags.Public | BindingFlags.Static);

            // append toggle
            var toggle = new UIToggleImage(TextureManager.Load("Images/UI/Settings_Toggle"), 13, 13, new Point(17, 1), new Point(1, 1));
            toggle.SetState((bool)field.GetValue(type));
            toggle.OnClick += (evt, elm) => field.SetValue(type, toggle.IsOn);
            toggleList.Add(toggle);

            // append text
            var text = new UIText($"Toggle NPC.{fieldName} {additionalInfo}");
            textList.Add(text);
        }

        private void SetAll(bool state)
        {
            NPC.downedBoss1 = NPC.downedBoss2 = NPC.downedBoss2 = NPC.downedBoss3 = NPC.downedQueenBee = NPC.downedSlimeKing = NPC.downedGoblins =
            NPC.downedFrost = NPC.downedPirates = NPC.downedClown = NPC.downedPlantBoss = NPC.downedGolemBoss = NPC.downedGolemBoss = NPC.downedMartians = NPC.downedFishron =
            NPC.downedHalloweenTree = NPC.downedHalloweenKing = NPC.downedChristmasIceQueen = NPC.downedChristmasTree = NPC.downedChristmasIceQueen = NPC.downedChristmasTree =
            NPC.downedChristmasSantank = NPC.downedAncientCultist = NPC.downedMoonlord = NPC.downedTowerSolar = NPC.downedTowerVortex = NPC.downedTowerNebula = NPC.downedTowerStardust =
            NPC.downedMechBossAny = NPC.downedMechBoss1 = NPC.downedMechBoss2 = NPC.downedAncientCultist = NPC.downedMechBoss3 = state;

            ModContent.GetInstance<Creativetools>().UserInterface.SetState(new DownedBossToggleUI());
        }
    }
}
