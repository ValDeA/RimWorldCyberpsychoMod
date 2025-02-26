using RimWorld;
using System;
using Verse.AI;
using Verse;
using RimWorldCyberPsychoMod;

namespace RimWorldCyberpsychoMod.State
{
    public class MentalState_CyberPsycho : MentalState
    {
        public const string MENTALSTATE_CYBERPSYCHO = "MentalStateCyberPsycho";
        public const string HEDIFF_CYBERPSYCHOBOOST = "CyberPsychoBoost";
        public const string THOUGHT_HUMANITY_LEVEL = "HumanityLevelThought";

        private const float ATTACK_CHANCE = 0.1f;
        private const float SEARCH_RADIUS = 10f;

        public override RandomSocialMode SocialModeMax()
        {
            return RandomSocialMode.Off;
        }
        public override void PostStart(string reason)
        {
            base.PostStart(reason);
            ApplyCyberPsychoHediff();

            // 사이버 사이코 상태 진입 시 관련 무드 제거
            if(ModBase.HasSpecificMood(pawn, THOUGHT_HUMANITY_LEVEL))
            {
                pawn.needs.mood.thoughts.memories.RemoveMemoriesOfDef(ThoughtDef.Named(THOUGHT_HUMANITY_LEVEL));
            }
        }
        public override void PostEnd()
        {
            base.PostEnd();

            CompCP comp = pawn.GetComp<CompCP>();
            if (comp == null) return;

            if (comp.humanity >= CPMod.settings.cyberPsychoThreshold2)
            {
                comp.ResetCyberPsychoCooldown();
                RemoveCyberPsychoHediff();

                // 사이버 사이코 관련 무드 갱신
                UpdatePsychoMood();
            }
            else
            {
                // 인간성이 cyberPsychoThreshold2 미만일 때 상태 종료를 막음
                pawn.mindState.mentalStateHandler.TryStartMentalState(def, null, true);
            }
        }
        public override void MentalStateTick()
        {
            base.MentalStateTick();

            CompCP comp = pawn.GetComp<CompCP>();
            if (comp != null)
            {
                if (comp.humanity <= CPMod.settings.cyberPsychoThreshold2)
                {
                    // 인간성이 cyberPsychoThreshold2 미만일 때 회복 불가능하게 설정
                    def.recoveryMtbDays = float.PositiveInfinity;
                }
            }

            if (pawn != null && pawn.Map != null)
            {
                // 림이 쓰러지거나 행동 불가 상태인지 확인
                if (pawn.Downed || pawn.health.Dead)
                {
                    RecoverFromState(); // 정신 상태 강제 해제
                }
                else
                {
                    if (Rand.Chance(ATTACK_CHANCE))
                    {
                        TryAttackNearbyPawn();
                    }
                }
            } 
        }

        private void UpdatePsychoMood()
        {
            // 상황적 사고 업데이트
            pawn.needs.mood?.thoughts?.situational?.Notify_SituationalThoughtsDirty();
        }
        private void TryAttackNearbyPawn()
        {
            if (pawn == null || pawn.Map == null)
            {
                return;
            }

            Pawn targetPawn = FindNearbyTarget();
            if (targetPawn != null)
            {
                Job attackJob = JobMaker.MakeJob(JobDefOf.AttackMelee, targetPawn);
                pawn.jobs.StartJob(attackJob, JobCondition.InterruptForced);
            }
        }
        private Pawn FindNearbyTarget()
        {
            if (pawn == null || pawn.Map == null)
            {
                return null;
            }

            return (Pawn)GenClosest.ClosestThingReachable(pawn.Position, pawn.Map,
                ThingRequest.ForGroup(ThingRequestGroup.Pawn),
                PathEndMode.Touch, TraverseParms.For(pawn), SEARCH_RADIUS,
                (Thing x) => x is Pawn p && p != pawn && !p.Dead && !p.Downed && pawn.CanReach(p, PathEndMode.Touch, Danger.Deadly));
        }

        private void ApplyCyberPsychoHediff()
        {
            Hediff hediff = HediffMaker.MakeHediff(DefDatabase<HediffDef>.GetNamed(HEDIFF_CYBERPSYCHOBOOST), pawn);
            pawn.health.AddHediff(hediff);
        }

        private void RemoveCyberPsychoHediff()
        {
            Hediff hediff = pawn.health.hediffSet.GetFirstHediffOfDef(DefDatabase<HediffDef>.GetNamed(HEDIFF_CYBERPSYCHOBOOST));
            if (hediff != null)
            {
                pawn.health.RemoveHediff(hediff);
            }
        }
    }
}
