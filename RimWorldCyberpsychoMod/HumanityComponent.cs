using Verse;

public class HumanityComponent : ThingComp
{
    public float humanity;

    public override void Initialize(CompProperties props)
    {
        base.Initialize(props);
        humanity = Rand.Range(5f, 17f);
    }

    public override void PostExposeData()
    {
        base.PostExposeData();
        Scribe_Values.Look(ref humanity, "humanity", 10f);
    }

    public override string CompInspectStringExtra()
    {
        return "Humanity: " + humanity.ToString("F1");
    }
}
