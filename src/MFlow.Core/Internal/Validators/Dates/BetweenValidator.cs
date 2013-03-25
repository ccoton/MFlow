using System;

namespace MFlow.Core.Internal.Validators.Dates
{
    /// <summary>
    ///     Between Validator
    /// </summary>
    class BetweenValidator : IBetweenValidator
    {
        public bool Validate(DateTime input, Between<DateTime> value)
        {
            return input > value.Lower && input < value.Upper;
        }
    }
}
