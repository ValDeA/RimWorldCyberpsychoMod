using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse.AI;
using Verse;

namespace RimWorldCyberpsychoMod.State
{
    public class MentalState_CyberPsycho : MentalState
    {
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
        }
        public override void PostEnd()
        {
            base.PostEnd();
            RemoveCyberPsychoHediff();
        }
        public override void MentalStateTick()
        {
            base.MentalStateTick();

            if (pawn != null && pawn.Map != null && Rand.Chance(ATTACK_CHANCE))
            {
                TryAttackNearbyPawn();
            }
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
            Hediff hediff = HediffMaker.MakeHediff(DefDatabase<HediffDef>.GetNamed("CyberPsychoBoost"), pawn);
            pawn.health.AddHediff(hediff);
        }

        private void RemoveCyberPsychoHediff()
        {
            Hediff hediff = pawn.health.hediffSet.GetFirstHediffOfDef(DefDatabase<HediffDef>.GetNamed("CyberPsychoBoost"));
            if (hediff != null)
            {
                pawn.health.RemoveHediff(hediff);
            }
        }
    }
}
