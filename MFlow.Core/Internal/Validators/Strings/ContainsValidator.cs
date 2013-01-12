using System;
using System.Collections.Generic;

namespace MFlow.Core.Internal.Validators.Strings
{
    /// <summary>
    ///     Contains Validator
    /// </summary>
    class ContainsValidator : IComparisonValidator<string, string>
    {
        static IDictionary<string, bool> cache = new Dictionary<string, bool>();
        
        public bool Validate(string input, string value)
        {
            if (input == null || value == null)
                return false;
        
            var key = string.Format("{0} == {1}", input, value);
        
            if (!cache.ContainsKey(key))
                cache [key] = input.Contains(value);
            return cache [key];
        }
    }
}
