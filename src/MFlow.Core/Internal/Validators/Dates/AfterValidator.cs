using MFlow.Core.Validation.Validators.Dates;
using System;

namespace MFlow.Core.Internal.Validators.Dates
{
    /// <summary>
    ///     After Validator
    /// </summary>
    class AfterValidator : IAfterValidator
    {
        public bool Validate(DateTime input, DateTime value)
        {
            return input > value;
        }
    }
}
