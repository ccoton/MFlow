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
        ///     Checks if the expressions evaluates to a string that is empty
        /// </summary>
        public IFluentValidation<T> NotEmpty(Expression<Func<T, string>> expression, string message = "")
        {
            Func<T, string> compiled = expression.Compile();
            Expression<Func<T, bool>> derived = f => !string.IsNullOrEmpty(compiled.Invoke(_target));
            base.If(derived, _resolver.Resolve<T, string>(expression), message);
            return this;
        }

        /// <summary>
        ///     Checks if the expression evaluates to a string that matches the regEx 
        /// </summary>
        public IFluentValidation<T> RegEx(Expression<Func<T, string>> expression, string regEx,  string message = "", ConditionType conditionType = ConditionType.And)
        {
            Func<T, string> compiled = expression.Compile();
            Expression<Func<T, bool>> derived = f => !string.IsNullOrEmpty(compiled.Invoke(_target)) && new Regex(regEx).IsMatch(compiled.Invoke(_target));
            base.And(derived, _resolver.Resolve<T, string>(expression), message);
            return this;
        }

        /// <summary>
        ///     Checks if the expression evaluates to a string that is an email address 
        /// </summary>
        public IFluentValidation<T> IsEmail(Expression<Func<T, string>> expression, string message = "", ConditionType conditionType = ConditionType.And)
        {
            RegEx(expression, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", message: message, conditionType: conditionType);
            return this;
        }

        /// <summary>
        ///     Checks if the expressions evaluates to a string that contains value
        /// </summary>
        public IFluentValidation<T> Contains(Expression<Func<T, string>> expression, string value, string message = "")
        {
            Func<T, string> compiled = expression.Compile();
            Expression<Func<T, bool>> derived = f => !string.IsNullOrEmpty(compiled.Invoke(_target)) && compiled.Invoke(_target).Contains(value);
            base.If(derived, _resolver.Resolve<T, string>(expression), message);
            return this;
        }
    }
}
