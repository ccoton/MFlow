using MFlow.Core.Conditions;
using MFlow.Core.Validation.Context;
using System;
using System.Linq.Expressions;

namespace MFlow.Core.Internal.Validators.Strings
{
    class ApplyStringValidator<T> : IApplyStringValidator<T, string>
    {
        readonly T _target;
        readonly ICurrentValidationContext<T> _currentContext;
        readonly IExpressionBuilder<T> _expressionBuilder;
        readonly IPropertyNameResolver _propertyNameResolver;
        readonly IMessageResolver _messageResolver;

        public ApplyStringValidator(T target,
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

        public IFluentCondition<T> Apply(IComparisonValidator<string, int> validator, Validation.Enums.ValidationType type, int value)
        {
            Expression<Func<T, string>> expression = _currentContext.GetExpression<string>();
            Func<T, string> compiled = _expressionBuilder.Compile(expression);
            Expression<Func<T, bool>> derived = f => validator.Validate(_expressionBuilder.Invoke(compiled, _target), value);
            var propertyName = _propertyNameResolver.Resolve<T, string>(expression);
            var message = _messageResolver.Resolve(expression, value.ToString(), type, string.Empty);
            return new FluentCondition<T>(derived, _currentContext.ConditionType, propertyName, message, string.Empty, _currentContext.ConditionOutput);
        }

        public IFluentCondition<T> Apply(IComparisonValidator<string, string> validator, Validation.Enums.ValidationType type, string value)
        {
            Expression<Func<T, string>> expression = _currentContext.GetExpression<string>();
            Func<T, string> compiled = _expressionBuilder.Compile(expression);
            Expression<Func<T, bool>> derived = f => validator.Validate(_expressionBuilder.Invoke(compiled, _target), value);
            var propertyName = _propertyNameResolver.Resolve<T, string>(expression);
            var message = _messageResolver.Resolve(expression, value, type, string.Empty);
            return new FluentCondition<T>(derived, _currentContext.ConditionType, propertyName, message, string.Empty, _currentContext.ConditionOutput);
        }

        public IFluentCondition<T> Apply(IValidator<string> validator, Validation.Enums.ValidationType type)
        {
            Expression<Func<T, string>> expression = _currentContext.GetExpression<string>();
            Func<T, string> compiled = _expressionBuilder.Compile(expression);
            Expression<Func<T, bool>> derived = f => validator.Validate(_expressionBuilder.Invoke(compiled, _target));
            var propertyName = _propertyNameResolver.Resolve<T, string>(expression);
            var message = _messageResolver.Resolve(expression, type, string.Empty);
            return new FluentCondition<T>(derived, _currentContext.ConditionType, propertyName, message, string.Empty, _currentContext.ConditionOutput);
        }
    }
}
