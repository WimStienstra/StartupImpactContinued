using System;
using Verse;

namespace StartupImpact
{
    public enum ProfilerType
    {
        Ticks = 1,
        Stopwatch = 2,
        Date = 3,
    }

    public class StartupImpactSettings : ModSettings
    {
        public static bool IsMono() {
            return Type.GetType("Mono.Runtime") != null;
        }

        public ProfilerType profilerType = ProfilerType.Stopwatch; // Changed default to Stopwatch since it's now fixed and more accurate
        public bool resolveReferences = !IsMono();

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look(ref profilerType, "profilerType", ProfilerType.Stopwatch); // Updated default
            Scribe_Values.Look(ref resolveReferences, "resolveReferences", !IsMono());
        }
    }
}
