using MFlow.Core.Conditions;
using MFlow.Core.Validation.Context;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MFlow.Core.Internal.Validators.Collections
{
    class ApplyCollectionValidator<T, TValidate> : IApplyCollectionValidator<T, TValidate>
    {
        readonly T _target;
        readonly ICurrentValidationContext<T> _currentContext;
        readonly IExpressionBuilder<T> _expressionBuilder;
        readonly IPropertyNameResolver _propertyNameResolver;
        readonly IMessageResolver _messageResolver;

        public ApplyCollectionValidator(T target,
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

        public IFluentCondition<T> Apply(IComparisonValidator<ICollection<TValidate>, TValidate> validator, Validation.Enums.ValidationType type, TValidate value)
        {
            Expression<Func<T, ICollection<TValidate>>> expression = _currentContext.GetExpression<ICollection<TValidate>>();
            Func<T, ICollection<TValidate>> compiled = _expressionBuilder.Compile(expression);
            Expression<Func<T, bool>> derived = f => validator.Validate(_expressionBuilder.Invoke(compiled, _target), value);
            var propertyName = _propertyNameResolver.Resolve<T, ICollection<TValidate>>(expression);
            var message = _messageResolver.Resolve(expression, value, type, string.Empty);
            return new FluentCondition<T>(derived, _currentContext.ConditionType, propertyName, message, string.Empty, _currentContext.ConditionOutput);
        }
    }
}
