using MFlow.Core.Internal.Validators;
using System.Collections.Generic;

namespace MFlow.Core.Internal.Validators.Generic
{
    public interface INoneValidator<T> : IComparisonValidator<ICollection<T>, T>
    {
    }
}

