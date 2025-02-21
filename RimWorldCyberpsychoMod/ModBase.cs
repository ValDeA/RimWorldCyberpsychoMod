using System;
using System.Reflection;
using HarmonyLib;
using RimWorld;
using Verse;

namespace RimWorldCyberPsychoMod
{
    public class ModBase : Mod
    {
        public ModBase(ModContentPack content) : base(content)
        {
            LongEventHandler.QueueLongEvent(Init, "Initializing RimWorldCyberPsychoMod", false, null);
        }

        private void Init()
        {
            Log.Message("RimWorldCyberPsychoMod: Starting initialization");

            // Harmony 패치 적용
            var harmony = new Harmony("RimWorld.CyberpsychoMod");
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            Log.Message("RimWorldCyberPsychoMod: Initialization complete");
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

                // 동적으로 Humanity StatDef 가져오기
                var humanityStat = DefDatabase<StatDef>.GetNamed("Humanity", false);
                if (humanityStat != null)
                {
                    Log.Message($"Generated new pawn {__result.LabelCap} with humanity stat successfully loaded.");
                }
                else
                {
                    Log.Error($"Failed to load Humanity StatDef for pawn {__result.LabelCap}");
                }
            }
        }
    }

    [StaticConstructorOnStartup]
    public static class HumanityStatDefInitializer
    {
        static HumanityStatDefInitializer()
        {
            Log.Message("HumanityStatDefInitializer: Initializing Humanity StatDef");

            var humanityStat = new StatDef
            {
                defName = "Humanity",
                label = "Humanity",
                description = "Measure of a pawn's humanity. Not visible in-game.",
                category = StatCategoryDefOf.BasicsPawn,
                defaultBaseValue = 50f,
                minValue = 0f,
                maxValue = 200f,
                showIfUndefined = false,
                toStringStyle = ToStringStyle.Integer,
                workerClass = typeof(StatWorker_Humanity)
            };

            DefDatabase<StatDef>.Add(humanityStat);
            Log.Message("HumanityStatDefInitializer: Humanity StatDef added to DefDatabase");
        }
    }
}
