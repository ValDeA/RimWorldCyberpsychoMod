using HarmonyLib;
using Verse;
using RimWorld;
using System.Linq;
using System.Reflection;
using System;

namespace RimWorldCyberPsychoMod
{
    [StaticConstructorOnStartup]
    public static class CyberPsychoMod
    {
        static CyberPsychoMod()
        {
            var harmony = new Harmony("RimWorld.CyberpsychoMod");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            Log.Message("CyberPsychoMod initialized");
        }
    }

    [HarmonyPatch(typeof(PawnGenerator), "GenerateNewPawn")]
    public static class Patch_PawnGenerator_GenerateNewPawn
    {
        public static void Postfix(Pawn __result)
        {
            if (__result != null && !__result.GetComps<HumanityComponent>().Any())
            {
                var humanityComp = new HumanityComponent();
                __result.AllComps.Add(humanityComp);
                Log.Message($"Added HumanityComponent to {__result.LabelCap}");
            }
        }
    }
}
