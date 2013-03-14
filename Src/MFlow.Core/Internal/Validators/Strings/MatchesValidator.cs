
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MFlow.Core.Internal.Validators.Strings
{
    /// <summary>
    ///     Matches Validator
    /// </summary>
    class MatchesValidator : IMatchesValidator
    {
        public bool Validate(string input, string value)
        {
            if (input == null || value == null)
                return false;
            return new Regex(value).IsMatch(input);
        }
    }
}
