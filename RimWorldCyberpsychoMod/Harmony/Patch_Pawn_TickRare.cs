using HarmonyLib;
using RimWorldCyberPsychoMod;
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
            if (comp != null && __instance.RaceProps.Humanlike)
            {
                comp.CheckForCyberPsycho();
            }
        }
    }
}
