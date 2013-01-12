using System;

namespace MFlow.Core.Internal.Validators.Dates
{
    /// <summary>
    ///     Before Validator
    /// </summary>
    class BeforeValidator : ICompareValidator<DateTime, DateTime>
    {
        public bool Validate (DateTime input, DateTime value)
        {
            return input < value;
        }
    }
}
