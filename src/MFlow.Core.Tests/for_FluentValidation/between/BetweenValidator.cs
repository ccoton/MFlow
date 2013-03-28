using MFlow.Core.Internal.Validators.Numbers;
using MFlow.Core.Validation.Validators.Numbers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class BetweenIntValidator : IBetweenValidator
    {
        public bool Validate(int input, Internal.Validators.Between<int> value)
        {
            return input == 100;
        }
    }
}
