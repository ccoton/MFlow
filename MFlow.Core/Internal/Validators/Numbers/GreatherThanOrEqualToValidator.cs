
using System;
using System.Collections.Generic;
using MFlow.Core.Validation.Enums;

namespace MFlow.Core.Internal.Validators.Numbers
{
	/// <summary>
	///     GreaterThanOrEqualTo Validator
	/// </summary>
	class GreaterThanOrEqualToValidator : ICompareValidator<int, int>
	{
        public bool Validate(int input, int value)
        {
        	return input >= value;
        }
	}
}
