using RimWorld;
using Verse;

namespace RimWorldCyberPsychoMod
{
    public class StatWorker_Humanity : StatWorker
    {
        public override float GetValueUnfinalized(StatRequest req, bool applyPostProcess = true)
        {
            if (req.HasThing && req.Thing is Pawn pawn)
            {
                var comp = pawn.GetComp<CompCP>();
                if (comp != null)
                {
                    return comp.humanity;
                }
            }
            return 0f;
        }
    }
}