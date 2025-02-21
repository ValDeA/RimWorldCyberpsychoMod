using System.Reflection;
using HarmonyLib;
using RimWorld;
using Verse;

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

    [HarmonyPatch(typeof(PawnGenerator))]
    [HarmonyPatch("GenerateNewPawn")]
    public static class Patch_PawnGenerator_GenerateNewPawn
    {
        [HarmonyPostfix]
        public static void Postfix(ref Pawn __result)
        {
            if (__result != null)
            {
                var humanityComp = __result.GetComp<HumanityComponent>();
                if (humanityComp == null)
                {
                    humanityComp = new HumanityComponent();
                    __result.AllComps.Add(humanityComp);
                }
                Log.Message($"Generated new pawn {__result.LabelCap} with humanity {humanityComp.humanity}");
            }
        }
    }

    [DefOf]
    public static class HumanityStatDefOf
    {
        public static StatDef Humanity;

        static HumanityStatDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(HumanityStatDefOf));
        }
    }

    [StaticConstructorOnStartup]
    public static class HumanityStatDefInitializer
    {
        static HumanityStatDefInitializer()
        {
            HumanityStatDefOf.Humanity = new StatDef
            {
                defName = "Humanity",
                label = "Humanity",
                description = "Measure of a pawn's humanity. Not visible in-game.",
                category = StatCategoryDefOf.BasicsPawn,
                defaultBaseValue = 50f,
                minValue = 0f,
                maxValue = 200f,
                showIfUndefined = false,
                toStringStyle = ToStringStyle.Integer
            };

            DefDatabase<StatDef>.Add(HumanityStatDefOf.Humanity);
        }
    }
}
