
using MFlow.Core.Validation.Validators.Strings;
using System;
using System.Collections.Generic;

namespace MFlow.Core.Internal.Validators.Strings
{
    /// <summary>
    ///     Numeric Validator
    /// </summary>
    public class NumericValidator : INumericValidator
    {
        public bool Validate(string input)
        {
            if (input == null)
                return false;

            var number = 0;
            return int.TryParse(input, out number);

        }
    }
}
