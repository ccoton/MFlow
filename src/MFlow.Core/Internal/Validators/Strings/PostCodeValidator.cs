using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace MFlow.Core.Internal.Validators.Strings
{
    class PostCodeValidator : IPostCodeValidator
    {
        const string regEx = @"^([A-PR-UWYZ0-9][A-HK-Y0-9][AEHMNPRTVXY0-9]?[ABEHMNPRVWXY0-9]? {1,2}[0-9][ABD-HJLN-UW-Z]{2}|GIR 0AA)$";
        static Regex reg = new Regex(regEx);

        public bool Validate(string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;
            return reg.IsMatch(input.ToUpper());
        }
    }
}
