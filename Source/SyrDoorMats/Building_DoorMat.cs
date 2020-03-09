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
        static FieldInfo carriedFilthList = typeof(Pawn_FilthTracker).GetField("carriedFilth", BindingFlags.NonPublic | BindingFlags.Instance);
        public override void Tick()
        {
            base.Tick();
            if (!Gen.IsHashIntervalTick(this, 10))
            {
                return;
            }
            foreach (IntVec3 intVec in GenAdj.OccupiedRect(this))
            {
                if (!GenGrid.Impassable(intVec, Map))
                {
                    foreach (Pawn pawn in (from s in Map.thingGrid.ThingsAt(intVec)
                                           where s.GetType() == typeof(Pawn)
                                           select s).Cast<Pawn>())
                    {
                        if (pawn != null)
                        {
                            //pawn.filth.GetType().GetMethod("TryDropFilth", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(pawn.filth, null);
                            List<Filth> carriedFilth = (List<Filth>)carriedFilthList.GetValue(pawn.filth);
                            if (!carriedFilth.NullOrEmpty())
                            {
                                FilthMaker.TryMakeFilth(pawn.Position, pawn.Map, carriedFilth[0].def, carriedFilth[0].sources);
                                carriedFilth.Clear();
                            }
                        }
                    }
                }
            }
        }

        public override void DrawGUIOverlay()
        {
        }
    }
}
