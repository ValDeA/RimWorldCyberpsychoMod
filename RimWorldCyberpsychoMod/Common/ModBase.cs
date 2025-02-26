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
            Harmony.DEBUG = true;
            Log.Message("RimWorldCyberPsychoMod: Starting initialization");

            // Harmony 패치 적용
            var harmony = new Harmony("Cyberpsycho");
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            Log.Message("RimWorldCyberPsychoMod: Initialization complete");
        }

        public static bool HasSpecificMood(Pawn pawn, string thoughtDefName)
        {
            if (pawn == null || pawn.needs?.mood?.thoughts?.memories == null)
            {
                return false;
            }

            ThoughtDef thoughtDef = DefDatabase<ThoughtDef>.GetNamed(thoughtDefName, false);
            if (thoughtDef == null)
            {
                Log.Warning($"ThoughtDef '{thoughtDefName}' not found.");
                return false;
            }

            return pawn.needs.mood.thoughts.memories.Memories.Any(memory => memory.def == thoughtDef);
        }

    }
}
