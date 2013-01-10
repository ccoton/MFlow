using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using MFlow.Core.Conditions;
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
            Expression<Func<T, string>> expression = _currentContext.GetExpression<string>();
            Func<T, string> compiled = _expressionBuilder.Compile(expression);
            Expression<Func<T, bool>> derived = f => !string.IsNullOrEmpty(_expressionBuilder.Invoke(compiled, _target));
            If(derived, _resolver.Resolve<T, string>(expression), _messageResolver.Resolve(expression, Enums.ValidationType.NotEmpty, string.Empty) );
            return this;
        }

        /// <summary>
        ///     Checks if the expression evaluates to a string that matches the regEx 
        /// </summary>
        public IFluentValidation<T> Mathes(string regEx)
        {
            Expression<Func<T, string>> expression = _currentContext.GetExpression<string>();
            Func<T, string> compiled = _expressionBuilder.Compile(expression);
            Expression<Func<T, bool>> derived = f => !string.IsNullOrEmpty(compiled.Invoke(_target)) && new Regex(regEx).IsMatch(compiled.Invoke(_target));
            If(derived, _resolver.Resolve<T, string>(expression), _messageResolver.Resolve(expression, regEx, Enums.ValidationType.RegEx, string.Empty));
            return this;
        }

        /// <summary>
        ///     Checks if the expression evaluates to a string that is an email address 
        /// </summary>
        public IFluentValidation<T> IsEmail()
        {
            Expression<Func<T, string>> expression = _currentContext.GetExpression<string>();
            var regEx = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
            Func<T, string> compiled = _expressionBuilder.Compile(expression);
            Expression<Func<T, bool>> derived = f => !string.IsNullOrEmpty(compiled.Invoke(_target)) && new Regex(regEx).IsMatch(compiled.Invoke(_target));
            If(derived, _resolver.Resolve<T, string>(expression), _messageResolver.Resolve(expression, Enums.ValidationType.IsEmail, string.Empty) );
            return this;
        }

        /// <summary>
        ///     Checks if the expressions evaluates to a string that contains value
        /// </summary>
        public IFluentValidation<T> Contains(string value)
        {
            Expression<Func<T, string>> expression = _currentContext.GetExpression<string>();
            Func<T, string> compiled = _expressionBuilder.Compile(expression);
            Expression<Func<T, bool>> derived = f => !string.IsNullOrEmpty(compiled.Invoke(_target)) && compiled.Invoke(_target).Contains(value);
            If(derived, _resolver.Resolve<T, string>(expression), _messageResolver.Resolve(expression, value, Enums.ValidationType.Contains, string.Empty));
            return this;
        }

        /// <summary>
        ///     Checks if the expression evaluates to a string that is of length
        /// </summary>
        public IFluentValidation<T> IsLength(int length)
        {
            Expression<Func<T, string>> expression = _currentContext.GetExpression<string>();
            Func<T, string> compiled = _expressionBuilder.Compile(expression);
            Expression<Func<T, bool>> derived = f => !string.IsNullOrEmpty(compiled.Invoke(_target)) && compiled.Invoke(_target).Length == length;
            If(derived, _resolver.Resolve<T, string>(expression), _messageResolver.Resolve(expression, length.ToString(), Enums.ValidationType.IsLength, string.Empty));
            return this;
        }
        
        /// <summary>
        ///     Checks if the expression evaluates to a string longer than length
        /// </summary>
        public IFluentValidation<T> IsLongerThan(int length)
        {
            Expression<Func<T, string>> expression = _currentContext.GetExpression<string>();
            Func<T, string> compiled = _expressionBuilder.Compile(expression);
            Expression<Func<T, bool>> derived = f => !string.IsNullOrEmpty(compiled.Invoke(_target)) && compiled.Invoke(_target).Length > length;
            If(derived, _resolver.Resolve<T, string>(expression), _messageResolver.Resolve(expression, length.ToString(), Enums.ValidationType.IsLongerThan, string.Empty));
            return this;
        }
        
        /// <summary>
        ///     Checks if the expression evaluates to a string shorter than length
        /// </summary>
        public IFluentValidation<T> IsShorterThan(int length)
        {
            Expression<Func<T, string>> expression = _currentContext.GetExpression<string>();
            Func<T, string> compiled = _expressionBuilder.Compile(expression);
            Expression<Func<T, bool>> derived = f => !string.IsNullOrEmpty(compiled.Invoke(_target)) && compiled.Invoke(_target).Length < length;
            If(derived, _resolver.Resolve<T, string>(expression), _messageResolver.Resolve(expression, length.ToString(), Enums.ValidationType.IsShorterThan, string.Empty));
            return this;
        }

        /// <summary>
        ///     Check if the expressions evaluates to a string matching a credit card pattern
        /// </summary>
        public IFluentValidation<T> IsCreditCard()
        {
            Expression<Func<T, string>> expression = _currentContext.GetExpression<string>();
            var internalValidator = new CreditCardValidator();
            Func<T, string> compiled = _expressionBuilder.Compile(expression);
            Expression<Func<T, bool>> derived = f => internalValidator.Validate(compiled.Invoke(_target));
            If(derived, _resolver.Resolve<T, string>(expression), _messageResolver.Resolve(expression, Enums.ValidationType.IsCreditCard, string.Empty));
            return this;
        }

        /// <summary>
        ///     Check if the expression evaluates to a string that is a post code
        /// </summary>
        public IFluentValidation<T> IsPostCode()
        {
            Expression<Func<T, string>> expression = _currentContext.GetExpression<string>();
            var regEx = @"(GIR 0AA)|((([A-Z-[QVX]][0-9][0-9]?)|(([A-Z-[QVX]][A-Z-[IJZ]][0-9][0-9]?)|(([A-Z-[QVX]][0-9][A-HJKSTUW])|([A-Z-[QVX]][A-Z-[IJZ]][0-9][ABEHMNPRVWXY])))) [0-9][A-Z-[CIKMOV]]{2})";
            Func<T, string> compiled = _expressionBuilder.Compile(expression);
            Expression<Func<T, bool>> derived = f => !string.IsNullOrEmpty(compiled.Invoke(_target)) && new Regex(regEx).IsMatch(compiled.Invoke(_target));
            If(derived, _resolver.Resolve<T, string>(expression), _messageResolver.Resolve(expression, Enums.ValidationType.IsPostCode, string.Empty));
            return this;
        }

        /// <summary>
        ///     Check if the expression evaluates to a string that is a zip code
        /// </summary>
        public IFluentValidation<T> IsZipCode()
        {
            Expression<Func<T, string>> expression = _currentContext.GetExpression<string>();
            var regEx = @"^[0-9]{5}(-[0-9]{4})?$";
            Func<T, string> compiled = _expressionBuilder.Compile(expression);
            Expression<Func<T, bool>> derived = f => !string.IsNullOrEmpty(compiled.Invoke(_target)) && new Regex(regEx).IsMatch(compiled.Invoke(_target));
            If(derived, _resolver.Resolve<T, string>(expression), _messageResolver.Resolve(expression, Enums.ValidationType.IsPostCode, string.Empty));
            return this;
        }

		/// <summary>
		///     Check if the expression evaluates to a string that is numeric
		/// </summary>
		public IFluentValidation<T> IsNumeric()
		{
			var number = 0;
			Expression<Func<T, string>> expression = _currentContext.GetExpression<string>();
			Func<T, string> compiled = _expressionBuilder.Compile(expression);
			Expression<Func<T, bool>> derived = f => !string.IsNullOrEmpty(compiled.Invoke(_target)) && int.TryParse(compiled.Invoke(_target), out number);
			If(derived, _resolver.Resolve<T, string>(expression), _messageResolver.Resolve(expression, Enums.ValidationType.IsNumeric, string.Empty));
			return this;
		}

		/// <summary>
		///     Check if the expression evaluates to a string that is alpha only
		/// </summary>
		public IFluentValidation<T> IsAlpha()
		{
			Expression<Func<T, string>> expression = _currentContext.GetExpression<string>();
			Func<T, string> compiled = _expressionBuilder.Compile(expression);
			Expression<Func<T, bool>> derived = f => !string.IsNullOrEmpty(compiled.Invoke(_target)) && compiled.Invoke(_target).ToCharArray().All(c=> Char.IsLetter(c));
			If(derived, _resolver.Resolve<T, string>(expression), _messageResolver.Resolve(expression, Enums.ValidationType.IsAlpha, string.Empty));
			return this;
		}
    }
}
