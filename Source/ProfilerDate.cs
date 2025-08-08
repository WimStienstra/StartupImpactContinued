using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StartupImpact
{
    class ProfilerDate : ProfilerSingleThread
    {
        DateTime startTime;

        public override void start()
        {
            startTime = DateTime.UtcNow;
        }

        public override int stop()
        {
            return (int)(DateTime.UtcNow - startTime).TotalMilliseconds;
        }

        public override int stopAndStart()
        {
            DateTime now = DateTime.UtcNow;
            int passed = (int)(now - startTime).TotalMilliseconds;
            startTime = now;
            return passed;
        }
    }
}
