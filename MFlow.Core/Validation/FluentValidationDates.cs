﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MFlow.Core.Conditions;
using MFlow.Core.Events;
using MFlow.Core.Internal;

namespace MFlow.Core.Validation
{
    /// <summary>
    ///     A fluent validation implementation
    /// </summary>
    public partial class FluentValidation<T> : FluentConditions<T>, IFluentValidation<T>
    {
        /// <summary>
        ///     Checks if the expression evaluates to a date that is less that the value 
        /// </summary>
        public IFluentValidation<T> Before(DateTime value, ConditionType conditionType = ConditionType.And)
        {
            Expression<Func<T, DateTime>> expression = (Expression<Func<T, DateTime>> )_expressions.Last();
            Func<T, DateTime> compiled = expression.Compile();
            Expression<Func<T, bool>> derived = f => compiled.Invoke(_target) < value;
            If(derived, _resolver.Resolve<T, DateTime>(expression), _messageResolver.Resolve(expression, value, Enums.ValidationType.Before, string.Empty), conditionType);
            return this;
        }

        /// <summary>
        ///     Checks if the expression evaluates to a date that is greater than the value 
        /// </summary>
        public IFluentValidation<T> After(DateTime value, ConditionType conditionType = ConditionType.And)
        {
            Expression<Func<T, DateTime>> expression = (Expression<Func<T, DateTime>>)_expressions.Last();
            Func<T, DateTime> compiled = expression.Compile();
            Expression<Func<T, bool>> derived = f => compiled.Invoke(_target) > value;
            If(derived, _resolver.Resolve<T, DateTime>(expression), _messageResolver.Resolve(expression, value, Enums.ValidationType.After, string.Empty), conditionType);
            return this;
        }

        /// <summary>
        ///     Checks if the expression evaluates to a date that is less than or equal to the value 
        /// </summary>
        public IFluentValidation<T> On(DateTime value, ConditionType conditionType = ConditionType.And)
        {
            Expression<Func<T, DateTime>> expression = (Expression<Func<T, DateTime>>)_expressions.Last();
            Func<T, DateTime> compiled = expression.Compile();
            Expression<Func<T, bool>> derived = f => compiled.Invoke(_target).Date == value.Date;
            If(derived, _resolver.Resolve<T, DateTime>(expression), _messageResolver.Resolve(expression, value, Enums.ValidationType.On, string.Empty), conditionType);
            return this;
        }
    }
}
