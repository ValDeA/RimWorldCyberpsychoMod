using HarmonyLib;
using RimWorld;
using System.Linq;
using UnityEngine;
using Verse;

namespace RimWorldCyberPsychoMod
{
    [HarmonyPatch(typeof(CharacterCardUtility), "DrawCharacterCard")]
    public static class Patch_CharacterCardUtility_DrawCharacterCard
    {
        public static void Postfix(Rect rect, Pawn pawn, bool showInventory)
        {
            if (pawn == null || !pawn.GetComps<HumanityComponent>().Any())
                return;

            HumanityComponent humanityComp = pawn.GetComps<HumanityComponent>().First();

            float curY = rect.y + 30f;
            Rect statRect = new Rect(rect.x + 20f, curY, 200f, 20f);

            // 인간성 스탯 레이블
            Widgets.Label(statRect, "Humanity:");

            // 인간성 값
            statRect.x += 100f;
            Widgets.Label(statRect, humanityComp.humanity.ToString("F1"));

            // 인간성 바
            curY += 22f;
            Rect barRect = new Rect(rect.x + 20f, curY, 200f, 14f);
            float fillPercent = Mathf.Clamp01(humanityComp.humanity / 20f); // 20을 최대값으로 가정
            Widgets.FillableBar(barRect, fillPercent, BarTextures.HumanityBarTex);

            // 툴팁
            if (Mouse.IsOver(barRect))
            {
                TooltipHandler.TipRegion(barRect, "HumanityTooltip".Translate());
            }
        }
    }

    // 인간성 바 텍스처를 위한 정적 클래스
    public static class BarTextures
    {
        public static readonly Texture2D HumanityBarTex = SolidColorMaterials.NewSolidColorTexture(new Color(0.2f, 0.6f, 1f));
    }
}