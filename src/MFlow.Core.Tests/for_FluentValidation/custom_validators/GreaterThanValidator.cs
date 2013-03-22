using MFlow.Core.Internal.Validators.Numbers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation.custom_validators
{
    public class GreaterThanValidator : ILessThanValidator
    {
        public bool Validate(int input, int value)
        {
            return input == 100;
        }
    }
}
