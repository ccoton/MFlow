
using System;
using System.Collections.Generic;
using System.Linq;

namespace MFlow.Core.Internal.Validators.Strings
{
	/// <summary>
	///     Alpha Validator
	/// </summary>
	public class AlphaValidator : IValidator<string>
	{
		static IDictionary<string, bool> cache = new Dictionary<string, bool>();
		
        public bool Validate(string input)
        {
        	if(input==null)
        		return false;
        	if(!cache.ContainsKey(input))
        		cache[input] = input.ToCharArray().All(c=> Char.IsLetter(c));
        	return cache[input];
        }
	}
}
