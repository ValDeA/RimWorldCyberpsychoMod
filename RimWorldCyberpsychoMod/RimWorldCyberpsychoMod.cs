using HarmonyLib;
using Verse;
using RimWorld;
using System.Linq;

namespace RimWorldCyberPsychoMod
{
    [StaticConstructorOnStartup]
    public static class CyberPsychoMod
    {
        static CyberPsychoMod()
        {
            var harmony = new Harmony("com.yourname.rimworldcyberpsychomod");
            harmony.PatchAll();
        }
    }

    [HarmonyPatch(typeof(PawnGenerator), "GenerateNewPawn")]
    public static class Patch_PawnGenerator_GenerateNewPawn
    {
        public static void Postfix(Pawn __result)
        {
            if (__result != null && !__result.GetComps<HumanityComponent>().Any())
            {
                __result.AllComps.Add(new HumanityComponent());
            }
        }
    }
}
