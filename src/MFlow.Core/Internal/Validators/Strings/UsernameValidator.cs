using System.Text.RegularExpressions;
using System.Collections.Generic;
using MFlow.Core.Validation.Validators.Strings;

namespace MFlow.Core.Internal.Validators.Strings
{
    class UsernameValidator : IUsernameValidator
    {
        const string regEx = @"[A-Za-z][A-Za-z0-9._]{5,14}";
        static Regex reg = new Regex(regEx);

        public bool Validate(string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;
            return reg.IsMatch(input.ToLower());
        }
    }
}
