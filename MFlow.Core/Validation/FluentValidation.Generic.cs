using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MFlow.Core.Conditions;
using MFlow.Core.Internal.Validators;
using MFlow.Core.Internal.Validators.Generic;
using MFlow.Core.Validation.Checker;

namespace MFlow.Core.Validation
{
    /// <summary>
    ///     A fluent validation implementation
    /// </summary>
    public partial class FluentValidation<T> : FluentConditions<T>, IFluentValidation<T>, IFluentValidationGeneric<T>
    {
        IList<Func<IFluentValidation<T>>> _dependencies = new List<Func<IFluentValidation<T>>>();

        /// <summary>
        ///     Checks if the expression evaluates to an object that is equal to the value expression
        /// </summary>
        public IFluentValidation<T> IsEqualTo<C>(Expression<Func<T, C>> valueExpression)
        {
            return ApplyGenericComparisonValidator(_validatorFactory.GetValidator<C, C, IEqualToValidator<C,C>>(), Enums.ValidationType.Equal, valueExpression);
        }

        /// <summary>
        ///     Checks if the expression evaluates to an object that is equal to the value
        /// </summary>
        public IFluentValidation<T> IsEqualTo<C>(C value)
        {
            return ApplyGenericComparisonValidator(_validatorFactory.GetValidator<C, C, IEqualToValidator<C,C>>(), Enums.ValidationType.Equal, value);
        }

        /// <summary>
        ///     Checks if the expression evaluates to an object that is equal to the value expression
        /// </summary>
        public IFluentValidation<T> IsNotEqualTo<C>(Expression<Func<T, C>> valueExpression)
        {
            return ApplyGenericComparisonValidator(_validatorFactory.GetValidator<C, C, INotEqualToValidator<C,C>>(), Enums.ValidationType.NotEqual, valueExpression);
        }

        /// <summary>
        ///     Checks if the expression evaluates to an object that is not equal to the value
        /// </summary>
        public IFluentValidation<T> IsNotEqualTo<C>(C value)
        {
            return ApplyGenericComparisonValidator(_validatorFactory.GetValidator<C, C, INotEqualToValidator<C,C>>(), Enums.ValidationType.NotEqual, value);
        }

        /// <summary>
        ///     Is the item required
        /// </summary>
        public IFluentValidation<T> IsRequired<C>()
        {
            return ApplyGenericValidator(_validatorFactory.GetValidator<C, IRequiredValidator<C>>(), Enums.ValidationType.IsRequired);
        }
        
        IFluentValidation<T> ApplyGenericValidator<C>(IValidator<C> validator, Enums.ValidationType type)
        {
            Expression<Func<T, C>> expression = _currentContext.GetExpression<C>();
            Func<T, C> compiled = _expressionBuilder.Compile(expression);
            Expression<Func<T, bool>> derived = f => validator.Validate(_expressionBuilder.Invoke(compiled, _target));
            BuildIf(derived, _resolver.Resolve<T, C>(expression), _messageResolver.Resolve(expression, type, string.Empty));
            return this;
        }
        
        IFluentValidation<T> ApplyGenericComparisonValidator<C>(IComparisonValidator<C, C> validator, Enums.ValidationType type, C value)
        {
            Expression<Func<T, C>> expression = _currentContext.GetExpression<C>();
            Func<T, C> compiled = _expressionBuilder.Compile(expression);
            Expression<Func<T, bool>> derived = f => validator.Validate(_expressionBuilder.Invoke(compiled, _target), value);
            BuildIf(derived, _resolver.Resolve<T, C>(expression), _messageResolver.Resolve(expression, value, type, string.Empty));
            return this;
        }
        
        IFluentValidation<T> ApplyGenericComparisonValidator<C>(IComparisonValidator<C, C> validator, Enums.ValidationType type, Expression<Func<T, C>> value)
        {
            Expression<Func<T, C>> expression = _currentContext.GetExpression<C>();
            Func<T, C> compiled = _expressionBuilder.Compile(expression);
            Func<T, C> compiledValue = _expressionBuilder.Compile(value);
            Expression<Func<T, bool>> derived = f => validator.Validate(_expressionBuilder.Invoke(compiled, _target), _expressionBuilder.Invoke(compiledValue, _target));
            BuildIf(derived, _resolver.Resolve<T, C>(expression), _messageResolver.Resolve(expression, value, type, string.Empty));
            return this;
        }
    }
}
