using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MFlow.Core.Conditions;
using MFlow.Core.Events;

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
        IFluentValidation<T> Before(Expression<Func<T, DateTime>> expression, DateTime value, string message = "", ConditionType conditionType = ConditionType.And);

        /// <summary>
        ///     Checks if the expression evaluates to a date that is greater than the value 
        /// </summary>
        IFluentValidation<T> After(Expression<Func<T, DateTime>> expression, DateTime value, string message = "", ConditionType conditionType = ConditionType.And);

        /// <summary>
        ///     Checks if the expression evaluates to a date that is equal to the value 
        /// </summary>
        IFluentValidation<T> On(Expression<Func<T, DateTime>> expression, DateTime value, string message = "", ConditionType conditionType = ConditionType.And);

    }
}
