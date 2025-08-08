using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Verse;

namespace StartupImpact
{

    public class Profiler
    {
        string what;
        public readonly ProfilerType profilerType;

        public Profiler(string w)
        {
            profilerType = StartupImpact.settings.profilerType;
            what = w;
        }

        public Dictionary<string, int> metrics = new Dictionary<string, int>();
        public int totalImpact = 0;

        public ProfilerSingleThread CreateProfiler()
        {
            switch (profilerType)
            {
                case ProfilerType.Date:
                    return new ProfilerDate() { what = what };
                case ProfilerType.Stopwatch:
                    return new ProfilerStopwatch() { what = what };
                default:
                case ProfilerType.Ticks:
                    return new ProfilerTickCount() { what = what };
            }
        }

        ProfilerSingleThread singleProfiler = null; // Since RimWorld is single-threaded, we only need one profiler

        ProfilerSingleThread threadProfiler()
        {
            // RimWorld is single-threaded, so we can optimize by using just one profiler instance
            if (singleProfiler == null)
                singleProfiler = CreateProfiler();
                
            return singleProfiler;
        }

        public void Start(string cat)
        {
            threadProfiler().Start(cat);
        }

        public int Stop(string inCat)
        {
            string cat;
            int ms = threadProfiler().Stop(inCat, out cat);

            // Since RimWorld is single-threaded, we don't need to check thread ID
            totalImpact += ms;

            int total;
            metrics.TryGetValue(cat, out total);
            total += ms;
            metrics[cat] = total;

            return ms;
        }

        public int Impact(string v)
        {
            int res;
            metrics.TryGetValue(v, out res);
            return res;
        }
    }
}
