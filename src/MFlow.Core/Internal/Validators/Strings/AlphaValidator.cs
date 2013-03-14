using System;
using System.Collections.Generic;
using System.Linq;

namespace MFlow.Core.Internal.Validators.Strings
{
    /// <summary>
    ///     Alpha Validator
    /// </summary>
    public class AlphaValidator : IAlphaValidator
    {
        public bool Validate(string input)
        {
            if (input == null)
                return false;
            return input.ToCharArray().All(c => Char.IsLetter(c));
        }
    }
}
