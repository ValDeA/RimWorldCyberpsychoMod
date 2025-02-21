using RimWorld;
using Verse;
using HarmonyLib;
using System.Collections.Generic;

namespace RimWorldCyberPsychoMod
{
    [StaticConstructorOnStartup]
    public static class HumanityStatDefInitializer
    {
        static HumanityStatDefInitializer()
        {
            // 새로운 HumanityStatDef 생성
            StatDef humanityStat = new StatDef
            {
                defName = "Humanity",
                label = "Humanity",
                description = "Measure of a pawn's humanity. Lower values increase risk of cyber psychosis.",
                category = StatCategoryDefOf.BasicsPawn,
                defaultBaseValue = 10f,
                minValue = 0f,
                maxValue = 20f,
                showIfUndefined = false,
                toStringStyle = ToStringStyle.Integer
            };

            // StatDef 등록
            DefDatabase<StatDef>.Add(humanityStat);

            // 림의 기본 스탯 목록에 Humanity 추가
            var harmony = new Harmony("com.yourname.rimworldcyberpsychomod");
            harmony.Patch(AccessTools.Method(typeof(PawnCapacityDef), "GetListForReading"),
                postfix: new HarmonyMethod(typeof(HumanityStatDefInitializer), nameof(AddHumanityToStatList)));
        }

        public static void AddHumanityToStatList(ref List<PawnCapacityDef> __result)
        {
            if (!__result.Any(cap => cap.defName == "Humanity"))
            {
                __result.Add(DefDatabase<PawnCapacityDef>.GetNamed("Humanity"));
            }
        }
    }

}
