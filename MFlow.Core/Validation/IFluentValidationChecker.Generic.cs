using System;
using System.Linq.Expressions;

namespace MFlow.Core.Validation
{
    /// <summary>
    ///     A fluent validation interface
    /// </summary>
    public partial interface IFluentValidationChecker<T>
    {
        /// <summary>
        ///     Checks if the expression evaluates to an object that is equal to the value expression 
        /// </summary>
        IFluentValidation<T> IsEqualTo<C>(Expression<Func<T, C>> valueExpression);

        /// <summary>
        ///     Checks if the expression evaluates to an object that is equal to the value 
        /// </summary>
        IFluentValidation<T> IsEqualTo<C>(C value);

        /// <summary>
        ///     Checks if the expression evaluates to an object that is not equal to the value expression 
        /// </summary>
        IFluentValidation<T> IsNotEqualTo<C>(Expression<Func<T, C>> valueExpression);

        /// <summary>
        ///     Checks if the expression evaluates to an object that is not equal to the value 
        /// </summary>
        IFluentValidation<T> IsNotEqualTo<C>(C value);

        /// <summary>
        ///     Is the item required
        /// </summary>
        IFluentValidation<T> IsRequired<C>();
    }
}
