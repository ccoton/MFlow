using MFlow.Core.Conditions;
using MFlow.Core.Internal.Validators;
using MFlow.Core.Internal.Validators.Numbers;
using System.Collections.Generic;
using System.Linq;

namespace MFlow.Core.Validation
{
    /// <summary>
    ///     A fluent validation implementation
    /// </summary>
    public partial class FluentValidation<T> : FluentConditions<T>, IFluentValidation<T>, IFluentValidationNumber<T>
    {
        /// <summary>
        ///     Checks if the expression evaluates to an int that is less that the value
        /// </summary>
        public IFluentValidation<T> IsLessThan(int value)
        {
            return ApplyIntComparisonValidator(_validatorFactory.GetValidators<int, int, ILessThanValidator>(), Enums.ValidationType.LessThan, value);
        }

        /// <summary>
        ///     Checks if the expression evaluates to an int that is greater that the value
        /// </summary>
        public IFluentValidation<T> IsGreaterThan(int value)
        {
            return ApplyIntComparisonValidator(_validatorFactory.GetValidators<int, int, IGreaterThanValidator>(), Enums.ValidationType.GreaterThan, value);
        }

        /// <summary>
        ///     Checks if the expression evaluates to an int that is less than or equal to the value
        /// </summary>
        public IFluentValidation<T> IsLessThanOrEqualTo(int value)
        {
            return ApplyIntComparisonValidator(_validatorFactory.GetValidators<int, int, ILessThanOrEqualToValidator>(), Enums.ValidationType.LessThanOrEqualTo, value);
        }

        /// <summary>
        ///     Checks if the expression evaluates to an int that is greater than or equal to the value
        /// </summary>
        public IFluentValidation<T> IsGreaterThanOrEqualTo(int value)
        {
            return ApplyIntComparisonValidator(_validatorFactory.GetValidators<int, int, IGreaterThanOrEqualToValidator>(), Enums.ValidationType.GreaterThanOrEqualTo, value);
        }

        /// <summary>
        ///     Checks if the expression evaluates to an int that is between the lower and upper 
        /// </summary>
        public IFluentValidation<T> IsBetween(int lower, int upper)
        {
            return ApplyIntBetweenValidator(_validatorFactory.GetValidators<int, Between<int>, IBetweenValidator>(), Enums.ValidationType.IsBetween, lower, upper);
        }

        FluentValidation<T> ApplyIntComparisonValidator(ICollection<IComparisonValidator<int, int>> validators, Enums.ValidationType type, int value)
        {
            _validatorToCondition.ForInt(_currentContext, validators, type, value)
                .ToList()
                .ForEach(c => BuildIf(c.Condition, c.Key, c.Message));

            return this;
        }

        FluentValidation<T> ApplyIntBetweenValidator(ICollection<IComparisonValidator<int, Between<int>>> validators, Enums.ValidationType type, int lower, int upper)
        {
            _validatorToCondition.ForInt(_currentContext, validators, type, lower, upper)
                .ToList()
                .ForEach(c => BuildIf(c.Condition, c.Key, c.Message));

            return this;
        }

    }
}
