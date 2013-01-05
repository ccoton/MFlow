using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using MFlow.Core.Conditions;
using MFlow.Core.Conditions.Enums;
using MFlow.Core.Events;
using MFlow.Core.Internal;

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
        public IFluentValidation<T> IsBefore(DateTime value, ConditionType conditionType = ConditionType.And)
        {
            Expression<Func<T, DateTime>> expression = GetCurrentExpression<DateTime>();
            Func<T, DateTime> compiled = expression.Compile();
            Expression<Func<T, bool>> derived = f => compiled.Invoke(_target) < value;
            If(derived, _resolver.Resolve<T, DateTime>(expression), _messageResolver.Resolve(expression, value, Enums.ValidationType.Before, string.Empty), conditionType);
            return this;
        }

        /// <summary>
        ///     Checks if the expression evaluates to a date that is greater than the value 
        /// </summary>
        public IFluentValidation<T> IsAfter(DateTime value, ConditionType conditionType = ConditionType.And)
        {
            Expression<Func<T, DateTime>> expression = GetCurrentExpression<DateTime>();
            Func<T, DateTime> compiled = expression.Compile();
            Expression<Func<T, bool>> derived = f => compiled.Invoke(_target) > value;
            If(derived, _resolver.Resolve<T, DateTime>(expression), _messageResolver.Resolve(expression, value, Enums.ValidationType.After, string.Empty), conditionType);
            return this;
        }

        /// <summary>
        ///     Checks if the expression evaluates to a date that is less than or equal to the value 
        /// </summary>
        public IFluentValidation<T> IsOn(DateTime value, ConditionType conditionType = ConditionType.And)
        {
            Expression<Func<T, DateTime>> expression = GetCurrentExpression<DateTime>();
            Func<T, DateTime> compiled = expression.Compile();
            Expression<Func<T, bool>> derived = f => compiled.Invoke(_target).Date == value.Date;
            If(derived, _resolver.Resolve<T, DateTime>(expression), _messageResolver.Resolve(expression, value, Enums.ValidationType.On, string.Empty), conditionType);
            return this;
        }

        /// <summary>
        ///     Checks if the expression evaluates to a data that is this year
        /// </summary>
        public IFluentValidation<T> IsThisYear(ConditionType conditionType = ConditionType.And)
        {
            Expression<Func<T, DateTime>> expression = GetCurrentExpression<DateTime>();
            Func<T, DateTime> compiled = expression.Compile();
            Expression<Func<T, bool>> derived = f => compiled.Invoke(_target).Date.Year == DateTime.Now.Year;
            If(derived, _resolver.Resolve<T, DateTime>(expression), _messageResolver.Resolve(expression, Enums.ValidationType.IsThisYear, string.Empty), conditionType);
            return this;
        }

        /// <summary>
        ///     Checks if the expression evaluates to a date that is this month
        /// </summary>
        public IFluentValidation<T> IsThisMonth(ConditionType conditionType = ConditionType.And)
        {
            Expression<Func<T, DateTime>> expression = GetCurrentExpression<DateTime>();
            Func<T, DateTime> compiled = expression.Compile();
            Expression<Func<T, bool>> derived = f => compiled.Invoke(_target).Date.Year == DateTime.Now.Year && compiled.Invoke(_target).Month == DateTime.Now.Month;
            If(derived, _resolver.Resolve<T, DateTime>(expression), _messageResolver.Resolve(expression, Enums.ValidationType.IsThisMonth, string.Empty), conditionType);
            return this;
        }

        /// <summary>
        ///     Checks if the expression evaluates to a date that is this week
        /// </summary>
        public IFluentValidation<T> IsThisWeek(ConditionType conditionType = ConditionType.And)
        {
            Expression<Func<T, DateTime>> expression = GetCurrentExpression<DateTime>();
            Func<T, DateTime> compiled = expression.Compile();
            Expression<Func<T, bool>> derived = f => compiled.Invoke(_target).Date.Year == DateTime.Now.Year && compiled.Invoke(_target).Month == DateTime.Now.Month &&
                Thread.CurrentThread.CurrentCulture.Calendar.GetWeekOfYear(compiled.Invoke(_target), CalendarWeekRule.FirstDay, DayOfWeek.Monday) ==
                Thread.CurrentThread.CurrentCulture.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            If(derived, _resolver.Resolve<T, DateTime>(expression), _messageResolver.Resolve(expression, Enums.ValidationType.IsThisMonth, string.Empty), conditionType);
            return this;
        }

        /// <summary>
        ///     Checks if the expression evaluates to a date that is today
        /// </summary>
        public IFluentValidation<T> IsToday(ConditionType conditionType = ConditionType.And)
        {
            Expression<Func<T, DateTime>> expression = GetCurrentExpression<DateTime>();
            Func<T, DateTime> compiled = expression.Compile();
            Expression<Func<T, bool>> derived = f => compiled.Invoke(_target).Date == DateTime.Now.Date;
            If(derived, _resolver.Resolve<T, DateTime>(expression), _messageResolver.Resolve(expression, Enums.ValidationType.IsToday, string.Empty), conditionType);
            return this;
        }
    }
}
