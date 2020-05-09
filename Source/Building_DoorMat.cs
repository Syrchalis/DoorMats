using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using RimWorld;
using Verse;
using Verse.AI;
using System.Reflection;

namespace SyrDoorMats
{
    public class Building_DoorMat : Building
    {
        public static FieldInfo carriedFilthList = typeof(Pawn_FilthTracker).GetField("carriedFilth", BindingFlags.NonPublic | BindingFlags.Instance);

        public void Notify_PawnApproaching(Pawn pawn)
        {
            if (pawn.Faction == null || pawn.Faction.HostileTo(Faction.OfPlayer))
            {
                return;
            }
            List<Filth> carriedFilth = (List<Filth>)carriedFilthList.GetValue(pawn.filth);
            if (!carriedFilth.NullOrEmpty())
            {
                Filth filth = carriedFilth.RandomElement();
                FilthMaker.TryMakeFilth(Position, Map, filth.def, filth.sources);
                carriedFilthList.SetValue(pawn.filth, new List<Filth>());
            }
        }
        public override void DrawGUIOverlay()
        {
            return;
        }
    }
}
