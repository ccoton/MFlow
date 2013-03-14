using MFlow.Core.Conditions;

namespace MFlow.Core.Internal.Validators.Numbers
{
    interface IApplyNumericValidator<T, TValidate>
    {
        IFluentCondition<T> Apply(IComparisonValidator<int, int> validator, Validation.Enums.ValidationType type, int value);
    }
}
