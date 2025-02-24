using HarmonyLib;
using RimWorldCyberPsychoMod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RimWorldCyberpsychoMod.Harmony
{
    [HarmonyPatch(typeof(Pawn), "TickRare")]
    public static class Patch_Pawn_TickRare
    {
        [HarmonyPostfix]
        public static void CheckCyberPsycho(Pawn __instance)
        {
            CompCP comp = __instance.GetComp<CompCP>();
            if (comp != null)
            {
                comp.CheckForCyberPsycho();
            }
        }
    }
}
