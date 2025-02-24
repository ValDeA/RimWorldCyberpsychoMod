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
    [HarmonyPatch(typeof(Pawn), "PostApplyDamage")]
    static class Patch_Pawn_PostApplyDamage
    {
        static void Humanity_PostApplyDamage(Pawn __instance)
        {
            CompCP humanityComp = __instance.GetComp<CompCP>();
            if (humanityComp != null)
            {
                humanityComp.AdjustHumanityForImplants(__instance);
            }
        }
    }
}
