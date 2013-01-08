﻿namespace MFlow.Core.Validation
{
    /// <summary>
    ///     A fluent validation interface
    /// </summary>
    public partial interface IFluentValidation<T>
    {
        /// <summary>
        ///     Checks if the expression evaluates to an int that is less than the value 
        /// </summary>
        IFluentValidation<T> IsLessThan(int value);

        /// <summary>
        ///     Checks if the expression evaluates to an int that is greater than the value 
        /// </summary>
        IFluentValidation<T> IsGreaterThan(int value);

        /// <summary>
        ///     Checks if the expression evaluates to an int that is less than or equal to the value 
        /// </summary>
        IFluentValidation<T> IsLessThanOrEqualTo(int value);

        /// <summary>
        ///     Checks if the expression evaluates to an int that is greater than or equal to the value 
        /// </summary>
        IFluentValidation<T> IsGreaterThanOrEqualTo(int value);
    }
}
