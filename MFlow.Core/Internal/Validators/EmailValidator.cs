using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace MFlow.Core.Internal.Validators
{
    class EmailValidator : IValidator<string>
    {
		static IDictionary<string, bool> cache = new Dictionary<string, bool>();
		const string regEx =  @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
		static Regex reg = new Regex(regEx, RegexOptions.IgnoreCase);
		
        public bool Validate(string input)
        {
        	if(!cache.ContainsKey(input))
        		cache[input] = !string.IsNullOrEmpty(input) && reg.IsMatch(input);
        	return cache[input];
        }
    }
}
