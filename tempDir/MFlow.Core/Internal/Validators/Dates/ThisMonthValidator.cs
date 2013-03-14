using System;

namespace MFlow.Core.Internal.Validators.Dates
{
    /// <summary>
    ///     ThisMonth Validator
    /// </summary>
    public class ThisMonthValidator : IThisMonthValidator
    {
        public bool Validate (DateTime input)
        {
            return input.Month == DateTime.Now.Month && input.Year == DateTime.Now.Year;
        }
    }
}
