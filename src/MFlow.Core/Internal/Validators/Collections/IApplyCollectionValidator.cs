using MFlow.Core.Conditions;
using System.Collections.Generic;

namespace MFlow.Core.Internal.Validators.Collections
{
    interface IApplyCollectionValidator<T, TValidate>
    {
        IFluentCondition<T> Apply(IComparisonValidator<ICollection<TValidate>, TValidate> validator, Validation.Enums.ValidationType type, TValidate value);
    }
}
