using System;
using System.Linq.Expressions;
using MFlow.Core.Conditions;
using MFlow.Core.Internal.Validators.Strings;

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
            var notEmptyValidator = _validatorFactory.GetValidator<string, INotEmptyValidator>();
            Expression<Func<T, string>> expression = _currentContext.GetExpression<string>();
            Func<T, string> compiled = _expressionBuilder.Compile(expression);
            Expression<Func<T, bool>> derived = f => notEmptyValidator.Validate(_expressionBuilder.Invoke(compiled, _target));
            If(derived, _resolver.Resolve<T, string>(expression), _messageResolver.Resolve(expression, Enums.ValidationType.NotEmpty, string.Empty));
            return this;
        }

        /// <summary>
        ///     Checks if the expression evaluates to a string that matches the regEx 
        /// </summary>
        public IFluentValidation<T> Matches(string regEx)
        {
            var matchesValidator = _validatorFactory.GetValidator<string, string, IMatchesValidator>();
            Expression<Func<T, string>> expression = _currentContext.GetExpression<string>();
            Func<T, string> compiled = _expressionBuilder.Compile(expression);
            Expression<Func<T, bool>> derived = f => matchesValidator.Validate(compiled.Invoke(_target), regEx);
            If(derived, _resolver.Resolve<T, string>(expression), _messageResolver.Resolve(expression, regEx, Enums.ValidationType.RegEx, string.Empty));
            return this;
        }

        /// <summary>
        ///     Checks if the expression evaluates to a string that is an email address 
        /// </summary>
        public IFluentValidation<T> IsEmail()
        {
            var emailValidator = _validatorFactory.GetValidator<string, IEmailValidator>();
            Expression<Func<T, string>> expression = _currentContext.GetExpression<string>();
            Func<T, string> compiled = _expressionBuilder.Compile(expression);
            Expression<Func<T, bool>> derived = f => emailValidator.Validate(compiled.Invoke(_target));
            If(derived, _resolver.Resolve<T, string>(expression), _messageResolver.Resolve(expression, Enums.ValidationType.IsEmail, string.Empty));
            return this;
        }

        /// <summary>
        ///     Checks if the expressions evaluates to a string that contains value
        /// </summary>
        public IFluentValidation<T> Contains(string value)
        {
            var containsValidator = _validatorFactory.GetValidator<string, string, IContainsValidator>();
            Expression<Func<T, string>> expression = _currentContext.GetExpression<string>();
            Func<T, string> compiled = _expressionBuilder.Compile(expression);
            Expression<Func<T, bool>> derived = f => containsValidator.Validate(compiled.Invoke(_target), value);
            If(derived, _resolver.Resolve<T, string>(expression), _messageResolver.Resolve(expression, value, Enums.ValidationType.Contains, string.Empty));
            return this;
        }

        /// <summary>
        ///     Checks if the expression evaluates to a string that is of length
        /// </summary>
        public IFluentValidation<T> IsLength(int length)
        {
            var lengthValidator = _validatorFactory.GetValidator<string, int, ILengthValidator>();
            Expression<Func<T, string>> expression = _currentContext.GetExpression<string>();
            Func<T, string> compiled = _expressionBuilder.Compile(expression);
            Expression<Func<T, bool>> derived = f => lengthValidator.Validate(compiled.Invoke(_target), length);
            If(derived, _resolver.Resolve<T, string>(expression), _messageResolver.Resolve(expression, length.ToString(), Enums.ValidationType.IsLength, string.Empty));
            return this;
        }
        
        /// <summary>
        ///     Checks if the expression evaluates to a string longer than length
        /// </summary>
        public IFluentValidation<T> IsLongerThan(int length)
        {
            var longerValidator = _validatorFactory.GetValidator<string, int, ILongerValidator>();
            Expression<Func<T, string>> expression = _currentContext.GetExpression<string>();
            Func<T, string> compiled = _expressionBuilder.Compile(expression);
            Expression<Func<T, bool>> derived = f => longerValidator.Validate(compiled.Invoke(_target), length);
            If(derived, _resolver.Resolve<T, string>(expression), _messageResolver.Resolve(expression, length.ToString(), Enums.ValidationType.IsLongerThan, string.Empty));
            return this;
        }
        
        /// <summary>
        ///     Checks if the expression evaluates to a string shorter than length
        /// </summary>
        public IFluentValidation<T> IsShorterThan(int length)
        {
            var shorterValidator = _validatorFactory.GetValidator<string, int, IShorterValidator>();
            Expression<Func<T, string>> expression = _currentContext.GetExpression<string>();
            Func<T, string> compiled = _expressionBuilder.Compile(expression);
            Expression<Func<T, bool>> derived = f => shorterValidator.Validate(compiled.Invoke(_target), length);
            If(derived, _resolver.Resolve<T, string>(expression), _messageResolver.Resolve(expression, length.ToString(), Enums.ValidationType.IsShorterThan, string.Empty));
            return this;
        }

        /// <summary>
        ///     Check if the expressions evaluates to a string matching a credit card pattern
        /// </summary>
        public IFluentValidation<T> IsCreditCard()
        {
            var internalValidator = _validatorFactory.GetValidator<string, ICreditCardValidator>();
            Expression<Func<T, string>> expression = _currentContext.GetExpression<string>();
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
            var postCodeValidator = _validatorFactory.GetValidator<string, IPostCodeValidator>();
            Expression<Func<T, string>> expression = _currentContext.GetExpression<string>();
            Func<T, string> compiled = _expressionBuilder.Compile(expression);
            Expression<Func<T, bool>> derived = f => postCodeValidator.Validate(compiled.Invoke(_target));
            If(derived, _resolver.Resolve<T, string>(expression), _messageResolver.Resolve(expression, Enums.ValidationType.IsPostCode, string.Empty));
            return this;
        }

        /// <summary>
        ///     Check if the expression evaluates to a string that is a zip code
        /// </summary>
        public IFluentValidation<T> IsZipCode()
        {
            var zipCodeValidator = _validatorFactory.GetValidator<string, IZipCodeValidator>();
            ;
            Expression<Func<T, string>> expression = _currentContext.GetExpression<string>();
            Func<T, string> compiled = _expressionBuilder.Compile(expression);
            Expression<Func<T, bool>> derived = f => zipCodeValidator.Validate(compiled.Invoke(_target));
            If(derived, _resolver.Resolve<T, string>(expression), _messageResolver.Resolve(expression, Enums.ValidationType.IsPostCode, string.Empty));
            return this;
        }

        /// <summary>
        ///     Check if the expression evaluates to a string that is numeric
        /// </summary>
        public IFluentValidation<T> IsNumeric()
        {
            var numericValidator = _validatorFactory.GetValidator<string, INumericValidator>();
            Expression<Func<T, string>> expression = _currentContext.GetExpression<string>();
            Func<T, string> compiled = _expressionBuilder.Compile(expression);
            Expression<Func<T, bool>> derived = f => numericValidator.Validate(compiled.Invoke(_target));
            If(derived, _resolver.Resolve<T, string>(expression), _messageResolver.Resolve(expression, Enums.ValidationType.IsNumeric, string.Empty));
            return this;
        }

        /// <summary>
        ///     Check if the expression evaluates to a string that is alpha only
        /// </summary>
        public IFluentValidation<T> IsAlpha()
        {
            var alphaValidator = _validatorFactory.GetValidator<string, IAlphaValidator>();
            Expression<Func<T, string>> expression = _currentContext.GetExpression<string>();
            Func<T, string> compiled = _expressionBuilder.Compile(expression);
            Expression<Func<T, bool>> derived = f => alphaValidator.Validate(compiled.Invoke(_target));
            If(derived, _resolver.Resolve<T, string>(expression), _messageResolver.Resolve(expression, Enums.ValidationType.IsAlpha, string.Empty));
            return this;
        }
        
        /// <summary>
        ///    Check if the expressions evaluates to a string that is a date
        /// </summary>
        public IFluentValidation<T> IsDate()
        {
			var dateValidator = _validatorFactory.GetValidator<string, IDateValidator>();
            Expression<Func<T, string>> expression = _currentContext.GetExpression<string>();
            Func<T, string> compiled = _expressionBuilder.Compile(expression);
            Expression<Func<T, bool>> derived = f => dateValidator.Validate(compiled.Invoke(_target));
            If(derived, _resolver.Resolve<T, string>(expression), _messageResolver.Resolve(expression, Enums.ValidationType.IsDate, string.Empty));
            return this;
        }
    }
}
