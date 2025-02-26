using RimWorld;
using RimWorldCyberpsychoMod.State;
using RimWorldCyberPsychoMod;
using Verse;

namespace RimWorldCyberpsychoMod.Woker
{
    public class ThoughtWorker_HumanityLevel : ThoughtWorker
    {
        protected override ThoughtState CurrentStateInternal(Pawn p)
        {
            CompCP comp = p.GetComp<CompCP>();
            if (comp == null)
                return ThoughtState.Inactive;

            // 사이버 사이코 상태
            if (p.InMentalState && p.MentalState.def == DefDatabase<MentalStateDef>.GetNamed(MentalState_CyberPsycho.MENTALSTATE_CYBERPSYCHO))
                return ThoughtState.ActiveAtStage(5);

            // 사이버 사이코 상태에서 회복
            if (comp.ticksSinceLastCyberPsycho >= 0 && comp.ticksSinceLastCyberPsycho <= 60000)
                return ThoughtState.ActiveAtStage(4);

            // 사이버 사이코가 아님
            if (comp.humanity >= 80)
                return ThoughtState.ActiveAtStage(0);
            else if (comp.humanity > 40)
                return ThoughtState.ActiveAtStage(1);
            else if (comp.humanity > 30)
                return ThoughtState.ActiveAtStage(2);
            else
                return ThoughtState.ActiveAtStage(3);
        }
    }
}
