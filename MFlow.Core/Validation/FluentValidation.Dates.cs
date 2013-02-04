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
    public partial class FluentValidation<T> : FluentConditions<T>, IFluentValidation<T>
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
            Expression<Func<T, DateTime>> expression = _currentContext.GetExpression<DateTime>();
            Func<T, DateTime> compiled = _expressionBuilder.Compile(expression);
            Expression<Func<T, bool>> derived = f => validator.Validate(_expressionBuilder.Invoke(compiled, _target));
            BuildIf(derived, _resolver.Resolve<T, DateTime>(expression), _messageResolver.Resolve(expression, type, string.Empty));
            return this;
        }
        
        FluentValidation<T> ApplyDateComparisonValidator(IComparisonValidator<DateTime, DateTime> validator, Enums.ValidationType type, DateTime value)
        {
            Expression<Func<T, DateTime>> expression = _currentContext.GetExpression<DateTime>();
            Func<T, DateTime> compiled = _expressionBuilder.Compile(expression);
            Expression<Func<T, bool>> derived = f => validator.Validate(_expressionBuilder.Invoke(compiled, _target), value);
            BuildIf(derived, _resolver.Resolve<T, DateTime>(expression), _messageResolver.Resolve(expression, value, type, string.Empty));
            return this;
        }
    }
}
