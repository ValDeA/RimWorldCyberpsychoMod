using RimWorld;
using System;
using Verse;

namespace RimWorldCyberPsychoMod
{
    public class CompCP : ThingComp
    {
        public int humanity;

        public override void Initialize(CompProperties props)
        {
            base.Initialize(props);
            humanity = Rand.Range(0, 20);
            Log.Message($"Present humanity for {parent.LabelCap} to {humanity}");
        }

        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Values.Look(ref humanity, "CP_Humanity", 50);
        }


        public void AdjustHumanityForImplants(Pawn pawn)
        {
            int humanityReduction = 0;

            foreach (Hediff hediff in pawn.health.hediffSet.hediffs)
            {
                if (hediff is Hediff_AddedPart addedPart)
                {
                    humanityReduction += GetHumanityReductionForPart(addedPart);
                }
            }

            AdjustHumanity(-humanityReduction);
        }

        private int GetHumanityReductionForPart(Hediff_AddedPart part)
        {
            if (part.def.spawnThingOnRemoved != null)
            {
                ThingDef thingDef = part.def.spawnThingOnRemoved;

                if (thingDef.techLevel == TechLevel.Spacer)
                    return 10;
                else if (thingDef.techLevel == TechLevel.Ultra)
                    return 15;
                else if (thingDef.techLevel == TechLevel.Archotech)
                    return 20;
                else
                    return 5;
            }
            return 0;
        }

        public void CheckForCyberPsycho()
        {
            if (parent is Pawn pawn && pawn.RaceProps.Humanlike && humanity < 20)
            {
                if (!pawn.InMentalState && !pawn.Downed && !pawn.health.hediffSet.HasHediff(HediffDefOf.BloodLoss))
                {
                    pawn.mindState.mentalStateHandler.TryStartMentalState(DefDatabase<MentalStateDef>.GetNamed("CyberPsycho"), null, true);
                }
            }
        }
        public void AdjustHumanity(int amount)
        {
            humanity = ClampValue(humanity + amount, 0, 200);
            CheckForCyberPsycho();
            Log.Message($"Adjusted humanity for {parent.LabelCap} to {humanity}");
        }
        int ClampValue(int value, int min, int max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }
    }
}