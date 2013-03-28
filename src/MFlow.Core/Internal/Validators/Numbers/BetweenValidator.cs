using MFlow.Core.Validation.Validators.Numbers;
using System;

namespace MFlow.Core.Internal.Validators.Numbers
{
    /// <summary>
    ///     Between Validator
    /// </summary>
    class BetweenValidator : IBetweenValidator
    {
        public bool Validate(int input, Between<int> value)
        {
            return input > value.Lower && input < value.Upper;
        }
    }
}
