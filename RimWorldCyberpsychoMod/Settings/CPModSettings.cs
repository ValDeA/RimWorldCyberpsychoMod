﻿using Verse;

namespace RimWorldCyberpsychoMod.Settings
{
    public class CPModSettings : ModSettings
    {
        public int humanityMax = 100;
        public int humanityBase = 40;

        public int cyberPsychoThreshold1 = 20; // 사이버 사이코 발병 수치
        public int cyberPsychoThreshold2 = 0;  // 사이버 사이코 영구 지속

        public override void ExposeData()
        {
            Scribe_Values.Look(ref humanityMax, "humanityMax", 100);
            Scribe_Values.Look(ref humanityBase, "humanityBase", 40);

            Scribe_Values.Look(ref cyberPsychoThreshold1, "cyberPsychoThreshold1", 20);
            Scribe_Values.Look(ref cyberPsychoThreshold2, "cyberPsychoThreshold2", 0);
            base.ExposeData();
        }
    }
}
