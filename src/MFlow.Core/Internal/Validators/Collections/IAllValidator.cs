using System.Collections.Generic;

namespace MFlow.Core.Internal.Validators.Collections
{
    public interface IAllValidator<T> : IComparisonValidator<ICollection<T>, ICollection<T>>
    {
    }
}