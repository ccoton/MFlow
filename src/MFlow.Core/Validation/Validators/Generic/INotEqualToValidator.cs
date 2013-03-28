using MFlow.Core.Validation.Validators;

namespace MFlow.Core.Validation.Validators.Generic
{
    public interface INotEqualToValidator<TInput, TCompare> : IComparisonValidator<TInput, TCompare>
    {
    }
}

