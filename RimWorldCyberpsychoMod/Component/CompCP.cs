using RimWorld;
using System.Linq;
using Verse;
using RimWorldCyberpsychoMod.State;

namespace RimWorldCyberPsychoMod
{
    public class CompCP : ThingComp
    {
        private int cooldown_ticks = 60000;

        public int humanity;
        public int ticksSinceLastCyberPsycho = -1;

        public override void Initialize(CompProperties props)
        {
            base.Initialize(props);
            if (IsValidPawn(out Pawn pawn))
            {
                humanity = Rand.Range(CPMod.settings.humanityBase, CPMod.settings.humanityMax);
                AdjustHumanityForImplants(pawn);
            }
        }

        public override void PostExposeData()
        {
            base.PostExposeData();

            Scribe_Values.Look(ref humanity, CPMod.SETTINGS_HUMANITY_BASE, CPMod.settings.humanityBase);
            Scribe_Values.Look(ref ticksSinceLastCyberPsycho, CPMod.SETTINGS_LAST_CP, -1);
        }

        public override void CompTick()
        {
            base.CompTick();
            if (ticksSinceLastCyberPsycho >= 0)
            {
                ticksSinceLastCyberPsycho++;
            }
        }

        public void AdjustHumanity(int amount)
        {
            if (IsValidPawn(out Pawn pawn))
            {
                humanity = ClampValue(humanity + amount, -9999, CPMod.settings.humanityMax);
                CheckForCyberPsycho();
                Log.Message($"Adjusted humanity for {pawn.LabelCap} to {humanity}");
            }
        }

        public void AdjustHumanityForImplants(Pawn pawn)
        {
            int humanityReduction = pawn.health.hediffSet.hediffs
                .OfType<Hediff_AddedPart>()
                .Sum(part => GetHumanityReductionForPart(part));

            AdjustHumanity(-humanityReduction);
        }
        public void CheckForCyberPsycho()
        {
            // 팩션을 찾은 후 콜
            if (Find.FactionManager != null && Find.FactionManager.OfPlayer != null)
            {
                if (parent is Pawn pawn && IsValidPawn(out pawn))
                {
                    if (humanity < CPMod.settings.cyberPsychoThreshold1 && CanEnterCyberPsycho(pawn))
                    {
                        pawn.mindState.mentalStateHandler.TryStartMentalState(DefDatabase<MentalStateDef>.GetNamed(MentalState_CyberPsycho.MENTALSTATE_CYBERPSYCHO), null, true);
                        ResetCyberPsychoCooldown();

                        if(humanity < CPMod.settings.cyberPsychoThreshold2)
                        {
                            cooldown_ticks = 1;
                        }
                    }
                }
            }
        }
        public void ResetCyberPsychoCooldown()
        {
            ticksSinceLastCyberPsycho = 0;
        }

        private int GetHumanityReductionForPart(Hediff_AddedPart part)
        {
            if (part.def.spawnThingOnRemoved == null)
            {
                return 0;
            }

            TechLevel techLevel = part.def.spawnThingOnRemoved.techLevel;

            switch (techLevel)
            {
                case TechLevel.Spacer:
                    return 10;
                case TechLevel.Ultra:
                    return 15;
                case TechLevel.Archotech:
                    return 20;
                default:
                    return 5;
            }
        }
        private bool CanEnterCyberPsycho(Pawn pawn) =>
            !pawn.InMentalState && !pawn.Downed && !pawn.Dead && (ticksSinceLastCyberPsycho == -1 || ticksSinceLastCyberPsycho > cooldown_ticks);

        private bool IsValidPawn(out Pawn pawn)
        {
            pawn = parent as Pawn;

            if(CPMod.settings.isValidColonist)
            {
                return pawn != null && pawn.RaceProps.Humanlike && !pawn.Dead && pawn.Faction != null && pawn.Faction.IsPlayer;
            }
            else
            {
                return pawn != null && pawn.RaceProps.Humanlike && !pawn.Dead;
            }
        }

        int ClampValue(int value, int min, int max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }
    }
}