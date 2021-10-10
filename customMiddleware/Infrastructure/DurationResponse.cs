using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace customMiddleware.Infrastructure
{
    public class DurationResponse
    {
        private Stopwatch stopwatch;
        public DurationResponse()
        {
           
        }

        public void Start()
        {

            stopwatch = Stopwatch.StartNew();
            Thread.Sleep(new Random().Next(1000, 5000));

        }

        public long Duration
        {
            get {
                stopwatch.Stop();
                return stopwatch.ElapsedMilliseconds;
            }
        }
    }
}
