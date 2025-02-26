using HarmonyLib;
using RimWorldCyberPsychoMod;
using Verse;

namespace RimWorldCyberpsychoMod.Harmony
{
    [HarmonyPatch(typeof(Pawn), "PostApplyDamage")]
    static class Patch_Pawn_PostApplyDamage
    {
        static void Humanity_PostApplyDamage(Pawn __instance)
        {
            if (__instance.RaceProps.Humanlike)
            {
                CompCP humanityComp = __instance.GetComp<CompCP>();
                if (humanityComp != null)
                {
                    humanityComp.AdjustHumanityForImplants(__instance);
                }
            }
        }
    }
}
