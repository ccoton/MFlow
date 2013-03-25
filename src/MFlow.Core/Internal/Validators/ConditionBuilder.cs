using MFlow.Core.Conditions;
using MFlow.Core.Internal.Validators.Collections;
using MFlow.Core.Internal.Validators.Dates;
using MFlow.Core.Internal.Validators.Extension;
using MFlow.Core.Internal.Validators.Generic;
using MFlow.Core.Internal.Validators.Numbers;
using MFlow.Core.Internal.Validators.Strings;
using MFlow.Core.Validation.Configuration;
using MFlow.Core.Validation.Context;
using MFlow.Core.Validation.Enums;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MFlow.Core.Internal.Validators
{
    /// <summary>
    ///     Builds conditions from validators
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class ConditionBuilder<T> : MFlow.Core.Internal.Validators.IBuildConditions<T>
    {
        readonly T _target;
        readonly IExpressionBuilder<T> _expressionBuilder;
        readonly IPropertyNameResolver _propertyNameResolver;
        readonly IMessageResolver _messageResolver;
        readonly IConfigureFluentValidation _configuration;

        /// <summary>
        ///    Constructor
        /// </summary>
        public ConditionBuilder(T target,
            IExpressionBuilder<T> expressionBuilder,
            IPropertyNameResolver propertyNameResolver,
            IMessageResolver messageResolver,
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
            var conditions = new List<IFluentCondition<T>>();
            foreach (var validator in validators.ToApply(_configuration))
            {
                IFluentCondition<T> condition;

                if (currentContext.IsNullable)
                {
                    condition = new ApplyNullableDateValidator<T>(_target, currentContext, _expressionBuilder,
                        _propertyNameResolver, _messageResolver).Apply(validator, type);
                }
                else
                {
                    condition = new ApplyDateValidator<T>(_target, currentContext, _expressionBuilder,
                        _propertyNameResolver, _messageResolver).Apply(validator, type);
                }
                conditions.Add(condition);
            }
            return conditions;
        }

        /// <summary>
        ///     Build conditions for date time validators
        /// </summary>
        public ICollection<IFluentCondition<T>> ForDateTime(ICurrentValidationContext<T> currentContext, ICollection<IComparisonValidator<DateTime, DateTime>> validators, ValidationType type, DateTime value)
        {
            var conditions = new List<IFluentCondition<T>>();
            foreach (var validator in validators.ToApply(_configuration))
            {
                IFluentCondition<T> condition;

                if (currentContext.IsNullable)
                {
                    condition = new ApplyNullableDateValidator<T>(_target, currentContext, _expressionBuilder,
                        _propertyNameResolver, _messageResolver).Apply(validator, type, value);
                }
                else
                {
                    condition = new ApplyDateValidator<T>(_target, currentContext, _expressionBuilder,
                        _propertyNameResolver, _messageResolver).Apply(validator, type, value);
                }
                conditions.Add(condition);
            }
            return conditions;
        }

        /// <summary>
        ///     Build conditions for date time validators
        /// </summary>
        public ICollection<IFluentCondition<T>> ForDateTime(ICurrentValidationContext<T> currentContext, ICollection<IComparisonValidator<DateTime, Between<DateTime>>> validators, ValidationType type, DateTime lower, DateTime upper)
        {
            var conditions = new List<IFluentCondition<T>>();
            foreach (var validator in validators.ToApply(_configuration))
            {
                IFluentCondition<T> condition;

                if (currentContext.IsNullable)
                {
                    condition = new ApplyNullableDateValidator<T>(_target, currentContext, _expressionBuilder,
                        _propertyNameResolver, _messageResolver).Apply(validator, type, lower, upper);
                }
                else
                {
                    condition = new ApplyDateValidator<T>(_target, currentContext, _expressionBuilder,
                        _propertyNameResolver, _messageResolver).Apply(validator, type, lower, upper);
                }
                conditions.Add(condition);
            }
            return conditions;
        }

        /// <summary>
        ///     Build conditions for int validators
        /// </summary>
        public ICollection<IFluentCondition<T>> ForInt(ICurrentValidationContext<T> currentContext, ICollection<IComparisonValidator<int, Between<int>>> validators, ValidationType type, int lower, int upper)
        {
            var conditions = new List<IFluentCondition<T>>();
            foreach (var validator in validators.ToApply(_configuration))
            {
                IFluentCondition<T> condition;
                if (currentContext.IsNullable)
                {
                    condition = new ApplyNullableIntValidator<T>(_target, currentContext, _expressionBuilder,
                        _propertyNameResolver, _messageResolver).Apply(validator, type, lower, upper);
                }
                else
                {
                    condition = new ApplyIntValidator<T>(_target, currentContext, _expressionBuilder,
                        _propertyNameResolver, _messageResolver).Apply(validator, type, lower, upper);
                }
                conditions.Add(condition);
            }

            return conditions;
        }

        /// <summary>
        ///     Build conditions for int validators
        /// </summary>
        public ICollection<IFluentCondition<T>> ForInt(ICurrentValidationContext<T> currentContext, ICollection<IComparisonValidator<int, int>> validators, ValidationType type, int value)
        {
            var conditions = new List<IFluentCondition<T>>();
            foreach (var validator in validators.ToApply(_configuration))
            {
                IFluentCondition<T> condition;
                if (currentContext.IsNullable)
                {
                    condition = new ApplyNullableIntValidator<T>(_target, currentContext, _expressionBuilder,
                        _propertyNameResolver, _messageResolver).Apply(validator, type, value);
                }
                else
                {
                    condition = new ApplyIntValidator<T>(_target, currentContext, _expressionBuilder,
                        _propertyNameResolver, _messageResolver).Apply(validator, type, value);
                }
                conditions.Add(condition);
            }

            return conditions;
        }

        /// <summary>
        ///     Build conditions for string validators
        /// </summary>
        public ICollection<IFluentCondition<T>> ForString(ICurrentValidationContext<T> currentContext, ICollection<IValidator<string>> validators, ValidationType type)
        {
            var conditions = new List<IFluentCondition<T>>();
            foreach (var validator in validators.ToApply(_configuration))
            {
                IFluentCondition<T> condition;
                condition = new ApplyStringValidator<T>(_target, currentContext, _expressionBuilder,
                    _propertyNameResolver, _messageResolver).Apply(validator, type);

                conditions.Add(condition);
            }

            return conditions;
        }

        /// <summary>
        ///     Build conditions for string validators
        /// </summary>
        public ICollection<IFluentCondition<T>> ForString(ICurrentValidationContext<T> currentContext, ICollection<IComparisonValidator<string, string>> validators, ValidationType type, string value)
        {
            var conditions = new List<IFluentCondition<T>>();
            foreach (var validator in validators.ToApply(_configuration))
            {
                IFluentCondition<T> condition;
                condition = new ApplyStringValidator<T>(_target, currentContext, _expressionBuilder,
                    _propertyNameResolver, _messageResolver).Apply(validator, type, value);

                conditions.Add(condition);
            }

            return conditions;
        }

        /// <summary>
        ///     Build conditions for string validators
        /// </summary>
        public ICollection<IFluentCondition<T>> ForString(ICurrentValidationContext<T> currentContext, ICollection<IComparisonValidator<string, int>> validators, ValidationType type, int value)
        {
            var conditions = new List<IFluentCondition<T>>();
            foreach (var validator in validators.ToApply(_configuration))
            {
                IFluentCondition<T> condition;
                condition = new ApplyStringValidator<T>(_target, currentContext, _expressionBuilder,
                    _propertyNameResolver, _messageResolver).Apply(validator, type, value);

                conditions.Add(condition);
            }

            return conditions;
        }

        /// <summary>
        ///     Build conditions for collection validators
        /// </summary>
        public ICollection<IFluentCondition<T>> ForCollectionOf<TValidate>(ICurrentValidationContext<T> currentContext, ICollection<IComparisonValidator<ICollection<TValidate>, TValidate>> validators, ValidationType type, TValidate value)
        {
            var conditions = new List<IFluentCondition<T>>();
            foreach (var validator in validators.ToApply(_configuration))
            {
                IFluentCondition<T> condition;
                condition = new ApplyCollectionValidator<T, TValidate>(_target, currentContext, _expressionBuilder,
                    _propertyNameResolver, _messageResolver).Apply(validator, type, value);

                conditions.Add(condition);
            }

            return conditions;
        }

        /// <summary>
        ///     Build conditions for generic validators
        /// </summary>
        public ICollection<IFluentCondition<T>> For<TValidate>(ICurrentValidationContext<T> currentContext, ICollection<IValidator<TValidate>> validators, ValidationType type)
        {
            var conditions = new List<IFluentCondition<T>>();
            foreach (var validator in validators.ToApply(_configuration))
            {
                IFluentCondition<T> condition;
                condition = new ApplyGenericValidator<T, TValidate>(_target, currentContext, _expressionBuilder,
                    _propertyNameResolver, _messageResolver).Apply(validator, type);

                conditions.Add(condition);
            }

            return conditions;
        }

        /// <summary>
        ///     Build conditions for generic validators
        /// </summary>
        public ICollection<IFluentCondition<T>> For<TValidate>(ICurrentValidationContext<T> currentContext, ICollection<IComparisonValidator<TValidate, TValidate>> validators, ValidationType type, TValidate value)
        {
            var conditions = new List<IFluentCondition<T>>();
            foreach (var validator in validators.ToApply(_configuration))
            {
                IFluentCondition<T> condition;
                condition = new ApplyGenericValidator<T, TValidate>(_target, currentContext, _expressionBuilder,
                    _propertyNameResolver, _messageResolver).Apply(validator, type, value);

                conditions.Add(condition);
            }

            return conditions;
        }

        /// <summary>
        ///     Build conditions for generic validators
        /// </summary>
        public ICollection<IFluentCondition<T>> For<TValidate>(ICurrentValidationContext<T> currentContext, ICollection<IComparisonValidator<TValidate, TValidate>> validators, ValidationType type, Expression<Func<T, TValidate>> value)
        {
            var conditions = new List<IFluentCondition<T>>();
            foreach (var validator in validators.ToApply(_configuration))
            {
                IFluentCondition<T> condition;
                condition = new ApplyGenericValidator<T, TValidate>(_target, currentContext, _expressionBuilder,
                    _propertyNameResolver, _messageResolver).Apply(validator, type, value);

                conditions.Add(condition);
            }

            return conditions;
        }
    }
}
