using RimWorld;
using RimWorldCyberPsychoMod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RimWorldCyberpsychoMod.Woker
{
    public class ThoughtWorker_CyberPsycho : ThoughtWorker
    {
        protected override ThoughtState CurrentStateInternal(Pawn p)
        {
            CompCP compCP = p.GetComp<CompCP>();
            if (compCP != null && compCP.humanity < 20)
            {
                return ThoughtState.ActiveDefault;
            }
            return ThoughtState.Inactive;
        }
    }
}
