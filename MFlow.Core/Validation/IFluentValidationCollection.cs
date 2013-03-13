using System;
using System.Linq.Expressions;

namespace MFlow.Core.Validation
{
    /// <summary>
    ///     A fluent validation interface
    /// </summary>
    public partial interface IFluentValidationCollection<T> : IFluentValidationGeneric<T>
    {
        /// <summary>
        ///     Checks if the expression evaluates to a collection containing any item equal to the value
        /// </summary>
        IFluentValidation<T> Any<C>(C value);

        /// <summary>
        ///     Checks if the expression evaluates to a collection not containing any item equal to the value
        /// </summary>
        IFluentValidation<T> None<C>(C value);
    }
}
