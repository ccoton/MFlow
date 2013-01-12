using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace MFlow.Core.Internal.Validators.Strings
{
    class PostCodeValidator : IValidator<string>
    {
        static IDictionary<string, bool> cache = new Dictionary<string, bool> ();
        const string regEx = @"(GIR 0AA)|((([A-Z-[QVX]][0-9][0-9]?)|(([A-Z-[QVX]][A-Z-[IJZ]][0-9][0-9]?)|(([A-Z-[QVX]][0-9][A-HJKSTUW])|([A-Z-[QVX]][A-Z-[IJZ]][0-9][ABEHMNPRVWXY])))) [0-9][A-Z-[CIKMOV]]{2})";
        static Regex reg = new Regex (regEx, RegexOptions.IgnoreCase);

        public bool Validate (string input)
        {
            if (string.IsNullOrEmpty (input))
                return false;
            if (!cache.ContainsKey (input))
                cache [input] = reg.IsMatch (input);
            return cache [input];
        }
    }
}
