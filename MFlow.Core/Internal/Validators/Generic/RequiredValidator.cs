
using System;
using System.Collections.Generic;
using MFlow.Core.Validation.Enums;

namespace MFlow.Core.Internal.Validators.Generic
{
	/// <summary>
	///     Required Validator
	/// </summary>
	class RequiredValidator<T> : IValidator<T>
	{
        public bool Validate(T input)
        {
        	return input != null && !string.IsNullOrEmpty(input.ToString()) && !input.Equals(default(T));
        }
	}
}
