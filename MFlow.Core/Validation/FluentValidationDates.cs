using System;
using System.Linq.Expressions;
using MFlow.Core.Conditions;
using MFlow.Core.Internal.Validators;
using MFlow.Core.Internal.Validators.Dates;

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
            return ApplyDateComparisonValidator(_validatorFactory.GetValidator<DateTime, DateTime, IBeforeValidator>(), Enums.ValidationType.Before, value);
        }

        /// <summary>
        ///     Checks if the expression evaluates to a date that is greater than the value
        /// </summary>
        public IFluentValidation<T> IsAfter(DateTime value)
        {
            return ApplyDateComparisonValidator(_validatorFactory.GetValidator<DateTime, DateTime, IAfterValidator>(), Enums.ValidationType.After, value);
        }

        /// <summary>
        ///     Checks if the expression evaluates to a date that is less than or equal to the value
        /// </summary>
        public IFluentValidation<T> IsOn(DateTime value)
        {
            return ApplyDateComparisonValidator(_validatorFactory.GetValidator<DateTime, DateTime, IOnValidator>(), Enums.ValidationType.On, value);
        }

        /// <summary>
        ///     Checks if the expression evaluates to a data that is this year
        /// </summary>
        public IFluentValidation<T> IsThisYear()
        {
            return ApplyDateValidator(_validatorFactory.GetValidator<DateTime, IThisYearValidator>(), Enums.ValidationType.IsThisYear);
        }

        /// <summary>
        ///     Checks if the expression evaluates to a date that is this month
        /// </summary>
        public IFluentValidation<T> IsThisMonth()
        {
            return ApplyDateValidator(_validatorFactory.GetValidator<DateTime, IThisMonthValidator>(), Enums.ValidationType.IsThisMonth);
        }

        /// <summary>
        ///     Checks if the expression evaluates to a date that is this week
        /// </summary>
        public IFluentValidation<T> IsThisWeek()
        {
            return ApplyDateValidator(_validatorFactory.GetValidator<DateTime, IThisWeekValidator>(), Enums.ValidationType.IsThisWeek);
        }

        /// <summary>
        ///     Checks if the expression evaluates to a date that is today
        /// </summary>
        public IFluentValidation<T> IsToday()
        {
            return ApplyDateValidator(_validatorFactory.GetValidator<DateTime, ITodayValidator>(), Enums.ValidationType.IsToday);
        }
        
        IFluentValidation<T> ApplyDateValidator(IValidator<DateTime> validator, Enums.ValidationType type)
        {
            var condition = _validatorToCondition.ForDateTime(_currentContext, validator, type);
            BuildIf(condition.Condition, condition.Key, condition.Message); 
            return this;
        }
        
        FluentValidation<T> ApplyDateComparisonValidator(IComparisonValidator<DateTime, DateTime> validator, Enums.ValidationType type, DateTime value)
        {
            IFluentCondition<T> condition;
            if (_currentContext.IsNullable)
            {
                condition = new ApplyNullableDateValidator<T>(_target, _currentContext, _expressionBuilder,
                    _resolver, _messageResolver).Apply(validator, type, value);
            }
            else
            {
                condition = new ApplyDateValidator<T>(_target, _currentContext, _expressionBuilder,
                    _resolver, _messageResolver).Apply(validator, type, value);
            }

            BuildIf(condition.Condition, condition.Key, condition.Message);
            return this;
        }
    }
}
