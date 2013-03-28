using MFlow.Core.Conditions;
using MFlow.Core.Validation.Validators;
using System;

namespace MFlow.Core.Internal.Validators.Strings
{
    interface IApplyStringValidator<T, TValidate>
    {
        IFluentCondition<T> Apply(IComparisonValidator<string, int> validator, Validation.Enums.ValidationType type, int value);
        IFluentCondition<T> Apply(IComparisonValidator<string, string> validator, Validation.Enums.ValidationType type, string value);
        IFluentCondition<T> Apply(IValidator<string> validator, Validation.Enums.ValidationType type);
    }
}
