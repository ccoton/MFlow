using MFlow.Core.Conditions;
using MFlow.Core.Validation.Context;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MFlow.Core.Internal.Validators.Generic
{
    class ApplyGenericValidator<T, TValidate> : IApplyGenericValidator<T, TValidate>
    {
        readonly T _target;
        readonly ICurrentValidationContext<T> _currentContext;
        readonly IExpressionBuilder<T> _expressionBuilder;
        readonly IPropertyNameResolver _propertyNameResolver;
        readonly IMessageResolver _messageResolver;

        public ApplyGenericValidator(T target,
            ICurrentValidationContext<T> context,
            IExpressionBuilder<T> expressionBuilder, 
            IPropertyNameResolver propertyNameResolver,
            IMessageResolver messageResolver)
        {
            _target = target;
            _currentContext = context;
            _expressionBuilder = expressionBuilder;
            _propertyNameResolver = propertyNameResolver;
            _messageResolver = messageResolver;
        }

        public IFluentCondition<T> Apply(IValidator<TValidate> validator, Validation.Enums.ValidationType type)
        {
            Expression<Func<T, TValidate>> expression = _currentContext.GetExpression<TValidate>();
            Func<T, TValidate> compiled = _expressionBuilder.Compile(expression);
            Expression<Func<T, bool>> derived = f => validator.Validate(_expressionBuilder.Invoke(compiled, _target));
            var propertyName = _propertyNameResolver.Resolve<T, TValidate>(expression);
            var message = _messageResolver.Resolve(expression, type, string.Empty);
            return new FluentCondition<T>(derived, _currentContext.ConditionType, propertyName, message, string.Empty, _currentContext.ConditionOutput);
        }

        public IFluentCondition<T> Apply(IComparisonValidator<TValidate, TValidate> validator, Validation.Enums.ValidationType type, TValidate value)
        {
            Expression<Func<T, TValidate>> expression = _currentContext.GetExpression<TValidate>();
            Func<T, TValidate> compiled = _expressionBuilder.Compile(expression);
            Expression<Func<T, bool>> derived = f => validator.Validate(_expressionBuilder.Invoke(compiled, _target), value);
            var propertyName = _propertyNameResolver.Resolve<T, TValidate>(expression);
            var message = _messageResolver.Resolve(expression, value, type, string.Empty);
            return new FluentCondition<T>(derived, _currentContext.ConditionType, propertyName, message, string.Empty, _currentContext.ConditionOutput);
        }

        public IFluentCondition<T> Apply(IComparisonValidator<TValidate, TValidate> validator, Validation.Enums.ValidationType type, Expression<Func<T, TValidate>> value)
        {
            Expression<Func<T, TValidate>> expression = _currentContext.GetExpression<TValidate>();
            Func<T, TValidate> compiled = _expressionBuilder.Compile(expression);
            Func<T, TValidate> compiledValue = _expressionBuilder.Compile(value);
            Expression<Func<T, bool>> derived = f => validator.Validate(_expressionBuilder.Invoke(compiled, _target), _expressionBuilder.Invoke(compiledValue, _target));
            var propertyName = _propertyNameResolver.Resolve<T, TValidate>(expression);
            var message = _messageResolver.Resolve(expression, value, type, string.Empty);
            return new FluentCondition<T>(derived, _currentContext.ConditionType, propertyName, message, string.Empty, _currentContext.ConditionOutput);
        }
    }
}
