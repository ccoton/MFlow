using MFlow.Core.Internal.Validators;
using System.Collections.Generic;

namespace MFlow.Core.Internal.Validators.Generic
{
    public interface IAnyValidator<T> : IComparisonValidator<ICollection<T>, T>
    {
    }
}

