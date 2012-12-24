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
        ///     Checks if the expressions evaluates to a string that is empty
        /// </summary>
        IFluentValidation<T> NotEmpty(Expression<Func<T, string>> expression, string message = "");

        /// <summary>
        ///     Checks if the expression evaluates to a string that matches the regEx 
        /// </summary>
        IFluentValidation<T> RegEx(Expression<Func<T, string>> expression, string regEx, string message = "", ConditionType conditionType = ConditionType.And);

        /// <summary>
        ///     Checks if the expression evaluates to a string that is an email address 
        /// </summary>
        IFluentValidation<T> IsEmail(Expression<Func<T, string>> expression, string message = "", ConditionType conditionType = ConditionType.And);

    }
}
