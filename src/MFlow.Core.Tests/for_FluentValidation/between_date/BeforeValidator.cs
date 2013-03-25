using MFlow.Core.Internal.Validators.Dates;
using MFlow.Core.Internal.Validators.Numbers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation
{
    public class BetweenDateValidator : IBetweenValidator
    {
        public bool Validate(DateTime input, Internal.Validators.Between<DateTime> value)
        {
            return input.Year == 2000;
        }
    }
}
