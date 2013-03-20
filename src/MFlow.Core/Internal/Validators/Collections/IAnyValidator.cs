using MFlow.Core.Internal.Validators;
using System.Collections.Generic;

namespace MFlow.Core.Internal.Validators.Collections
{
    public interface IAnyValidator<T> : IComparisonValidator<ICollection<T>, T>
    {
    }
}

