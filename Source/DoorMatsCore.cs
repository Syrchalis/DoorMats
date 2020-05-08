using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using RimWorld;
using Verse;
using UnityEngine;

namespace SyrDoorMats
{
    public class DoorMatsCore : Mod
    {
        public static DoorMatsSettings settings;

        public DoorMatsCore(ModContentPack content) : base(content)
        {
            settings = GetSettings<DoorMatsSettings>();
        }

        public override string SettingsCategory() => "SyrDoorMatsSettingsCategory".Translate();

        public override void DoSettingsWindowContents(Rect inRect)
        {
            checked
            {
                Listing_Standard listing_Standard = new Listing_Standard();
                listing_Standard.Begin(inRect);
                listing_Standard.Label("SyrDoorMatsSlowdown".Translate() + ": " + DoorMatsSettings.slowdown, -1, "SyrDoorMatsSlowdownToolTip".Translate());
                listing_Standard.Gap(6f);
                DoorMatsSettings.slowdown = (int)listing_Standard.Slider(GenMath.RoundTo(DoorMatsSettings.slowdown, 5), 0, 100);
                listing_Standard.Gap(24f);
                if (listing_Standard.ButtonText("SyrDoorMatsDefaultSettings".Translate(), "SyrDoorMatsDefaultSettingsTooltip".Translate()))
                {
                    DoorMatsSettings.slowdown = 40;
                }
                listing_Standard.End();
                settings.Write();
            }
        }

        public override void WriteSettings()
        {
            base.WriteSettings();
        }
    }

    public class DoorMatsSettings : ModSettings
    {
        public static int slowdown = 40;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look<int>(ref slowdown, "SyrDoorMatsSlowdown", 40, true);
        }
    }
}
