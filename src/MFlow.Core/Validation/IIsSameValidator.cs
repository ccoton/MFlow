using System.Collections.Generic;

namespace MFlow.Core.Validation.Validators.Collections
{
    public interface IIsSameValidator<T> : IComparisonValidator<ICollection<T>, ICollection<T>>
    {
    }
}