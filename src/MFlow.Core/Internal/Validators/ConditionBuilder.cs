using MFlow.Core.Conditions;
using MFlow.Core.ExpressionBuilder;
using MFlow.Core.Internal.Validators.Collections;
using MFlow.Core.Internal.Validators.Dates;
using MFlow.Core.Internal.Validators.Extension;
using MFlow.Core.Internal.Validators.Generic;
using MFlow.Core.Internal.Validators.Numbers;
using MFlow.Core.Internal.Validators.Strings;
using MFlow.Core.MessageResolver;
using MFlow.Core.Validation.Context;
using MFlow.Core.Validation.Enums;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MFlow.Core.Configuration;

namespace MFlow.Core.Internal.Validators
{
    /// <summary>
    ///     Builds conditions from validators
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class ConditionBuilder<T> : MFlow.Core.Internal.Validators.IBuildConditions<T>
    {
        readonly T _target;
        readonly IBuildExpressions _expressionBuilder;
        readonly IPropertyNameResolver _propertyNameResolver;
        readonly IResolveValidationMessages _messageResolver;
        readonly IConfigureFluentValidation _configuration;

        /// <summary>
        ///    Constructor
        /// </summary>
        public ConditionBuilder(T target,
            IBuildExpressions expressionBuilder,
            IPropertyNameResolver propertyNameResolver,
            IResolveValidationMessages messageResolver,
            IConfigureFluentValidation configuration)
        {
            _target = target;
            _expressionBuilder = expressionBuilder;
            _propertyNameResolver = propertyNameResolver;
            _messageResolver = messageResolver;
            _configuration = configuration;
        }

        /// <summary>
        ///     Build conditions for date time validators
        /// </summary>
        public ICollection<IFluentCondition<T>> ForDateTime(ICurrentValidationContext<T> currentContext, ICollection<IValidator<DateTime>> validators, ValidationType type)
        {
            return BuildConditions(currentContext,
                validators, type, (a, b) =>
                {
                    return currentContext.IsNullable ?
                    new ApplyNullableDateValidator<T>(_target, currentContext, _expressionBuilder,
                        _propertyNameResolver, _messageResolver).Apply(a, b) :
                    new ApplyDateValidator<T>(_target, currentContext, _expressionBuilder,
                        _propertyNameResolver, _messageResolver).Apply(a, b);
                });
        }

        /// <summary>
        ///     Build conditions for date time validators
        /// </summary>
        public ICollection<IFluentCondition<T>> ForDateTime(ICurrentValidationContext<T> currentContext, ICollection<IComparisonValidator<DateTime, DateTime>> validators, ValidationType type, DateTime value)
        {
            return BuildConditions(currentContext,
                validators, type, value, (a, b, c) =>
                {
                    return currentContext.IsNullable ?
                    new ApplyNullableDateValidator<T>(_target, currentContext, _expressionBuilder,
                        _propertyNameResolver, _messageResolver).Apply(a, b, c) :
                    new ApplyDateValidator<T>(_target, currentContext, _expressionBuilder,
                        _propertyNameResolver, _messageResolver).Apply(a, b, c);
                });
        }

        /// <summary>
        ///     Build conditions for date time validators
        /// </summary>
        public ICollection<IFluentCondition<T>> ForDateTime(ICurrentValidationContext<T> currentContext, ICollection<IComparisonValidator<DateTime, Between<DateTime>>> validators, ValidationType type, DateTime lower, DateTime upper)
        {
            return BuildConditions(currentContext,
                validators, type, lower, upper, (a, b, c, d) =>
                {
                    return currentContext.IsNullable ?
                    new ApplyNullableDateValidator<T>(_target, currentContext, _expressionBuilder,
                        _propertyNameResolver, _messageResolver).Apply(a, b, c, d) :
                    new ApplyDateValidator<T>(_target, currentContext, _expressionBuilder,
                        _propertyNameResolver, _messageResolver).Apply(a, b, c, d);
                });
        }

        /// <summary>
        ///     Build conditions for int validators
        /// </summary>
        public ICollection<IFluentCondition<T>> ForInt(ICurrentValidationContext<T> currentContext, ICollection<IComparisonValidator<int, Between<int>>> validators, ValidationType type, int lower, int upper)
        {
            return BuildConditions(currentContext,
                validators, type, lower, upper, (a, b, c, d) =>
                {
                    return currentContext.IsNullable ?
                    new ApplyNullableIntValidator<T>(_target, currentContext, _expressionBuilder,
                        _propertyNameResolver, _messageResolver).Apply(a, b, c, d) :
                    new ApplyIntValidator<T>(_target, currentContext, _expressionBuilder,
                        _propertyNameResolver, _messageResolver).Apply(a, b, c, d);
                });
        }

        /// <summary>
        ///     Build conditions for int validators
        /// </summary>
        public ICollection<IFluentCondition<T>> ForInt(ICurrentValidationContext<T> currentContext, ICollection<IComparisonValidator<int, int>> validators, ValidationType type, int value)
        {
            return BuildConditions(currentContext,
                validators, type, value, (a, b, c) =>
                {
                    return currentContext.IsNullable ?
                    new ApplyNullableIntValidator<T>(_target, currentContext, _expressionBuilder,
                        _propertyNameResolver, _messageResolver).Apply(a, b, c) :
                    new ApplyIntValidator<T>(_target, currentContext, _expressionBuilder,
                        _propertyNameResolver, _messageResolver).Apply(a, b, c);
                });
        }

        /// <summary>
        ///     Build conditions for string validators
        /// </summary>
        public ICollection<IFluentCondition<T>> ForString(ICurrentValidationContext<T> currentContext, ICollection<IValidator<string>> validators, ValidationType type)
        {
            return BuildConditions(currentContext,
                validators, type, (a, b) =>
                {
                    return new ApplyStringValidator<T>(_target, currentContext, _expressionBuilder,
                        _propertyNameResolver, _messageResolver).Apply(a, b);
                });
        }

        /// <summary>
        ///     Build conditions for string validators
        /// </summary>
        public ICollection<IFluentCondition<T>> ForString(ICurrentValidationContext<T> currentContext, ICollection<IComparisonValidator<string, string>> validators, ValidationType type, string value)
        {
            return BuildConditions(currentContext,
                validators, type, value, (a, b, c) =>
                {
                    return new ApplyStringValidator<T>(_target, currentContext, _expressionBuilder,
                        _propertyNameResolver, _messageResolver).Apply(a, b, c);
                });
        }

        /// <summary>
        ///     Build conditions for string validators
        /// </summary>
        public ICollection<IFluentCondition<T>> ForString(ICurrentValidationContext<T> currentContext, ICollection<IComparisonValidator<string, int>> validators, ValidationType type, int value)
        {
            return BuildConditions(currentContext,
                validators, type, value, (a, b, c) =>
                {
                    return new ApplyStringValidator<T>(_target, currentContext, _expressionBuilder,
                        _propertyNameResolver, _messageResolver).Apply(a, b, c);
                });
        }

        /// <summary>
        ///     Build conditions for collection validators
        /// </summary>
        public ICollection<IFluentCondition<T>> ForCollectionOf<TValidate>(ICurrentValidationContext<T> currentContext, ICollection<IComparisonValidator<ICollection<TValidate>, TValidate>> validators, ValidationType type, TValidate value)
        {
            return BuildConditions(currentContext,
                validators, type, value, (a, b, c) =>
                {
                    return new ApplyCollectionValidator<T, TValidate>(_target, currentContext, _expressionBuilder,
                        _propertyNameResolver, _messageResolver).Apply(a, b, c);
                });
        }

        /// <summary>
        ///     Build conditions for collection validators
        /// </summary>
        public ICollection<IFluentCondition<T>> ForCollectionOf<TValidate>(ICurrentValidationContext<T> currentContext, ICollection<IComparisonValidator<ICollection<TValidate>, ICollection<TValidate>>> validators, ValidationType type, ICollection<TValidate> values)
        {
            return BuildConditions(currentContext,
                validators, type, values, (a, b, c) =>
                {
                    return new ApplyCollectionValidator<T, TValidate>(_target, currentContext, _expressionBuilder,
                        _propertyNameResolver, _messageResolver).Apply(a, b, c);
                });
        }

        /// <summary>
        ///     Build conditions for generic validators
        /// </summary>
        public ICollection<IFluentCondition<T>> For<TValidate>(ICurrentValidationContext<T> currentContext, ICollection<IValidator<TValidate>> validators, ValidationType type)
        {
            return BuildConditions(currentContext,
                validators, type, (a, b) =>
                {
                    return new ApplyGenericValidator<T, TValidate>(_target, currentContext, _expressionBuilder,
                        _propertyNameResolver, _messageResolver).Apply(a, b);
                });
        }

        /// <summary>
        ///     Build conditions for generic validators
        /// </summary>
        public ICollection<IFluentCondition<T>> For<TValidate>(ICurrentValidationContext<T> currentContext, ICollection<IComparisonValidator<TValidate, TValidate>> validators, ValidationType type, TValidate value)
        {
            return BuildConditions(currentContext,
                validators, type, value, (a, b, c) =>
                {
                    return new ApplyGenericValidator<T, TValidate>(_target, currentContext, _expressionBuilder,
                        _propertyNameResolver, _messageResolver).Apply(a, b, c);
                });
        }

        /// <summary>
        ///     Build conditions for generic validators
        /// </summary>
        public ICollection<IFluentCondition<T>> For<TValidate>(ICurrentValidationContext<T> currentContext, ICollection<IComparisonValidator<TValidate, TValidate>> validators, ValidationType type, Expression<Func<T, TValidate>> value)
        {
            return BuildConditions(currentContext,
                validators, type, value, (a, b, c) => {
                    return new ApplyGenericValidator<T, TValidate>(_target, currentContext, _expressionBuilder,
                        _propertyNameResolver, _messageResolver).Apply(a, b, c);
                });
        }

        /// <summary>
        ///     Build conditions validators
        /// </summary>
        ICollection<IFluentCondition<T>> BuildConditions<TValidate, TCompare>(
            ICurrentValidationContext<T> currentContext,
            ICollection<IComparisonValidator<TValidate, Between<TCompare>>> validators,
            ValidationType type,
            TCompare lower,
            TCompare upper,
            Func<IComparisonValidator<TValidate, Between<TCompare>>, ValidationType, TCompare, TCompare, IFluentCondition<T>> builder)
        {
            var conditions = new List<IFluentCondition<T>>();
            foreach (var validator in validators.ToApply(_configuration))
            {
                IFluentCondition<T> condition;
                condition = builder(validator, type, lower, upper);
                conditions.Add(condition);
            }

            return conditions;
        }

        /// <summary>
        ///     Build conditions validators
        /// </summary>
        ICollection<IFluentCondition<T>> BuildConditions<TValidate, TCompare>(
            ICurrentValidationContext<T> currentContext, 
            ICollection<IComparisonValidator<TValidate, TCompare>> validators, 
            ValidationType type, 
            TCompare value,
            Func<IComparisonValidator<TValidate, TCompare>, ValidationType, TCompare, IFluentCondition<T>> builder)
        {
            var conditions = new List<IFluentCondition<T>>();
            foreach (var validator in validators.ToApply(_configuration))
            {
                IFluentCondition<T> condition;
                condition = builder(validator, type, value);
                conditions.Add(condition);
            }

            return conditions;
        }

        /// <summary>
        ///     Build conditions validators
        /// </summary>
        ICollection<IFluentCondition<T>> BuildConditions<TValidate, TCompare>(
            ICurrentValidationContext<T> currentContext,
            ICollection<IComparisonValidator<TValidate, TCompare>> validators,
            ValidationType type,
            Expression<Func<T, TValidate>> value,
            Func<IComparisonValidator<TValidate, TCompare>, ValidationType, Expression<Func<T, TValidate>>, IFluentCondition<T>> builder)
        {
            var conditions = new List<IFluentCondition<T>>();
            foreach (var validator in validators.ToApply(_configuration))
            {
                IFluentCondition<T> condition;
                condition = builder(validator, type, value);
                conditions.Add(condition);
            }

            return conditions;
        }

        /// <summary>
        ///     Build conditions validators
        /// </summary>
        ICollection<IFluentCondition<T>> BuildConditions<TValidate>(
            ICurrentValidationContext<T> currentContext,
            ICollection<IValidator<TValidate>> validators,
            ValidationType type,
            Func<IValidator<TValidate>, ValidationType, IFluentCondition<T>> builder)
        {
            var conditions = new List<IFluentCondition<T>>();
            foreach (var validator in validators.ToApply(_configuration))
            {
                IFluentCondition<T> condition;
                condition = builder(validator, type);
                conditions.Add(condition);
            }

            return conditions;
        }
    }
}
