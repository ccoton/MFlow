using MFlow.Core.Validation.Validators;
using System.Collections.Generic;

namespace MFlow.Core.Validation.Validators.Collections
{
    public interface INoneValidator<T> : IComparisonValidator<ICollection<T>, T>
    {
    }
}

