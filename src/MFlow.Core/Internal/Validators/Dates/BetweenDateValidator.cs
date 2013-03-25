using System;
using System.Linq;

namespace MFlow.Core.Internal.Validators.Dates
{
    /// <summary>
    ///     Between Validator
    /// </summary>
    class BetweenDateValidator : IBetweenDateValidator
    {
        public bool Validate(DateTime input, DateTime[] value)
        {
            return value.All(v => input > value[0] && input < value[1]);
        }
    }
}
