using MFlow.Core.Validation.Validators.Dates;
using System;

namespace MFlow.Core.Internal.Validators.Dates
{
    /// <summary>
    ///     Before Validator
    /// </summary>
    class BeforeValidator : IBeforeValidator
    {
        public bool Validate(DateTime input, DateTime value)
        {
            return input < value;
        }
    }
}
