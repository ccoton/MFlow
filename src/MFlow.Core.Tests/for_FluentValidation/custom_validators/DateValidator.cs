using MFlow.Core.Internal.Validators.Strings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation.custom_validators
{
    public class DateValidator : IDateValidator
    {
        public bool Validate(string input)
        {
            return input == "customdatevalidator";
        }
    }
}
