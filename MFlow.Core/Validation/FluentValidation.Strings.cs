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
using MFlow.Core.Internal.Validators;

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
        public IFluentValidation<T> IsNotEmpty()
        {
            Expression<Func<T, string>> expression = GetCurrentExpression<string>();
            Func<T, string> compiled = expression.Compile();
            Expression<Func<T, bool>> derived = f => !string.IsNullOrEmpty(compiled.Invoke(_target));
            base.If(derived, _resolver.Resolve<T, string>(expression), _messageResolver.Resolve(expression, Enums.ValidationType.NotEmpty, string.Empty));
            return this;
        }

        /// <summary>
        ///     Checks if the expression evaluates to a string that matches the regEx 
        /// </summary>
        public IFluentValidation<T> Mathes(string regEx, ConditionType conditionType = ConditionType.And)
        {
            Expression<Func<T, string>> expression = GetCurrentExpression<string>();
            Func<T, string> compiled = expression.Compile();
            Expression<Func<T, bool>> derived = f => !string.IsNullOrEmpty(compiled.Invoke(_target)) && new Regex(regEx).IsMatch(compiled.Invoke(_target));
            base.And(derived, _resolver.Resolve<T, string>(expression), _messageResolver.Resolve(expression, regEx, Enums.ValidationType.RegEx, string.Empty));
            return this;
        }

        /// <summary>
        ///     Checks if the expression evaluates to a string that is an email address 
        /// </summary>
        public IFluentValidation<T> IsEmail(ConditionType conditionType = ConditionType.And)
        {
            Expression<Func<T, string>> expression = GetCurrentExpression<string>();
            var regEx = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
            Func<T, string> compiled = expression.Compile();
            Expression<Func<T, bool>> derived = f => !string.IsNullOrEmpty(compiled.Invoke(_target)) && new Regex(regEx).IsMatch(compiled.Invoke(_target));
            base.And(derived, _resolver.Resolve<T, string>(expression), _messageResolver.Resolve(expression, Enums.ValidationType.IsEmail, string.Empty));
            return this;
        }

        /// <summary>
        ///     Checks if the expressions evaluates to a string that contains value
        /// </summary>
        public IFluentValidation<T> Contains(string value)
        {
            Expression<Func<T, string>> expression = GetCurrentExpression<string>();
            Func<T, string> compiled = expression.Compile();
            Expression<Func<T, bool>> derived = f => !string.IsNullOrEmpty(compiled.Invoke(_target)) && compiled.Invoke(_target).Contains(value);
            base.If(derived, _resolver.Resolve<T, string>(expression), _messageResolver.Resolve(expression, value, Enums.ValidationType.Contains, string.Empty));
            return this;
        }

        /// <summary>
        ///     Checks if the expression evaluates to a string that is of length
        /// </summary>
        public IFluentValidation<T> IsLength(int length)
        {
            Expression<Func<T, string>> expression = GetCurrentExpression<string>();
            Func<T, string> compiled = expression.Compile();
            Expression<Func<T, bool>> derived = f => !string.IsNullOrEmpty(compiled.Invoke(_target)) && compiled.Invoke(_target).Length == length;
            base.If(derived, _resolver.Resolve<T, string>(expression), _messageResolver.Resolve(expression, length.ToString(), Enums.ValidationType.IsLength, string.Empty));
            return this;
        }

        /// <summary>
        ///     Check if the expressions evaluates to a string matching a credit card pattern
        /// </summary>
        public IFluentValidation<T> IsCreditCard(ConditionType conditionType = ConditionType.And)
        {
            var internalValidator = new CreditCardValidator();
            Expression<Func<T, string>> expression = GetCurrentExpression<string>();
            Func<T, string> compiled = expression.Compile();
            Expression<Func<T, bool>> derived = f => internalValidator.Validate(compiled.Invoke(_target));
            base.If(derived, _resolver.Resolve<T, string>(expression), _messageResolver.Resolve(expression, Enums.ValidationType.IsCreditCard, string.Empty));
            return this;
        }

        /// <summary>
        ///     Check if the expression evaluates to a string that is a post code
        /// </summary>
        public IFluentValidation<T> IsPostCode(ConditionType conditionType = ConditionType.And)
        {
            var regEx = @"(GIR 0AA)|((([A-Z-[QVX]][0-9][0-9]?)|(([A-Z-[QVX]][A-Z-[IJZ]][0-9][0-9]?)|(([A-Z-[QVX]][0-9][A-HJKSTUW])|([A-Z-[QVX]][A-Z-[IJZ]][0-9][ABEHMNPRVWXY])))) [0-9][A-Z-[CIKMOV]]{2})";
            Expression<Func<T, string>> expression = GetCurrentExpression<string>();
            Func<T, string> compiled = expression.Compile();
            Expression<Func<T, bool>> derived = f => !string.IsNullOrEmpty(compiled.Invoke(_target)) && new Regex(regEx).IsMatch(compiled.Invoke(_target));
            base.If(derived, _resolver.Resolve<T, string>(expression), _messageResolver.Resolve(expression, Enums.ValidationType.IsPostCode, string.Empty));
            return this;
        }

        /// <summary>
        ///     Check if the expression evaluates to a strin that is a zip code
        /// </summary>
        public IFluentValidation<T> IsZipCode(ConditionType conditionType = ConditionType.And)
        {
            var regEx = @"^[0-9]{5}(-[0-9]{4})?$";
            Expression<Func<T, string>> expression = GetCurrentExpression<string>();
            Func<T, string> compiled = expression.Compile();
            Expression<Func<T, bool>> derived = f => !string.IsNullOrEmpty(compiled.Invoke(_target)) && new Regex(regEx).IsMatch(compiled.Invoke(_target));
            base.If(derived, _resolver.Resolve<T, string>(expression), _messageResolver.Resolve(expression, Enums.ValidationType.IsPostCode, string.Empty));
            return this;
        }
    }
}
