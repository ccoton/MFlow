using MFlow.Core.Conditions;
using MFlow.Core.Validation.Context;
using MFlow.Core.Validation.Enums;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
namespace MFlow.Core.Internal.Validators
{
    /// <summary>
    ///    A contract for building conditions from validators
    /// </summary>
    public interface IBuildConditions<T>
    {
        /// <summary>
        ///     Build conditions for date time validators
        /// </summary>
        ICollection<IFluentCondition<T>> ForDateTime(ICurrentValidationContext<T> currentContext, ICollection<IValidator<DateTime>> validators, ValidationType type);

        /// <summary>
        ///     Build conditions for date time validators
        /// </summary>
        ICollection<IFluentCondition<T>> ForDateTime(ICurrentValidationContext<T> currentContext, ICollection<IComparisonValidator<DateTime, DateTime>> validators, ValidationType type, DateTime value);

        /// <summary>
        ///     Build conditions for date time validators
        /// </summary>
        ICollection<IFluentCondition<T>> ForDateTime(ICurrentValidationContext<T> currentContext, ICollection<IComparisonValidator<DateTime, Between<DateTime>>> validators, ValidationType type, DateTime lower, DateTime upper);
        
        /// <summary>
        ///     Build conditions for int validators
        /// </summary>
        ICollection<IFluentCondition<T>> ForInt(ICurrentValidationContext<T> currentContext, ICollection<IComparisonValidator<int, Between<int>>> validators, ValidationType type, int lower, int upper);

        /// <summary>
        ///     Build conditions for int validators
        /// </summary>
        ICollection<IFluentCondition<T>> ForInt(ICurrentValidationContext<T> currentContext, ICollection<IComparisonValidator<int, int>> validators, ValidationType type, int value);

        /// <summary>
        ///     Build conditions for string validators
        /// </summary>
        ICollection<IFluentCondition<T>> ForString(ICurrentValidationContext<T> currentContext, ICollection<IValidator<string>> validators, ValidationType type);

        /// <summary>
        ///     Build conditions for string validators
        /// </summary>
        ICollection<IFluentCondition<T>> ForString(ICurrentValidationContext<T> currentContext, ICollection<IComparisonValidator<string, string>> validators, ValidationType type, string value);

        /// <summary>
        ///     Build conditions for string validators
        /// </summary>
        ICollection<IFluentCondition<T>> ForString(ICurrentValidationContext<T> currentContext, ICollection<IComparisonValidator<string, int>> validators, ValidationType type, int value);

        /// <summary>
        ///     Build conditions for collection validators
        /// </summary>
        ICollection<IFluentCondition<T>> ForCollectionOf<TValidate>(ICurrentValidationContext<T> currentContext, ICollection<IComparisonValidator<ICollection<TValidate>, TValidate>> validators, ValidationType type, TValidate value);

        /// <summary>
        ///     Build conditions for collection validators
        /// </summary>
        ICollection<IFluentCondition<T>> ForCollectionOf<TValidate>(ICurrentValidationContext<T> currentContext, ICollection<IComparisonValidator<ICollection<TValidate>, ICollection<TValidate>>> validators, ValidationType type, ICollection<TValidate> values);

        /// <summary>
        ///     Build conditions for generic validators
        /// </summary>
        ICollection<IFluentCondition<T>> For<TValidate>(ICurrentValidationContext<T> currentContext, ICollection<IValidator<TValidate>> validators, ValidationType type);

        /// <summary>
        ///     Build conditions for generic validators
        /// </summary>
        ICollection<IFluentCondition<T>> For<TValidate>(ICurrentValidationContext<T> currentContext, ICollection<IComparisonValidator<TValidate, TValidate>> validators, ValidationType type, TValidate value);

        /// <summary>
        ///     Build conditions for generic validators
        /// </summary>
        ICollection<IFluentCondition<T>> For<TValidate>(ICurrentValidationContext<T> currentContext, ICollection<IComparisonValidator<TValidate, TValidate>> validators, ValidationType type, Expression<Func<T, TValidate>> value);
    }
}
