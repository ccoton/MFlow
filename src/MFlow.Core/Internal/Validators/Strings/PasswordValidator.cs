using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace MFlow.Core.Internal.Validators.Strings
{
    class PasswordValidator : IPasswordValidator
    {
        const string regEx = @"^.*(?=.{4,10})(?=.*\d)(?=.*[a-zA-Z]).*$";
        static Regex reg = new Regex(regEx);

        public bool Validate(string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;
            return reg.IsMatch(input.ToLower());
        }
    }
}
