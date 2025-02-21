using RimWorld;
using Verse;

namespace RimWorldCyberpsychoMod
{
    [StaticConstructorOnStartup]
    public static class HumanityStatDefInitializer
    {
        static HumanityStatDefInitializer()
        {
            StatDef humanityStat = new StatDef
            {
                defName = "Humanity",
                label = "Humanity",
                description = "Measure of a pawn's humanity. Not visible in-game.",
                category = StatCategoryDefOf.BasicsPawn,
                defaultBaseValue = 50f,
                minValue = 0f,
                maxValue = 200f,
                showIfUndefined = false,
                toStringStyle = ToStringStyle.Integer
            };

            DefDatabase<StatDef>.Add(humanityStat);
        }
    }

    public class StatWorker_Humanity : StatWorker
    {
        public override float GetValueUnfinalized(StatRequest req, bool applyPostProcess = true)
        {
            if (req.HasThing && req.Thing is Pawn pawn)
            {
                var comp = pawn.GetComp<HumanityComponent>();
                if (comp != null)
                {
                    return comp.humanity;
                }
            }
            return 0f;
        }
    }
}
