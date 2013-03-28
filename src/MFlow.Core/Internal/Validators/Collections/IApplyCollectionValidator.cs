using MFlow.Core.Conditions;
using MFlow.Core.Validation.Validators;
using System.Collections.Generic;

namespace MFlow.Core.Internal.Validators.Collections
{
    interface IApplyCollectionValidator<T, TValidate>
    {
        IFluentCondition<T> Apply(IComparisonValidator<ICollection<TValidate>, TValidate> validator, Validation.Enums.ValidationType type, TValidate value);
        IFluentCondition<T> Apply(IComparisonValidator<ICollection<TValidate>, ICollection<TValidate>> validator, Validation.Enums.ValidationType type, ICollection<TValidate> values);
    }
}
