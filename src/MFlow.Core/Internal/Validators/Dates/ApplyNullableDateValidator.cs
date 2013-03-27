using MFlow.Core.Conditions;
using MFlow.Core.ExpressionBuilder;
using MFlow.Core.MessageResolver;
using MFlow.Core.Validation.Context;
using System;
using System.Linq.Expressions;

namespace MFlow.Core.Internal.Validators.Dates
{
    /// <summary>
    ///     Applies a nullable date based validator
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class ApplyNullableDateValidator<T> : IApplyDateValidator<T, DateTime?>
    {
        readonly T _target;
        readonly ICurrentValidationContext<T> _currentContext;
        readonly IBuildExpressions _expressionBuilder;
        readonly IPropertyNameResolver _propertyNameResolver;
        readonly IResolveValidationMessages _messageResolver;

        /// <summary>
        ///     Constructor
        /// </summary>
        public ApplyNullableDateValidator(T target,
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
        ///     Applies a nullable date based comparison validator
        /// </summary>
        public IFluentCondition<T> Apply(IComparisonValidator<DateTime, DateTime> validator, Validation.Enums.ValidationType type, DateTime value)
        {
            Expression<Func<T, DateTime?>> expression = _currentContext.GetExpression<DateTime?>();
            Func<T, DateTime?> compiled = _expressionBuilder.Compile(expression);
            Expression<Func<T, bool>> derived = f => validator.Validate(_expressionBuilder.Invoke(compiled, _target) ?? default(DateTime), value);
            var propertyName = _propertyNameResolver.Resolve<T, DateTime?>(expression);
            var message = _messageResolver.Resolve(expression, value, type, string.Empty);
            return new FluentCondition<T>(derived, _currentContext.ConditionType, propertyName, message, string.Empty, _currentContext.ConditionOutput);
        }

        /// <summary>
        ///     Applies a nullable date based validator
        /// </summary>
        public IFluentCondition<T> Apply(IComparisonValidator<DateTime, Between<DateTime>> validator, Validation.Enums.ValidationType type, DateTime lower, DateTime upper)
        {
            Expression<Func<T, DateTime?>> expression = _currentContext.GetExpression<DateTime?>();
            Func<T, DateTime?> compiled = _expressionBuilder.Compile(expression);
            Expression<Func<T, bool>> derived = f => validator.Validate(_expressionBuilder.Invoke(compiled, _target) ?? default(DateTime), new Between<DateTime>() { Lower = lower, Upper = upper });
            var propertyName = _propertyNameResolver.Resolve<T, DateTime?>(expression);
            var message = _messageResolver.Resolve(expression, lower, upper, type, string.Empty);
            return new FluentCondition<T>(derived, _currentContext.ConditionType, propertyName, message, string.Empty, _currentContext.ConditionOutput);
        }

        /// <summary>
        ///     Applies a nullable date based validator
        /// </summary>
        public IFluentCondition<T> Apply(IValidator<DateTime> validator, Validation.Enums.ValidationType type)
        {
            Expression<Func<T, DateTime?>> expression = _currentContext.GetExpression<DateTime?>();
            Func<T, DateTime?> compiled = _expressionBuilder.Compile(expression);
            Expression<Func<T, bool>> derived = f => validator.Validate(_expressionBuilder.Invoke(compiled, _target)??default(DateTime));
            var propertyName = _propertyNameResolver.Resolve<T, DateTime?>(expression);
            var message = _messageResolver.Resolve(expression, type, string.Empty);
            return new FluentCondition<T>(derived, _currentContext.ConditionType, propertyName, message, string.Empty, _currentContext.ConditionOutput);
        }
    }
}
