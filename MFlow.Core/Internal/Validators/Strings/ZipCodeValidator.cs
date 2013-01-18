using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace MFlow.Core.Internal.Validators.Strings
{
    class ZipCodeValidator : IZipCodeValidator
    {
        static IDictionary<string, bool> cache = new Dictionary<string, bool>();
        const string regEx = @"^[0-9]{5}(-[0-9]{4})?$";
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
