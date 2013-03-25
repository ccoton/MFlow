using MFlow.Core.Conditions;
using MFlow.Core.Internal.Validators;
using MFlow.Core.Internal.Validators.Dates;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MFlow.Core.Validation
{
    /// <summary>
    ///     A fluent validation implementation
    /// </summary>
    public partial class FluentValidation<T> : FluentConditions<T>, IFluentValidation<T>, IFluentValidationDate<T>
    {
        /// <summary>
        ///     Checks if the expression evaluates to a date that is less that the value
        /// </summary>
        public IFluentValidation<T> IsBefore(DateTime value)
        {
            return ApplyDateComparisonValidator(_validatorFactory.GetValidators<DateTime, DateTime, IBeforeValidator>(), Enums.ValidationType.Before, value);
        }

        /// <summary>
        ///     Checks if the expression evaluates to a date that is greater than the value
        /// </summary>
        public IFluentValidation<T> IsAfter(DateTime value)
        {
            return ApplyDateComparisonValidator(_validatorFactory.GetValidators<DateTime, DateTime, IAfterValidator>(), Enums.ValidationType.After, value);
        }

        /// <summary>
        ///     Checks if the expression evaluates to a date that is less than or equal to the value
        /// </summary>
        public IFluentValidation<T> IsOn(DateTime value)
        {
            return ApplyDateComparisonValidator(_validatorFactory.GetValidators<DateTime, DateTime, IOnValidator>(), Enums.ValidationType.On, value);
        }

        /// <summary>
        ///     Checks if the expression evaluates to a data that is this year
        /// </summary>
        public IFluentValidation<T> IsThisYear()
        {
            return ApplyDateValidator(_validatorFactory.GetValidators<DateTime, IThisYearValidator>(), Enums.ValidationType.IsThisYear);
        }

        /// <summary>
        ///     Checks if the expression evaluates to a date that is this month
        /// </summary>
        public IFluentValidation<T> IsThisMonth()
        {
            return ApplyDateValidator(_validatorFactory.GetValidators<DateTime, IThisMonthValidator>(), Enums.ValidationType.IsThisMonth);
        }

        /// <summary>
        ///     Checks if the expression evaluates to a date that is this week
        /// </summary>
        public IFluentValidation<T> IsThisWeek()
        {
            return ApplyDateValidator(_validatorFactory.GetValidators<DateTime, IThisWeekValidator>(), Enums.ValidationType.IsThisWeek);
        }

        /// <summary>
        ///     Checks if the expression evaluates to a date that is today
        /// </summary>
        public IFluentValidation<T> IsToday()
        {
            return ApplyDateValidator(_validatorFactory.GetValidators<DateTime, ITodayValidator>(), Enums.ValidationType.IsToday);
        }

        /// <summary>
        ///     Checks if the expression evaluates to a date that is between earliest and latest
        /// </summary>
        public IFluentValidation<T> IsBetween(DateTime earliest, DateTime latest)
        {
            return ApplyDateBetweenValidator(_validatorFactory.GetValidators<DateTime, Between<DateTime>, IBetweenValidator>(), Enums.ValidationType.IsBetween, earliest, latest);
        }

        IFluentValidation<T> ApplyDateValidator(ICollection<IValidator<DateTime>> validators, Enums.ValidationType type)
        {
            _validatorToCondition.ForDateTime(_currentContext, validators, type)
                .ToList()
                .ForEach(c => BuildIf(c.Condition, c.Key, c.Message));

            return this;
        }

        FluentValidation<T> ApplyDateBetweenValidator(ICollection<IComparisonValidator<DateTime, Between<DateTime>>> validators, Enums.ValidationType type, DateTime lower, DateTime upper)
        {
            _validatorToCondition.ForDateTime(_currentContext, validators, type, lower, upper)
                .ToList()
                .ForEach(c => BuildIf(c.Condition, c.Key, c.Message));

            return this;
        }

        FluentValidation<T> ApplyDateComparisonValidator(ICollection<IComparisonValidator<DateTime, DateTime>> validators, Enums.ValidationType type, DateTime value)
        {
            _validatorToCondition.ForDateTime(_currentContext, validators, type, value)
                .ToList()
                .ForEach(c => BuildIf(c.Condition, c.Key, c.Message));

            return this;
        }
    }
}
