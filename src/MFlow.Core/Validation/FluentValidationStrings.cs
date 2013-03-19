using System;
using System.Linq.Expressions;
using MFlow.Core.Conditions;
using MFlow.Core.Internal.Validators;
using MFlow.Core.Internal.Validators.Strings;
using System.Collections.Generic;
using System.Linq;

namespace MFlow.Core.Validation
{
    /// <summary>
    ///     A fluent validation implementation
    /// </summary>
    public partial class FluentValidation<T> : FluentConditions<T>, IFluentValidation<T>, IFluentValidationString<T>
    {

        /// <summary>
        ///     Checks if the expression evaluates to a string that is empty
        /// </summary>
        public IFluentValidation<T> IsNotEmpty()
        {
            return ApplyStringValidator(
                _validatorFactory.GetValidator<string, INotEmptyValidator>(), Enums.ValidationType.NotEmpty
               );
        }

        /// <summary>
        ///     Checks if the expression evaluates to a string that matches the regEx
        /// </summary>
        public IFluentValidation<T> Matches(string regEx)
        {
            return ApplyStringComparisonValidator(
                _validatorFactory.GetValidator<string, string, IMatchesValidator>(), Enums.ValidationType.RegEx, regEx
               );
        }

        /// <summary>
        ///     Checks if the expression evaluates to a string that is an email address
        /// </summary>
        public IFluentValidation<T> IsEmail()
        {
            return ApplyStringValidator(
                _validatorFactory.GetValidator<string, IEmailValidator>(), Enums.ValidationType.IsEmail
               );
        }

        /// <summary>
        ///     Checks if the expressions evaluates to a string that contains value
        /// </summary>
        public IFluentValidation<T> Contains(string value)
        {
            return ApplyStringComparisonValidator(
                _validatorFactory.GetValidator<string, string, IContainsValidator>(), Enums.ValidationType.Contains, value
               );
        }

        /// <summary>
        ///     Checks if the expression evaluates to a string that is of length
        /// </summary>
        public IFluentValidation<T> IsLength(int length)
        {
            return ApplyStringIntComparisonValidator(
                _validatorFactory.GetValidator<string, int, ILengthValidator>(), Enums.ValidationType.IsLength, length
               );
        }

        /// <summary>
        ///     Checks if the expression evaluates to a string longer than length
        /// </summary>
        public IFluentValidation<T> IsLongerThan(int length)
        {
            return ApplyStringIntComparisonValidator(
                _validatorFactory.GetValidator<string, int, ILongerValidator>(), Enums.ValidationType.IsLongerThan, length
               );
        }

        /// <summary>
        ///     Checks if the expression evaluates to a string shorter than length
        /// </summary>
        public IFluentValidation<T> IsShorterThan(int length)
        {
            return ApplyStringIntComparisonValidator(
                _validatorFactory.GetValidator<string, int, IShorterValidator>(), Enums.ValidationType.IsShorterThan, length
               );
        }

        /// <summary>
        ///     Check if the expressions evaluates to a string matching a credit card pattern
        /// </summary>
        public IFluentValidation<T> IsCreditCard()
        {
            return ApplyStringValidator(
                _validatorFactory.GetValidator<string, ICreditCardValidator>(), Enums.ValidationType.IsCreditCard
               );
        }

        /// <summary>
        ///     Check if the expression evaluates to a string that is a post code
        /// </summary>
        public IFluentValidation<T> IsPostCode()
        {
            return ApplyStringValidator(
                _validatorFactory.GetValidator<string, IPostCodeValidator>(), Enums.ValidationType.IsPostCode
               );
        }

        /// <summary>
        ///     Check if the expression evaluates to a string that is a zip code
        /// </summary>
        public IFluentValidation<T> IsZipCode()
        {
            return ApplyStringValidator(
                _validatorFactory.GetValidator<string, IZipCodeValidator>(), Enums.ValidationType.IsZipCode
               );
        }

        /// <summary>
        ///     Check if the expression evaluates to a string that is numeric
        /// </summary>
        public IFluentValidation<T> IsNumeric()
        {
            return ApplyStringValidator(
                _validatorFactory.GetValidator<string, INumericValidator>(), Enums.ValidationType.IsNumeric
               );
        }

        /// <summary>
        ///     Check if the expression evaluates to a string that is alpha only
        /// </summary>
        public IFluentValidation<T> IsAlpha()
        {
            return ApplyStringValidator(
                _validatorFactory.GetValidator<string, IAlphaValidator>(), Enums.ValidationType.IsAlpha
               );
        }

        /// <summary>
        ///    Check if the expressions evaluates to a string that is a date
        /// </summary>
        public IFluentValidation<T> IsDate()
        {
            return ApplyStringValidator(
                _validatorFactory.GetValidator<string, IDateValidator>(), Enums.ValidationType.IsDate
               );
        }

        /// <summary>
        ///    Check if the expressions evaluates to a string that is a valid password
        /// </summary>
        public IFluentValidation<T> IsPassword()
        {
            return ApplyStringValidator(
                _validatorFactory.GetValidator<string, IPasswordValidator>(), Enums.ValidationType.IsPassword
               );
        }

        IFluentValidation<T> ApplyStringValidator(ICollection<IValidator<string>> validators, Enums.ValidationType type)
        {
            foreach (var validator in validators)
            {

                if (validator.GetType().Assembly == typeof(IFluentValidationLoader).Assembly)
                {

                }

                Expression<Func<T, string>> expression = _currentContext.GetExpression<string>();
                Func<T, string> compiled = _expressionBuilder.Compile(expression);
                Expression<Func<T, bool>> derived = f => validator.Validate(compiled.Invoke(_target));
                BuildIf(derived, _resolver.Resolve<T, string>(expression), _messageResolver.Resolve(expression, type, string.Empty));
            }
            return this;
        }

        FluentValidation<T> ApplyStringComparisonValidator(ICollection<IComparisonValidator<string, string>> validators, Enums.ValidationType type, string value)
        {
            foreach (var validator in validators)
            {
                Expression<Func<T, string>> expression = _currentContext.GetExpression<string>();
                Func<T, string> compiled = _expressionBuilder.Compile(expression);
                Expression<Func<T, bool>> derived = f => validator.Validate(compiled.Invoke(_target), value);
                BuildIf(derived, _resolver.Resolve<T, string>(expression), _messageResolver.Resolve(expression, value, type, string.Empty));
            }
            return this;
        }

        FluentValidation<T> ApplyStringIntComparisonValidator(ICollection<IComparisonValidator<string, int>> validators, Enums.ValidationType type, int value)
        {
            foreach (var validator in validators)
            {
                Expression<Func<T, string>> expression = _currentContext.GetExpression<string>();
                Func<T, string> compiled = _expressionBuilder.Compile(expression);
                Expression<Func<T, bool>> derived = f => validator.Validate(compiled.Invoke(_target), value);
                BuildIf(derived, _resolver.Resolve<T, string>(expression), _messageResolver.Resolve(expression, value.ToString(), type, string.Empty));
            }
            return this;
        }
    }
}
