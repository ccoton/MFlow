using System;
using System.Linq.Expressions;
using MFlow.Core.Conditions;
using MFlow.Core.Internal.Validators.Numbers;

namespace MFlow.Core.Validation
{
    /// <summary>
    ///     A fluent validation implementation
    /// </summary>
    public partial class FluentValidation<T> : FluentConditions<T>, IFluentValidation<T>
    {
        /// <summary>
        ///     Checks if the expression evaluates to an int that is less that the value 
        /// </summary>
        public IFluentValidation<T> IsLessThan (int value)
        {
            var lessThanValidator = new LessThanValidator ();
            Expression<Func<T, int>> expression = _currentContext.GetExpression<int> ();
            Func<T, int> compiled = _expressionBuilder.Compile (expression);
            Expression<Func<T, bool>> derived = f => lessThanValidator.Validate (_expressionBuilder.Invoke (compiled, _target), value);
            If (derived, _resolver.Resolve<T, int> (expression), _messageResolver.Resolve (expression, value, Enums.ValidationType.LessThan, string.Empty));
            return this;
        }

        /// <summary>
        ///     Checks if the expression evaluates to an int that is greater that the value 
        /// </summary>
        public IFluentValidation<T> IsGreaterThan (int value)
        {
            var greaterThanValidator = new GreaterThanValidator ();
            Expression<Func<T, int>> expression = _currentContext.GetExpression<int> ();
            Func<T, int> compiled = _expressionBuilder.Compile (expression);
            Expression<Func<T, bool>> derived = f => greaterThanValidator.Validate (_expressionBuilder.Invoke (compiled, _target), value);
            If (derived, _resolver.Resolve<T, int> (expression), _messageResolver.Resolve (expression, value, Enums.ValidationType.GreaterThan, string.Empty));
            return this;
        }

        /// <summary>
        ///     Checks if the expression evaluates to an int that is less than or equal to the value 
        /// </summary>
        public IFluentValidation<T> IsLessThanOrEqualTo (int value)
        {
            var lessThanOrEqualToValidator = new LessThanOrEqualToValidator ();
            Expression<Func<T, int>> expression = _currentContext.GetExpression<int> ();
            Func<T, int> compiled = _expressionBuilder.Compile (expression);
            Expression<Func<T, bool>> derived = f => lessThanOrEqualToValidator.Validate (_expressionBuilder.Invoke (compiled, _target), value);
            If (derived, _resolver.Resolve<T, int> (expression), _messageResolver.Resolve (expression, value, Enums.ValidationType.LessThanOrEqualTo, string.Empty));
            return this;
        }

        /// <summary>
        ///     Checks if the expression evaluates to an int that is greater than or equal to the value 
        /// </summary>
        public IFluentValidation<T> IsGreaterThanOrEqualTo (int value)
        {
            var greaterThanOrEqualToValidator = new GreaterThanOrEqualToValidator ();
            Expression<Func<T, int>> expression = _currentContext.GetExpression<int> ();
            Func<T, int> compiled = _expressionBuilder.Compile (expression);
            Expression<Func<T, bool>> derived = f => greaterThanOrEqualToValidator.Validate (_expressionBuilder.Invoke (compiled, _target), value);
            If (derived, _resolver.Resolve<T, int> (expression), _messageResolver.Resolve (expression, value, Enums.ValidationType.GreaterThanOrEqualTo, string.Empty));
            return this;
        }
   
    }
}
