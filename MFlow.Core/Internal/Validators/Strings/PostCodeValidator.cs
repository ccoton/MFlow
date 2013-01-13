using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace MFlow.Core.Internal.Validators.Strings
{
    class PostCodeValidator : IValidator<string>
    {
        static IDictionary<string, bool> cache = new Dictionary<string, bool>();
        const string regEx = @"^([A-PR-UWYZ0-9][A-HK-Y0-9][AEHMNPRTVXY0-9]?[ABEHMNPRVWXY0-9]? {1,2}[0-9][ABD-HJLN-UW-Z]{2}|GIR 0AA)$";
        static Regex reg = new Regex(regEx, RegexOptions.IgnoreCase);

        public bool Validate(string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;
            if (!cache.ContainsKey(input))
                cache [input] = reg.IsMatch(input);
            return cache [input];
        }
    }
}
