using MFlow.Core.Statistics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace MFlow.Performance.Statistics
{
    class PerformanceRecorder : IRecordValidationStatistics
    {
        public ICollection<Core.Validation.IValidationResult<T>> RunAndRecord<T>(Func<ICollection<Core.Validation.IValidationResult<T>>> action)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var results = action();
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
            return results;
        }
    }
}
