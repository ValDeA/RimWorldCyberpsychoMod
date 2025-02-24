using RimWorld;

namespace RimWorldCyberpsychoMod
{
    [DefOf]
    public static class CPHumanityDefOf
    {
        public static StatDef Humanity;

        static CPHumanityDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(CPHumanityDefOf));
        }

    }
}