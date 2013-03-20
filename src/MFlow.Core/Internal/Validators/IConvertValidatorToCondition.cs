using MFlow.Core.Conditions;
using MFlow.Core.Validation.Configuration;
using MFlow.Core.Validation.Context;
using MFlow.Core.Validation.Enums;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
namespace MFlow.Core.Internal.Validators
{
    public interface IConvertValidatorToCondition<T>
    {
        ICollection<IFluentCondition<T>> ForDateTime(ICurrentValidationContext<T> currentContext, ICollection<IValidator<DateTime>> validators, ValidationType type);
        ICollection<IFluentCondition<T>> ForDateTime(ICurrentValidationContext<T> currentContext, ICollection<IComparisonValidator<DateTime, DateTime>> validators, ValidationType type, DateTime value);
        ICollection<IFluentCondition<T>> ForInt(ICurrentValidationContext<T> currentContext, ICollection<IComparisonValidator<int, int>> validators, ValidationType type, int value);
        ICollection<IFluentCondition<T>> ForString(ICurrentValidationContext<T> currentContext, ICollection<IValidator<string>> validators, ValidationType type);
        ICollection<IFluentCondition<T>> ForString(ICurrentValidationContext<T> currentContext, ICollection<IComparisonValidator<string, string>> validators, ValidationType type, string value);
        ICollection<IFluentCondition<T>> ForString(ICurrentValidationContext<T> currentContext, ICollection<IComparisonValidator<string, int>> validators, ValidationType type, int value);
        ICollection<IFluentCondition<T>> ForCollectionOf<TValidate>(ICurrentValidationContext<T> currentContext, ICollection<IComparisonValidator<ICollection<TValidate>, TValidate>> validators, ValidationType type, TValidate value);
        ICollection<IFluentCondition<T>> For<TValidate>(ICurrentValidationContext<T> currentContext, ICollection<IValidator<TValidate>> validators, ValidationType type);
        ICollection<IFluentCondition<T>> For<TValidate>(ICurrentValidationContext<T> currentContext, ICollection<IComparisonValidator<TValidate, TValidate>> validators, ValidationType type, TValidate value);
        ICollection<IFluentCondition<T>> For<TValidate>(ICurrentValidationContext<T> currentContext, ICollection<IComparisonValidator<TValidate, TValidate>> validators, ValidationType type, Expression<Func<T, TValidate>> value);
    }
}
