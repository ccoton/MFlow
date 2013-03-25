using MFlow.Core.Conditions;
using MFlow.Core.Validation.Context;
using System;
using System.Linq.Expressions;

namespace MFlow.Core.Internal.Validators.Dates
{
    class ApplyDateValidator<T> : IApplyDateValidator<T, DateTime>
    {
        readonly T _target;
        readonly ICurrentValidationContext<T> _currentContext;
        readonly IExpressionBuilder<T> _expressionBuilder;
        readonly IPropertyNameResolver _propertyNameResolver;
        readonly IMessageResolver _messageResolver;

        public ApplyDateValidator(T target,
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

        public IFluentCondition<T> Apply(IComparisonValidator<DateTime, DateTime> validator, Validation.Enums.ValidationType type, DateTime value)
        {
            Expression<Func<T, DateTime>> expression = _currentContext.GetExpression<DateTime>();
            Func<T, DateTime> compiled = _expressionBuilder.Compile(expression);
            Expression<Func<T, bool>> derived = f => validator.Validate(_expressionBuilder.Invoke(compiled, _target), value);
            var propertyName = _propertyNameResolver.Resolve<T, DateTime>(expression);
            var message = _messageResolver.Resolve(expression, value, type, string.Empty);
            return new FluentCondition<T>(derived, _currentContext.ConditionType, propertyName, message, string.Empty, _currentContext.ConditionOutput);
        }

        public IFluentCondition<T> Apply(IComparisonValidator<DateTime, Between<DateTime>> validator, Validation.Enums.ValidationType type, DateTime lower, DateTime upper)
        {
            Expression<Func<T, DateTime>> expression = _currentContext.GetExpression<DateTime>();
            Func<T, DateTime> compiled = _expressionBuilder.Compile(expression);
            Expression<Func<T, bool>> derived = f => validator.Validate(_expressionBuilder.Invoke(compiled, _target), new Between<DateTime>() { Lower = lower, Upper = upper });
            var propertyName = _propertyNameResolver.Resolve<T, DateTime>(expression);
            var message = _messageResolver.Resolve(expression, lower, type, string.Empty);
            return new FluentCondition<T>(derived, _currentContext.ConditionType, propertyName, message, string.Empty, _currentContext.ConditionOutput);
        }

        public IFluentCondition<T> Apply(IValidator<DateTime> validator, Validation.Enums.ValidationType type)
        {
            Expression<Func<T, DateTime>> expression = _currentContext.GetExpression<DateTime>();
            Func<T, DateTime> compiled = _expressionBuilder.Compile(expression);
            Expression<Func<T, bool>> derived = f => validator.Validate(_expressionBuilder.Invoke(compiled, _target));
            var propertyName = _propertyNameResolver.Resolve<T, DateTime>(expression);
            var message = _messageResolver.Resolve(expression, type, string.Empty);
            return new FluentCondition<T>(derived, _currentContext.ConditionType, propertyName, message, string.Empty, _currentContext.ConditionOutput);
        }
    }
}
