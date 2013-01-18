using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace MFlow.Core.Internal.Validators.Strings
{
    class EmailValidator : IEmailValidator
    {
        static IDictionary<string, bool> cache = new Dictionary<string, bool>();
        const string regEx = @"^(?:[\w\!\#\$\%\&\'\*\+\-\/\=\?\^\`\{\|\}\~]+\.)*[\w\!\#\$\%\&\'\*\+\-\/\=\?\^\`\{\|\}\~]+@(?:(?:(?:[a-zA-Z0-9](?:[a-zA-Z0-9\-](?!\.)){0,61}[a-zA-Z0-9]?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9\-](?!$)){0,61}[a-zA-Z0-9]?)|(?:\[(?:(?:[01]?\d{1,2}|2[0-4]\d|25[0-5])\.){3}(?:[01]?\d{1,2}|2[0-4]\d|25[0-5])\]))$";
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
