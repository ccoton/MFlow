using System;
using System.Globalization;
using System.Threading;

namespace MFlow.Core.Internal.Validators.Dates
{
    /// <summary>
    ///     ThisWeek Validator
    /// </summary>
    public class ThisWeekValidator : IValidator<DateTime>
    {
        public bool Validate (DateTime input)
        {
            var thisWeek = Thread.CurrentThread.CurrentCulture.Calendar.GetWeekOfYear (DateTime.Now, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            var week = Thread.CurrentThread.CurrentCulture.Calendar.GetWeekOfYear (input, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        	
            return input.Year == DateTime.Now.Year && thisWeek == week;
        }
    }
}
