using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using HarmonyLib;
using RimWorld;
using Verse;
using static UnityEngine.Experimental.Rendering.RayTracingAccelerationStructure;

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
            Harmony.DEBUG = true;
            Log.Message("RimWorldCyberPsychoMod: Starting initialization");

            // HumanityStatDefInitializer의 정적 생성자 호출을 보장
            // RuntimeHelpers.RunClassConstructor(typeof(HumanityStatDefInitializer).TypeHandle);

            // Harmony 패치 적용
            var harmony = new Harmony("Cyberpsycho");
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            Log.Message("RimWorldCyberPsychoMod: Initialization complete");
        }

        /*
        internal static void CheckIncompatibleMods()
        {
            //--Log.Message("First::CheckIncompatibleMods() called");
            List<string> incompatible_mods = new List<string> {
                "Bogus Test Mod That Doesn't Exist",
                "Lost Forest"
            };
            foreach (string incompatible_mod in incompatible_mods)
            {
                foreach (ModMetaData installed_mod in ModLister.AllInstalledMods)
                {
                    if (installed_mod.Active && installed_mod.Name.Contains(incompatible_mod))
                    {
                        ErrorIncompatibleMod(installed_mod);
                    }
                }
            }
        }

        private static void CheckingCompatibleMods()
        {
            {//Humanoid Alien Races Framework 2.0
                xxx.xenophobia = DefDatabase<TraitDef>.GetNamedSilentFail("Xenophobia");
                if (xxx.xenophobia is null)
                {
                    xxx.AlienFrameworkIsActive = false;
                }
                else
                {
                    xxx.AlienFrameworkIsActive = true;
                    if (RJWSettings.DevMode) ModLog.Message("Humanoid Alien Races 2.0 is detected. Xenophile and Xenophobe traits active.");
                }
            }

            {//relations-orientation mods
                {//RomanceDiversified
                    xxx.straight = DefDatabase<TraitDef>.GetNamedSilentFail("Straight");
                    xxx.faithful = DefDatabase<TraitDef>.GetNamedSilentFail("Faithful");
                    xxx.philanderer = DefDatabase<TraitDef>.GetNamedSilentFail("Philanderer");
                    xxx.polyamorous = DefDatabase<TraitDef>.GetNamedSilentFail("Polyamorous");
                    if (xxx.straight is null || xxx.faithful is null || xxx.philanderer is null)
                    {
                        xxx.RomanceDiversifiedIsActive = false;
                    }
                    else
                    {
                        xxx.RomanceDiversifiedIsActive = true;
                        if (RJWSettings.DevMode) ModLog.Message("RomanceDiversified is detected.");
                    }

                    // any mod that features a polyamorous trait
                    if (xxx.polyamorous is null)
                    {
                        xxx.AnyPolyamoryModIsActive = false;
                    }
                    else
                    {
                        xxx.AnyPolyamoryModIsActive = true;
                    }
                }

                {//[SYR] Individuality
                    xxx.SYR_CreativeThinker = DefDatabase<TraitDef>.GetNamedSilentFail("SYR_CreativeThinker");
                    xxx.SYR_Haggler = DefDatabase<TraitDef>.GetNamedSilentFail("SYR_Haggler");
                    if (xxx.SYR_CreativeThinker is null || xxx.SYR_Haggler is null)
                    {
                        xxx.IndividualityIsActive = false;
                        if (RJWPreferenceSettings.sexuality_distribution == RJWPreferenceSettings.Rjw_sexuality.SYRIndividuality)
                            RJWPreferenceSettings.sexuality_distribution = RJWPreferenceSettings.Rjw_sexuality.Vanilla;
                    }
                    else
                    {
                        xxx.IndividualityIsActive = true;
                        if (RJWSettings.DevMode) ModLog.Message("Individuality is detected.");
                    }
                }

                {//Psychology
                    xxx.prude = DefDatabase<TraitDef>.GetNamedSilentFail("Prude");
                    xxx.lecher = DefDatabase<TraitDef>.GetNamedSilentFail("Lecher");
                    xxx.polygamous = DefDatabase<TraitDef>.GetNamedSilentFail("Polygamous");
                    if (xxx.prude is null || xxx.lecher is null || xxx.polygamous is null)
                    {
                        xxx.PsychologyIsActive = false;
                        if (RJWPreferenceSettings.sexuality_distribution == RJWPreferenceSettings.Rjw_sexuality.Psychology)
                            RJWPreferenceSettings.sexuality_distribution = RJWPreferenceSettings.Rjw_sexuality.Vanilla;
                    }
                    else
                    {
                        xxx.PsychologyIsActive = true;
                        if (RJWSettings.DevMode) ModLog.Message("Psychology is detected. (Note: only partially supported)");
                    }

                    // any mod that features a polygamous trait
                    if (xxx.polygamous is null)
                    {
                        xxx.AnyPolygamyModIsActive = false;
                    }
                    else
                    {
                        xxx.AnyPolygamyModIsActive = true;
                    }
                }

                if (xxx.PsychologyIsActive == false && xxx.IndividualityIsActive == false)
                {
                    if (RJWPreferenceSettings.sexuality_distribution != RJWPreferenceSettings.Rjw_sexuality.Vanilla)
                    {
                        RJWPreferenceSettings.sexuality_distribution = RJWPreferenceSettings.Rjw_sexuality.Vanilla;
                    }
                }
            }

            {//SimpleSlavery
                xxx.Enslaved = DefDatabase<HediffDef>.GetNamedSilentFail("Enslaved");
                if (xxx.Enslaved is null)
                {
                    xxx.SimpleSlaveryIsActive = false;
                }
                else
                {
                    xxx.SimpleSlaveryIsActive = true;
                    if (RJWSettings.DevMode) ModLog.Message("SimpleSlavery is detected.");
                }
            }

            {//[KV] Consolidated Traits
                xxx.RCT_NeatFreak = DefDatabase<TraitDef>.GetNamedSilentFail("RCT_NeatFreak");
                xxx.RCT_Savant = DefDatabase<TraitDef>.GetNamedSilentFail("RCT_Savant");
                xxx.RCT_Inventor = DefDatabase<TraitDef>.GetNamedSilentFail("RCT_Inventor");
                xxx.RCT_AnimalLover = DefDatabase<TraitDef>.GetNamedSilentFail("RCT_AnimalLover");
                if (xxx.RCT_NeatFreak is null || xxx.RCT_Savant is null || xxx.RCT_Inventor is null || xxx.RCT_AnimalLover is null)
                {
                    xxx.CTIsActive = false;
                }
                else
                {
                    xxx.CTIsActive = true;
                    if (RJWSettings.DevMode) ModLog.Message("Consolidated Traits found, adding trait compatibility.");
                }
            }


            {//Rimworld of Magic
                xxx.Succubus = DefDatabase<TraitDef>.GetNamedSilentFail("Succubus");
                xxx.Warlock = DefDatabase<TraitDef>.GetNamedSilentFail("Warlock");
                xxx.TM_Mana = DefDatabase<NeedDef>.GetNamedSilentFail("TM_Mana");
                xxx.TM_ShapeshiftHD = DefDatabase<HediffDef>.GetNamedSilentFail("TM_ShapeshiftHD");
                if (xxx.Succubus is null || xxx.Warlock is null || xxx.TM_Mana is null || xxx.TM_ShapeshiftHD is null)
                {
                    xxx.RoMIsActive = false;
                }
                else
                {
                    xxx.RoMIsActive = true;
                    if (RJWSettings.DevMode) ModLog.Message("Rimworld of Magic is detected.");
                }
            }


            {//Nightmare Incarnation
                xxx.NI_Need_Mana = DefDatabase<NeedDef>.GetNamedSilentFail("NI_Need_Mana");
                if (xxx.NI_Need_Mana is null)
                {
                    xxx.NightmareIncarnationIsActive = false;
                }
                else
                {
                    xxx.NightmareIncarnationIsActive = true;
                    if (RJWSettings.DevMode) ModLog.Message("Nightmare Incarnation is detected.");
                }
            }

            {//DubsBadHygiene
                xxx.DBHThirst = DefDatabase<NeedDef>.GetNamedSilentFail("DBHThirst");
                if (xxx.DBHThirst is null)
                {
                    xxx.DubsBadHygieneIsActive = false;
                }
                else
                {
                    xxx.DubsBadHygieneIsActive = true;
                    if (RJWSettings.DevMode) ModLog.Message("Dubs Bad Hygiene is detected.");
                }
            }

            {//Immortals
                xxx.IH_Immortal = DefDatabase<HediffDef>.GetNamedSilentFail("IH_Immortal");
                if (xxx.IH_Immortal is null)
                {
                    xxx.ImmortalsIsActive = false;
                }
                else
                {
                    xxx.ImmortalsIsActive = true;
                    if (RJWSettings.DevMode) ModLog.Message("Immortals is detected.");
                }
            }

            {// RJW-EX
                xxx.RjwEx_Armbinder = DefDatabase<HediffDef>.GetNamedSilentFail("Armbinder");
                if (xxx.RjwEx_Armbinder is null) { xxx.RjwExIsActive = false; }
                else { xxx.RjwExIsActive = true; }

            }

            {//Combat Extended
                xxx.MuscleSpasms = DefDatabase<HediffDef>.GetNamedSilentFail("MuscleSpasms");
                if (xxx.MuscleSpasms is null)
                {
                    xxx.CombatExtendedIsActive = false;
                }
                else
                {
                    xxx.CombatExtendedIsActive = true;
                    if (RJWSettings.DevMode) ModLog.Message("Combat Extended is detected. Current compatibility unknown, use at your own risk. ");
                }
            }
        }
        */

    }


    /*
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
        }*/
}
