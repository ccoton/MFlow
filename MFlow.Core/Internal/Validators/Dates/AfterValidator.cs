using System;

namespace MFlow.Core.Internal.Validators.Dates
{
    /// <summary>
    ///     After Validator
    /// </summary>
    class AfterValidator : IComparisonValidator<DateTime, DateTime>
    {
        public bool Validate(DateTime input, DateTime value)
        {
            return input > value;
        }
    }
}
