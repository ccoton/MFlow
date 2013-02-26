using System;
using System.Collections.Generic;
using System.Linq;

namespace MFlow.Core.Internal.Validators.Generic
{
    /// <summary>
    ///     Any Validator
    /// </summary>
    class AnyValidator<T> : IAnyValidator<T>
    {
        public bool Validate(ICollection<T> input, T value)
        {
            if (input == null || value == null)
                return false;

            return input.Any(i => i.Equals(value));
        }
    }
}
