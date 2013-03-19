﻿using MFlow.Core.Conditions;
using MFlow.Core.Validation.Configuration;
using MFlow.Core.Validation.Context;
using MFlow.Core.Validation.Enums;
using System;
using System.Collections.Generic;
namespace MFlow.Core.Internal.Validators
{
    public interface IValidatorToCondition<T>
    {
        IFluentCondition<T> ForDateTime(ICurrentValidationContext<T> currentContext, IValidator<DateTime> validator, ValidationType type);
        IFluentCondition<T> ForDateTime(ICurrentValidationContext<T> currentContext, IComparisonValidator<DateTime, DateTime> validator, ValidationType type, DateTime value);
        ICollection<IFluentCondition<T>> ForInt(ICurrentValidationContext<T> currentContext, ICollection<IComparisonValidator<int, int>> validators, ValidationType type, int value);
        ICollection<IFluentCondition<T>> ForString(ICurrentValidationContext<T> currentContext, ICollection<IValidator<string>> validators, ValidationType type);
        ICollection<IFluentCondition<T>> ForString(ICurrentValidationContext<T> currentContext, ICollection<IComparisonValidator<string, string>> validators, ValidationType type, string value);
        ICollection<IFluentCondition<T>> ForString(ICurrentValidationContext<T> currentContext, ICollection<IComparisonValidator<string, int>> validators, ValidationType type, int value);
    }
}
