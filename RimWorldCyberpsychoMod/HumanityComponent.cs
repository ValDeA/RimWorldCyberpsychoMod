using System;
using Verse;

namespace RimWorldCyberPsychoMod
{
    public class HumanityComponent : ThingComp
    {
        public float humanity;

        public override void Initialize(CompProperties props)
        {
            base.Initialize(props);
            humanity = Rand.Range(0f, 100f);
        }

        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Values.Look(ref humanity, "humanity", 50f);
        }

        public void AdjustHumanity(float amount)
        {
            humanity = ClampValue(humanity + amount, 0f, 200f);
            Log.Message($"Adjusted humanity for {parent.LabelCap} to {humanity}");
        }
        float ClampValue(float value, float min, float max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }
    }
}