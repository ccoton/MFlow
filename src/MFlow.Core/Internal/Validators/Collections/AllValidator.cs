using System.Collections.Generic;
using System.Linq;

namespace MFlow.Core.Internal.Validators.Collections
{
    /// <summary>
    ///     Any Validator
    /// </summary>
    class AllValidator<T> : IAllValidator<T>
    {
        public bool Validate(ICollection<T> input, ICollection<T> value)
        {
            if (input == null || value == null)
                return false;
            var foo = input.Any(u => u.Equals(value));
            return foo;
        }
    }
}