
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MFlow.Core.Internal.Validators.Strings
{
    /// <summary>
    ///     Matches Validator
    /// </summary>
    class MatchesValidator : ICompareValidator<string, string>
    {
        static IDictionary<string, bool> cache = new Dictionary<string, bool> ();

        public bool Validate (string input, string value)
        {
            if (input == null || value == null)
                return false;

            var key = string.Format ("{0} == {1}", input, value);

            if (!cache.ContainsKey (key))
                cache [key] = new Regex (value).IsMatch (input);
            return cache [key];
        }
    }
}
