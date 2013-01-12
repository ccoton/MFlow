using System;

namespace MFlow.Core.Internal.Validators.Dates
{
    /// <summary>
    ///     Today Validator
    /// </summary>
    public class TodayValidator : IValidator<DateTime>
    {
        public bool Validate (DateTime input)
        {
            return input.Date == DateTime.Now.Date;
        }
    }
}
