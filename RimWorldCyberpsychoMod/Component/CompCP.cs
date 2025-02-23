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
            humanity = Rand.Range(50, 100);
            Log.Message($"Present humanity for {parent.LabelCap} to {humanity}");
        }

        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Values.Look(ref humanity, "CP_Humanity", 50);
        }

        public void AdjustHumanity(int amount)
        {
            humanity = ClampValue(humanity + amount, 0, 200);
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