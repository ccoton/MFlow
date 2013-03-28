using System;
using MFlow.Core.Validation.Validators;
using MFlow.Core.Internal.Validators;

namespace MFlow.Core.Validation.Validators.Numbers
{
    public interface IBetweenValidator : IComparisonValidator<int, Between<int>>
    {
    }
}

