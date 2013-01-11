
using System;
using System.Collections.Generic;
using MFlow.Core.Validation.Enums;

namespace MFlow.Core.Internal.Validators.Dates
{
	/// <summary>
	///     After Validator
	/// </summary>
	class AfterValidator : ICompareValidator<DateTime, DateTime>
	{
        public bool Validate(DateTime input, DateTime value)
        {
        	return input > value;
        }
	}
}
