using System;
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
        ///     Checks if the expression evaluates to an int that is less that the value 
        /// </summary>
        public IFluentValidation<T> LessThan(Expression<Func<T, int>> expression, int value, string message = "", ConditionType conditionType = ConditionType.And)
        {
            Func<T, int> compiled = expression.Compile();
            Expression<Func<T, bool>> derived = f => compiled.Invoke(_target) < value;
            If(derived, _resolver.Resolve<T, int>(expression), message, conditionType);
            return this;
        }

        /// <summary>
        ///     Checks if the expression evaluates to an int that is greater that the value 
        /// </summary>
        public IFluentValidation<T> GreaterThan(Expression<Func<T, int>> expression, int value, string message = "", ConditionType conditionType = ConditionType.And)
        {
            Func<T, int> compiled = expression.Compile();
            Expression<Func<T, bool>> derived = f => compiled.Invoke(_target) > value;
            If(derived, _resolver.Resolve<T, int>(expression), message, conditionType);
            return this;
        }

        /// <summary>
        ///     Checks if the expression evaluates to an int that is less than or equal to the value 
        /// </summary>
        public IFluentValidation<T> LessThanOrEqualTo(Expression<Func<T, int>> expression, int value, string message = "", ConditionType conditionType = ConditionType.And)
        {
            Func<T, int> compiled = expression.Compile();
            Expression<Func<T, bool>> derived = f => compiled.Invoke(_target) <= value;
            If(derived, _resolver.Resolve<T, int>(expression), message, conditionType);
            return this;
        }

        /// <summary>
        ///     Checks if the expression evaluates to an int that is greater than or equal to the value 
        /// </summary>
        public IFluentValidation<T> GreaterThanOrEqualTo(Expression<Func<T, int>> expression, int value, string message = "", ConditionType conditionType = ConditionType.And)
        {
            Func<T, int> compiled = expression.Compile();
            Expression<Func<T, bool>> derived = f => compiled.Invoke(_target) >= value;
            If(derived, _resolver.Resolve<T, int>(expression), message, conditionType);
            return this;
        }
   
    }
}
