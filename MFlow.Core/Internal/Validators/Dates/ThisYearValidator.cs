
using System;

namespace MFlow.Core.Internal.Validators.Dates
{
    /// <summary>
    ///     ThisYear Validator
    /// </summary>
    public class ThisYearValidator : IThisYearValidator
    {
        public bool Validate (DateTime input)
        {
            return input.Year == DateTime.Now.Year;
        }
    }
}
