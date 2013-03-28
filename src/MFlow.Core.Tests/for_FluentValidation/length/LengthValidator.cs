using MFlow.Core.Internal.Validators.Strings;
using MFlow.Core.Validation.Validators.Strings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class LengthValidator : ILengthValidator
    {
        public bool Validate(string input, int value)
        {
            return input == "customlengthvalidator";
        }
    }
}
