using MFlow.Core.Validation.Validators.Strings;
using System;

namespace MFlow.Core.Internal.Validators.Strings
{
    public class DateValidator : IDateValidator
    {
        public bool Validate(string input)
        {
            var date = DateTime.Now;
            return DateTime.TryParse(input, out date);
        }
    }
}
