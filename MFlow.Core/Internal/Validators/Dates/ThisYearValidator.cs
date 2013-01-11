
using System;
using System.Collections.Generic;

namespace MFlow.Core.Internal.Validators.Dates
{
	/// <summary>
	///     ThisYear Validator
	/// </summary>
	public class ThisYearValidator : IValidator<DateTime>
	{
        public bool Validate(DateTime input)
        {
        	return input.Year == DateTime.Now.Year;
        }
	}
}
