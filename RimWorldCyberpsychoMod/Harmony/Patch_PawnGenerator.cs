using HarmonyLib;
using RimWorldCyberPsychoMod;
using Verse;

namespace RimWorldCyberpsychoMod.Harmony
{
    [HarmonyPatch(typeof(PawnGenerator), "GenerateNewPawnInternal")]
    static class Patch_PawnGenerator
    {
        [HarmonyPostfix]
        public static void Postfix(ref PawnGenerationRequest request, ref Pawn __result)
        {
            if (__result != null)
            {
                var humanityComp = __result.GetComp<CompCP>();
                if (humanityComp == null)
                {
                    humanityComp = new CompCP();
                    __result.AllComps.Add(humanityComp);
                }
                Log.Message($"Generated new pawn {__result.LabelCap} with humanity {humanityComp.humanity}");
            }
        }

    }
}
