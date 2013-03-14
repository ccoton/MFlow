using System;
using System.Collections.Generic;

namespace MFlow.Core.Internal.Validators.Strings
{
    /// <summary>
    ///     Length Validator
    /// </summary>
    class LengthValidator : ILengthValidator
    {
        public bool Validate(string input, int value)
        {
            if (input == null)
                return false;

            return input.Length == value;
        }
    }
}
