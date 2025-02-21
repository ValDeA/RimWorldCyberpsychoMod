using RimWorld;
using Verse;

namespace RimWorldCyberpsychoMod
{
    public class StatWorker_Humanity : StatWorker
    {
        public override float GetValueUnfinalized(StatRequest req, bool applyPostProcess = true)
        {
            if (req.HasThing && req.Thing is Pawn pawn)
            {
                var comp = pawn.GetComp<HumanityComponent>();
                return comp?.humanity ?? 0f;
            }
            return 0f;
        }
    }
}
