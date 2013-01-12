using System;

namespace MFlow.Core.Internal.Validators.Dates
{
    /// <summary>
    ///     On Validator
    /// </summary>
    class OnValidator : ICompareValidator<DateTime, DateTime>
    {
        public bool Validate (DateTime input, DateTime value)
        {
            return input.Date == value.Date;
        }
    }
}
