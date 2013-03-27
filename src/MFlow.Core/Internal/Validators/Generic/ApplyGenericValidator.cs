using MFlow.Core.Conditions;
using MFlow.Core.Validation.Context;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MFlow.Core.Validation.Enums;
using MFlow.Core.MessageResolver;

namespace MFlow.Core.Internal.Validators.Generic
{
    /// <summary>
    ///     Applies a generic based validator
    /// </summary>
    class ApplyGenericValidator<T, TValidate> : IApplyGenericValidator<T, TValidate>
    {
        readonly T _target;
        readonly ICurrentValidationContext<T> _currentContext;
        readonly IExpressionBuilder<T> _expressionBuilder;
        readonly IPropertyNameResolver _propertyNameResolver;
        readonly IResolveValidationMessages _messageResolver;

        /// <summary>
        ///     Constructor
        /// </summary>
        public ApplyGenericValidator(T target,
            ICurrentValidationContext<T> context,
            IExpressionBuilder<T> expressionBuilder, 
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
        ///     Apply the validator
        /// </summary>
        public IFluentCondition<T> Apply(IValidator<TValidate> validator, Validation.Enums.ValidationType type)
        {
            Expression<Func<T, TValidate>> expression = _currentContext.GetExpression<TValidate>();
            Func<T, TValidate> compiled = _expressionBuilder.Compile(expression);
            Expression<Func<T, bool>> derived = f => validator.Validate(_expressionBuilder.Invoke(compiled, _target));
            var propertyName = _propertyNameResolver.Resolve<T, TValidate>(expression);
            var message = _messageResolver.Resolve(expression, type, string.Empty);
            return new FluentCondition<T>(derived, _currentContext.ConditionType, propertyName, message, string.Empty, _currentContext.ConditionOutput);
        }

        /// <summary>
        ///     Apply the comparison validator
        /// </summary>
        public IFluentCondition<T> Apply(IComparisonValidator<TValidate, TValidate> validator, Validation.Enums.ValidationType type, TValidate value)
        {
            Expression<Func<T, TValidate>> expression = _currentContext.GetExpression<TValidate>();
            Func<T, TValidate> compiled = _expressionBuilder.Compile(expression);
            Expression<Func<T, bool>> derived = f => validator.Validate(_expressionBuilder.Invoke(compiled, _target), value);
            var propertyName = _propertyNameResolver.Resolve<T, TValidate>(expression);
            var message = _messageResolver.Resolve(expression, value, type, string.Empty);
            return new FluentCondition<T>(derived, _currentContext.ConditionType, propertyName, message, string.Empty, _currentContext.ConditionOutput);
        }

        /// <summary>
        ///     Apply the comparison validator
        /// </summary>
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
