using MFlow.Core.Conditions;
using MFlow.Core.Conditions.Enums;
using MFlow.Core.Internal.Validators.Dates;
using MFlow.Core.Internal.Validators.Numbers;
using MFlow.Core.Validation.Context;
using MFlow.Core.Validation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Internal.Validators
{
    class ValidatorToCondition<T> : MFlow.Core.Internal.Validators.IValidatorToCondition<T>
    {

        readonly T _target;
        readonly IExpressionBuilder<T> _expressionBuilder;
        readonly IPropertyNameResolver _propertyNameResolver;
        readonly IMessageResolver _messageResolver;

        public ValidatorToCondition(T target,
            IExpressionBuilder<T> expressionBuilder,
            IPropertyNameResolver propertyNameResolver,
            IMessageResolver messageResolver)
        {
            _target = target;
            _expressionBuilder = expressionBuilder;
            _propertyNameResolver = propertyNameResolver;
            _messageResolver = messageResolver;
        }

        public IFluentCondition<T> ForDateTime(ICurrentValidationContext<T> currentContext, IValidator<DateTime> validator, ValidationType type)
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

            return condition;
        }

        public IFluentCondition<T> ForDateTime(ICurrentValidationContext<T> currentContext, IComparisonValidator<DateTime, DateTime> validator, ValidationType type, DateTime value)
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

            return condition;
        }

        public IFluentCondition<T> ForInt(ICurrentValidationContext<T> currentContext, IComparisonValidator<int, int> validator, ValidationType type, int value)
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

            return condition;
        }
    }
}
