﻿using MFlow.Core.Conditions;
using MFlow.Core.Validation.Context;
using MFlow.Core.Validation.Enums;
using System;
namespace MFlow.Core.Internal.Validators
{
    interface IValidatorToCondition<T>
    {
        IFluentCondition<T> ForDateTime(ICurrentValidationContext<T> currentContext, IValidator<DateTime> validator, ValidationType type);
        IFluentCondition<T> ForDateTime(ICurrentValidationContext<T> currentContext, IComparisonValidator<DateTime, DateTime> validator, ValidationType type, DateTime value);
        IFluentCondition<T> ForInt(ICurrentValidationContext<T> currentContext, IComparisonValidator<int, int> validator, ValidationType type, int value);
    }
}