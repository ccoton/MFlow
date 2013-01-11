using System;
using System.Globalization;
using System.Linq.Expressions;
using System.Threading;

using MFlow.Core.Conditions;
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
        	var beforeValidator = new BeforeValidator();
            Expression<Func<T, DateTime>> expression = _currentContext.GetExpression<DateTime>();
            Func<T, DateTime> compiled = _expressionBuilder.Compile(expression);
            Expression<Func<T, bool>> derived = f => beforeValidator.Validate(_expressionBuilder.Invoke(compiled, _target), value);
            If(derived, _resolver.Resolve<T, DateTime>(expression), _messageResolver.Resolve(expression, value, Enums.ValidationType.Before, string.Empty));
            return this;
        }

        /// <summary>
        ///     Checks if the expression evaluates to a date that is greater than the value 
        /// </summary>
        public IFluentValidation<T> IsAfter(DateTime value)
        {
        	var afterValidator = new AfterValidator();
            Expression<Func<T, DateTime>> expression = _currentContext.GetExpression<DateTime>();
            Func<T, DateTime> compiled = _expressionBuilder.Compile(expression);
            Expression<Func<T, bool>> derived = f => afterValidator.Validate(_expressionBuilder.Invoke(compiled, _target), value);
            If(derived, _resolver.Resolve<T, DateTime>(expression), _messageResolver.Resolve(expression, value, Enums.ValidationType.After, string.Empty));
            return this;
        }

        /// <summary>
        ///     Checks if the expression evaluates to a date that is less than or equal to the value 
        /// </summary>
        public IFluentValidation<T> IsOn(DateTime value)
        {
        	var onValidator = new OnValidator();
            Expression<Func<T, DateTime>> expression = _currentContext.GetExpression<DateTime>();
            Func<T, DateTime> compiled = _expressionBuilder.Compile(expression);
            Expression<Func<T, bool>> derived = f => onValidator.Validate(_expressionBuilder.Invoke(compiled, _target), value);
            If(derived, _resolver.Resolve<T, DateTime>(expression), _messageResolver.Resolve(expression, value, Enums.ValidationType.On, string.Empty));
            return this;
        }

        /// <summary>
        ///     Checks if the expression evaluates to a data that is this year
        /// </summary>
        public IFluentValidation<T> IsThisYear()
        {
        	var thisYearValidator = new ThisYearValidator();
            Expression<Func<T, DateTime>> expression = _currentContext.GetExpression<DateTime>();
            Func<T, DateTime> compiled = _expressionBuilder.Compile(expression);
            Expression<Func<T, bool>> derived = f => thisYearValidator.Validate(_expressionBuilder.Invoke(compiled, _target));
            If(derived, _resolver.Resolve<T, DateTime>(expression), _messageResolver.Resolve(expression, Enums.ValidationType.IsThisYear, string.Empty));
            return this;
        }

        /// <summary>
        ///     Checks if the expression evaluates to a date that is this month
        /// </summary>
        public IFluentValidation<T> IsThisMonth()
        {
        	var thisMonthValidator = new ThisMonthValidator();
            Expression<Func<T, DateTime>> expression = _currentContext.GetExpression<DateTime>();
            Func<T, DateTime> compiled = _expressionBuilder.Compile(expression);
            Expression<Func<T, bool>> derived = f => thisMonthValidator.Validate(_expressionBuilder.Invoke(compiled, _target));
            If(derived, _resolver.Resolve<T, DateTime>(expression), _messageResolver.Resolve(expression, Enums.ValidationType.IsThisMonth, string.Empty));
            return this;
        }

        /// <summary>
        ///     Checks if the expression evaluates to a date that is this week
        /// </summary>
        public IFluentValidation<T> IsThisWeek()
        {
        	var thisWeekValidator = new ThisWeekValidator();
            Expression<Func<T, DateTime>> expression = _currentContext.GetExpression<DateTime>();
            Func<T, DateTime> compiled = _expressionBuilder.Compile(expression);
            Expression<Func<T, bool>> derived = f => thisWeekValidator.Validate(_expressionBuilder.Invoke(compiled, _target));
            If(derived, _resolver.Resolve<T, DateTime>(expression), _messageResolver.Resolve(expression, Enums.ValidationType.IsThisMonth, string.Empty));
            return this;
        }

        /// <summary>
        ///     Checks if the expression evaluates to a date that is today
        /// </summary>
        public IFluentValidation<T> IsToday()
        {
        	var todayValidator = new TodayValidator();
            Expression<Func<T, DateTime>> expression = _currentContext.GetExpression<DateTime>();
            Func<T, DateTime> compiled = _expressionBuilder.Compile(expression);
            Expression<Func<T, bool>> derived = f => todayValidator.Validate(_expressionBuilder.Invoke(compiled, _target));
            If(derived, _resolver.Resolve<T, DateTime>(expression), _messageResolver.Resolve(expression, Enums.ValidationType.IsToday, string.Empty));
            return this;
        }
    }
}
