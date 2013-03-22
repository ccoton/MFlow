using MFlow.Core.Internal.Validators.Numbers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class GreaterThanValidator : IGreaterThanValidator
    {
        public bool Validate(int input, int value)
        {
            return input == 100;
        }
    }
}
