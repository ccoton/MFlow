using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MFlow.Core.Conditions;
using MFlow.Core.Internal.Validators;
using MFlow.Core.Internal.Validators.Generic;
using System.Linq;

namespace MFlow.Core.Validation
{
    /// <summary>
    ///     A fluent validation implementation
    /// </summary>
    public partial class FluentValidation<T> : FluentConditions<T>, IFluentValidation<T>, IFluentValidationGeneric<T>
    {
        IList<Func<IFluentValidation<T>>> _dependencies = new List<Func<IFluentValidation<T>>>();

        /// <summary>
        ///     Checks if the expression evaluates to an object that is equal to the value expression
        /// </summary>
        public IFluentValidation<T> IsEqualTo<C>(Expression<Func<T, C>> valueExpression)
        {
            return ApplyGenericComparisonValidator(_validatorFactory.GetValidator<C, C, IEqualToValidator<C, C>>(), Enums.ValidationType.Equal, valueExpression);
        }

        /// <summary>
        ///     Checks if the expression evaluates to an object that is equal to the value
        /// </summary>
        public IFluentValidation<T> IsEqualTo<C>(C value)
        {
            return ApplyGenericComparisonValidator(_validatorFactory.GetValidator<C, C, IEqualToValidator<C, C>>(), Enums.ValidationType.Equal, value);
        }

        /// <summary>
        ///     Checks if the expression evaluates to an object that is equal to the value expression
        /// </summary>
        public IFluentValidation<T> IsNotEqualTo<C>(Expression<Func<T, C>> valueExpression)
        {
            return ApplyGenericComparisonValidator(_validatorFactory.GetValidator<C, C, INotEqualToValidator<C, C>>(), Enums.ValidationType.NotEqual, valueExpression);
        }

        /// <summary>
        ///     Checks if the expression evaluates to an object that is not equal to the value
        /// </summary>
        public IFluentValidation<T> IsNotEqualTo<C>(C value)
        {
            return ApplyGenericComparisonValidator(_validatorFactory.GetValidator<C, C, INotEqualToValidator<C, C>>(), Enums.ValidationType.NotEqual, value);
        }

        /// <summary>
        ///     Is the item required
        /// </summary>
        public IFluentValidation<T> IsRequired<C>()
        {
            return ApplyGenericValidator(_validatorFactory.GetValidator<C, IRequiredValidator<C>>(), Enums.ValidationType.IsRequired);
        }

        /// <summary>
        ///     Checks to make sure the expression evaluates to a value that is not null
        /// </summary>
        public IFluentValidation<T> IsNotNull<C>()
        {
            return ApplyGenericValidator(_validatorFactory.GetValidator<C, INotNullValidator<C>>(), Enums.ValidationType.IsNotNull);
        }

        IFluentValidation<T> ApplyGenericValidator<C>(ICollection<IValidator<C>> validators, Enums.ValidationType type)
        {
            _validatorToCondition.For(_currentContext, validators, type)
                .ToList()
                .ForEach(c => BuildIf(c.Condition, c.Key, c.Message));

            return this;
        }

        IFluentValidation<T> ApplyGenericComparisonValidator<C>(ICollection<IComparisonValidator<C, C>> validators, Enums.ValidationType type, C value)
        {
            _validatorToCondition.For(_currentContext, validators, type, value)
                .ToList()
                .ForEach(c => BuildIf(c.Condition, c.Key, c.Message));

            return this;
        }

        IFluentValidation<T> ApplyGenericComparisonValidator<C>(ICollection<IComparisonValidator<C, C>> validators, Enums.ValidationType type, Expression<Func<T, C>> value)
        {
            _validatorToCondition.For(_currentContext, validators, type, value)
                .ToList()
                .ForEach(c => BuildIf(c.Condition, c.Key, c.Message));

            return this;
        }
    }
}
