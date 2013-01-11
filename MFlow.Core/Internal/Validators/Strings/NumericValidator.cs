
using System;
using System.Collections.Generic;

namespace MFlow.Core.Internal.Validators.Strings
{
	/// <summary>
	///     Numeric Validator
	/// </summary>
	public class NumericValidator : IValidator<string>
	{
		static IDictionary<string, bool> cache = new Dictionary<string, bool>();
		
        public bool Validate(string input)
        {
        	if(input==null)
        		return false;
        	if(!cache.ContainsKey(input))
        	{
        		var number = 0;
        		cache[input] = int.TryParse(input, out number);
        	}
        	return cache[input];
        }
	}
}
