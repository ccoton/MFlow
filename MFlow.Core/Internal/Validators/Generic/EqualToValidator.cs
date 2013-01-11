
using System;
using System.Collections.Generic;
using MFlow.Core.Validation.Enums;

namespace MFlow.Core.Internal.Validators.Generic
{
	/// <summary>
	///     EqualTo Validator
	/// </summary>
	class EqualToValidator<T, T2> : ICompareValidator<T, T2>
	{
        public bool Validate(T input, T2 value)
        {
        	return input != null && input.Equals(value);
        }
	}
}
