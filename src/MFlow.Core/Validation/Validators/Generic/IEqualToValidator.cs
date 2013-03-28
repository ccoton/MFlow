using MFlow.Core.Validation.Validators;

namespace MFlow.Core.Validation.Validators.Generic
{
    public interface IEqualToValidator<TInput, TCompare> : IComparisonValidator<TInput, TCompare>
    {
    }
}

