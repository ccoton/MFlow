﻿using MFlow.Core.Internal.Validators.Dates;
using MFlow.Core.Internal.Validators.Strings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_FluentValidation.custom_validators
{
    public class ThisMonthValidator : IThisMonthValidator
    {
        public bool Validate(DateTime input)
        {
            return input == DateTime.Parse("01-01-2001");
        }
    }
}
