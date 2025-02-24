using RimWorldCyberPsychoMod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse.AI;
using Verse;
using RimWorld;

namespace RimWorldCyberpsychoMod.Woker
{
    public class MentalStateWorker_CyberPsycho : MentalStateWorker
    {
        public override bool StateCanOccur(Pawn pawn)
        {
            if (!base.StateCanOccur(pawn))
            {
                return false;
            }

            // 인간형 림인지 확인
            if (!pawn.RaceProps.Humanlike)
            {
                return false;
            }
            // 쓰러졌거나 죽어가는 상태인지 확인
            if (pawn.Downed || pawn.health.hediffSet.HasHediff(HediffDefOf.BloodLoss))
            {
                return false;
            }

            CompCP compCP = pawn.GetComp<CompCP>();
            return compCP != null && compCP.humanity < 20; // humanity가 20 미만일 때 발생 가능
        }
    }
}
