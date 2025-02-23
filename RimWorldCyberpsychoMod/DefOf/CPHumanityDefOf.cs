using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RimWorldCyberpsychoMod
{
    [DefOf]
    public static class CPHumanityDefOf
    {
        public static StatDef Humanity;

        static CPHumanityDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(CPHumanityDefOf));
        }

    }
}
