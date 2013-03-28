
using System.Collections.Generic;

namespace MFlow.Core.Validation.Validators.Collections
{
    public interface IAnyValidator<T> : IComparisonValidator<ICollection<T>, T>
    {
    }
}

