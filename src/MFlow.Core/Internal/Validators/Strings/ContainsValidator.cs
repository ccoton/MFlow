using System;
using System.Collections.Generic;

namespace MFlow.Core.Internal.Validators.Strings
{
    /// <summary>
    ///     Contains Validator
    /// </summary>
    class ContainsValidator : IContainsValidator
    {       
        public bool Validate(string input, string value)
        {
            if (input == null || value == null)
                return false;
        
            return input.Contains(value);
        }
    }
}
