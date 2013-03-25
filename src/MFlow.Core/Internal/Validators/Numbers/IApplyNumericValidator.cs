using MFlow.Core.Conditions;

namespace MFlow.Core.Internal.Validators.Numbers
{
    public interface IApplyNumericValidator<T, TValidate>
    {
        IFluentCondition<T> Apply(IComparisonValidator<int, int> validator, Validation.Enums.ValidationType type, int value);
        IFluentCondition<T> Apply(IComparisonValidator<int, Between<int>> validator, Validation.Enums.ValidationType type, int lower, int upper);
    }
}
