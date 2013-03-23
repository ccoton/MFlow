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
            return ApplyStringValidators(
                _validatorFactory.GetValidators<string, INotEmptyValidator>(), Enums.ValidationType.NotEmpty
               );
        }

        /// <summary>
        ///     Checks if the expression evaluates to a string that matches the regEx
        /// </summary>
        public IFluentValidation<T> Matches(string regEx)
        {
            return ApplyStringComparisonValidator(
                _validatorFactory.GetValidators<string, string, IMatchesValidator>(), Enums.ValidationType.RegEx, regEx
               );
        }

        /// <summary>
        ///     Checks if the expression evaluates to a string that is an email address
        /// </summary>
        public IFluentValidation<T> IsEmail()
        {
            return ApplyStringValidators(
                _validatorFactory.GetValidators<string, IEmailValidator>(), Enums.ValidationType.IsEmail
               );
        }

        /// <summary>
        ///     Checks if the expressions evaluates to a string that contains value
        /// </summary>
        public IFluentValidation<T> Contains(string value)
        {
            return ApplyStringComparisonValidator(
                _validatorFactory.GetValidators<string, string, IContainsValidator>(), Enums.ValidationType.Contains, value
               );
        }

        /// <summary>
        ///     Checks if the expression evaluates to a string that is of length
        /// </summary>
        public IFluentValidation<T> IsLength(int length)
        {
            return ApplyStringIntComparisonValidator(
                _validatorFactory.GetValidators<string, int, ILengthValidator>(), Enums.ValidationType.IsLength, length
               );
        }

        /// <summary>
        ///     Checks if the expression evaluates to a string longer than length
        /// </summary>
        public IFluentValidation<T> IsLongerThan(int length)
        {
            return ApplyStringIntComparisonValidator(
                _validatorFactory.GetValidators<string, int, ILongerValidator>(), Enums.ValidationType.IsLongerThan, length
               );
        }

        /// <summary>
        ///     Checks if the expression evaluates to a string shorter than length
        /// </summary>
        public IFluentValidation<T> IsShorterThan(int length)
        {
            return ApplyStringIntComparisonValidator(
                _validatorFactory.GetValidators<string, int, IShorterValidator>(), Enums.ValidationType.IsShorterThan, length
               );
        }

        /// <summary>
        ///     Check if the expressions evaluates to a string matching a credit card pattern
        /// </summary>
        public IFluentValidation<T> IsCreditCard()
        {
            return ApplyStringValidators(
                _validatorFactory.GetValidators<string, ICreditCardValidator>(), Enums.ValidationType.IsCreditCard
               );
        }

        /// <summary>
        ///     Check if the expression evaluates to a string that is a post code
        /// </summary>
        public IFluentValidation<T> IsPostCode()
        {
            return ApplyStringValidators(
                _validatorFactory.GetValidators<string, IPostCodeValidator>(), Enums.ValidationType.IsPostCode
               );
        }

        /// <summary>
        ///     Check if the expression evaluates to a string that is a zip code
        /// </summary>
        public IFluentValidation<T> IsZipCode()
        {
            return ApplyStringValidators(
                _validatorFactory.GetValidators<string, IZipCodeValidator>(), Enums.ValidationType.IsZipCode
               );
        }

        /// <summary>
        ///     Check if the expression evaluates to a string that is numeric
        /// </summary>
        public IFluentValidation<T> IsNumeric()
        {
            return ApplyStringValidators(
                _validatorFactory.GetValidators<string, INumericValidator>(), Enums.ValidationType.IsNumeric
               );
        }

        /// <summary>
        ///     Check if the expression evaluates to a string that is alpha only
        /// </summary>
        public IFluentValidation<T> IsAlpha()
        {
            return ApplyStringValidators(
                _validatorFactory.GetValidators<string, IAlphaValidator>(), Enums.ValidationType.IsAlpha
               );
        }

        /// <summary>
        ///    Check if the expressions evaluates to a string that is a date
        /// </summary>
        public IFluentValidation<T> IsDate()
        {
            return ApplyStringValidators(
                _validatorFactory.GetValidators<string, IDateValidator>(), Enums.ValidationType.IsDate
               );
        }

        /// <summary>
        ///    Check if the expressions evaluates to a string that is a valid password
        /// </summary>
        public IFluentValidation<T> IsPassword()
        {
            return ApplyStringValidators(
                _validatorFactory.GetValidators<string, IPasswordValidator>(), Enums.ValidationType.IsPassword
               );
        }

        /// <summary>
        ///    Check if the expressions evaluates to a string that is a valid username
        /// </summary>
        public IFluentValidation<T> IsUsername()
        {
            return ApplyStringValidators(
                _validatorFactory.GetValidators<string, IUsernameValidator>(), Enums.ValidationType.IsUsername
               );
        }

        /// <summary>
        ///    Check if the expressions evaluates to a string that is a valid url
        /// </summary>
        public IFluentValidation<T> IsUrl()
        {
            return ApplyStringValidators(
                _validatorFactory.GetValidators<string, IUrlValidator>(), Enums.ValidationType.IsUrl
               );
        }

        IFluentValidation<T> ApplyStringValidators(ICollection<IValidator<string>> validators, Enums.ValidationType type)
        {
            _validatorToCondition.ForString(_currentContext, validators, type)
                .ToList()
                .ForEach(c => BuildIf(c.Condition, c.Key, c.Message));
                
            return this;
        }

        FluentValidation<T> ApplyStringComparisonValidator(ICollection<IComparisonValidator<string, string>> validators, Enums.ValidationType type, string value)
        {
            _validatorToCondition.ForString(_currentContext, validators, type, value)
                .ToList()
                .ForEach(c => BuildIf(c.Condition, c.Key, c.Message));

            return this;
        }

        FluentValidation<T> ApplyStringIntComparisonValidator(ICollection<IComparisonValidator<string, int>> validators, Enums.ValidationType type, int value)
        {
            _validatorToCondition.ForString(_currentContext, validators, type, value)
                .ToList()
                .ForEach(c => BuildIf(c.Condition, c.Key, c.Message));

            return this;
        }
    }
}
