using System;

namespace MFlow.Core.Validation
{
    /// <summary>
    ///     A fluent validation interface
    /// </summary>
    public partial interface IFluentValidation<T>
    {
        /// <summary>
        ///     Checks if the expression evaluates to a date that is less than the value 
        /// </summary>
        IFluentValidation<T> IsBefore(DateTime value);

        /// <summary>
        ///     Checks if the expression evaluates to a date that is greater than the value 
        /// </summary>
        IFluentValidation<T> IsAfter(DateTime value);

        /// <summary>
        ///     Checks if the expression evaluates to a date that is equal to the value 
        /// </summary>
        IFluentValidation<T> IsOn(DateTime value);

        /// <summary>
        ///     Checks if the expression evaluates to a date that is this year
        /// </summary>
        IFluentValidation<T> IsThisYear();

        /// <summary>
        ///     Checks if the expression evaluates to a date that is this month
        /// </summary>
        IFluentValidation<T> IsThisMonth();

        /// <summary>
        ///     Checks if the expression evaluates to a date that is this week
        /// </summary>
        IFluentValidation<T> IsThisWeek();

        /// <summary>
        ///     Checks if the expression evaluates to a date that is today
        /// </summary>
        IFluentValidation<T> IsToday();

    }
}
