using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MFlow.Core.Conditions;
using MFlow.Core.Internal.Validators.Generic;

namespace MFlow.Core.Validation
{
    /// <summary>
    ///     A fluent validation implementation
    /// </summary>
    public partial class FluentValidation<T> : FluentConditions<T>, IFluentValidation<T>
    {
        IList<Func<IFluentValidation<T>>> _dependencies = new List<Func<IFluentValidation<T>>>();

        /// <summary>
        ///     Checks if the expression evaluates to an object that is equal to the value expression 
        /// </summary>
        public IFluentValidation<T> IsEqualTo<C>(Expression<Func<T, C>> valueExpression)
        {
            var equalToValidator = _validatorFactory.GetValidator<C,C, IEqualToValidator<C,C>>();
            Expression<Func<T, C>> expression = _currentContext.GetExpression<C>();
            Func<T, C> compiled = _expressionBuilder.Compile(expression);
            Func<T, C> compiledValue = _expressionBuilder.Compile(valueExpression);
            Expression<Func<T, bool>> derived = f => equalToValidator.Validate(_expressionBuilder.Invoke(compiled, _target), _expressionBuilder.Invoke(compiledValue, _target));
            If(derived, _resolver.Resolve<T, C>(expression), _messageResolver.Resolve(expression, valueExpression, Enums.ValidationType.Equal, string.Empty));
            return this;
        }

        /// <summary>
        ///     Checks if the expression evaluates to an object that is equal to the value 
        /// </summary>
        public IFluentValidation<T> IsEqualTo<C>(C value)
        {
            var equalToValidator = _validatorFactory.GetValidator<C,C, IEqualToValidator<C,C>>();
            Expression<Func<T, C>> expression = _currentContext.GetExpression<C>();
            Func<T, C> compiled = _expressionBuilder.Compile(expression);
            Expression<Func<T, bool>> derived = f => equalToValidator.Validate(_expressionBuilder.Invoke(compiled, _target), value);
            If(derived, _resolver.Resolve<T, C>(expression), _messageResolver.Resolve(expression, value, Enums.ValidationType.Equal, string.Empty));
            return this;
        }

        /// <summary>
        ///     Checks if the expression evaluates to an object that is equal to the value expression 
        /// </summary>
        public IFluentValidation<T> IsNotEqualTo<C>(Expression<Func<T, C>> valueExpression)
        {
            var notEqualToValidator = _validatorFactory.GetValidator<C,C, INotEqualToValidator<C,C>>();
            Expression<Func<T, C>> expression = _currentContext.GetExpression<C>();
            Func<T, C> compiled = _expressionBuilder.Compile(expression);
            Func<T, C> compiledValue = _expressionBuilder.Compile(valueExpression);
            Expression<Func<T, bool>> derived = f => notEqualToValidator.Validate(_expressionBuilder.Invoke(compiled, _target), _expressionBuilder.Invoke(compiledValue, _target));
            If(derived, _resolver.Resolve<T, C>(expression), _messageResolver.Resolve(expression, valueExpression, Enums.ValidationType.NotEqual, string.Empty));
            return this;
        }

        /// <summary>
        ///     Checks if the expression evaluates to an object that is not equal to the value 
        /// </summary>
        public IFluentValidation<T> IsNotEqualTo<C>(C value)
        {
            var notEqualToValidator = _validatorFactory.GetValidator<C,C, INotEqualToValidator<C,C>>();
            ;
            Expression<Func<T, C>> expression = _currentContext.GetExpression<C>();
            Func<T, C> compiled = _expressionBuilder.Compile(expression);
            Expression<Func<T, bool>> derived = f => notEqualToValidator.Validate(_expressionBuilder.Invoke(compiled, _target), value);
            If(derived, _resolver.Resolve<T, C>(expression), _messageResolver.Resolve(expression, value, Enums.ValidationType.NotEqual, string.Empty));
            return this;
        }

        /// <summary>
        ///     Is the item required
        /// </summary>
        public IFluentValidation<T> IsRequired<C>()
        {
            var requiredValidator = _validatorFactory.GetValidator<C, IRequiredValidator<C>>();
            Expression<Func<T, C>> expression = _currentContext.GetExpression<C>();
            Func<T, C> compiled = _expressionBuilder.Compile(expression);
            Expression<Func<T, bool>> derived = f => requiredValidator.Validate(_expressionBuilder.Invoke(compiled, _target));
            If(derived, _resolver.Resolve<T, C>(expression), _messageResolver.Resolve(expression, Enums.ValidationType.IsRequired, string.Empty));
            return this;
        }

        /// <summary>
        ///     Evaluates another validation instance that this one depends on
        /// </summary>
        public IFluentValidation<T> DependsOn<D>(IFluentValidation<D> validator)
        {
            Expression<Func<T, bool>> derived = f => validator.Satisfied(true);
            base.And(derived);
            return this;
        }

        /// <summary>
        ///     Evaluates another validation instance that this one depends on
        /// </summary>
        public IFluentValidation<T> DependsOn<D>(Expression<Func<T, D>> validator) where D : IFluentValidation<T>
        {
            Func<T, D> compiled = _expressionBuilder.Compile(validator);
            _dependencies.Add(() => compiled.Invoke(_target));
            Expression<Func<T, bool>> derived = f => compiled.Invoke(_target).Satisfied(true);
            base.And(derived, message: string.Empty);
            return this;
        }
    }
}
