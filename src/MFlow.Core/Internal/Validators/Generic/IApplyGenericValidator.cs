using MFlow.Core.Conditions;
using MFlow.Core.Validation.Validators;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MFlow.Core.Internal.Validators.Generic
{
    interface IApplyGenericValidator<T, TValidate>
    {
        IFluentCondition<T> Apply(IComparisonValidator<TValidate, TValidate> validator, Validation.Enums.ValidationType type, TValidate value);
        IFluentCondition<T> Apply(IComparisonValidator<TValidate, TValidate> validator, Validation.Enums.ValidationType type, Expression<Func<T, TValidate>> value);
        IFluentCondition<T> Apply(IValidator<TValidate> validator, Validation.Enums.ValidationType type);
    }
}
