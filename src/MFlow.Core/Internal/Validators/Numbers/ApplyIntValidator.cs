using MFlow.Core.Conditions;
using MFlow.Core.Validation.Context;
using System;
using System.Linq.Expressions;

namespace MFlow.Core.Internal.Validators.Numbers
{
    /// <summary>
    ///     Apply an int based validator
    /// </summary>
    class ApplyIntValidator<T> : IApplyNumericValidator<T, int>
    {
        readonly T _target;
        readonly ICurrentValidationContext<T> _currentContext;
        readonly IExpressionBuilder<T> _expressionBuilder;
        readonly IPropertyNameResolver _propertyNameResolver;
        readonly IMessageResolver _messageResolver;

        /// <summary>
        ///     Constructor
        /// </summary>
        public ApplyIntValidator(T target,
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

        /// <summary>
        ///     Apply a comparison validator
        /// </summary>
        public IFluentCondition<T> Apply(IComparisonValidator<int, int> validator, Validation.Enums.ValidationType type, int value)
        {
            Expression<Func<T, int>> expression = _currentContext.GetExpression<int>();
            Func<T, int> compiled = _expressionBuilder.Compile(expression);
            Expression<Func<T, bool>> derived = f => validator.Validate(_expressionBuilder.Invoke(compiled, _target), value);
            var propertyName = _propertyNameResolver.Resolve<T, int>(expression);
            var message = _messageResolver.Resolve(expression, value, type, string.Empty);
            return new FluentCondition<T>(derived, _currentContext.ConditionType, propertyName, message, string.Empty, _currentContext.ConditionOutput);
        }

        /// <summary>
        ///     Apply a comparison validator
        /// </summary>
        public IFluentCondition<T> Apply(IComparisonValidator<int, Between<int>> validator, Validation.Enums.ValidationType type, int lower, int upper)
        {
            Expression<Func<T, int>> expression = _currentContext.GetExpression<int>();
            Func<T, int> compiled = _expressionBuilder.Compile(expression);
            Expression<Func<T, bool>> derived = f => validator.Validate(_expressionBuilder.Invoke(compiled, _target), new Between<int> { Lower = lower, Upper = upper });
            var propertyName = _propertyNameResolver.Resolve<T, int>(expression);
            var message = _messageResolver.Resolve(expression, lower, upper, type, string.Empty);
            return new FluentCondition<T>(derived, _currentContext.ConditionType, propertyName, message, string.Empty, _currentContext.ConditionOutput);
        }
    }
}
