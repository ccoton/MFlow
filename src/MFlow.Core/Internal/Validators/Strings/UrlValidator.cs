using System.Text.RegularExpressions;
using System.Collections.Generic;
using System;

namespace MFlow.Core.Internal.Validators.Strings
{
    class UrlValidator : IUrlValidator
    {
        public bool Validate(string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;
            Uri result;
            return Uri.TryCreate(input, UriKind.Absolute, out result) 
                && result.Scheme == Uri.UriSchemeHttp;
        }
    }
}
