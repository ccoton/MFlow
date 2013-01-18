using System.Collections.Generic;

namespace MFlow.Core.Internal.Validators.Strings
{
    /// <summary>
    ///     IsNotEmpty Validator
    /// </summary>
    public class NotEmptyValidator : INotEmptyValidator
    {
        static IDictionary<string, bool> cache = new Dictionary<string, bool>();

        public bool Validate(string input)
        {
            if (input == null)
                return false;
            if (!cache.ContainsKey(input))
                cache [input] = input != "";
            return cache [input];
        }
    }
}
