using RimWorldCyberpsychoMod.Settings;
using UnityEngine;
using Verse;

namespace RimWorldCyberPsychoMod
{
    public class CPMod : Mod
    {
        private const string SETTINGS_CATEGOTY = "Cyber Psycho Mod";

        public const string SETTINGS_HUMANITY_BASE = "CP_Humanity";
        public const string SETTINGS_LAST_CP = "CP_LastCyberPsycho";

        public static CPModSettings settings;

        public CPMod(ModContentPack content) : base(content)
        {
            settings = GetSettings<CPModSettings>();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(inRect);

            listingStandard.Label("HumanityBase".Translate() + settings.humanityBase);
            settings.humanityBase = (int)listingStandard.Slider(settings.humanityBase, 0, settings.humanityMax);
            listingStandard.Label("HumanityMax".Translate() + settings.humanityMax);
            settings.humanityMax = (int)listingStandard.Slider(settings.humanityMax, settings.humanityBase, 200);

            listingStandard.Label("CyberPsychoThreshold1".Translate() + settings.cyberPsychoThreshold1);
            settings.cyberPsychoThreshold1 = (int)listingStandard.Slider(settings.cyberPsychoThreshold1, 0, settings.humanityBase);
            listingStandard.Label("CyberPsychoThreshold2".Translate() + settings.cyberPsychoThreshold2);
            settings.cyberPsychoThreshold2 = (int)listingStandard.Slider(settings.cyberPsychoThreshold2, -100, settings.cyberPsychoThreshold1);

            listingStandard.End();
            base.DoSettingsWindowContents(inRect);
        }

        public override string SettingsCategory()
        {
            return SETTINGS_CATEGOTY;
        }
    }
}
