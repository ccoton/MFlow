using MFlow.Core.Validation.Validators.Strings;
using System.Collections.Generic;

namespace MFlow.Core.Internal.Validators.Strings
{
    /// <summary>
    ///     IsNotEmpty Validator
    /// </summary>
    public class NotEmptyValidator : INotEmptyValidator
    {
        public bool Validate(string input)
        {
            if (input == null)
                return false;
            return input != "";
        }
    }
}
