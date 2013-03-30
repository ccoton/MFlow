using System;
using System.Collections.Generic;
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

        /// <summary>
        ///     Checks if the expression evaluates to a collection containing all items in the values collection
        /// </summary>
        IFluentValidation<T> All<C>(ICollection<C> values);

        /// <summary>
        ///     Checks if the expression evaluates to a collection containing an identicalset of items as the values collection
        /// </summary>
        IFluentValidation<T> IsSame<C>(ICollection<C> values);
    }
}
