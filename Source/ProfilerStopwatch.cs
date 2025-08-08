using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace StartupImpact
{
    class ProfilerStopwatch : ProfilerSingleThread
    {
        Stopwatch stopwatch = new Stopwatch();

        public override void start()
        {
            stopwatch.Start();
        }

        public override int stop()
        {
            stopwatch.Stop();
            int ms = (int)stopwatch.Elapsed.TotalMilliseconds;
            stopwatch.Reset();
            return ms;
        }
    }
}
