
using System;
using System.Collections.Generic;
using MFlow.Core.Validation.Enums;

namespace MFlow.Core.Internal.Validators.Strings
{
	/// <summary>
	///     Longer Validator
	/// </summary>
	class LongerValidator : ICompareValidator<string, int>
	{
		static IDictionary<string, bool> cache = new Dictionary<string, bool>();
		
        public bool Validate(string input, int value)
        {
        	if(input==null)
        		return false;
        	
        	var key = string.Format("{0} == {1}", input, value);
        	
        	if(!cache.ContainsKey(key))
        	   cache[key] = input.Length > value;
        	return cache[key];
        }
	}
}
