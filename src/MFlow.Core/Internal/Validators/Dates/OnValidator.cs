using MFlow.Core.Validation.Validators.Dates;
using System;

namespace MFlow.Core.Internal.Validators.Dates
{
    /// <summary>
    ///     On Validator
    /// </summary>
    class OnValidator : IOnValidator
    {
        public bool Validate(DateTime input, DateTime value)
        {
            return input.Date == value.Date;
        }
    }
}
