﻿using MFlow.Core.Conditions;
using System;

namespace MFlow.Core.Internal.Validators.Dates
{
    interface IApplyDateValidator<T, TValidate>
    {
        IFluentCondition<T> Apply(IComparisonValidator<DateTime, DateTime> validator, Validation.Enums.ValidationType type, DateTime value);
        IFluentCondition<T> Apply(IValidator<DateTime> validator, Validation.Enums.ValidationType type);
    }
}