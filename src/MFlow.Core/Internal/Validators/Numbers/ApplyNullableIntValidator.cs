using MFlow.Core.Conditions;
using MFlow.Core.ExpressionBuilder;
using MFlow.Core.MessageResolver;
using MFlow.Core.Validation.Context;
using System;
using System.Linq.Expressions;

namespace MFlow.Core.Internal.Validators.Numbers
{
    /// <summary>
    ///     Apply a nullable int based validator
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class ApplyNullableIntValidator<T> : IApplyNumericValidator<T, int?>
    {
        readonly T _target;
        readonly ICurrentValidationContext<T> _currentContext;
        readonly IBuildExpressions _expressionBuilder;
        readonly IPropertyNameResolver _propertyNameResolver;
        readonly IResolveValidationMessages _messageResolver;

        /// <summary>
        ///     Constructor
        /// </summary>
        public ApplyNullableIntValidator(T target,
            ICurrentValidationContext<T> context,
            IBuildExpressions expressionBuilder, 
            IPropertyNameResolver propertyNameResolver,
            IResolveValidationMessages messageResolver)
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
            Expression<Func<T, int?>> expression = _currentContext.GetExpression<int?>();
            Func<T, int?> compiled = _expressionBuilder.Compile(expression);
            Expression<Func<T, bool>> derived = f => validator.Validate(_expressionBuilder.Invoke(compiled, _target) ?? default(int), value);
            var propertyName = _propertyNameResolver.Resolve<T, int?>(expression);
            var message = _messageResolver.Resolve(expression, value, type, string.Empty);
            return new FluentCondition<T>(derived, _currentContext.ConditionType, propertyName, message, string.Empty, _currentContext.ConditionOutput);
        }

        /// <summary>
        ///     Apply a comparison validator
        /// </summary>
        public IFluentCondition<T> Apply(IComparisonValidator<int, Between<int>> validator, Validation.Enums.ValidationType type, int lower, int upper)
        {
            Expression<Func<T, int?>> expression = _currentContext.GetExpression<int?>();
            Func<T, int?> compiled = _expressionBuilder.Compile(expression);
            Expression<Func<T, bool>> derived = f => validator.Validate(_expressionBuilder.Invoke(compiled, _target) ?? default(int), new Between<int> { Lower = lower, Upper = upper });
            var propertyName = _propertyNameResolver.Resolve<T, int?>(expression);
            var message = _messageResolver.Resolve(expression, lower, upper, type, string.Empty);
            return new FluentCondition<T>(derived, _currentContext.ConditionType, propertyName, message, string.Empty, _currentContext.ConditionOutput);
        }
    }
}
