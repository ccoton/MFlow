﻿using System;
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
        ///     Checks if the expression evaluates to an int that is less than the value 
        /// </summary>
        IFluentValidation<T> LessThan(Expression<Func<T, int>> expression, int value, string message = "", ConditionType conditionType = ConditionType.And);

        /// <summary>
        ///     Checks if the expression evaluates to an int that is greater than the value 
        /// </summary>
        IFluentValidation<T> GreaterThan(Expression<Func<T, int>> expression, int value, string message = "", ConditionType conditionType = ConditionType.And);

        /// <summary>
        ///     Checks if the expression evaluates to an int that is less than or equal to the value 
        /// </summary>
        IFluentValidation<T> LessThanOrEqualTo(Expression<Func<T, int>> expression, int value, string message = "", ConditionType conditionType = ConditionType.And);

        /// <summary>
        ///     Checks if the expression evaluates to an int that is greater than or equal to the value 
        /// </summary>
        IFluentValidation<T> GreaterThanOrEqualTo(Expression<Func<T, int>> expression, int value, string message = "", ConditionType conditionType = ConditionType.And);
    }
}