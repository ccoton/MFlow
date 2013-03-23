using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace MFlow.Core.Internal.Validators.Strings
{
    class ZipCodeValidator : IZipCodeValidator
    {
        const string regEx = @"^[0-9]{5}(-[0-9]{4})?$";
        static Regex reg = new Regex(regEx);

        public bool Validate(string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;
            return reg.IsMatch(input.ToLower());
        }
    }
}
