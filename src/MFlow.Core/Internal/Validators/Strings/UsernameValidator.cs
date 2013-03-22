using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace MFlow.Core.Internal.Validators.Strings
{
    class UsernameValidator : IUsernameValidator
    {
        const string regEx = @"[A-Za-z][A-Za-z0-9._]{5,14}";
        static Regex reg = new Regex(regEx, RegexOptions.IgnoreCase);

        public bool Validate(string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;
            return reg.IsMatch(input);
        }
    }
}
