using MFlow.Core.Validation.Validators.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MFlow.Core.Validation.Validators.Collections
{
    /// <summary>
    ///     Any Validator
    /// </summary>
    class AllValidator<T> : IAllValidator<T>
    {
        public bool Validate(ICollection<T> input, ICollection<T> values)
        {
            if (input == null || values == null)
                return false;
            return input.Intersect(values).Count() == input.Count;
        }
    }
}