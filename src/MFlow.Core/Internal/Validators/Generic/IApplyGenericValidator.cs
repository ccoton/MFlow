using MFlow.Core.Conditions;
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

        IFluentCondition<T> Apply(IComparisonValidator<ICollection<TValidate>, ICollection<TValidate>> validator,
                                  Validation.Enums.ValidationType type, ICollection<TValidate> values);
    }
}
