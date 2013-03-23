using MFlow.Core.Validation;
using System;
using System.Collections.Generic;

namespace MFlow.Core.Statistics
{
    /// <summary>
    ///     A null implementation of a statistics recorder that can be used by default
    /// </summary>
    public class NullValidationStatisticsRecorder : IRecordValidationStatistics
    {
        /// <summary>
        ///     Does nothing other than run the validation
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <returns></returns>
        public ICollection<IValidationResult<T>> RunAndRecord<T>(Func<ICollection<IValidationResult<T>>> action)
        {
            return action();
        }
    }
}
