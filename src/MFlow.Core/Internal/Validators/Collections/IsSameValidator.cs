using System.Collections.Generic;
using System.Linq;

namespace MFlow.Core.Validation.Validators.Collections
{
    /// <summary>
    ///     Any Validator
    /// </summary>
    class IsSameValidator<T> : IIsSameValidator<T>
    {
        public bool Validate(ICollection<T> input, ICollection<T> values)
        {
            if (input == null || values == null)
                return false;
            return input.Count == values.Count && new HashSet<T>(input).SetEquals(values);
        }
    }
}