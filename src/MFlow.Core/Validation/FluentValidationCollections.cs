using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MFlow.Core.Conditions;
using MFlow.Core.Internal.Validators;
using MFlow.Core.Internal.Validators.Generic;

namespace MFlow.Core.Validation
{
    /// <summary>
    ///     A fluent validation implementation
    /// </summary>
    public partial class FluentValidation<T> : FluentConditions<T>, IFluentValidation<T>, IFluentValidationCollection<T>
    {
        /// <summary>
        ///     Checks if the expression evaluates to a collection containing any item equal to the value
        /// </summary>
        public IFluentValidation<T> Any<C>(C value)
        {
            return ApplyGenericCollectionValidator(_validatorFactory.GetValidator<ICollection<C>, C, IAnyValidator<C>>(), Enums.ValidationType.Any, value);
        }

        /// <summary>
        ///     Checks if the expression evaluates to a collection not containing any item equal to the value
        /// </summary>
        public IFluentValidation<T> None<C>(C value)
        {
            return ApplyGenericCollectionValidator(_validatorFactory.GetValidator<ICollection<C>, C, INoneValidator<C>>(), Enums.ValidationType.None, value);
        }

        IFluentValidation<T> ApplyGenericCollectionValidator<C>(ICollection<IComparisonValidator<ICollection<C>, C>> validators, Enums.ValidationType type, C value)
        {
            foreach (var validator in validators)
            {
                Expression<Func<T, ICollection<C>>> expression = _currentContext.GetExpression<ICollection<C>>();
                Func<T, ICollection<C>> compiled = _expressionBuilder.Compile(expression);
                Expression<Func<T, bool>> derived = f => validator.Validate(_expressionBuilder.Invoke(compiled, _target), value);
                BuildIf(derived, _resolver.Resolve<T, ICollection<C>>(expression), _messageResolver.Resolve(expression, value, type, string.Empty));
            }
            return this;
        }
    }
}
