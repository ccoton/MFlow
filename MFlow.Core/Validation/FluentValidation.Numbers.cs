using System;
using System.Linq.Expressions;
using MFlow.Core.Conditions;
using MFlow.Core.Internal.Validators;
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
        public IFluentValidation<T> IsLessThan(int value)
        {
            return ApplyIntComparisonValidator(_validatorFactory.GetValidator<int, int, ILessThanValidator>(), Enums.ValidationType.LessThan, value);
        }

        /// <summary>
        ///     Checks if the expression evaluates to an int that is greater that the value
        /// </summary>
        public IFluentValidation<T> IsGreaterThan(int value)
        {
            return ApplyIntComparisonValidator(_validatorFactory.GetValidator<int, int, IGreaterThanValidator>(), Enums.ValidationType.GreaterThan, value);
        }

        /// <summary>
        ///     Checks if the expression evaluates to an int that is less than or equal to the value
        /// </summary>
        public IFluentValidation<T> IsLessThanOrEqualTo(int value)
        {
            return ApplyIntComparisonValidator(_validatorFactory.GetValidator<int, int, ILessThanOrEqualToValidator>(), Enums.ValidationType.LessThanOrEqualTo, value);
        }

        /// <summary>
        ///     Checks if the expression evaluates to an int that is greater than or equal to the value
        /// </summary>
        public IFluentValidation<T> IsGreaterThanOrEqualTo(int value)
        {
            return ApplyIntComparisonValidator(_validatorFactory.GetValidator<int, int, IGreaterThanOrEqualToValidator>(), Enums.ValidationType.GreaterThanOrEqualTo, value);
        }
        
        FluentValidation<T> ApplyIntComparisonValidator(IComparisonValidator<int, int> validator, Enums.ValidationType type, int value)
        {
            Expression<Func<T, int>> expression = _currentContext.GetExpression<int>();
            Func<T, int> compiled = _expressionBuilder.Compile(expression);
            Expression<Func<T, bool>> derived = f => validator.Validate(_expressionBuilder.Invoke(compiled, _target), value);
            If(derived, _resolver.Resolve<T, int>(expression), _messageResolver.Resolve(expression, value, type, string.Empty));
            return this;
        }
        
    }
}
