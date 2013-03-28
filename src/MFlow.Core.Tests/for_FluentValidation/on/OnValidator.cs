using MFlow.Core.Internal.Validators.Dates;
using MFlow.Core.Internal.Validators.Numbers;
using MFlow.Core.Validation.Validators.Dates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class OnValidator : IOnValidator
    {
        public bool Validate(DateTime input, DateTime value)
        {
            return input.Year == 2000;
        }
    }
}
