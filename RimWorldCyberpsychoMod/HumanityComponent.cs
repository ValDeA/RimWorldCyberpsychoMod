using UnityEngine;
using Verse;

public class HumanityComponent : ThingComp
{
    public float humanity;

    public override void Initialize(CompProperties props)
    {
        base.Initialize(props);
        humanity = Rand.Range(0f, 100f);
        Log.Message($"Initialized HumanityComponent for {parent.LabelCap} with humanity {humanity}");
    }

    public override void PostExposeData()
    {
        base.PostExposeData();
        Scribe_Values.Look(ref humanity, "humanity", 50f);
    }

    public void AdjustHumanity(float amount)
    {
        humanity = Mathf.Clamp(humanity + amount, 0f, 200f);
        Log.Message($"Adjusted humanity for {parent.LabelCap} to {humanity}");
    }
}
