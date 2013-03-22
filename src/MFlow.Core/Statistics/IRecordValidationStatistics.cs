using MFlow.Core.Validation;
using System;
using System.Collections.Generic;

namespace MFlow.Core.Statistics
{
    /// <summary>
    ///     A contract representing something that can record validation statistics
    /// </summary>
    public interface IRecordValidationStatistics
    {
        /// <summary>
        ///     Run a validation function and record something
        /// </summary>
        ICollection<IValidationResult<T>> RunAndRecord<T>(Func<ICollection<IValidationResult<T>>> action);
    }
}
