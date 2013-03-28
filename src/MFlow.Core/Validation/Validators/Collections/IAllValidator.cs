using System.Collections.Generic;

namespace MFlow.Core.Validation.Validators.Collections
{
    public interface IAllValidator<T> : IComparisonValidator<ICollection<T>, ICollection<T>>
    {
    }
}