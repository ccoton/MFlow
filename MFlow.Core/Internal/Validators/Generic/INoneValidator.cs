using MFlow.Core.Internal.Validators;
using System.Collections.Generic;

namespace MFlow.Core.Internal.Validators.Generic
{
    interface INoneValidator<T> : IComparisonValidator<ICollection<T>, T>
    {
    }
}

