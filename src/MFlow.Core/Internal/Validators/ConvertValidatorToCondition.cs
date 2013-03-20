using MFlow.Core.Conditions;
using MFlow.Core.Internal.Validators.Dates;
using MFlow.Core.Internal.Validators.Numbers;
using MFlow.Core.Internal.Validators.Strings;
using MFlow.Core.Validation.Configuration;
using MFlow.Core.Validation.Context;
using MFlow.Core.Validation.Enums;
using System;
using System.Collections.Generic;
using MFlow.Core.Internal.Validators.Extension;

namespace MFlow.Core.Internal.Validators
{
    class ConvertValidatorToCondition<T> : MFlow.Core.Internal.Validators.IConvertValidatorToCondition<T>
    {

        readonly T _target;
        readonly IExpressionBuilder<T> _expressionBuilder;
        readonly IPropertyNameResolver _propertyNameResolver;
        readonly IMessageResolver _messageResolver;
        readonly IConfigureFluentValidation _configuration;

        public ConvertValidatorToCondition(T target,
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
    }
}
